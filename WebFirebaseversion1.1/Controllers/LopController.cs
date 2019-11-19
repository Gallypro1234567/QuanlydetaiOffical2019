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
    public class LopController : Controller
    {
        // GET: Thông tin inhVien
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {

            string path = Server.MapPath("~/quanlydetaiOffical.json");
            FirestoreDb db = FirebaseConnect.connectFB(path); 
            var list = new List<LopModels>();
            Query allobjQuery = db.Collection("lop"); 
            QuerySnapshot allobjQueryQuerySnapshot = await allobjQuery.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in allobjQueryQuerySnapshot.Documents)
            { 
                if (documentSnapshot.Exists)
                { 
                    LopModels obj = documentSnapshot.ConvertTo<LopModels>();
                    obj.LO_MA = documentSnapshot.Id;
                    obj.LO_TEN = documentSnapshot.GetValue<string>("LO_TEN");
                    obj.LO_SOLUONG = documentSnapshot.GetValue<string>("LO_SOLUONG");
                    obj.LO_THOIGIAN = documentSnapshot.GetValue<string>("LO_THOIGIAN"); 
                    list.Add(obj);
                } 
            }

            return View(list);


        }

        // GET: Chi tiết sinh viên
        public async System.Threading.Tasks.Task<ActionResult> Details(string id)
        {
            string path = Server.MapPath("~/quanlydetaiOffical.json");
            FirestoreDb db = FirebaseConnect.connectFB(path);

            
            var list_lop = new List<LopModels>();
            Query allobjQueryyy = db.Collection("lop");
            QuerySnapshot allobjQueryQuerySnapshottt = await allobjQueryyy.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshottt in allobjQueryQuerySnapshottt.Documents)
            {

                if (documentSnapshottt.Exists && (documentSnapshottt.Id == id))
                {

                    LopModels obj = documentSnapshottt.ConvertTo<LopModels>();
                    obj.LO_MA = documentSnapshottt.Id;
                    obj.LO_TEN = documentSnapshottt.GetValue<string>("LO_TEN");
                    obj.LO_SOLUONG = documentSnapshottt.GetValue<string>("LO_SOLUONG");
                    obj.LO_THOIGIAN = documentSnapshottt.GetValue<string>("LO_THOIGIAN");

                    list_lop.Add(obj);
                }

            }

            var list_sv = new List<Sinhvien>();
            Query allobjQuery = db.Collection("lop").Document(id).Collection("SINHVIEN");
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

            ViewModels final = new ViewModels();
            final.Lop = list_lop;
            final.Sinhvien = list_sv;
            return View(final);
        
             
            
        }

        public ActionResult Create()
        {

            var lop = new LopModels();
            lop.LO_MA = CreateID.createID_byDocument("lo");

            return View(lop);
        }
        public ActionResult Back(string id)
        {
            return RedirectToAction(id, "lop/Details");
        }

        // POST: xử lý sự kiện khi muốn tạo thông tin 
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Create(LopModels obj)
        {
            try
            {
                // TODO: Add insert logic here

                string path = Server.MapPath("~/quanlydetaiOffical.json");
                FirestoreDb db = FirebaseConnect.connectFB(path);
                DocumentReference docRef = db.Collection("lop").Document(obj.LO_MA);
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

            return View(sinhvien);
        }
        // POST: xử lý sự kiện khi muốn tạo thông tin 
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> CreateSV(string id, SinhvienModels obj)
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

            DocumentReference docRefff = db.Collection("lop").Document(id).Collection("SINHVIEN").Document(obj.SV_ID);
            Dictionary<string, object> createee = new Dictionary<string, object>
                {
                    { "LO_SV_HOTEN",obj.SV_HOTEN },
                    { "LO_SV_EMAIL",obj.SV_SDT },
                    { "LO_SV_SDT",obj.SV_EMAIL },


                };
            await docRefff.SetAsync(createee);



            return RedirectToAction(id, "lop/Details");




        }

        // GET: Thông tin sinh viên cần sửa đổi
        public async System.Threading.Tasks.Task<ActionResult> Edit(string id)
        {
            string path = Server.MapPath("~/quanlydetaiOffical.json");
            FirestoreDb db = FirebaseConnect.connectFB(path);
            var list = new List<LopModels>();
            DocumentReference docRef = db.Collection("lop").Document(id);
            DocumentSnapshot documentSnapshot = await docRef.GetSnapshotAsync();
            LopModels obj = new LopModels();
            if (documentSnapshot.Exists)
            {
                obj = documentSnapshot.ConvertTo<LopModels>();
                obj.LO_MA = documentSnapshot.Id;
                obj.LO_TEN = documentSnapshot.GetValue<string>("LO_TEN");
                obj.LO_SOLUONG = documentSnapshot.GetValue<string>("LO_SOLUONG");
                obj.LO_THOIGIAN = documentSnapshot.GetValue<string>("LO_THOIGIAN");
                 
            }
            return View(obj);
        }

        // POST: xử lý sự kiện khi cần thay đổi thông tin 
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Edit(string id, LopModels obj)
        {
            try
            {
                // TODO: Add insert logic here

                string path = Server.MapPath("~/quanlydetaiOffical.json");
                FirestoreDb db = FirebaseConnect.connectFB(path);
                DocumentReference docRef = db.Collection("lop").Document(id);
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

        public async System.Threading.Tasks.Task<ActionResult> EditSV(string SV_ID)
        {
            string path = Server.MapPath("~/quanlydetaiOffical.json");
            FirestoreDb db = FirebaseConnect.connectFB(path);
            var list = new List<SinhvienModels>();
            DocumentReference docRef = db.Collection("sinhvien").Document(SV_ID);
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

        public async System.Threading.Tasks.Task<ActionResult> EditSV(string id,string SV_ID, SinhvienModels obj)
        {
            try
            {
                // TODO: Add insert logic here

                string path = Server.MapPath("~/quanlydetaiOffical.json");
                FirestoreDb db = FirebaseConnect.connectFB(path);
                DocumentReference docRef = db.Collection("sinhvien").Document(SV_ID);
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

                DocumentReference docRefff = db.Collection("lop").Document(id).Collection("SINHVIEN").Document(SV_ID);
                Dictionary<string, object> createee = new Dictionary<string, object>
                {
                    { "LO_SV_HOTEN",obj.SV_HOTEN },
                    { "LO_SV_EMAIL",obj.SV_SDT },
                    { "LO_SV_SDT",obj.SV_EMAIL },


                };
                await docRefff.SetAsync(createee);



                return RedirectToAction(id, "lop/Details");

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
                var list = new List<LopModels>();
                DocumentReference docRef = db.Collection("lop").Document(id);
                DocumentSnapshot documentSnapshot = await docRef.GetSnapshotAsync();
                LopModels obj = new LopModels();
                if (documentSnapshot.Exists)
                {
                    obj = documentSnapshot.ConvertTo<LopModels>();
                    obj.LO_MA = documentSnapshot.Id;
                    obj.LO_TEN = documentSnapshot.GetValue<string>("LO_TEN");
                    obj.LO_SOLUONG = documentSnapshot.GetValue<string>("LO_SOLUONG");
                    obj.LO_THOIGIAN = documentSnapshot.GetValue<string>("LO_THOIGIAN");
                    
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
                var list = new List<LopModels>();
                DocumentReference docRef = db.Collection("lop").Document(id); 
                await docRef.DeleteAsync(); 

                Query allobjQuery = db.Collection("lop").Document(id).Collection("SINHVIEN");
                QuerySnapshot allobjQueryQuerySnapshot = await allobjQuery.GetSnapshotAsync();
                IReadOnlyList<DocumentSnapshot> documents = allobjQueryQuerySnapshot.Documents;
                while(documents.Count > 0)
                {
                    foreach (DocumentSnapshot document in documents)
                    {
                        Console.WriteLine("Deleting document {0}", document.Id);
                        await document.Reference.DeleteAsync();
                    }
                    allobjQueryQuerySnapshot = await allobjQuery.GetSnapshotAsync();
                    documents = allobjQueryQuerySnapshot.Documents;
                } 
                return RedirectToAction("Index");

                 
            }
            catch (Exception)
            {
                return View();
            }
        }
        public async System.Threading.Tasks.Task<ActionResult> DeleteSV(string id, string SV_ID)
        {
            try
            {
                string path = Server.MapPath("~/quanlydetaiOffical.json");
                FirestoreDb db = FirebaseConnect.connectFB(path);
                var list = new List<Sinhvien>();
                DocumentReference docRef = db.Collection("lop").Document(id).Collection("SINHVIEN").Document(SV_ID);
                DocumentSnapshot documentSnapshot = await docRef.GetSnapshotAsync();
                Sinhvien obj = new Sinhvien();
                if (documentSnapshot.Exists)
                {
                    obj = documentSnapshot.ConvertTo<Sinhvien>();
                    obj.LO_SV_ID = documentSnapshot.Id;
                    obj.LO_SV_HOTEN = documentSnapshot.GetValue<string>("LO_SV_HOTEN");
                    obj.LO_SV_SDT = documentSnapshot.GetValue<string>("LO_SV_SDT");
                    obj.LO_SV_EMAIL = documentSnapshot.GetValue<string>("LO_SV_EMAIL");

                }
                return View(obj);
            }
            catch (Exception)
            {
                return View();
            }
        }

        // POST: Xử lý sự kiện cần xóa thông tin
        [HttpPost, ActionName("DeleteSV")]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> DeleteConfirmedSV(string id, string SV_ID)
        {
            try
            {

                string path = Server.MapPath("~/quanlydetaiOffical.json");
                FirestoreDb db = FirebaseConnect.connectFB(path);
               
                DocumentReference docRef = db.Collection("sinhvien").Document(SV_ID);

                await docRef.DeleteAsync();
                DocumentReference docRefff = db.Collection("lop").Document(id).Collection("SINHVIEN").Document(SV_ID);

                await docRefff.DeleteAsync();

                return RedirectToAction(id, "lop/Details");

            }
            catch (Exception)
            {
                return View();
            }
        }

    }
}