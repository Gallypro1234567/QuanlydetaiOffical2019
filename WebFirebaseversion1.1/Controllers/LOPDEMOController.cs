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
    public class LOPDEMOController : Controller
    {
        // GET: LOPDEMO
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {

            string path = Server.MapPath("~/quanlydetaiOffical.json");
            FirestoreDb db = FirebaseConnect.connectFB(path);

            var list = new List<LopDemo>();
            Query allobjQuery = db.Collection("lopdemo");

            QuerySnapshot allobjQueryQuerySnapshot = await allobjQuery.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in allobjQueryQuerySnapshot.Documents)
            {

                if (documentSnapshot.Exists)
                {

                    LopDemo obj = documentSnapshot.ConvertTo<LopDemo>();
                    obj.LO_MA = documentSnapshot.Id;
                    obj.LO_TEN = documentSnapshot.GetValue<string>("LO_TEN");
                    obj.LO_SOLUONG = documentSnapshot.GetValue<string>("LO_SOLUONG");
                    obj.LO_THOIGIAN = documentSnapshot.GetValue<string>("LO_THOIGIAN");
                    /*Query allobjQueryyy = db.Collection("lop").Document("123").Collection("SINHVIEN");
                    QuerySnapshot allobjQueryQuerySnapshotttt = await allobjQuery.GetSnapshotAsync();
                    foreach (DocumentSnapshot documentSnapshotttt in allobjQueryQuerySnapshot.Documents)
                    {
                        Sinhvien objjj = documentSnapshotttt.ConvertTo<Sinhvien>();
                        objjj.LO_SV_ID = documentSnapshot.Id;
                        objjj.LO_SV_HOTEN = documentSnapshot.GetValue<string>("LO_SV_HOTEN");
                        objjj.LO_SV_SDT = documentSnapshot.GetValue<string>("LO_SV_SDT");
                        objjj.LO_SV_EMAIL = documentSnapshot.GetValue<string>("LO_SV_EMAIL");
                    }*/
                    list.Add(obj);
                }
                

            }
            
            return View(list);
            


        }
        public async System.Threading.Tasks.Task<ActionResult> Detail(string id)
        { 
            string path = Server.MapPath("~/quanlydetaiOffical.json");
            FirestoreDb db = FirebaseConnect.connectFB(path);

            var list_sv = new List<Sinhvien>();
            Query allobjQuery = db.Collection("lopdemo").Document(id).Collection("SINHVIEN"); 
            QuerySnapshot allobjQueryQuerySnapshot = await allobjQuery.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in allobjQueryQuerySnapshot.Documents)
            { 
                if (documentSnapshot.Exists)
                {

                    Sinhvien sv = documentSnapshot.ConvertTo<Sinhvien>();
                    sv.LO_SV_ID = documentSnapshot.Id;
                    sv.LO_SV_HOTEN = documentSnapshot.GetValue<string>("LO_SV_HOTEN");
                    sv.LO_SV_SDT = documentSnapshot.GetValue<string>("LO_SV_SDT");
                    sv.LO_SV_EMAIL = documentSnapshot.GetValue<string>("LO_SV_EMAIL"); 
                    list_sv.Add(sv);
                } 
            }


            var list_lop = new List<LopDemo>();
            Query allobjQueryyy = db.Collection("lopdemo"); 
            QuerySnapshot allobjQueryQuerySnapshottt = await allobjQueryyy.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshottt in allobjQueryQuerySnapshottt.Documents)
            {

                if (documentSnapshottt.Exists && (documentSnapshottt.Id == id))
                {

                    LopDemo obj = documentSnapshottt.ConvertTo<LopDemo>();
                    obj.LO_MA = documentSnapshottt.Id;
                    obj.LO_TEN = documentSnapshottt.GetValue<string>("LO_TEN");
                    obj.LO_SOLUONG = documentSnapshottt.GetValue<string>("LO_SOLUONG");
                    obj.LO_THOIGIAN = documentSnapshottt.GetValue<string>("LO_THOIGIAN");

                    list_lop.Add(obj);
                }

            }

            /* DocumentReference docRef = db.Collection("lopdemo").Document(id);
             DocumentSnapshot documentSnapshottt = await docRef.GetSnapshotAsync();
             LopDemo list_lop = new LopDemo();
             if (documentSnapshottt.Exists)
             {
                 list_lop = documentSnapshottt.ConvertTo<LopDemo>();
                 list_lop.LO_MA = documentSnapshottt.Id;
                 list_lop.LO_TEN = documentSnapshottt.GetValue<string>("LO_TEN");
                 list_lop.LO_SOLUONG = documentSnapshottt.GetValue<string>("LO_SOLUONG");
                 list_lop.LO_THOIGIAN = documentSnapshottt.GetValue<string>("LO_THOIGIAN");

             }*/
            ViewModels final = new ViewModels();
            //final.LopDemo = list_lop;
            final.Sinhvien = list_sv;
            return View(final);
             
        }
        public ActionResult Create()
        {

            var lop = new LopDemo();
            lop.LO_MA = CreateID.createID_byDocument("lo");

            return View(lop);
        }
        // POST: xử lý sự kiện khi muốn tạo thông tin 
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Create(LopDemo obj, Sinhvien objj)
        {
            try
            {
                // TODO: Add insert logic here

                string path = Server.MapPath("~/quanlydetaiOffical.json");
                FirestoreDb db = FirebaseConnect.connectFB(path);
                DocumentReference docRef = db.Collection("lopdemo").Document(obj.LO_MA);
                Dictionary<string, object> create = new Dictionary<string, object>
                {
                    { "LO_TEN",obj.LO_TEN },
                    { "LO_SOLUONG",obj.LO_SOLUONG },
                    { "LO_THOIGIAN",obj.LO_THOIGIAN },
                     

                };
                await docRef.SetAsync(create); 
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }


        }
        public ActionResult CreateSV()
        {
            var sinhvien = new SinhvienModels();
            sinhvien.SV_ID = CreateID.createID_byDocument("sv");

            return View();
        }
        // POST: xử lý sự kiện khi muốn tạo thông tin 
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> CreateSV(string id,SinhvienModels obj)
        {
              // TODO: Add insert logic here

                string path = Server.MapPath("~/quanlydetaiOffical.json");
                FirestoreDb db = FirebaseConnect.connectFB(path);
                DocumentReference docRef = db.Collection("sinhvien").Document(obj.SV_ID);
                Dictionary<string, object> create = new Dictionary<string, object>
                {
                    { "SV_HOTEN",obj.SV_HOTEN },
                    { "SV_GIOITINH",obj.SV_GIOITINH },
                    { "SV_NGAYSINH",obj.SV_NGAYSINH },
                    { "SV_NOISINH",obj.SV_NOISINH },
                    { "SV_SDT",obj.SV_SDT },
                    { "SV_EMAIL",obj.SV_EMAIL },
                    { "SV_CMNN",obj.SV_CMNN },
                    { "SV_THOIGIANDAOTAO",obj.SV_THOIGIANDAOTAO }
                };
                await docRef.SetAsync(create);

                DocumentReference docRefff = db.Collection("lopdemo").Document(id).Collection("SINHVIEN").Document(obj.SV_ID);
                Dictionary<string, object> createee = new Dictionary<string, object>
                {
                    { "LO_SV_HOTEN",obj.SV_HOTEN },
                    { "LO_SV_EMAIL",obj.SV_SDT },
                    { "LO_SV_SDT",obj.SV_EMAIL },


                };
                await docRefff.SetAsync(createee);
                
               

                return RedirectToAction("Index");

             


        }
    }
}