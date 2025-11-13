using NigthlyService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;



namespace BLOBSqlToJpeg
{
    public partial class FormService : Form
    {

        #region PROPRIETES

        private DBPictureDBEntities DBCnx;

        private List<FILTEREDPICTURES> PictureList;
        private FILTEREDPICTURES SelectedPersonne;
        private string SaveFolderImages;


        private byte[] byteBLOBData;
        private DateTime LastRunDate = DateTime.MinValue;
        private string LogFilePath = Path.Combine(Application.StartupPath, "NightlyUpload.log");

        private string ConnectionString = "mongodb://localhost:27017";
        private string DatabaseName = "BlobImagesDb";
        private string CollectionName = "personnel";

        private MongoClient myclient;
        private IMongoDatabase mydatabase;
        private IMongoCollection<BsonDocument> mycollection;
        private List<MongoDbRowEnreg> MongoDbList;


        private int NumberOfTransactions = 0;

        #endregion


        public FormService()
        {
            InitializeComponent();

            SaveFolderImages = "c:\\BLOBSqlToJpeg\\SavePicturesFolder\\";

            DBCnx = new DBPictureDBEntities();

            myclient = new MongoClient(ConnectionString);
            mydatabase = myclient.GetDatabase(DatabaseName);
            mycollection = mydatabase.GetCollection<BsonDocument>(CollectionName);
        }


        #region UI BUTTONS ACTIONS

        private void FormService_Load(object sender, EventArgs e)
        {
            NightlyTimer?.Start();
            Libelle.Text = "Service is running...";
        }


        private void BtnManualUpload_Click(object sender, EventArgs e)
        {
            Libelle.Text = "Manual upload triggered—check NightlyUpload.log for progress.";
            BtnManualUpload.Enabled= false;
            this.Refresh();
            
            RunNightlyUpload();

            this.Refresh();
            BtnManualUpload.Enabled= true;
            Libelle.Text = "Service is running...";
        }


        private void BtnExit_Click(object sender, EventArgs e)
        {
            NightlyTimer?.Stop();
            Application.Exit();
        }

        #endregion


        #region NIGHTLY TIMER

        private void NightlyTimer_Tick(object sender, EventArgs e)
        {
            var timeStr = ConfigurationManager.AppSettings["NightlyRunTime"] ?? "02:00";

            if (!TimeSpan.TryParse(timeStr, out var scheduledTime))
            {
                LogToFile("Invalid NightlyRunTime in config.");
                return;
            }

            var now = DateTime.Now;
            var todayScheduled = now.Date + scheduledTime;

            if (now >= todayScheduled && LastRunDate.Date != now.Date)
            {
                LastRunDate = now.Date;
                LogToFile($"Nightly timer triggered at {now}.");

                RunNightlyUpload();
            }
        }

        #endregion


        #region NIGHTLY TASK UPLOAD

