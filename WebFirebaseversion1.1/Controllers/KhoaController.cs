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
    public class KhoaController : Controller
    {
        // GET: Thông tin inhVien
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {

            string path = Server.MapPath("~/quanlydetaiOffical.json");
            FirestoreDb db = FirebaseConnect.connectFB(path);

            var list = new List<KhoaModels>();
            Query allobjQuery = db.Collection("khoa");

            QuerySnapshot allobjQueryQuerySnapshot = await allobjQuery.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in allobjQueryQuerySnapshot.Documents)
            {

                if (documentSnapshot.Exists)
                {

                    KhoaModels obj = documentSnapshot.ConvertTo<KhoaModels>();
                    obj.KH_MA = documentSnapshot.Id;
                    obj.KH_TEN = documentSnapshot.GetValue<string>("KH_TEN");                     
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
                var list = new List<KhoaModels>();
                DocumentReference docRef = db.Collection("khoa").Document(id);
                DocumentSnapshot documentSnapshot = await docRef.GetSnapshotAsync();
                KhoaModels obj = new KhoaModels();
                if (documentSnapshot.Exists)
                {
                    obj = documentSnapshot.ConvertTo<KhoaModels>();
                    obj.KH_MA = documentSnapshot.Id;
                    obj.KH_TEN = documentSnapshot.GetValue<string>("KH_TEN");
                    
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
            var khoa = new KhoaModels();
            khoa.KH_MA = CreateID.createID_byDocument("kh");
            return View(khoa);
        }
        // POST: xử lý sự kiện khi muốn tạo thông tin 
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Create(KhoaModels obj)
        {
            try
            {
                // TODO: Add insert logic here
                string id = CreateID.createID_byDocument("kh");
                string path = Server.MapPath("~/quanlydetaiOffical.json");
                FirestoreDb db = FirebaseConnect.connectFB(path);
                DocumentReference docRef = db.Collection("khoa").Document(id);
                Dictionary<string, object> create = new Dictionary<string, object>
                {
                    { "KH_TEN",obj.KH_TEN }
                     
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
            var list = new List<KhoaModels>();
            DocumentReference docRef = db.Collection("khoa").Document(id);
            DocumentSnapshot documentSnapshot = await docRef.GetSnapshotAsync();
            KhoaModels obj = new KhoaModels();
            if (documentSnapshot.Exists)
            {
                obj = documentSnapshot.ConvertTo<KhoaModels>();
                obj.KH_MA = documentSnapshot.Id;
                obj.KH_TEN = documentSnapshot.GetValue<string>("KH_TEN");
                 
            }
            return View(obj);
        }

        // POST: xử lý sự kiện khi cần thay đổi thông tin 
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Edit(string id, KhoaModels obj)
        {
            try
            {
                // TODO: Add insert logic here

                string path = Server.MapPath("~/quanlydetaiOffical.json");
                FirestoreDb db = FirebaseConnect.connectFB(path);
                DocumentReference docRef = db.Collection("khoa").Document(id);
                Dictionary<string, object> create = new Dictionary<string, object>
                {
                    { "KH_TEN",obj.KH_TEN },
                    
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
                var list = new List<KhoaModels>();
                DocumentReference docRef = db.Collection("khoa").Document(id);
                DocumentSnapshot documentSnapshot = await docRef.GetSnapshotAsync();
                KhoaModels obj = new KhoaModels();
                if (documentSnapshot.Exists)
                {
                    obj = documentSnapshot.ConvertTo<KhoaModels>();
                    obj.KH_MA = documentSnapshot.Id;
                    obj.KH_TEN = documentSnapshot.GetValue<string>("KH_TEN");
                     
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
                var list = new List<KhoaModels>();
                DocumentReference docRef = db.Collection("khoa").Document(id);

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