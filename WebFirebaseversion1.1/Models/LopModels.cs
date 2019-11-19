using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebMVCFirebaseOffical.Models
{
    [FirestoreData]
    public class LopModels
    {
        [DisplayName("Mã Lớp")]
        [FirestoreProperty]
        public string LO_MA { set; get; }


        [DisplayName("Tên lớp")]
        [FirestoreProperty]
        public  string LO_TEN { set; get; }

        [DisplayName("Số lượng")]
        [FirestoreProperty]
        public string LO_SOLUONG { set; get; }

        [DisplayName("Thời gian đào tạo")]
        [FirestoreProperty]
        public string LO_THOIGIAN { set; get; }
    }
}