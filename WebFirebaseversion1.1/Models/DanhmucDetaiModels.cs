using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebMVCFirebaseOffical.Models
{
    [FirestoreData]
    public class DanhmucDetaiModels
    {
        [DisplayName("Mã danh mục")]
        [FirestoreProperty]
        public string DM_MA { set; get; }

        [DisplayName("Tên danh mục")]
        [FirestoreProperty]
        public string DM_TEN { set; get; }
 
    }
}