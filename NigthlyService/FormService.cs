using NigthlyService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;


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


        //private MongoUploader mongoUploader;
        //public string DatabaseName = "BlobImagesDb";
        //public string ConnectionString = "mongodb://localhost:27017";
        //public string CollectionName = "employees";
        //mongoUploader = new MongoUploader(ConnectionString, DatabaseName, CollectionName);
        #endregion


        public FormService()
        {
            InitializeComponent();

            SaveFolderImages = "c:\\BLOBSqlToJpeg\\SavePicturesFolder\\";

            DBCnx = new DBPictureDBEntities();
        }


        #region UI BUTTONS ACTIONS

        private void FormService_Load(object sender, EventArgs e)
        {
            NightlyTimer?.Start();
            Libelle.Text = "Service is running...";
        }

        private void BtnManualUpload_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(_ => RunNightlyUpload());
            MessageBox.Show("Manual upload triggered—check NightlyUpload.log for progress.");
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

                ThreadPool.QueueUserWorkItem(_ => RunNightlyUpload());
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
                              orderby p.EMPID
                              select new
                              {
                                  p.EMPID,
                                  p.FULLNAME,
                                  DeactivationDate = f.LASTCHANGED,
                                  BlobData = f.LNL_BLOB,
                                  f.FORMAT_IMAGE //where f.FORMAT_IMAGE == "jpeg"
                              }).ToList();


                LogToFile($"Prepared upload list with {rqList.Count} items.");

                var csvBuilder = new StringBuilder();
                csvBuilder.AppendLine("ImageFilePath,store,caseNumber,reportedloss,expdate,action"); // Headers; adjust if CaseNumber and Reported Loss are separate columns

                // Loop - Process each record
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
                            formatImage = "jpeg";

                            #region CODE MORT
                            //if (unEnreg.FORMAT_IMAGE != null)
                            //{
                            //    formatImage = unEnreg.FORMAT_IMAGE;
                            //    if ((formatImage != "jpg") && (fmt != formatImage))
                            //    {
                            //        formatImage = "jpg";
                            //    }
                            //}
                            #endregion
                        
                        }

                        // Build the data for the CSV
                        imageFileName = $"{empId}.jpeg";
                        fullImagePath = Path.Combine(imageDir, imageFileName);
                        File.WriteAllBytes(fullImagePath, blobData);

                        LogToFile($"Saved image: {imageFileName}");
                        imageFilePath = Path.Combine("c:\\BLOBSqlToJpeg\\SavePicturesFolder\\", imageFileName).Replace("\\", "/");
                        expDate = deactivationDate.ToString("yyyy-MM-dd HH:mm:ss");
                        caseNumber = $"{fullName}";
                        string csvRow = $"{imageFilePath},SCSPA,{caseNumber},0,{expDate},No Action Needed";
                        //string csvRow = $"{imageFilePath},{empId},SCSPA,{caseNumber},0,{expDate},No Action Needed";
                        
                        csvBuilder.AppendLine(csvRow);

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
                InvokeOnUIThread(() => MessageBox.Show(successMsg));
            }
            catch (Exception ex)
            {
                string errorMsg = "Nightly upload failed: " + ex.Message + "\nStackTrace: " + ex.StackTrace;
                LogToFile(errorMsg);
                InvokeOnUIThread(() => MessageBox.Show(errorMsg));
            }
        }


        private void BtnManualUpload_Click_1(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(_ => RunNightlyUpload());
            MessageBox.Show("Manual upload triggered—check NightlyUpload.log for progress.");
        }

        #endregion


        #region INVOKE ACTION & LOG TO FILE
        private void InvokeOnUIThread(Action action)
        {
            if (InvokeRequired)
            {
                Invoke(action);
            }
            else
            {
                action();
            }
        }

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
                return "jpg";
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
