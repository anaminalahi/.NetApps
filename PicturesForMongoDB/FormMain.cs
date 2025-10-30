using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace PicturesForMongoDB
{
    public partial class FormMain : Form
    {
        #region Propriétés

        public string SaveFolderImages = "c:\\BLOBSqlToJpeg\\SavePicturesFolder\\";

        public string ConnectionString = "mongodb://127.0.0.1:27017";
        public string DatabaseName = "lenel_db";
        public string CollectionName = "employees";

        public MongoClient myclient;
        public IMongoDatabase mydatabase;
        public IMongoCollection<BsonDocument> mycollection;

        public int NumberOfTransactions;
        public string Emp_ImageFile;
        public int nbRecords;

        public LNN_BLOBEntities DBCnx;
        private List<MMOBJS> PictureList;
        private MMOBJS SelectedPersonne;

        private byte[] byteBLOBData;

        #endregion

        public FormMain()
        {
            InitializeComponent();
            DBCnx = new LNN_BLOBEntities();

            myclient = new MongoClient(ConnectionString);
            mydatabase = myclient.GetDatabase(DatabaseName);
            mycollection = mydatabase.GetCollection<BsonDocument>(CollectionName);
        }

        private void BtnConnexion_Click(object sender, EventArgs e)
        {
            //  NIGHTLY JOB #1
            var laDate = DateTime.Today;
            PictureList = (from Infos in DBCnx.MMOBJS 
                           where (Infos.LASTCHANGED == laDate) orderby Infos.EMPID ascending select Infos).ToList();
            
            NumberOfTransactions= PictureList.Count;

            if (NumberOfTransactions == 0) 
            {
                //  Email the job did't run - rmtiptoes @gmail.com


                return;
            }
            else 
            {
                //  NIGHTLY JOB #2
                //  save PictureList to Emp_Image_File to FFServer
                string Emp_Image_File = @"c:\temp\Emp_Image_File.txt";
                if (File.Exists(Emp_Image_File)==true) 
                {
                    Emp_Image_Load();
                    AddFullName();
                    Load_Image_Record();
                    
                }
                else 
                { 
                    return ;
                }

            }
        }

        public void Load_Image_Record()
        {

        }

        public void Emp_Image_Load()
        {

        }

        public void AddFullName()
        {
            txtEmpId.Text = "ad00101";
            txtFullName.Text = "Maguette THIAM";
            PicBox.Image = null;

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddFullName();
        }

        private void CreateCsv() 
        {
        
        }

        private void BtnQuitter_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        #region OUTILS

        private void ConvertBlobToPicture(ref MMOBJS SelectedPersonne)
        {
            try
            {
                byteBLOBData = (byte[])(SelectedPersonne.LNL_BLOB);

                SelectedPersonne.FORMAT_IMAGE = DetectBlobType(byteBLOBData);

                MemoryStream strmBLOBData = new MemoryStream(byteBLOBData);
                PicBox.Image = Image.FromStream(strmBLOBData);
                PicBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        private byte[] BlobStringToBytes(string strData)
        {
            int numberChars = strData.Length;
            byte[] bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
            {
                if (i != 0)
                {
                    bytes[i / 2] = Convert.ToByte(strData.Substring(i, 2), 16);
                }
            }
            return bytes;
        }

        private string DetectBlobType(byte[] blobData)
        {
            // Check for JPEG
            if (blobData.Length > 3 && blobData[0] == 0xFF && blobData[1] == 0xD8 && blobData[2] == 0xFF)
            {
                return "JPEG";
            }

            // Check for PNG
            if (blobData.Length > 8 && blobData[0] == 0x89 && blobData[1] == 0x50 && blobData[2] == 0x4E && blobData[3] == 0x47)
            {
                return "PNG";
            }

            // Check for PDF
            if (blobData.Length > 4 && blobData[0] == 0x25 && blobData[1] == 0x50 && blobData[2] == 0x44 && blobData[3] == 0x46)
            {
                return "PDF";
            }

            // Check for GIF
            if (blobData.Length > 3 && blobData[0] == 0x47 && blobData[1] == 0x49 && blobData[2] == 0x46)
            {
                return "GIF";
            }

            // Check for BMP
            if (blobData.Length > 2 && blobData[0] == 0x42 && blobData[1] == 0x4D)
            {
                return "BMP";
            }

            // You can add more checks for other types...

            return "UNKWOWN";
        }

        #endregion


    }
}
