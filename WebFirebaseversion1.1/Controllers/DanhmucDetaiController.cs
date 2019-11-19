using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebFirebaseversion1._1.Models;
using WebMVCFirebaseOffical.App_Start;
using WebMVCFirebaseOffical.Models;

namespace WebMVCFirebaseOffical.Controllers
{
    public class DanhmucDetaiController : Controller
    {

        // GET: Thông tin inhVien
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {

            string path = Server.MapPath("~/quanlydetaiOffical.json");
            FirestoreDb db = FirebaseConnect.connectFB(path);

            var list = new List<DanhmucDetaiModels>();
            Query allobjQuery = db.Collection("danhmucdetai");

            QuerySnapshot allobjQueryQuerySnapshot = await allobjQuery.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in allobjQueryQuerySnapshot.Documents)
            {

                if (documentSnapshot.Exists)
                {

                    DanhmucDetaiModels obj = documentSnapshot.ConvertTo<DanhmucDetaiModels>();
                    obj.DM_MA = documentSnapshot.Id;
                    obj.DM_TEN = documentSnapshot.GetValue<string>("DM_TEN");
                     
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
                var list_dm = new List<DanhmucDetaiModels>(); 
                Query allobjQueryyy = db.Collection("danhmucdetai");
                QuerySnapshot allobjQueryQuerySnapshottt = await allobjQueryyy.GetSnapshotAsync();
                foreach (DocumentSnapshot documentSnapshottt in allobjQueryQuerySnapshottt.Documents)
                {

                    if (documentSnapshottt.Exists && (documentSnapshottt.Id == id))
                    {

                        DanhmucDetaiModels obj = documentSnapshottt.ConvertTo<DanhmucDetaiModels>();
                        obj.DM_MA = documentSnapshottt.Id;
                        obj.DM_TEN = documentSnapshottt.GetValue<string>("DM_TEN"); 
                        list_dm.Add(obj);
                    }

                }

                var list_dt = new List<DanhMuc_Detai>();
                Query allobjQuery = db.Collection("danhmucdetai").Document(id).Collection("DETAI");
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
                ViewModelsDetai final = new ViewModelsDetai();
                final.danhMuc_Detais = list_dt;
                final.danhmucDetaiModels = list_dm;
                return View(final);
            }
            catch (Exception)
            {
                return View();
            }
        }

        public async System.Threading.Tasks.Task<ActionResult> DetailsDT(string id)
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
            var danhmuc = new DanhmucDetaiModels();
            danhmuc.DM_MA = CreateID.createID_byDocument("dm");

            return View(danhmuc);
        }
        // POST: xử lý sự kiện khi muốn tạo thông tin 
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Create(DanhmucDetaiModels obj)
        {
            try
            {
                // TODO: Add insert logic here
                string id = CreateID.createID_byDocument("dm");
                string path = Server.MapPath("~/quanlydetaiOffical.json");
                FirestoreDb db = FirebaseConnect.connectFB(path);
                DocumentReference docRef = db.Collection("danhmucdetai").Document(id);
                Dictionary<string, object> create = new Dictionary<string, object>
                {
                    { "DM_TEN",obj.DM_TEN },
                     
                };
                await docRef.SetAsync(create);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }


        }
        public ActionResult CreateDT()
        {
            var detai = new DetaiModels();
            detai.DT_MA = CreateID.createID_byDocument("dm");

            return View(detai);
        }
        // POST: xử lý sự kiện khi muốn tạo thông tin 
        [HttpPost]
        [ValidateInput(false)]
        public async System.Threading.Tasks.Task<ActionResult> CreateDT(string id, DetaiModels dt)
        {
            // TODO: Add insert logic here

            string path = Server.MapPath("~/quanlydetaiOffical.json");
            FirestoreDb db = FirebaseConnect.connectFB(path); 
            DocumentReference docRef = db.Collection("detai").Document(dt.DT_MA);
            Dictionary<string, object> create = new Dictionary<string, object>
                {
                    { "DT_TEN",dt.DT_TEN },
                    { "DT_NOIDUNG",dt.DT_NOIDUNG },
                    { "DT_PHAMVI",dt.DT_PHAMVI },
                    { "DT_TUKHOA",dt.DT_TUKHOA },
                    { "DT_NGAYTAO",dt.DT_NGAYTAO },
                    { "DT_NGUOITAO",dt.DT_NGUOITAO },
                    { "DT_NGAYSUA",dt.DT_NGAYSUA },
                    { "DT_NGUOISUA",dt.DT_NGUOISUA },
                    { "DT_TRANGTHAI",dt.DT_TRANGTHAI }
                };
            await docRef.SetAsync(create);

            DocumentReference docRefff = db.Collection("danhmucdetai").Document(id).Collection("DETAI").Document(dt.DT_MA);
            Dictionary<string, object> createee = new Dictionary<string, object>
                {
                   { "DT_TEN",dt.DT_TEN },
                   { "DT_NGUOITAO",dt.DT_NGUOITAO },
                   { "DT_NGAYTAO",dt.DT_NGAYTAO },
                   { "DT_TRANGTHAI",dt.DT_TRANGTHAI } 
                };
            await docRefff.SetAsync(createee); 
            return RedirectToAction(id, "danhmucdetai/Details"); 
        }

