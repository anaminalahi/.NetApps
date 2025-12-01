using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.Windows.Forms;


namespace NightlyTimer
{
    public partial class NightlyService : ServiceBase
    {

        #region PROPRIETES

        private string ConnectionString = "mongodb://localhost:27017";
        private string DatabaseName = "BlobImagesDb";
        private string CollectionName = "personnel";

        private MongoClient myclient;
        private IMongoDatabase mydatabase;
        private IMongoCollection<BsonDocument> mycollection;
        private List<MongoDbRowEnreg> MongoDbList;


        public string LogFilePath;
        private System.Timers.Timer NightlyTimer;
        private DateTime LastRunDate = DateTime.MinValue;

        private const string ConnectionSql = "Server=FRPARMAP04;Database=LNN_BLOB;User Id=sa;Password=MyBAYE01To&92;";

        #endregion


        public NightlyService()
        {
            InitializeComponent();
        }


        protected override void OnStart(string[] args)
        {
            LogFilePath = Path.Combine(Application.StartupPath, "NightlyUpload.log");

            NightlyTimer = new System.Timers.Timer(60000);
            NightlyTimer.Elapsed += NightlyTimer_Elapsed;
            NightlyTimer.Start();
        }


        protected override void OnStop()
        {
            NightlyTimer.Stop();
            NightlyTimer.Dispose();
        }


        #region NIGHTLY TIMER ÊLAPSED

        private void NightlyTimer_Elapsed(object sender, ElapsedEventArgs e)
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

                LogToFile("=== Test de connexion SQL ===");
                LogToFile($"ConnectionString: {ConnectionSql}");

                RunNightlyUpload();
            }
        }

        #endregion


        #region NIGHTLY TASK UPLOAD

        public void RunNightlyUpload()
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

                string query = @"
                SELECT 
                    p.EMPID, 
                    p.FullName, 
                    f.LASTCHANGED AS DeactivationDate, 
                    f.LNL_BLOB AS BlobData, 
                    f.FORMAT_IMAGE 
                FROM dbo.PERSONNEL p 
                INNER JOIN dbo.FILTEREDPICTURES f ON CAST(p.EMPID AS INT) = f.EMPID 
                ORDER BY p.EMPID";  //WHERE f.FORMAT_IMAGE <> null 

                var csvBuilder = new StringBuilder();
                csvBuilder.AppendLine("ImageFilePath,store,caseNumber,reportedloss,expdate,action");

                int processedCount = 0;
                MongoDbList = new List<MongoDbRowEnreg>();

                myclient = new MongoClient(ConnectionString);
                mydatabase = myclient.GetDatabase(DatabaseName);
                mycollection = mydatabase.GetCollection<BsonDocument>(CollectionName);

                using (SqlConnection connection = new SqlConnection(ConnectionSql))
                {
                    try
                    {
                        LogToFile("Opening database connection.");
                        connection.Open();
                        LogToFile("connection successfull.");
                        try
                        {
                            using (SqlCommand command = new SqlCommand(query, connection))
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                LogToFile("Executing query.");
                                while (reader.Read())
                                {
                                    try
                                    {
                                        int empId = Convert.ToInt32(reader["EMPID"]);
                                        string fullName = reader["FullName"] == DBNull.Value ? "" : (string)reader["FullName"];
                                        DateTime deactivationDate = reader["DeactivationDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader["DeactivationDate"];

                                        string imageFileName = "";
                                        string fullImagePath = "";
                                        string imageFilePath = "";
                                        string expDate = "";
                                        string caseNumber = "";

                                        // Get the BLOB data
                                        byte[] blobData = reader["BlobData"] == DBNull.Value ? null : (byte[])reader["BlobData"];
                                        string formatImage = reader["FORMAT_IMAGE"] == DBNull.Value ? "" : (string)reader["FORMAT_IMAGE"];

                                        if (blobData != null)
                                        {
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

                                        processedCount++;

                                    }
                                    catch (Exception rowEx)
                                    {
                                        LogToFile($"Error processing row: {rowEx.Message}");
                                    }
                                }
                            }
                            LogToFile($"Query completed. Processed {processedCount} rows.");
                        }
                        catch (Exception ex)
                        {
                            LogToFile($"Error querying database: {ex.Message}\nStackTrace: {ex.StackTrace}");
                            return;
                        }
                    }
                    catch
                    {
                        LogToFile("connection failed.");
                    }
                }

                // Write CSV
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
            }
            catch (Exception ex)
            {
                string errorMsg = "Nightly upload failed: " + ex.Message + "\nStackTrace: " + ex.StackTrace;
                LogToFile(errorMsg);
            }
        }

        #endregion


        #region UPLOAD TO MONGODB

        private void UploadToMongoDB(int empId, string fullName, byte[] blobData, string formatImage, DateTime deactivationDate)
        {
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("EmpID", empId);

                var update = Builders<BsonDocument>.Update
                    .Set("FullName", fullName)
                    .Set("BlobData", blobData)
                    .Set("FormatImage", formatImage)
                    .Set("DeactivationDate", deactivationDate);
                var options = new UpdateOptions { IsUpsert = true };
                mycollection.UpdateOne(filter, update, options);
                LogToFile($"Uploaded/Updated EMPID {empId} to MongoDB.");
            }
            catch (Exception ex)
            {
                LogToFile($"Error uploading EMPID {empId} to MongoDB: {ex.Message}");
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


        #region LOG TO FILE

        private void LogToFile(string message)
        {
            string fullMsg = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}\n";
            File.AppendAllText(LogFilePath, fullMsg);
        }

        #endregion


    }
}
