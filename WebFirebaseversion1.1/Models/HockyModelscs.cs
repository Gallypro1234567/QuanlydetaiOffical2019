using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMVCFirebaseOffical.Models
{
    [FirestoreData]
    public class HockyModelscs
    {
        [FirestoreProperty]
        public string HK_MA { set; get; }

        [FirestoreProperty]
        public string HK_TEN { set; get; }
    }
}