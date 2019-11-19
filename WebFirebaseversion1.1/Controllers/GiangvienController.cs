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
    [RoutePrefix("giangvien")]
    public class GiangvienController : Controller
    {
        // GET: Thông tin sinhVien
      
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {

            string path = Server.MapPath("~/quanlydetaiOffical.json");
            FirestoreDb db = FirebaseConnect.connectFB(path);

            var list = new List<GiangvienModels>();
            Query allobjQuery = db.Collection("giangvien");

            QuerySnapshot allobjQueryQuerySnapshot = await allobjQuery.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in allobjQueryQuerySnapshot.Documents)
            {

                if (documentSnapshot.Exists)
                {

                    GiangvienModels obj = documentSnapshot.ConvertTo<GiangvienModels>();
                    obj.GV_MA = documentSnapshot.Id;
                    obj.GV_HOTEN = documentSnapshot.GetValue<string>("GV_HOTEN");
                    obj.GV_GIOITINH = documentSnapshot.GetValue<string>("GV_GIOITINH");
                    obj.GV_NGAYSINH = documentSnapshot.GetValue<string>("GV_NGAYSINH");
                    obj.GV_NOISINH = documentSnapshot.GetValue<string>("GV_NOISINH");
                    obj.GV_SDT = documentSnapshot.GetValue<string>("GV_SDT");
                    obj.GV_EMAIL = documentSnapshot.GetValue<string>("GV_EMAIL");
                    obj.GV_CMNN = documentSnapshot.GetValue<string>("GV_CMNN");
                    obj.GV_HOCVI = documentSnapshot.GetValue<string>("GV_HOCVI");
                     

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
                var list_gv = new List<GiangvienModels>();
                Query allobjQuery = db.Collection("giangvien");
                QuerySnapshot allobjQueryQuerySnapshot = await allobjQuery.GetSnapshotAsync();
                foreach (DocumentSnapshot documentSnapshot in allobjQueryQuerySnapshot.Documents)
                {

                    if (documentSnapshot.Exists && (documentSnapshot.Id == id))
                    {
                        GiangvienModels  obj = documentSnapshot.ConvertTo<GiangvienModels>();
                        obj.GV_MA = documentSnapshot.Id;
                        obj.GV_HOTEN = documentSnapshot.GetValue<string>("GV_HOTEN");
                        obj.GV_GIOITINH = documentSnapshot.GetValue<string>("GV_GIOITINH");
                        obj.GV_NGAYSINH = documentSnapshot.GetValue<string>("GV_NGAYSINH");
                        obj.GV_NOISINH = documentSnapshot.GetValue<string>("GV_NOISINH");
                        obj.GV_SDT = documentSnapshot.GetValue<string>("GV_SDT");
                        obj.GV_EMAIL = documentSnapshot.GetValue<string>("GV_EMAIL");
                        obj.GV_CMNN = documentSnapshot.GetValue<string>("GV_CMNN");
                        obj.GV_HOCVI = documentSnapshot.GetValue<string>("GV_HOCVI");
                        list_gv.Add(obj);
                    }

                }
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
                    }

                }
                
                ViewModelsGV_DT final = new ViewModelsGV_DT();
                final.danhmucDetaiModels = list_dm;
                final.giangvienModels = list_gv;
                return View(final);
               
            }
            catch (Exception)
            {
                return View();
            }
        }

        public async System.Threading.Tasks.Task<ActionResult> DetailsDM(string DM_ID, string GV_HOTEN, string GV_ID)
        {
            try
            {
                string path = Server.MapPath("~/quanlydetaiOffical.json");
                FirestoreDb db = FirebaseConnect.connectFB(path);

                var list_dm = new List<DanhmucDetaiModels>();
                Query allobjQueryDM = db.Collection("danhmucdetai");
                QuerySnapshot allobjQueryQuerySnapshotDM = await allobjQueryDM.GetSnapshotAsync();
                foreach (DocumentSnapshot documentSnapshotDM in allobjQueryQuerySnapshotDM.Documents)
                {

                    if (documentSnapshotDM.Exists && documentSnapshotDM.Id == DM_ID)
                    {

                        DanhmucDetaiModels obj = documentSnapshotDM.ConvertTo<DanhmucDetaiModels>();
                        obj.DM_MA = documentSnapshotDM.Id;
                        obj.DM_TEN = documentSnapshotDM.GetValue<string>("DM_TEN");

                        list_dm.Add(obj);
                    }

                }

                var list_gv = new List<GiangvienModels>();
                Query allobjQueryyyGV = db.Collection("giangvien");
                QuerySnapshot allobjQueryQuerySnapshotttGV = await allobjQueryyyGV.GetSnapshotAsync();
                foreach (DocumentSnapshot documentSnapshotttGV in allobjQueryQuerySnapshotttGV.Documents)
                {

                    if (documentSnapshotttGV.Exists && documentSnapshotttGV.Id == GV_ID)
                    {

                        GiangvienModels obj = documentSnapshotttGV.ConvertTo<GiangvienModels>();
                        obj.GV_MA = documentSnapshotttGV.Id;
                        obj.GV_HOTEN = documentSnapshotttGV.GetValue<string>("GV_HOTEN");
                        list_gv.Add(obj);
                    }

                }

                var list_dt = new List<DanhMuc_Detai>();
                Query allobjQuery = db.Collection("danhmucdetai").Document(DM_ID).Collection("DETAI").WhereEqualTo("DT_NGUOITAO", GV_HOTEN);
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
            var giangvien = new GiangvienModels();
            giangvien.GV_MA = CreateID.createID_byDocument("gv");
            return View(giangvien);
        }

        // POST: xử lý sự kiện khi muốn tạo thông tin 
        [HttpPost] 
        public async System.Threading.Tasks.Task<ActionResult> Create(GiangvienModels obj)
        {
            try
            {
                // TODO: Add insert logic here
                string id = CreateID.createID_byDocument("gv");
                string path = Server.MapPath("~/quanlydetaiOffical.json");
                FirestoreDb db = FirebaseConnect.connectFB(path);
                DocumentReference docRef = db.Collection("giangvien").Document(id);
                Dictionary<string, object> create = new Dictionary<string, object>
                {
                    { "GV_HOTEN",obj.GV_HOTEN },
                    { "GV_GIOITINH",obj.GV_GIOITINH },
                    { "GV_NGAYSINH",obj.GV_NGAYSINH },
                    { "GV_NOISINH",obj.GV_NOISINH },
                    { "GV_SDT",obj.GV_SDT },
                    { "GV_EMAIL",obj.GV_EMAIL },
                    { "GV_CMNN",obj.GV_CMNN },
                    { "GV_HOCVI",obj.GV_HOCVI }
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
        public async System.Threading.Tasks.Task<ActionResult> CreateDT(string id, string GV_HOTEN, string GV_ID, DetaiModels dt)
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
            return RedirectToAction("Index");
        }
             


        // GET: Thông tin sinh viên cần sửa đổi

        public async System.Threading.Tasks.Task<ActionResult> Edit(string id)
        {
            string path = Server.MapPath("~/quanlydetaiOffical.json");
            FirestoreDb db = FirebaseConnect.connectFB(path);
            var list = new List<GiangvienModels>();
            DocumentReference docRef = db.Collection("giangvien").Document(id);
            DocumentSnapshot documentSnapshot = await docRef.GetSnapshotAsync();
            GiangvienModels obj = new GiangvienModels();
            if (documentSnapshot.Exists)
            {
                obj = documentSnapshot.ConvertTo<GiangvienModels>();
                obj.GV_MA = documentSnapshot.Id;
                obj.GV_HOTEN = documentSnapshot.GetValue<string>("GV_HOTEN");
                obj.GV_GIOITINH = documentSnapshot.GetValue<string>("GV_GIOITINH");
                obj.GV_NGAYSINH = documentSnapshot.GetValue<string>("GV_NGAYSINH");
                obj.GV_NOISINH = documentSnapshot.GetValue<string>("GV_NOISINH");
                obj.GV_SDT = documentSnapshot.GetValue<string>("GV_SDT");
                obj.GV_EMAIL = documentSnapshot.GetValue<string>("GV_EMAIL");
                obj.GV_CMNN = documentSnapshot.GetValue<string>("GV_CMNN");
                obj.GV_HOCVI = documentSnapshot.GetValue<string>("GV_HOCVI");
            }
            return View(obj);
        }

        // POST: xử lý sự kiện khi cần thay đổi thông tin 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Edit(string id, GiangvienModels obj)
        {
            try
            {
                // TODO: Add insert logic here

                string path = Server.MapPath("~/quanlydetaiOffical.json");
                FirestoreDb db = FirebaseConnect.connectFB(path);
                DocumentReference docRef = db.Collection("giangvien").Document(id);
                Dictionary<string, object> create = new Dictionary<string, object>
                {
                    { "GV_HOTEN",obj.GV_HOTEN },
                    { "GV_GIOITINH",obj.GV_GIOITINH },
                    { "GV_NGAYSINH",obj.GV_NGAYSINH },
                    { "GV_NOISINH",obj.GV_NOISINH },
                    { "GV_SDT",obj.GV_SDT },
                    { "GV_EMAIL",obj.GV_EMAIL },
                    { "GV_CMNN",obj.GV_CMNN },
                    { "GV_HOCVI",obj.GV_HOCVI }
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
                var list = new List<GiangvienModels>();
                DocumentReference docRef = db.Collection("giangvien").Document(id);
                DocumentSnapshot documentSnapshot = await docRef.GetSnapshotAsync();
                GiangvienModels obj = new GiangvienModels();
                if (documentSnapshot.Exists)
                {
                    obj = documentSnapshot.ConvertTo<GiangvienModels>();
                    obj.GV_MA = documentSnapshot.Id;
                    obj.GV_HOTEN = documentSnapshot.GetValue<string>("GV_HOTEN");
                    obj.GV_GIOITINH = documentSnapshot.GetValue<string>("GV_GIOITINH");
                    obj.GV_NGAYSINH = documentSnapshot.GetValue<string>("GV_NGAYSINH");
                    obj.GV_NOISINH = documentSnapshot.GetValue<string>("GV_NOISINH");
                    obj.GV_SDT = documentSnapshot.GetValue<string>("GV_SDT");
                    obj.GV_EMAIL = documentSnapshot.GetValue<string>("GV_EMAIL");
                    obj.GV_CMNN = documentSnapshot.GetValue<string>("GV_CMNN");
                    obj.GV_HOCVI = documentSnapshot.GetValue<string>("GV_HOCVI");
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
                var list = new List<GiangvienModels>();
                DocumentReference docRef = db.Collection("giangvien").Document(id);

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

                return RedirectToAction( "index");

            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}