using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebFirebaseversion1._1.Models;
using WebMVCFirebaseOffical.App_Start;
using WebMVCFirebaseOffical.Models;

namespace WebFirebaseversion1._1.Controllers
{
    [RoutePrefix("home")]
    public class HomeController : Controller
    {
         [Route("index")]
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            try
            {
                string path = Server.MapPath("~/quanlydetaiOffical.json");
                FirestoreDb db = FirebaseConnect.connectFB(path);

                var list_dt = new List<DanhMuc_Detai>();
                var list_dm = new List<DanhmucDetaiModels>();
                Query allobjQueryDM = db.Collection("danhmucdetai");
                QuerySnapshot allobjQueryQuerySnapshotDM = await allobjQueryDM.GetSnapshotAsync();
                foreach (DocumentSnapshot documentSnapshotDM in allobjQueryQuerySnapshotDM.Documents)
                {

                    if (documentSnapshotDM.Exists)
                    {

                        DanhmucDetaiModels obj = documentSnapshotDM.ConvertTo<DanhmucDetaiModels>();
                        obj.DM_MA = documentSnapshotDM.Id;
                        obj.DM_TEN = documentSnapshotDM.GetValue<string>("DM_TEN"); 
                        list_dm.Add(obj);
                       
                        Query allobjQuery = db.Collection("danhmucdetai").Document(documentSnapshotDM.Id).Collection("DETAI");
                        QuerySnapshot allobjQueryQuerySnapshot = await allobjQuery.GetSnapshotAsync();
                        foreach (DocumentSnapshot documentSnapshot in allobjQueryQuerySnapshot.Documents)
                        {
                            if (documentSnapshot.Exists)
                            {

                                DanhMuc_Detai dt = documentSnapshot.ConvertTo<DanhMuc_Detai>();
                                dt.DT_MA = documentSnapshot.Id;
                                dt.DT_TEN = documentSnapshot.GetValue<string>("DT_TEN");
                                dt.DT_NGUOITAO = documentSnapshot.GetValue<string>("DT_NGUOITAO");
                                dt.DT_TRANGTHAI = documentSnapshot.GetValue<string>("DT_TRANGTHAI");
                                list_dt.Add(dt);
                            }
                        }
                    }

                }

                var list_gv = new List<GiangvienModels>();
                Query allobjQueryyyGV = db.Collection("giangvien");
                QuerySnapshot allobjQueryQuerySnapshotttGV = await allobjQueryyyGV.GetSnapshotAsync();
                foreach (DocumentSnapshot documentSnapshotttGV in allobjQueryQuerySnapshotttGV.Documents)
                {

                    if (documentSnapshotttGV.Exists)
                    {

                        GiangvienModels obj = documentSnapshotttGV.ConvertTo<GiangvienModels>();
                        obj.GV_MA = documentSnapshotttGV.Id;
                        obj.GV_HOTEN = documentSnapshotttGV.GetValue<string>("GV_HOTEN");
                        list_gv.Add(obj);
                    }

                }

               
                ViewModelsGV_DT final = new ViewModelsGV_DT();
                final.danhMuc_Detais = list_dt;
                final.danhmucDetaiModels = list_dm;
                final.giangvienModels = list_gv;
                return View(final);
            }
            catch (Exception)
            {
                return View();
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}