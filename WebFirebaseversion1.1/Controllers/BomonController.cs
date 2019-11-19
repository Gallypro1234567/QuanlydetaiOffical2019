using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMVCFirebaseOffical.App_Start;
using WebMVCFirebaseOffical.Models;

namespace WebMVCFirebaseOffical.Controllers
{
    public class BomonController : Controller
    {
        // GET: Thông tin inhVien
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {

            string path = Server.MapPath("~/quanlydetaiOffical.json");
            FirestoreDb db = FirebaseConnect.connectFB(path);

            var list = new List<BomonModels>();
            Query allobjQuery = db.Collection("BOMON");

            QuerySnapshot allobjQueryQuerySnapshot = await allobjQuery.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in allobjQueryQuerySnapshot.Documents)
            {

                if (documentSnapshot.Exists)
                {

                    BomonModels obj = documentSnapshot.ConvertTo<BomonModels>();
                    obj.BM_MA = documentSnapshot.Id;
                    obj.BM_TEN = documentSnapshot.GetValue<string>("BM_TEN");
                    obj.BM_CANBO_PHUTRACH = documentSnapshot.GetValue<string>("BM_CANBO_PHUTRACH");                     
                    list.Add(obj);
                }

            }
            return View(list);


        }

        // GET: Chi tiết sinh viên
        public async System.Threading.Tasks.Task<ActionResult> Details(string id)
        {
            try
            {
                string path = Server.MapPath("~/quanlydetaiOffical.json");
                FirestoreDb db = FirebaseConnect.connectFB(path);
                var list = new List<BomonModels>();
                DocumentReference docRef = db.Collection("BOMON").Document(id);
                DocumentSnapshot documentSnapshot = await docRef.GetSnapshotAsync();
                BomonModels obj = new BomonModels();
                if (documentSnapshot.Exists)
                {
                    obj = documentSnapshot.ConvertTo<BomonModels>();
                    obj.BM_MA = documentSnapshot.Id;
                    obj.BM_TEN = documentSnapshot.GetValue<string>("BM_TEN");
                    obj.BM_CANBO_PHUTRACH = documentSnapshot.GetValue<string>("BM_CANBO_PHUTRACH");
                     
                }
                return View(obj);
            }
            catch (Exception)
            {
                return View();
            }
        }

        public ActionResult Create()
        {
            var bomon = new BomonModels();
            bomon.BM_MA = CreateID.createID_byDocument("bm");
            return View(bomon);
        }
        // POST: xử lý sự kiện khi muốn tạo thông tin 
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Create(BomonModels obj)
        {
            try
            {
                // TODO: Add insert logic here
                string id = CreateID.createID_byDocument("bm");
                string path = Server.MapPath("~/quanlydetaiOffical.json");
                FirestoreDb db = FirebaseConnect.connectFB(path);
                DocumentReference docRef = db.Collection("BOMON").Document(id);
                Dictionary<string, object> create = new Dictionary<string, object>
                {
                    { "BM_TEN",obj.BM_TEN },
                    { "BM_CANBO_PHUTRACH",obj.BM_CANBO_PHUTRACH },
                      
                };
                await docRef.SetAsync(create);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }


        }

        // GET: Thông tin sinh viên cần sửa đổi
        public async System.Threading.Tasks.Task<ActionResult> Edit(string id)
        {
            string path = Server.MapPath("~/quanlydetaiOffical.json");
            FirestoreDb db = FirebaseConnect.connectFB(path);
            var list = new List<BomonModels>();
            DocumentReference docRef = db.Collection("BOMON").Document(id);
            DocumentSnapshot documentSnapshot = await docRef.GetSnapshotAsync();
            BomonModels obj = new BomonModels();
            if (documentSnapshot.Exists)
            {
                obj = documentSnapshot.ConvertTo<BomonModels>();
                obj.BM_MA = documentSnapshot.Id;
                obj.BM_TEN = documentSnapshot.GetValue<string>("BM_TEN");
                obj.BM_CANBO_PHUTRACH = documentSnapshot.GetValue<string>("BM_CANBO_PHUTRACH");
                
            }
            return View(obj);
        }

        // POST: xử lý sự kiện khi cần thay đổi thông tin 
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Edit(string id, BomonModels obj)
        {
            try
            {
                // TODO: Add insert logic here

                string path = Server.MapPath("~/quanlydetaiOffical.json");
                FirestoreDb db = FirebaseConnect.connectFB(path);
                DocumentReference docRef = db.Collection("BOMON").Document(id);
                Dictionary<string, object> create = new Dictionary<string, object>
                {
                    { "BM_TEN",obj.BM_TEN },
                    { "BM_CANBO_PHUTRACH",obj.BM_CANBO_PHUTRACH },
                     
                };
                await docRef.SetAsync(create);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: Lấy thông tin cần xóa
        public async System.Threading.Tasks.Task<ActionResult> Delete(string id)
        {
            try
            {
                string path = Server.MapPath("~/quanlydetaiOffical.json");
                FirestoreDb db = FirebaseConnect.connectFB(path);
                var list = new List<BomonModels>();
                DocumentReference docRef = db.Collection("BOMON").Document(id);
                DocumentSnapshot documentSnapshot = await docRef.GetSnapshotAsync();
                BomonModels obj = new BomonModels();
                if (documentSnapshot.Exists)
                {
                    obj = documentSnapshot.ConvertTo<BomonModels>();
                    obj.BM_MA = documentSnapshot.Id;
                    obj.BM_TEN = documentSnapshot.GetValue<string>("BM_TEN");
                    obj.BM_CANBO_PHUTRACH = documentSnapshot.GetValue<string>("BM_CANBO_PHUTRACH");
                    
                }
                return View(obj);
            }
            catch (Exception)
            {
                return View();
            }
        }

        // POST: Xử lý sự kiện cần xóa thông tin
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> DeleteConfirmed(string id)
        {
            try
            {

                string path = Server.MapPath("~/quanlydetaiOffical.json");
                FirestoreDb db = FirebaseConnect.connectFB(path);
                var list = new List<BomonModels>();
                DocumentReference docRef = db.Collection("BOMON").Document(id);

                await docRef.DeleteAsync();

                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}