using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFirebaseversion1._1.Models
{
    [FirestoreData]
    public class Khoa_BomonModels
    {
        [FirestoreProperty]
        public string KBM_ID { set; get; }

        [FirestoreProperty]
        public string KBM_TENBOMON { set; get; }

        [FirestoreProperty]
        public string KBM_PHUTRACH { set; get; }
    }
}