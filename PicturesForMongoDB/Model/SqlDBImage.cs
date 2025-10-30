using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicturesForMongoDB.Model
{
    public class SqlDBImage
    {
        public int EMPID { get; set; }
        public int OBJECT { get; set; }
        public int TYPE { get; set; }
        public byte[] LNL_BLOB { get; set; }
        public Nullable<System.DateTime> LASTCHANGED { get; set; }
        public Nullable<int> ACCEPTANCETHRESHOLD { get; set; }
        public Nullable<short> BIO_BODYPART { get; set; }
        public byte[] LNL_BLOB_TXT { get; set; }
        public string FORMAT_IMAGE { get; set; }
    }
}
