using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace BLOBSqlToJpeg
{
    public class NightlyUploaderService
    {
        private readonly Func<SqlConnection> _connFactory;
        //private readonly FormMain _parentForm; // For logging access

        public NightlyUploaderService(Func<SqlConnection> connFactory)
        {
            _connFactory = connFactory;
        }

        public int Run()
        {
            string connectionString;
            try
            {
                using (var conn = _connFactory())
                {
                    connectionString = conn.ConnectionString;
                }
            }
            catch (Exception ex)
            {
                LogToFile($"Error extracting connection string: {ex.Message}");
                return 0;
            }

            string outputDir = @"c:\BLOBSqlToJpeg\";
            string imageDir = Path.Combine(outputDir, "SavePicturesFolder\\");
            string csvFilePath = Path.Combine(outputDir, "output.csv");

            // Ensure directories exist
            try
            {
                Directory.CreateDirectory(imageDir);
                Directory.CreateDirectory(outputDir);
                LogToFile("Directories created.");
            }
            catch (Exception ex)
            {
                LogToFile($"Error creating directories: {ex.Message}");
                return 0;
            }

            // SQL Query: Join PERSONEL and FILTEREDPICTURES on EMPID, assuming one record per EMPID for simplicity
            // Use LASTCHANGED as proxy for Deactivation Date
            // Filter by FORMAT_IMAGE if needed; here assuming JPEG
            string query = @"
                SELECT 
                    p.EMPID, 
                    p.FirstName, 
                    p.LastName, 
                    CONCAT(p.FirstName, ' ', p.LastName) AS FullName, 
                    f.LASTCHANGED AS DeactivationDate, 
                    f.LNL_BLOB AS BlobData, 
                    f.FORMAT_IMAGE 
                FROM dbo.PERSONNEL p 
                INNER JOIN dbo.FILTEREDPICTURES f ON CAST(p.EMPID AS INT) = f.EMPID 
                WHERE f.FORMAT_IMAGE = 'JPEG' 
                ORDER BY p.EMPID";

            //WHERE f.FORMAT_IMAGE = 'JPEG'-- Adjust filter as needed; assuming JPEG images
            
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("ImageFilePath,caseNumber,reportedloss,expdate,Action");
            //Headers; adjust if CaseNumber and Reported Loss are separate columns

            int processedCount = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    LogToFile("Opening database connection.");
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        LogToFile("Executing query.");
                        while (reader.Read())
                        {
                            try
                            {
                                // Use indexer with casting to handle column names
                                int empId = Convert.ToInt32(reader["EMPID"]);

                                string firstName = reader["FirstName"] == DBNull.Value ? "" : (string)reader["FirstName"];
                                string lastName = reader["LastName"] == DBNull.Value ? "" : (string)reader["LastName"];
                                string fullName = reader["FullName"] == DBNull.Value ? "" : (string)reader["FullName"];

                                DateTime deactivationDate = reader["DeactivationDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader["DeactivationDate"];

                                byte[] blobData = reader["BlobData"] == DBNull.Value ? null : (byte[])reader["BlobData"];
                                string formatImage = reader["FORMAT_IMAGE"] == DBNull.Value ? "" : (string)reader["FORMAT_IMAGE"];

                                if (blobData != null && formatImage.ToUpper() == "JPEG")
                                {
                                    string imageFileName = $"{empId}.jpg";
                                    string fullImagePath = Path.Combine(imageDir, imageFileName);

                                    File.WriteAllBytes(fullImagePath, blobData);

                                    //File.WriteAllBytes(fullImagePath, imageFileName);  // Need to convert string to byte
                                    LogToFile($"Saved image: {imageFileName}");

                                    //string fullName = $"{firstName} {lastName}".Trim();
                                    string imageFilePath = Path.Combine("c:\\BLOBSqlToJpeg\\SavePicturesFolder\\", imageFileName).Replace("\\", "/"); 
                                    
                                    // Use forward slashes if needed for CSV compatibility
                                    
                                    //string timeIncident = deactivationDate.ToString("yyyy-MM-dd HH:mm:ss"); // Format as needed
                                    string expDate = deactivationDate.ToString("yyyy-MM-dd HH:mm:ss"); // Format as needed
                                    
                                    //string caseNumberWithLoss = $"{fullName},0"; // Combined as per query example; adjust if separate columns
                                    string caseNumber = $"{fullName},0"; // Combined as per query example; adjust if separate columns

                                    //string csvRow = $"{imageFilePath},{empId},SCSPA,{caseNumber},0,{timeIncident},No Action Needed";
                                    string csvRow = $"{imageFilePath},{empId},SCSPA,{caseNumber},0,{expDate},No Action Needed";
                                    
                                    csvBuilder.AppendLine(csvRow);
                                    
                                    processedCount++;
                                }
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
                    return 0;
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
                return processedCount; // Still return count even if CSV fails
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

            return processedCount;
        }

        private void LogToFile(string message)
        {
            try
            {
                string logDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string logPath = Path.Combine(logDirectory, "NightlyUpload.log");
                //string logPath = Path.Combine(Application.StartupPath, "NightlyUpload.log");
                string fullMsg = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}\n";
                File.AppendAllText(logPath, fullMsg);
            }
            catch
            {
                // Silent fail on logging
            }
        }
    }
}