using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFirebaseversion1._1.Models
{
    [FirestoreData]
    public class DanhMuc_Detai
    {
    
        [FirestoreProperty]
        public string DT_MA { set; get; }

        [FirestoreProperty]
        public string DT_TEN { set; get; }

        [FirestoreProperty]
        public string DT_NGUOITAO { set; get; }

        [FirestoreProperty]
        public string DT_TRANGTHAI { set; get; }

    }
}