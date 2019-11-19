using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFirebaseversion1._1.Models
{
    [FirestoreData]
    public class Sinhvien
    {
        [FirestoreProperty]
        public string LO_SV_ID { set; get; }
        [FirestoreProperty]
        public string LO_SV_SDT { set; get; }

        [FirestoreProperty]
        public string LO_SV_HOTEN { set; get; }

        [FirestoreProperty]
        public string LO_SV_EMAIL { set; get; }
    }
}