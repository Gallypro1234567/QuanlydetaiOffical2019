using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebMVCFirebaseOffical.Models
{
    [FirestoreData]
    public class SinhvienModels
    {

        [DisplayName("ID")]
        [FirestoreProperty]
        public string SV_ID {  get; set; }

        [DisplayName("Họ và Tên")]
        [FirestoreProperty]
        public string SV_HOTEN {get; set; }

        [DisplayName("Giới Tính")]
        [FirestoreProperty]
        public string SV_GIOITINH {   get; set;  }

        [DisplayName("Ngày sinh")]
        [FirestoreProperty]
        public string SV_NGAYSINH {   get; set; }

        [DisplayName("Nơi sinh")]
        [FirestoreProperty]
        public string SV_NOISINH {   get; set; }

        [DisplayName("Số điện thoại")]
        [FirestoreProperty]
        public string SV_SDT {   get; set; }

        [DisplayName("Email")]
        [FirestoreProperty]
        public string SV_EMAIL {   get; set; }

        [DisplayName("CMNN")]
        [FirestoreProperty]
        public string SV_CMNN {   get; set; }

        [DisplayName("Niên khóa")]
        [FirestoreProperty]
        public string SV_THOIGIANDAOTAO {  get; set; }




    }
}