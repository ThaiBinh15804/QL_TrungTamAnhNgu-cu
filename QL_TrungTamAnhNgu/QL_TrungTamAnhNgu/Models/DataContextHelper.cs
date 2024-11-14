using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace QL_TrungTamAnhNgu.Models
{
    public class DataContextHelper
    {
        public static DataClasses1DataContext CreateDataContext(string username, string password)
        {
            // Tạo connection string động với username và password từ người dùng
            string connectionString = "Data Source=THAIBINH-LAPTOP;Initial Catalog=QL_TrungTamAnhNgu;User ID="+ username +";Password=" + password +";";
        
            // Khởi tạo DataContext với connection string tùy chỉnh
            return new DataClasses1DataContext(connectionString);
        }
    }
}