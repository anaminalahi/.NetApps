using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Timers;

namespace FaceFirst_AutoImageLoad
{
    partial class FaceFirstAutoImageLoad : ServiceBase
    {

        #region PROPRIETES

        public string LogDirectory;
        public string LogFilePath;
        private Timer MyTimer;
        private DateTime LastRunDate = DateTime.MinValue;

        string environment = ConfigurationManager.AppSettings["Environment"];
        private readonly string ConnectionSql;

        #endregion


        public FaceFirstAutoImageLoad()
        {
            InitializeComponent();

            //Connection to the LH-SQL-DEV or LH-SQL-PROD depending on the environment variable in the app.config
            if (environment == "DEV")
            {
                ConnectionSql = ConfigurationManager.ConnectionStrings["DevConnection"].ConnectionString;
            }
            else
            {
                ConnectionSql = ConfigurationManager.ConnectionStrings["ProdConnection"].ConnectionString;
            }
        }

        protected override void OnStart(string[] args)
        {
            //LogDirectory = @"\\sg-ff-dev\D$\FF-Autoenrollment-Tool\Logs";
            LogDirectory = @"C:\FF-Autoenrollment-Tool\Logs";
            LogFilePath = Path.Combine(LogDirectory, "FaceFirstAutoImageUpload.log");

            MyTimer = new Timer(60000);
            MyTimer.Elapsed += MyTimer_Elapsed;
            MyTimer.Start();
        }

        protected override void OnStop()
        {
            MyTimer.Stop();
            MyTimer.Dispose();
        }


        #region MyTIMERELAPSED

        private void MyTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var timeStr = ConfigurationManager.AppSettings["FaceFirstAutoImageLoad"] ?? "02:00";

            if (!TimeSpan.TryParse(timeStr, System.Globalization.CultureInfo.InvariantCulture, out TimeSpan scheduledTime))
            {
                LogToFile("Invalid FaceFirstAutoImageLoad in config.");
                return;
            }

            var now = DateTime.Now;
            var todayScheduled = now.Date + scheduledTime;

            if (now >= todayScheduled && LastRunDate.Date != now.Date)
            {
                LastRunDate = now.Date;
                LogToFile($"FaceFirstAutoImageLoad triggered at {now}.");
                LogToFile($"ConnectionString:{ConnectionSql}");

                MyTimerUpload();
            }
        }

        #endregion


        #region CREATE DIRECTORIES

        private void CreateDirectories(string imageDir, string csvDir)
        {
            try
            {
                Directory.CreateDirectory(imageDir);
                Directory.CreateDirectory(csvDir);
                LogToFile("Directories created.");
            }
            catch (Exception ex)
            {
                LogToFile($"Error creating directories: {ex.Message}");
                throw;
            }
        }

        #endregion


        #region MyTIMERUPLOAD

        private void MyTimerUpload()
        {
            LogToFile("Running MyTimerUpload.");

            try
            {

                //string outputDirff = @"sg-ff-dev\\D$";
                string outputDirff = @"C:\";

                string imageDir = Path.Combine(outputDirff, "SavePicturesFolder");
                string exeDir = Path.Combine(outputDirff, "FF-Autoenrollment-Tool");
                string csvDir = Path.Combine(outputDirff, "CsvFolder");
                string csvFilePath = Path.Combine(csvDir, "FaceFirstOutput.csv");

                CreateDirectories(imageDir, csvDir);

                LogToFile("FaceFirstAutoImage upload started");

                string query = @"
                SELECT 
                    p.EMPID, 
                    p.FullName, 
                    f.LASTCHANGED AS DeactivationDate, 
                    f.LNL_BLOB AS BlobData, 
                    f.FORMAT_IMAGE 
                FROM dbo.PERSONNEL p 
                INNER JOIN dbo.FILTEREDPICTURES f ON CAST(p.EMPID AS INT) = f.EMPID 
                ORDER BY p.EMPID";

                var csvBuilder = new StringBuilder();
                csvBuilder.AppendLine("ImageFilePath,store,caseNumber,reportedloss,expdate,action");

                int nbEnregs = ProcessSqlRows(query, imageDir, csvBuilder);

                WriteCsv(csvFilePath, csvBuilder);

                TriggerExe(exeDir);

            }
            catch (Exception ex)
            {
                string errorMsg = "FaceFirst_AutoImage upload failed: " + ex.Message + "\nStackTrace: " + ex.StackTrace;
                LogToFile(errorMsg);
            }
        }


        private int ProcessSqlRows(string query, string imageDir, StringBuilder csvBuilder)
        {
            int processedCount = 0;
            using (SqlConnection connection = new SqlConnection(ConnectionSql))
            {
                try
                {
                    LogToFile("Opening database connection.");
                    connection.Open();
                    LogToFile("connection successfull.");

                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        LogToFile("Executing query.");
                        while (reader.Read())
                        {
                            ProcessRow(reader, imageDir, csvBuilder, ref processedCount);
                        }
                    }
                    LogToFile($"Query completed. Processed {processedCount} rows.");
                }
                catch (Exception ex)
                {
                    LogToFile($"Error querying database: {ex.Message}\nStackTrace: {ex.StackTrace}");
                    throw;
                }
            }
            return processedCount;
        }

        private void ProcessRow(SqlDataReader reader, string imageDir, StringBuilder csvBuilder, ref int processedCount)
        {
            try
            {
                int empId = Convert.ToInt32(reader["EMPID"]);
                string fullName = reader["FullName"] == DBNull.Value ? "" : (string)reader["FullName"];
                DateTime deactivationDate = reader["DeactivationDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader["DeactivationDate"];
                byte[] blobData = reader["BlobData"] == DBNull.Value ? null : (byte[])reader["BlobData"];

                string formatImage = string.Empty;
                if (blobData != null)
                {
                    formatImage = DetectBlobType(blobData);
                    if (formatImage == "unknown")
                    {
                        LogToFile($"Unknown blob format for EMPID {empId}. Skipping.");
                        return;
                    }
                }

                string imageFileName = $"{empId}_0.jpeg";
                string fullImagePath = Path.Combine(imageDir, imageFileName);
                File.WriteAllBytes(fullImagePath, blobData);
                LogToFile($"Saved image: {imageFileName}");

                string expDate = deactivationDate.ToString("yyyy-MM-dd HH:mm:ss");
                string caseNumber = $"{fullName}";
                string csvRow = $"{fullImagePath},SCSPA,{caseNumber},0,{expDate},No Action Needed";
                csvBuilder.AppendLine(csvRow);

                processedCount++;
            }
            catch (Exception rowEx)
            {
                LogToFile($"Error processing row: {rowEx.Message}");
            }
        }

        private void WriteCsv(string csvFilePath, StringBuilder csvBuilder)
        {
            try
            {
                File.WriteAllText(csvFilePath, csvBuilder.ToString(), Encoding.UTF8);
                LogToFile($"CSV file generated at: {csvFilePath}");
            }
            catch (Exception ex)
            {
                LogToFile($"Error writing CSV: {ex.Message}");
            }
        }


        // This method assumes the EXE is already built and placed in the output directory. D:\FF-Autoenrollment-Tool\FaceFirst.Tools.AutoEnroller.exe
        private void TriggerExe(string exeDir)
        {
            string exePath = Path.Combine(exeDir, "FaceFirst.Tools.AutoEnroller.exe");
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
