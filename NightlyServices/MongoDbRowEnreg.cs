using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NigthlyServices
{
    public class MongoDbRowEnreg
    {
        public ObjectId Id { get; set; }
        public int EmpID { get; set; }
        public string FullName { get; set; }
        public byte[]  BlobData { get; set; }
        public string FormatImage { get; set; }
        public DateTime DeactivationDate { get; set; }
    }
}
