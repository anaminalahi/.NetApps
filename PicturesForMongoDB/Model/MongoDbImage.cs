using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicturesForMongoDB.Model
{
    public class MongoDbImage
    {
        public ObjectId Id { get; set; }
        public int EmpID { get; set; }
        public int BadgeId { get; set; }
        public int MmobjsEmpids { get; set; }

        public string FullName { get; set; }

        public byte LnlBlobImage { get; set; }
        public DateTime DeactivationDate { get; set; }

        public string Poi { get; set; }

    }
}
