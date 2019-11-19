using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebMVCFirebaseOffical.Models
{
    [FirestoreData]
    public class GiangvienModels
    {
        [DisplayName("Mã giảng viên")]
        [FirestoreProperty]
        public string GV_MA { set; get; }

        [DisplayName("Họ và tên")]
        [FirestoreProperty]
        public string GV_HOTEN { set; get; }

        [DisplayName("Giới tính")]
        [FirestoreProperty]
        public string GV_GIOITINH { set; get; }

        [DisplayName("Ngày sinh")]
        [FirestoreProperty]
        public string GV_NGAYSINH { set; get; }

        [DisplayName("Nơi sinh")]
        [FirestoreProperty]
        public string GV_NOISINH { set; get; }

        [DisplayName("Số Điện Thoại")]
        [FirestoreProperty]
        public string GV_SDT { set; get; }


        [DisplayName("Email")]
        [FirestoreProperty]
        public string GV_EMAIL { set; get; }


        [DisplayName("CMNN")]
        [FirestoreProperty]
        public string GV_CMNN { set; get; }

        [DisplayName("Học vị")]
        [FirestoreProperty]
        public string GV_HOCVI { set; get; }


        [DisplayName("Hướng nghiên cứu")]
        [FirestoreProperty]
        public string GV_HUONGNGHIENCUU { set; get; }

         
        

    }
}