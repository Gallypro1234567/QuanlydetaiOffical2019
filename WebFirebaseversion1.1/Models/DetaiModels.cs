using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebMVCFirebaseOffical.Models
{
    [FirestoreData]
    public class DetaiModels
    {
        [DisplayName("Mã đề tài")]
        [FirestoreProperty]
        public string DT_MA { set; get; }

        [DisplayName("Tên đề tài")]
        [FirestoreProperty]
        public string DT_TEN { set; get; }

        [DisplayName("Nội dung")]
        [FirestoreProperty]
        public string DT_NOIDUNG { set; get; }


        [DisplayName("Từ khóa")]
        [FirestoreProperty]
        public string DT_TUKHOA { set; get; }

        [DisplayName("Ngày tạo")]
        [FirestoreProperty]
        public string DT_NGAYTAO { set; get; }

        [DisplayName("Người Tạo")]
        [FirestoreProperty]
        public string DT_NGUOITAO { set; get; }

        [DisplayName("Ngày sửa")]
        [FirestoreProperty]
        public string DT_NGAYSUA { set; get; }

        [DisplayName("Người sửa")]
        [FirestoreProperty]
        public string DT_NGUOISUA { set; get; }

        [DisplayName("Phạm vi")]
        [FirestoreProperty]
        public string DT_PHAMVI { set; get; }

        [DisplayName("Trạng thái")]
        [FirestoreProperty]
        public string DT_TRANGTHAI { set; get; }
    }
}