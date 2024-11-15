using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QL_TrungTamAnhNgu.Models;
using System.Configuration;
using System.Web.Configuration;
using System.Web.Security;

namespace QL_TrungTamAnhNgu.Controllers
{
    [Authorize]
    public class GiangVienController : Controller
    {
        //
        // GET: /GiangVien/

        DataClasses1DataContext data = new DataClasses1DataContext();

        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult DangNhap()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult XuLyDangNhap(FormCollection c)
        {
            string username = c["username"];
            string password = c["password"];

            string newConnectionString = "Data Source=THAIBINH-LAPTOP;Initial Catalog=QL_TrungTamAnhNgu;User ID=" + username + ";Password=" + password + ";";
            data = new DataClasses1DataContext(newConnectionString);

            //// Lấy đối tượng Configuration hiện tại từ web.config
            //Configuration config = WebConfigurationManager.OpenWebConfiguration("~");

            //// Lấy phần tử connectionStrings từ cấu hình
            //ConnectionStringsSection connectionStringsSection = config.GetSection("connectionStrings") as ConnectionStringsSection;

            //if (connectionStringsSection != null)
            //{
            //    // Tìm chuỗi kết nối theo tên trong cấu hình
            //    ConnectionStringSettings connectionString = connectionStringsSection.ConnectionStrings["QL_TrungTamAnhNguConnectionString"];

            //    if (connectionString != null)
            //    {
            //        // Cập nhật chuỗi kết nối với giá trị mới
            //        connectionString.ConnectionString = newConnectionString;

            //        // Lưu lại các thay đổi vào file web.config
            //        config.Save(ConfigurationSaveMode.Modified);

            //        // Đảm bảo các thay đổi được áp dụng ngay lập tức
            //        ConfigurationManager.RefreshSection("connectionStrings");
            //    }
            //}

            
            var user = data.NguoiDungs.FirstOrDefault(u => u.TenTaiKhoan == username && u.MatKhau == password);
            if (user != null)
            {
                GiangVien gv = user.GiangViens.SingleOrDefault();
                Session["User"] = gv;
                
                FormsAuthentication.SetAuthCookie(user.TenTaiKhoan, false);
            }
            return RedirectToAction("Test", "GiangVien");    
        }


        public ActionResult Test()
        {
            GiangVien gv = (GiangVien)Session["User"];
            return View(data.LopHocs.Where(t => t.MaGiangVien == gv.MaGiangVien).ToList());
            
            //return View(data.NhomNguoiDungs.ToList());
        }


    }
}
