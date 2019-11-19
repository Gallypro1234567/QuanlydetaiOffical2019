using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebMVCFirebaseOffical.Models
{
    [FirestoreData]
    public class KhoaModels
    {
        [DisplayName("Mã khoa")]
        [FirestoreProperty]
        public string KH_MA { set; get; }

        [DisplayName("Tên khoa")]
        [FirestoreProperty]
        public string KH_TEN { set; get; }
    }
}