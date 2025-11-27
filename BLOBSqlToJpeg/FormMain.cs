using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace BLOBSqlToJpeg
{
    public partial class FormMain : Form
    {

        #region PROPRIETES

            private DBPictureDBEntities DBCnx;

            private List<FILTEREDPICTURES> PictureList;

            private FILTEREDPICTURES SelectedPersonne;

            private string strPhoto;
            private string FichExtension;
            private string SaveFolderImages;

            private byte[] byteBLOBData;

        #endregion


        #region PROPRIETES

            public FormMain()
            {
                InitializeComponent();

                DBCnx = new DBPictureDBEntities();

                TxtImagesFolder.Text = "c:\\BLOBSqlToJpeg\\SavePicturesFolder\\";
                SaveFolderImages = "c:\\BLOBSqlToJpeg\\SavePicturesFolder\\";
            }

            private void FormMain_Load(object sender, EventArgs e)
            {
                    PictureList = (from photo in DBCnx.FILTEREDPICTURES orderby photo.EMPID ascending select photo).ToList();

                    DBGridPersonel.DataSource = PictureList;
                    SelectedPersonne = PictureList.FirstOrDefault();

                    ConvertBlobToPicture(ref SelectedPersonne);
            }

            private void BtnSaveCurrent_Click(object sender, EventArgs e)
            {
                string nomFichImage = SelectedPersonne.EMPID +"_" + SelectedPersonne.TYPE + "." + SelectedPersonne.FORMAT_IMAGE;

                PicBox.Image.Save(SaveFolderImages + nomFichImage);
            }

            private void BtnSaveAll_Click(object sender, EventArgs e)
            {
                foreach (var item in PictureList)
                {
                    try
                    {
                        byteBLOBData = (byte[])item.LNL_BLOB;

                        MemoryStream strmBLOBData = new MemoryStream(byteBLOBData);

                        item.FORMAT_IMAGE = DetectBlobType(byteBLOBData);

                        string nomFichImage = item.EMPID + "_" + item.TYPE + "." + item.FORMAT_IMAGE;

                        PicBox.Image = Image.FromStream(strmBLOBData);

                        PicBox.SizeMode = PictureBoxSizeMode.Zoom;

                        PicBox.Image.Save(SaveFolderImages + nomFichImage);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally 
                    { 
                        PicBox.Image = null; 
                    }
                }
            }

            private void DBGridPersonel_CellClick(object sender, DataGridViewCellEventArgs e)
            {
                DataGridView temp = (DataGridView)sender;
                if (temp.CurrentRow == null) return;

                SelectedPersonne = (FILTEREDPICTURES)temp.CurrentRow.DataBoundItem;

                ConvertBlobToPicture(ref SelectedPersonne);
            }

            private void BtnExit_Click(object sender, EventArgs e)
            {
                Application.Exit();
            }

        #endregion


        #region OUTILS

        private void ConvertBlobToPicture(ref FILTEREDPICTURES SelectedPersonne)
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
