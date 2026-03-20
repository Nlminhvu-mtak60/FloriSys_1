using System.Security.Cryptography;
using System.Text;

namespace FloriSys.Services
{
    public static class SessionManager
    {
        public static string MaNV { get; set; }
        public static string HoTen { get; set; }
        public static string ChucVu { get; set; }
        public static string TaiKhoan { get; set; }
        public static string SoDienThoai { get; set; }

        public static bool IsAdmin => ChucVu == "Admin";
        public static bool IsCashier => ChucVu == "Cashier";
        public static bool IsWarehouse => ChucVu == "Warehouse";
        public static bool IsShipper => ChucVu == "Shipper";

        public static void Clear()
        {
            MaNV = HoTen = ChucVu = TaiKhoan = SoDienThoai = null;
        }

        public static string HashSHA256(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        public static string AvatarChar
        {
            get
            {
                if (string.IsNullOrEmpty(HoTen)) return "?"; 
                string[] parts = HoTen.Split(' ');
                return parts[parts.Length - 1].Substring(0, 1).ToUpper();
            }
        }

        public static string ChucVuDisplay
        {
            get
            {
                switch (ChucVu)
                {
                    case "Admin": return "Quản lý";
                    case "Cashier": return "Nhân viên bán hàng";
                    case "Warehouse": return "Nhân viên kho";
                    case "Shipper": return "Nhân viên giao hàng";
                    default: return ChucVu;
                }
            }
        }
    }
}
