using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebMVCFirebaseOffical.Models
{
    [FirestoreData]
    public class BomonModels
    {
        [DisplayName("Mã bộ môn")]
        [FirestoreProperty]
        public string BM_MA { set; get; }

        [DisplayName("Tên bộ môn")]
        [FirestoreProperty]
        public string BM_TEN { set; get; }

        [DisplayName("Cán bộ phụ trách")]
        [FirestoreProperty]
        public string BM_CANBO_PHUTRACH { set; get; }

    }
}