using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebFirebaseversion1._1.Models
{
    [FirestoreData]
    public class LopDemo
    {
        [DisplayName("Mã Lớp")]
        [FirestoreProperty]
        public string LO_MA { set; get; }


        [DisplayName("Tên lớp")]
        [FirestoreProperty]
        public string LO_TEN { set; get; }

        [DisplayName("Số lượng")]
        [FirestoreProperty]
        public string LO_SOLUONG { set; get; }

        [DisplayName("Thời gian đào tạo")]
        [FirestoreProperty]
        public string LO_THOIGIAN { set; get; }
        
        


    }
}