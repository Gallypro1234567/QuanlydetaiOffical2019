﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMVCFirebaseOffical.Models;

namespace WebFirebaseversion1._1.Models
{
    public class ViewModelsGV_DT
    {
        public List<DanhMuc_Detai> danhMuc_Detais { set; get; }
        public List<GiangvienModels> giangvienModels { set; get; } 
        public List<DanhmucDetaiModels> danhmucDetaiModels { set; get; }
        public List<SinhvienModels> sinhvienModels { set; get; }
    }
}