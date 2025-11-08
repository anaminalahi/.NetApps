using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace BLOBSqlToJpeg
{
    public partial class FormMain : Form
    {

        #region PROPRIETES

        private DBPictureDBEntities DBCnx;
        private List<FILTEREDPICTURES> PictureList;
        private FILTEREDPICTURES SelectedPersonne;
        private string SaveFolderImages;

        private byte[] byteBLOBData;
        private DateTime LastRunDate = DateTime.MinValue;
        private string LogFilePath = Path.Combine(Application.StartupPath, "NightlyUpload.log");
        
        public int NumberOfTransactions;
        public string Emp_ImageFile;
        public int nbRecords;

        private MongoUploader mongoUploader;
        public string DatabaseName = "lenel_db";
        public string ConnectionString = "mongodb://127.0.0.1:27017";
        public string CollectionName = "employees";

        //private System.Windows.Forms.Timer NightlyTimer;

        #endregion


        public FormMain()
        {
            InitializeComponent();

            TxtImagesFolder.Text = "c:\\BLOBSqlToJpeg\\SavePicturesFolder\\";
            SaveFolderImages = "c:\\BLOBSqlToJpeg\\SavePicturesFolder\\";

            DBCnx = new DBPictureDBEntities();
            mongoUploader = new MongoUploader(ConnectionString, DatabaseName, CollectionName);


            #region CODEMORT NIGHTLY SERVICES
            // Setup nightly timer
            //NightlyTimer = new System.Windows.Forms.Timer();
            //NightlyTimer.Interval = 60 * 1000;
            //NightlyTimer.Tick += NightlyTimer_Tick;
            //NightlyTimer.Start();

            // Add manual upload button
            //BtnManualUpload = new Button();
            //BtnManualUpload.Text = "Manual Upload";
            //BtnManualUpload.Location = new Point(790, 600);
            //BtnManualUpload.Size = new Size(70, 35);

            // Setup manual upload button event
            //BtnManualUpload.Click += (s, e) => {
            //    ThreadPool.QueueUserWorkItem(_ => RunNightlyUpload());
            //    MessageBox.Show("Manual upload triggered—check NightlyUpload.log for progress.");
            //};

            //this.Controls.Add(BtnManualUpload);
            #endregion

        }


        // APPLICATION CODE
        private void FormMain_Load(object sender, EventArgs e)
        {
            try
            {
                LogToFile("Application started.");

                // Test database connection SqlServer
                if (!DBCnx.Database.Exists())
                {
                    LogToFile("Database connection failed.");
                    MessageBox.Show("Database connection failed.");

                    return;
                }
                LogToFile("Database connection successful.");


                // Load pictures from DB SqlServer Database
                PictureList = (from photo in DBCnx.FILTEREDPICTURES orderby photo.EMPID ascending select photo).ToList();
                LogToFile($"Loaded {PictureList.Count} pictures from DB.");


                // Check first picture BLOB size ???
                if (PictureList.Count > 0 && PictureList[0].LNL_BLOB != null && PictureList[0].LNL_BLOB.Length > 0)
                {
                    LogToFile($"First picture BLOB size: {PictureList[0].LNL_BLOB.Length} bytes");
                }
                else if (PictureList.Count > 0)
                {
                    LogToFile("First picture has no BLOB data.");
                }


                // Pre-process PictureList to handle invalid BLOBs and prevent grid rendering errors
                foreach (var photo in PictureList)
                {
                    if (photo.LNL_BLOB == null || photo.LNL_BLOB.Length == 0)
                    {
                        photo.LNL_BLOB = null;
                        photo.FORMAT_IMAGE = "none";
                        continue;
                    }

                    photo.FORMAT_IMAGE = DetectBlobType(photo.LNL_BLOB);
                    // Null out non-image BLOBs to avoid Image.FromStream failures in grid
                    if (photo.FORMAT_IMAGE == "pdf" || photo.FORMAT_IMAGE == "unknown")
                    {
                        photo.LNL_BLOB = null;
                    }
                }

                // Bind to DataGridView
                DBGridPersonel.DataSource = PictureList;

                // Hide the LNL_BLOB column to prevent automatic image rendering attempts
                if (DBGridPersonel.Columns.Contains("LNL_BLOB"))
                {
                    DBGridPersonel.Columns["LNL_BLOB"].Visible = false;
                }

                // Handle DataGridView errors to suppress default dialogs
                DBGridPersonel.DataError += DBGridPersonel_DataError;

                // Select first picture by default
                SelectedPersonne = PictureList.FirstOrDefault();

                // Display first picture
                ConvertBlobToPicture(ref SelectedPersonne);
            }
            catch (Exception ex)
            {
                string errorMsg = "Error loading pictures: " + ex.Message;
                LogToFile(errorMsg);
                MessageBox.Show(errorMsg);
            }
        }



        #region NIGHTLY TASKS
        // Nightly timer tick event
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



        private void RunNightlyUpload()
        {
            try
            {
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
                    return;
                }

                LogToFile("Nightly upload started");

                #region CodeMort
                //var efConnString = ConfigurationManager.ConnectionStrings["DBPictureDBEntities"]?.ConnectionString;
                //if (string.IsNullOrEmpty(efConnString))
                //{
                //    string errorMsg = "DBPictureDBEntities connection string not found.";
                //    LogToFile(errorMsg);

                //    //InvokeOnUIThread(() => MessageBox.Show(errorMsg));
                //    return;
                //}

                //var sqlConnString = GetSqlConnectionStringFromEntity(efConnString);
                //if (string.IsNullOrEmpty(sqlConnString))
                //{
                //    string errorMsg = "Unable to extract SQL provider connection string from EF connection string.";
                //    LogToFile(errorMsg);

                //    //InvokeOnUIThread(() => MessageBox.Show(errorMsg));
                //    return;
                //}

                //LogToFile("Connection strings loaded successfully.");
                #endregion


                var rqList = (from p in DBCnx.PERSONNEL
                              join f in DBCnx.FILTEREDPICTURES
                                  on p.EMPID equals f.EMPID
                              orderby p.EMPID
                              select new
                              {
                                  p.EMPID,
                                  p.FirstName,
                                  p.LastName,
                                  FullName = p.FirstName + " " + p.LastName,
                                  DeactivationDate = f.LASTCHANGED,
                                  BlobData = f.LNL_BLOB,
                                  f.FORMAT_IMAGE
                              }).ToList();

                LogToFile($"Prepared upload list with {rqList.Count} items.");

                var csvBuilder = new StringBuilder();
                csvBuilder.AppendLine("ImageFilePath,caseNumber,reportedloss,expdate,Action");

                // Loop - Process each record
                foreach (var unEnreg in rqList)
                {
                    try
                    {
                        // Use indexer with casting to handle column names
                        int empId = Convert.ToInt32(unEnreg.EMPID);
                        string firstName = unEnreg.FirstName;
                        string lastName = unEnreg.LastName;
                        string fullName = unEnreg.FullName;
                        DateTime deactivationDate = (DateTime)unEnreg.DeactivationDate;

                        byte[] blobData = null;
                        if (unEnreg.BlobData != null)
                        {
                            blobData = (byte[])unEnreg.BlobData;
                        }

                        string formatImage = null;
                        if (unEnreg.FORMAT_IMAGE != null)
                        {
                            formatImage = unEnreg.FORMAT_IMAGE;
                            if (string.IsNullOrEmpty(formatImage)) formatImage = DetectBlobType(blobData);
                        }

                        // Build the data for the CSV
                        string imageFileName = $"{empId}.jpg";
                        string fullImagePath = Path.Combine(imageDir, imageFileName);
                        File.WriteAllBytes(fullImagePath, blobData);

                        LogToFile($"Saved image: {imageFileName}");
                        string imageFilePath = Path.Combine("c:\\BLOBSqlToJpeg\\SavePicturesFolder\\", imageFileName).Replace("\\", "/");
                        string expDate = deactivationDate.ToString("yyyy-MM-dd HH:mm:ss");
                        string caseNumber = $"{fullName},0";

                        string csvRow = $"{imageFilePath},{empId},SCSPA,{caseNumber},0,{expDate},No Action Needed";

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


                #region CODEMORT CONNECTION FACTORY
                //Func<SqlConnection> connFactory = () => new SqlConnection(sqlConnString);
                //var service = new NightlyUploaderService(connFactory);
                //int uploadedCount = service.Run();
                #endregion


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

        #endregion


        #region CODEMORT CONNECTION STRING PARSING
        //private string GetSqlConnectionStringFromEntity(string efConnString)
        //{
        //    if (string.IsNullOrEmpty(efConnString)) return null;
        //    var key = "provider connection string=";
        //    var idx = efConnString.IndexOf(key, StringComparison.InvariantCultureIgnoreCase);
        //    if (idx < 0) return null;
        //    var start = idx + key.Length;
        //    var rest = efConnString.Substring(start).Trim();
        //    if (rest.Length == 0) return null;
        //    if (rest[0] == '"' || rest[0] == '\'')
        //    {
        //        var quote = rest[0];
        //        var end = rest.IndexOf(quote, 1);
        //        if (end > 0) return rest.Substring(1, end - 1);
        //    }
        //    return rest.Trim().TrimEnd(';');
        //}
        #endregion


        #region DBGRID ACTIONS
        private void DBGridPersonel_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView temp = (DataGridView)sender;
            if (temp.CurrentRow == null) return;

            SelectedPersonne = (FILTEREDPICTURES)temp.CurrentRow.DataBoundItem;
            if (SelectedPersonne == null) return;

            ConvertBlobToPicture(ref SelectedPersonne);
        }

        private void DBGridPersonel_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Log the error and cancel to prevent default error dialog
            string errorMsg = $"DataGridView error: {e.Exception?.Message} at Row={e.RowIndex}, Col={e.ColumnIndex}";
            LogToFile(errorMsg);
            Console.WriteLine(errorMsg);
            e.Cancel = true;
        }
        #endregion


        #region NIGHTLY OTHER ACTIONS 6 LOG TO FILE
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


        #region UI BUTTONS ACTIONS
        private void BtnSelectFolder_Click(object sender, EventArgs e)
        {
            if (CMDFolder.ShowDialog() == DialogResult.OK)
            {
                TxtImagesFolder.Text = CMDFolder.SelectedPath;
                SaveFolderImages = CMDFolder.SelectedPath + "\\";
            }
        }

        private void BtnSaveCurrent_Click(object sender, EventArgs e)
        {
            if (SelectedPersonne == null)
            {
                MessageBox.Show("No picture selected");
                return;
            }

            Directory.CreateDirectory(SaveFolderImages);

            var bytes = SelectedPersonne.LNL_BLOB;
            if (bytes == null)
            {
                MessageBox.Show("Selected picture has no image data");
                return;
            }

            var fmt = SelectedPersonne.FORMAT_IMAGE;
            if (string.IsNullOrEmpty(fmt)) fmt = DetectBlobType(bytes);

            string nomFichImage = SelectedPersonne.EMPID + "_" + SelectedPersonne.TYPE + "." + fmt.ToLower();
            string fullPath = Path.Combine(SaveFolderImages, nomFichImage);

            File.WriteAllBytes(fullPath, bytes);
            MessageBox.Show($"Saved picture to: {fullPath}");
        }

        private void BtnSaveAll_Click(object sender, EventArgs e)
        {
            if (PictureList == null || PictureList.Count == 0)
            {
                MessageBox.Show("No pictures to save");
                return;
            }

            Directory.CreateDirectory(SaveFolderImages);

            int savedCount = 0;
            foreach (var item in PictureList)
            {
                try
                {
                    var byteBLOB = item.LNL_BLOB;
                    if (byteBLOB == null) continue;

                    string format = DetectBlobType(byteBLOB);  // Don't mutate entity

                    string nomFichImage = item.EMPID + "_" + item.TYPE + "." + format.ToLower();
                    string fullPath = Path.Combine(SaveFolderImages, nomFichImage);

                    File.WriteAllBytes(fullPath, byteBLOB);
                    savedCount++;
                }
                catch (Exception ex)
                {
                    string errorMsg = $"Error saving picture {item.EMPID}: {ex.Message}";
                    LogToFile(errorMsg);
                    Console.WriteLine(errorMsg);
                }
            }
            string successMsg = $"Saved {savedCount} pictures to {SaveFolderImages}";
            MessageBox.Show(successMsg);
            LogToFile(successMsg);
        }


        private void BtnManualUpload_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(_ => RunNightlyUpload());
            MessageBox.Show("Manual upload triggered—check NightlyUpload.log for progress.");
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion


        #region OUTILS DE CONVERSION BLOB TO PICTURE
        private void ConvertBlobToPicture(ref FILTEREDPICTURES SelectedPersonne)
        {
            try
            {
                if (SelectedPersonne == null || SelectedPersonne.LNL_BLOB == null || SelectedPersonne.LNL_BLOB.Length == 0)
                {
                    PicBox.Image = null;
                    return;
                }

                byte[] byteBLOBData = SelectedPersonne.LNL_BLOB;  // Local var, no field needed

                string format = DetectBlobType(byteBLOBData);
                SelectedPersonne.FORMAT_IMAGE = format;  // Optional: Update if needed

                // Only attempt to load if it's a supported image format
                if (format == "jpg" || format == "png" || format == "gif" || format == "bmp")
                {
                    using (MemoryStream strmBLOBData = new MemoryStream(byteBLOBData))
                    {
                        var img = Image.FromStream(strmBLOBData);
                        PicBox.Image = (Image)img.Clone();
                        img.Dispose();
                    }
                    PicBox.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    PicBox.Image = null;
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Parameter is not valid"))
            {
                string errorMsg = "Invalid image data - cannot display.";
                LogToFile(errorMsg);
                Console.WriteLine(errorMsg);
                PicBox.Image = null;
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                LogToFile(errorMsg);
                Console.WriteLine(errorMsg);
                PicBox.Image = null;
            }
        }


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