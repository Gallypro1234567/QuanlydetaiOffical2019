using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMVCFirebaseOffical.Models
{
    [FirestoreData]
    public class NamhocModels
    {
        [FirestoreProperty]
        public string NH_THOIGIAN { set; get; }
    }
}