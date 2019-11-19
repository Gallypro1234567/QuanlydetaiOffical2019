using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
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
    public class SinhVienController : Controller
    {
        
        // GET: Thông tin inhVien
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            
                string path = Server.MapPath("~/quanlydetaiOffical.json");
                FirestoreDb db = FirebaseConnect.connectFB(path);

                var list = new List<SinhvienModels>();
                Query allobjQuery = db.Collection("sinhvien");

                QuerySnapshot allobjQueryQuerySnapshot = await allobjQuery.GetSnapshotAsync();
                foreach (DocumentSnapshot documentSnapshot in allobjQueryQuerySnapshot.Documents)
                {                    
                 
                    if (documentSnapshot.Exists)
                    {
                         
                        SinhvienModels obj = documentSnapshot.ConvertTo<SinhvienModels>();
                        obj.SV_ID = documentSnapshot.Id;
                        obj.SV_GIOITINH = documentSnapshot.GetValue<string>("SV_GIOITINH");
                        obj.SV_HOTEN = documentSnapshot.GetValue<string>("SV_HOTEN");                        
                        obj.SV_NGAYSINH = documentSnapshot.GetValue<string>("SV_NGAYSINH");
                        obj.SV_NOISINH = documentSnapshot.GetValue<string>("SV_NOISINH");
                        obj.SV_SDT = documentSnapshot.GetValue<string>("SV_SDT");
                        obj.SV_EMAIL = documentSnapshot.GetValue<string>("SV_EMAIL");
                        obj.SV_CMNN = documentSnapshot.GetValue<string>("SV_CMNN");
                        obj.SV_THOIGIANDAOTAO = documentSnapshot.GetValue<string>("SV_THOIGIANDAOTAO");
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
                var list_sv = new List<SinhvienModels>();
                DocumentReference docRef = db.Collection("sinhvien").Document(id);
                DocumentSnapshot documentSnapshot = await docRef.GetSnapshotAsync();
                SinhvienModels obj = new SinhvienModels();
                if (documentSnapshot.Exists)
                {
                    obj = documentSnapshot.ConvertTo<SinhvienModels>();
                    obj.SV_ID = documentSnapshot.Id;
                    obj.SV_GIOITINH = documentSnapshot.GetValue<string>("SV_GIOITINH");
                    obj.SV_HOTEN = documentSnapshot.GetValue<string>("SV_HOTEN");
                    obj.SV_NGAYSINH = documentSnapshot.GetValue<string>("SV_NGAYSINH");
                    obj.SV_NOISINH = documentSnapshot.GetValue<string>("SV_NOISINH");
                    obj.SV_SDT = documentSnapshot.GetValue<string>("SV_SDT");
                    obj.SV_EMAIL = documentSnapshot.GetValue<string>("SV_EMAIL");
                    obj.SV_CMNN = documentSnapshot.GetValue<string>("SV_CMNN");
                    obj.SV_THOIGIANDAOTAO = documentSnapshot.GetValue<string>("SV_THOIGIANDAOTAO");
                    list_sv.Add(obj);
                }
                var list_dm = new List<DanhmucDetaiModels>();
                Query allobjQueryDM = db.Collection("danhmucdetai");

                QuerySnapshot allobjQueryQuerySnapshotDM = await allobjQueryDM.GetSnapshotAsync();
                foreach (DocumentSnapshot documentSnapshotDM in allobjQueryQuerySnapshotDM.Documents)
                {

                    if (documentSnapshotDM.Exists)
                    {

                        DanhmucDetaiModels dm = documentSnapshotDM.ConvertTo<DanhmucDetaiModels>();
                        dm.DM_MA = documentSnapshotDM.Id;
                        dm.DM_TEN = documentSnapshotDM.GetValue<string>("DM_TEN");

                        list_dm.Add(dm);
                    }

                }

                ViewModelsGV_DT final = new ViewModelsGV_DT();
                final.danhmucDetaiModels = list_dm;
                final.sinhvienModels = list_sv;
                return View(final);
            }
            catch (Exception)
            {
                return View();
            }
        }


        public async System.Threading.Tasks.Task<ActionResult> DetailsDM(string id)
        {

            try
            {
                string path = Server.MapPath("~/quanlydetaiOffical.json");
                FirestoreDb db = FirebaseConnect.connectFB(path);

                var list_dm = new List<DanhmucDetaiModels>();
                Query allobjQuery = db.Collection("danhmucdetai");

                QuerySnapshot allobjQueryQuerySnapshot = await allobjQuery.GetSnapshotAsync();
                foreach (DocumentSnapshot documentSnapshot in allobjQueryQuerySnapshot.Documents)
                {

                    if (documentSnapshot.Exists)
                    {

                        DanhmucDetaiModels obj = documentSnapshot.ConvertTo<DanhmucDetaiModels>();
                        obj.DM_MA = documentSnapshot.Id;
                        obj.DM_TEN = documentSnapshot.GetValue<string>("DM_TEN");

                        list_dm.Add(obj);
                    }

                }

               
                return View(list_dm);


            }
            catch (Exception)
            {
                return View();
            }

        }

        public async System.Threading.Tasks.Task<ActionResult> Details_LOAD_DETAI(string id)
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
        public ActionResult Create()
        {
            
            var sinhvien = new SinhvienModels();
            sinhvien.SV_ID = CreateID.createID_byDocument("sv");
         
            return View(sinhvien);

            
        }
        // POST: xử lý sự kiện khi muốn tạo thông tin 
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async System.Threading.Tasks.Task<ActionResult> Create(SinhvienModels obj, string id)
        {
            try
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
                return RedirectToAction("Index");
            }catch
            {
                return View();
            }      
                    
           
        }

        // GET: Thông tin sinh viên cần sửa đổi
        public async System.Threading.Tasks.Task<ActionResult> Edit(string id)
        {
            string path = Server.MapPath("~/quanlydetaiOffical.json");
            FirestoreDb db = FirebaseConnect.connectFB(path);
            var list = new List<SinhvienModels>();
            DocumentReference docRef = db.Collection("sinhvien").Document(id);
            DocumentSnapshot documentSnapshot = await docRef.GetSnapshotAsync();
            SinhvienModels obj = new SinhvienModels();
            if (documentSnapshot.Exists)
            {
                obj = documentSnapshot.ConvertTo<SinhvienModels>();
                obj.SV_ID = documentSnapshot.Id;
                obj.SV_GIOITINH = documentSnapshot.GetValue<string>("SV_GIOITINH");
                obj.SV_HOTEN = documentSnapshot.GetValue<string>("SV_HOTEN");
                obj.SV_NGAYSINH = documentSnapshot.GetValue<string>("SV_NGAYSINH");
                obj.SV_NOISINH = documentSnapshot.GetValue<string>("SV_NOISINH");
                obj.SV_SDT = documentSnapshot.GetValue<string>("SV_SDT");
                obj.SV_EMAIL = documentSnapshot.GetValue<string>("SV_EMAIL");
                obj.SV_CMNN = documentSnapshot.GetValue<string>("SV_CMNN");
                obj.SV_THOIGIANDAOTAO = documentSnapshot.GetValue<string>("SV_THOIGIANDAOTAO");
            }
            return View(obj);
        }

        // POST: xử lý sự kiện khi cần thay đổi thông tin 
        [HttpPost]
        [ValidateAntiForgeryToken]
                    
        public async System.Threading.Tasks.Task<ActionResult> Edit(string id, SinhvienModels obj)
        {
            try
            {
                // TODO: Add insert logic here
                 
                string path = Server.MapPath("~/quanlydetaiOffical.json");
                FirestoreDb db = FirebaseConnect.connectFB(path);
                DocumentReference docRef = db.Collection("sinhvien").Document(id);
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
                var list = new List<SinhvienModels>();
                DocumentReference docRef = db.Collection("sinhvien").Document(id);
                DocumentSnapshot documentSnapshot = await docRef.GetSnapshotAsync();
                SinhvienModels obj = new SinhvienModels();
                if (documentSnapshot.Exists)
                {
                    obj = documentSnapshot.ConvertTo<SinhvienModels>();
                    obj.SV_ID = documentSnapshot.Id;
                    obj.SV_HOTEN = documentSnapshot.GetValue<string>("SV_HOTEN");
                    obj.SV_GIOITINH = documentSnapshot.GetValue<string>("SV_GIOITINH");
                    obj.SV_NGAYSINH = documentSnapshot.GetValue<string>("SV_NGAYSINH");
                    obj.SV_NOISINH = documentSnapshot.GetValue<string>("SV_NOISINH");
                    obj.SV_SDT = documentSnapshot.GetValue<string>("SV_SDT");
                    obj.SV_EMAIL = documentSnapshot.GetValue<string>("SV_EMAIL");
                    obj.SV_CMNN = documentSnapshot.GetValue<string>("SV_CMNN");
                    obj.SV_THOIGIANDAOTAO = documentSnapshot.GetValue<string>("SV_THOIGIANDAOTAO");
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
                var list = new List<SinhvienModels>();
                DocumentReference docRef = db.Collection("sinhvien").Document(id);
                 
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