        // GET: Thông tin sinh viên cần sửa đổi
        public async System.Threading.Tasks.Task<ActionResult> Edit(string id)
        {
            string path = Server.MapPath("~/quanlydetaiOffical.json");
            FirestoreDb db = FirebaseConnect.connectFB(path);
            var list = new List<DanhmucDetaiModels>();
            DocumentReference docRef = db.Collection("danhmucdetai").Document(id);
            DocumentSnapshot documentSnapshot = await docRef.GetSnapshotAsync();
            DanhmucDetaiModels obj = new DanhmucDetaiModels();
            if (documentSnapshot.Exists)
            {
                obj = documentSnapshot.ConvertTo<DanhmucDetaiModels>();
                obj.DM_MA = documentSnapshot.Id;
                obj.DM_TEN = documentSnapshot.GetValue<string>("DM_TEN");
                
            }
            return View(obj);
        }

        // POST: xử lý sự kiện khi cần thay đổi thông tin 
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Edit(string id, DanhmucDetaiModels obj)
        {
            try
            {
                // TODO: Add insert logic here

                string path = Server.MapPath("~/quanlydetaiOffical.json");
                FirestoreDb db = FirebaseConnect.connectFB(path);
                DocumentReference docRef = db.Collection("danhmucdetai").Document(id);
                Dictionary<string, object> create = new Dictionary<string, object>
                {
                    { "DM_TEN",obj.DM_TEN },
                   
                };
                await docRef.SetAsync(create);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public async System.Threading.Tasks.Task<ActionResult> EditDT(string DT_ID)
        {
            string path = Server.MapPath("~/quanlydetaiOffical.json");
            FirestoreDb db = FirebaseConnect.connectFB(path);
            var list = new List<DetaiModels>();
            DocumentReference docRef = db.Collection("detai").Document(DT_ID);
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
        public async System.Threading.Tasks.Task<ActionResult> EditDT(string id, string DT_ID, DetaiModels obj)
        {
            try
            {
                string path = Server.MapPath("~/quanlydetaiOffical.json");
                FirestoreDb db = FirebaseConnect.connectFB(path);
                DocumentReference docRef = db.Collection("detai").Document(DT_ID);
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

                DocumentReference docRefff = db.Collection("danhmucdetai").Document(id).Collection("DETAI").Document(DT_ID);
                Dictionary<string, object> createee = new Dictionary<string, object>
                {
                   { "DT_TEN",obj.DT_TEN },
                   { "DT_NGUOITAO",obj.DT_NGUOITAO },
                   { "DT_NGAYTAO",obj.DT_NGAYTAO },
                   { "DT_TRANGTHAI",obj.DT_TRANGTHAI }

                };
                await docRefff.SetAsync(createee);



                return RedirectToAction(id, "danhmucdetai/Details");

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
                var list = new List<DanhmucDetaiModels>();
                DocumentReference docRef = db.Collection("danhmucdetai").Document(id);
                DocumentSnapshot documentSnapshot = await docRef.GetSnapshotAsync();
                DanhmucDetaiModels obj = new DanhmucDetaiModels();
                if (documentSnapshot.Exists)
                {
                    obj = documentSnapshot.ConvertTo<DanhmucDetaiModels>();
                    obj.DM_MA = documentSnapshot.Id;
                    obj.DM_TEN = documentSnapshot.GetValue<string>("DM_TEN");
                     
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
                var list = new List<DanhmucDetaiModels>();
                DocumentReference docRef = db.Collection("danhmucdetai").Document(id);

                await docRef.DeleteAsync();

                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                return View();
            }
        }
        public async System.Threading.Tasks.Task<ActionResult> DeleteDT(string id, string DT_ID)
        {
            try
            {
                string path = Server.MapPath("~/quanlydetaiOffical.json");
                FirestoreDb db = FirebaseConnect.connectFB(path);
                var list = new List<DanhMuc_Detai>();
                DocumentReference docRef = db.Collection("danhmucdetai").Document(id).Collection("DETAI").Document(DT_ID);
                DocumentSnapshot documentSnapshot = await docRef.GetSnapshotAsync();
                DanhMuc_Detai obj = new DanhMuc_Detai();
                if (documentSnapshot.Exists)
                {
                    obj = documentSnapshot.ConvertTo<DanhMuc_Detai>();
                    obj.DT_MA = documentSnapshot.Id;
                    obj.DT_TEN = documentSnapshot.GetValue<string>("DT_TEN");
                    obj.DT_NGUOITAO = documentSnapshot.GetValue<string>("DT_NGUOITAO");
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
        [HttpPost, ActionName("DeleteDT")]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> DeleteConfirmedDT(string id, string DT_ID)
        {
            try
            {

                string path = Server.MapPath("~/quanlydetaiOffical.json");
                FirestoreDb db = FirebaseConnect.connectFB(path);
                var list = new List<DanhmucDetaiModels>();
                DocumentReference docRef = db.Collection("danhmucdetai").Document(id).Collection("DETAI").Document(DT_ID);

                await docRef.DeleteAsync();
                DocumentReference docReff = db.Collection("detai").Document(DT_ID);

                await docReff.DeleteAsync();

                return RedirectToAction(id, "danhmucdetai/Details");

            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}