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
    public class DetaiController : Controller
    {

        // GET: Thông tin inhVien
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {

            string path = Server.MapPath("~/quanlydetaiOffical.json");
            FirestoreDb db = FirebaseConnect.connectFB(path);

            var list = new List<DetaiModels>();
            Query allobjQuery = db.Collection("detai");

            QuerySnapshot allobjQueryQuerySnapshot = await allobjQuery.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in allobjQueryQuerySnapshot.Documents)
            {

                if (documentSnapshot.Exists)
                {

                    DetaiModels obj = documentSnapshot.ConvertTo<DetaiModels>();
                    obj.DT_MA = documentSnapshot.Id;
                    obj.DT_TEN = documentSnapshot.GetValue<string>("DT_TEN");
                    obj.DT_NOIDUNG = documentSnapshot.GetValue<string>("DT_NOIDUNG");
                    obj.DT_PHAMVI = documentSnapshot.GetValue<string>("DT_PHAMVI");
                    obj.DT_TUKHOA = documentSnapshot.GetValue<string>("DT_TUKHOA");
                    obj.DT_NGAYTAO = documentSnapshot.GetValue<string>("DT_NGAYTAO");
                    obj.DT_NGUOITAO = documentSnapshot.GetValue<string>("DT_NGUOITAO");
                    obj.DT_NGUOITAO = documentSnapshot.GetValue<string>("DT_NGUOITAO");
                    obj.DT_NGAYSUA = documentSnapshot.GetValue<string>("DT_NGAYSUA");
                    obj.DT_NGUOISUA = documentSnapshot.GetValue<string>("DT_NGUOISUA");
                    obj.DT_TRANGTHAI = documentSnapshot.GetValue<string>("DT_TRANGTHAI");
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
                var list = new List<DetaiModels>();
                DocumentReference docRef = db.Collection("detai").Document(id);
                DocumentSnapshot documentSnapshot = await docRef.GetSnapshotAsync();
                DetaiModels obj = new DetaiModels();
                if (documentSnapshot.Exists)
                {
                    obj = documentSnapshot.ConvertTo<DetaiModels>();
                    obj.DT_MA = documentSnapshot.Id;
                    obj.DT_TEN = documentSnapshot.GetValue<string>("DT_TEN");
                    obj.DT_NOIDUNG = documentSnapshot.GetValue<string>("DT_NOIDUNG");
                    obj.DT_PHAMVI = documentSnapshot.GetValue<string>("DT_PHAMVI");
                    obj.DT_TUKHOA = documentSnapshot.GetValue<string>("DT_TUKHOA");
                    obj.DT_NGAYTAO = documentSnapshot.GetValue<string>("DT_NGAYTAO");
                    obj.DT_NGUOITAO = documentSnapshot.GetValue<string>("DT_NGUOITAO");
                    obj.DT_NGAYSUA = documentSnapshot.GetValue<string>("DT_NGAYSUA");
                    obj.DT_NGUOISUA = documentSnapshot.GetValue<string>("DT_NGUOISUA");
                    obj.DT_TRANGTHAI = documentSnapshot.GetValue<string>("DT_TRANGTHAI");
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
            var detai = new DetaiModels();
            detai.DT_MA = CreateID.createID_byDocument("dt");
            return View(detai);
        }
        // POST: xử lý sự kiện khi muốn tạo thông tin 
        [HttpPost]
        [ValidateInput(false)]
        public async System.Threading.Tasks.Task<ActionResult> Create(DetaiModels obj)
        {
            try
            {
                // TODO: Add insert logic here
                string id = CreateID.createID_byDocument("dt");
                string path = Server.MapPath("~/quanlydetaiOffical.json");
                FirestoreDb db = FirebaseConnect.connectFB(path);
                DocumentReference docRef = db.Collection("detai").Document(id);
                Dictionary<string, object> create = new Dictionary<string, object>
                {
                    { "DT_TEN",obj.DT_TEN },
                    { "DT_NOIDUNG",obj.DT_NOIDUNG },
                    { "DT_PHAMVI",obj.DT_PHAMVI },
                    { "DT_TUKHOA",obj.DT_TUKHOA },
                    { "DT_NGAYTAO",obj.DT_NGAYTAO },
                    { "DT_NGUOITAO",obj.DT_NGUOITAO },
                    { "DT_NGAYSUA",obj.DT_NGAYSUA },
                    { "DT_NGUOISUA",obj.DT_NGUOISUA },
                    { "DT_TRANGTHAI",obj.DT_TRANGTHAI }
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
            var list = new List<DetaiModels>();
            DocumentReference docRef = db.Collection("detai").Document(id);
            DocumentSnapshot documentSnapshot = await docRef.GetSnapshotAsync();
            DetaiModels obj = new DetaiModels();
            if (documentSnapshot.Exists)
            {
                obj = documentSnapshot.ConvertTo<DetaiModels>();
                obj.DT_MA = documentSnapshot.Id;
                obj.DT_TEN = documentSnapshot.GetValue<string>("DT_TEN");
                obj.DT_NOIDUNG = documentSnapshot.GetValue<string>("DT_NOIDUNG");
                obj.DT_PHAMVI = documentSnapshot.GetValue<string>("DT_PHAMVI");
                obj.DT_TUKHOA = documentSnapshot.GetValue<string>("DT_TUKHOA");
                obj.DT_NGAYTAO = documentSnapshot.GetValue<string>("DT_NGAYTAO");
                obj.DT_NGUOITAO = documentSnapshot.GetValue<string>("DT_NGUOITAO");
                obj.DT_NGAYSUA = documentSnapshot.GetValue<string>("DT_NGAYSUA");
                obj.DT_NGUOISUA = documentSnapshot.GetValue<string>("DT_NGUOISUA");
                obj.DT_TRANGTHAI = documentSnapshot.GetValue<string>("DT_TRANGTHAI");
            }
            return View(obj);
        }

        // POST: xử lý sự kiện khi cần thay đổi thông tin 
        [HttpPost]
        [ValidateInput(false)]
        public async System.Threading.Tasks.Task<ActionResult> Edit(string id, DetaiModels obj)
        {
            try
            {
                // TODO: Add insert logic here

                string path = Server.MapPath("~/quanlydetaiOffical.json");
                FirestoreDb db = FirebaseConnect.connectFB(path);
                DocumentReference docRef = db.Collection("detai").Document(id);
                Dictionary<string, object> create = new Dictionary<string, object>
                {
                    { "DT_TEN",obj.DT_TEN },
                    { "DT_NOIDUNG",obj.DT_NOIDUNG },
                    { "DT_PHAMVI",obj.DT_PHAMVI },
                    { "DT_TUKHOA",obj.DT_TUKHOA },
                    { "DT_NGAYTAO",obj.DT_NGAYTAO },
                    { "DT_NGUOITAO",obj.DT_NGUOITAO },
                    { "DT_NGAYSUA",obj.DT_NGAYSUA },
                    { "DT_NGUOISUA",obj.DT_NGUOISUA },
                    { "DT_TRANGTHAI",obj.DT_TRANGTHAI }
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
                var list = new List<DetaiModels>();
                DocumentReference docRef = db.Collection("detai").Document(id);
                DocumentSnapshot documentSnapshot = await docRef.GetSnapshotAsync();
                DetaiModels obj = new DetaiModels();
                if (documentSnapshot.Exists)
                {
                    obj = documentSnapshot.ConvertTo<DetaiModels>();
                    obj.DT_MA = documentSnapshot.Id;
                    obj.DT_TEN = documentSnapshot.GetValue<string>("DT_TEN");
                    obj.DT_NOIDUNG = documentSnapshot.GetValue<string>("DT_NOIDUNG");
                    obj.DT_PHAMVI = documentSnapshot.GetValue<string>("DT_PHAMVI");
                    obj.DT_TUKHOA = documentSnapshot.GetValue<string>("DT_TUKHOA");
                    obj.DT_NGAYTAO = documentSnapshot.GetValue<string>("DT_NGAYTAO");
                    obj.DT_NGUOITAO = documentSnapshot.GetValue<string>("DT_NGUOITAO");
                    obj.DT_NGAYSUA = documentSnapshot.GetValue<string>("DT_NGAYSUA");
                    obj.DT_NGUOISUA = documentSnapshot.GetValue<string>("DT_NGUOISUA");
                    obj.DT_TRANGTHAI = documentSnapshot.GetValue<string>("DT_TRANGTHAI");
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
                var list = new List<DetaiModels>();
                DocumentReference docReff = db.Collection("detai").Document(id);

                await docReff.DeleteAsync();
                Query allobjQueryyy = db.Collection("danhmucdetai");
                QuerySnapshot allobjQueryQuerySnapshottt = await allobjQueryyy.GetSnapshotAsync();
                foreach (DocumentSnapshot documentSnapshottt in allobjQueryQuerySnapshottt.Documents)
                {

                    if (documentSnapshottt.Exists )
                    {
                        DocumentReference docRef = db.Collection("danhmucdetai").Document(documentSnapshottt.Id).Collection("DETAI").Document(id);

                        await docRef.DeleteAsync();
                    }

                }
              
               

                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}