        private void RunNightlyUpload()
        {
            try
            {
                string outputDir = @"c:\BLOBSqlToJpeg\";
                string imageDir = Path.Combine(outputDir, "SavePicturesFolder\\");
                string csvFilePath = Path.Combine(outputDir, "Output.csv");
                
                try
                {
                    Directory.CreateDirectory(imageDir);
                    Directory.CreateDirectory(outputDir);
                    LogToFile("Directories created.");
                }
                catch (Exception ex)
                {
                    LogToFile($"Error creating directories: {ex.Message}");
                    return;
                }

                LogToFile("Nightly upload started");

                var rqList = (from p in DBCnx.PERSONNEL
                              join f in DBCnx.FILTEREDPICTURES
                                  on p.EMPID equals f.EMPID
                              where f.LNL_BLOB != null
                              orderby p.EMPID
                              select new
                              {
                                  p.EMPID,
                                  p.FULLNAME,
                                  DeactivationDate = f.LASTCHANGED,
                                  BlobData = f.LNL_BLOB,
                                  f.FORMAT_IMAGE
                              }).ToList();

                NumberOfTransactions = rqList.Count;

                LogToFile($"Prepared upload list with {NumberOfTransactions} items.");

                // If there are records to process
                if (NumberOfTransactions > 0)
                {
                    MongoDbList = new List<MongoDbRowEnreg>();

                    // Headers; adjust if CaseNumber and Reported Loss are separate columns
                    var csvBuilder = new StringBuilder();
                    csvBuilder.AppendLine("ImageFilePath,store,caseNumber,reportedloss,expdate,action"); 
                    
                    foreach (var unEnreg in rqList)
                    {
                        try
                        {
                            // Use indexer with casting to handle column names
                            int empId = Convert.ToInt32(unEnreg.EMPID);
                            string fullName = unEnreg.FULLNAME;
                        
                            DateTime deactivationDate = (DateTime)unEnreg.DeactivationDate;

                            string imageFileName = "";
                            string fullImagePath = "";
                            string imageFilePath = "";
                            string expDate = "";
                            string caseNumber = "";

                            // Get the BLOB data
                            byte[] blobData = null;
                            string formatImage = null;

                            if (unEnreg.BlobData != null)
                            {
                                blobData = (byte[])unEnreg.BlobData;
                                formatImage = DetectBlobType(blobData);
                                if (formatImage == "unknown")
                                {
                                    LogToFile($"Unknown blob format for EMPID {empId}. Skipping.");
                                    continue; // Skip this record
                                }
                            }

                            // Build the data for the CSV
                            imageFileName = $"{empId}_0.jpeg";
                            fullImagePath = Path.Combine(imageDir, imageFileName);
                            File.WriteAllBytes(fullImagePath, blobData);

                            LogToFile($"Saved image: {imageFileName}");
                            imageFilePath = Path.Combine("c:\\BLOBSqlToJpeg\\SavePicturesFolder\\", imageFileName).Replace("\\", "/");
                            expDate = deactivationDate.ToString("yyyy-MM-dd HH:mm:ss");
                            caseNumber = $"{fullName}";
                        
                            string csvRow = $"{imageFilePath},SCSPA,{caseNumber},0,{expDate},No Action Needed";
                        
                            csvBuilder.AppendLine(csvRow);

                            // Prepare MongoDB entry
                            MongoDbRowEnreg uneLigne = new MongoDbRowEnreg
                            {
                                EmpID = empId,
                                FullName = fullName,
                                DeactivationDate = deactivationDate,
                                BlobData = blobData,
                                FormatImage = "jpeg"
                            };

                            // Upload to MongoDBList
                            MongoDbList.Add(uneLigne);

                        }
                        catch (Exception rowEx)
                        {
                            LogToFile($"Error processing row: {rowEx.Message}");
                        }
                    }

                    // Write CSV File
                    try
                    {
                        File.WriteAllText(csvFilePath, csvBuilder.ToString(), Encoding.UTF8);
                        LogToFile($"CSV file generated at: {csvFilePath}");
                    }
                    catch (Exception ex)
                    {
                        LogToFile($"Error writing CSV: {ex.Message}");
                        return; // Still return count even if CSV fails
                    }


                    // Upload to MongoDB
                    foreach (var mongoEntry in MongoDbList)
                    {
                        UploadToMongoDB(mongoEntry.EmpID, mongoEntry.FullName, mongoEntry.BlobData, mongoEntry.FormatImage, mongoEntry.DeactivationDate);
                    }


                    // Trigger the EXE
                    string exePath = Path.Combine(outputDir, "FaceFirst.Tools.AutoEnroller.exe");
                    if (File.Exists(exePath))
                    {
                        try
                        {
                            Process.Start(exePath);
                            LogToFile($"Started {exePath}");
                        }
                        catch (Exception ex)
                        {
                            LogToFile($"Error starting EXE: {ex.Message}");
                        }
                    }
                    else
                    {
                        LogToFile($"EXE not found at: {exePath}");
                    }

                    string successMsg = $"Nightly upload completed. Processed {rqList.Count} items.";
                    LogToFile(successMsg);
                    MessageBox.Show(successMsg); 

                }
                else 
                { 
                    LogToFile("No records to process. Exiting nightly upload.");
                    return;
                }
            }
            catch (Exception ex)
            {
                string errorMsg = "Nightly upload failed: " + ex.Message + "\nStackTrace: " + ex.StackTrace;
                LogToFile(errorMsg);
                MessageBox.Show(errorMsg);
            }
        }

        #endregion

         
        #region TO MONGODB

        private void UploadToMongoDB(int empId, string fullname, byte[] blobData, string formatImage , DateTime deactivationDate)
        {
            try
            {
                var document = new BsonDocument
                {
                    { "empId", empId },
                    { "fullName", fullname },
                    { "blobData", Convert.ToBase64String(blobData) },
                    { "formatImage", formatImage },
                    { "deactivationDate", deactivationDate }
                };

                mycollection.InsertOne(document);
                LogToFile($"Uploaded EMPID {empId} to MongoDB.");
            }
            catch (Exception ex)
            {
                LogToFile($"Error uploading EMPID {empId} to MongoDB: {ex.Message}");
            }
        }

        #endregion


        #region LOG TO FILE

        private void LogToFile(string message)
        {
            try
            {
                string logDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string logPath = Path.Combine(logDirectory, "NightlyUpload.log");

                string fullMsg = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}\n";
                File.AppendAllText(LogFilePath, fullMsg);
            }
            catch
            {
                // Silent fail on logging
            }
        }

        #endregion


        #region DETECTION BLOB FORMATS

        private string DetectBlobType(byte[] blobData)
        {
            if (blobData == null || blobData.Length == 0) return "unknown";

            if (blobData.Length > 3 && blobData[0] == 0xFF && blobData[1] == 0xD8 && blobData[2] == 0xFF)
            {
                return "jpeg";
            }

            if (blobData.Length > 8 && blobData[0] == 0x89 && blobData[1] == 0x50 && blobData[2] == 0x4E && blobData[3] == 0x47)
            {
                return "png";
            }

            if (blobData.Length > 4 && blobData[0] == 0x25 && blobData[1] == 0x50 && blobData[2] == 0x44 && blobData[3] == 0x46)
            {
                return "pdf";
            }

            if (blobData.Length > 6 && blobData[0] == 0x47 && blobData[1] == 0x49 && blobData[2] == 0x46 &&
                blobData[3] == 0x38 && (blobData[4] == 0x37 || blobData[4] == 0x39) && blobData[5] == 0x61)
            {
                return "gif";
            }

            if (blobData.Length > 2 && blobData[0] == 0x42 && blobData[1] == 0x4D)
            {
                return "bmp";
            }

            return "unknown";
        }

        #endregion


    }
}
