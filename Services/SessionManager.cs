using System.Security.Cryptography;
using System.Text;
using FloriSys.Models;

namespace FloriSys.Services
{
    public static class SessionManager
    {
        /// <summary>
        /// Đối tượng NhanVien đang đăng nhập hiện tại (OOP chuẩn).
        /// </summary>
        public static NhanVien CurrentUser { get; set; }

        // Backward-compatible properties (đọc từ CurrentUser)
        public static string MaNV => CurrentUser?.MaNV;
        public static string HoTen => CurrentUser?.HoTen;
        public static string ChucVu => CurrentUser?.ChucVu;
        public static string TaiKhoan => CurrentUser?.TaiKhoan;
        public static string SoDienThoai => CurrentUser?.SoDienThoai;

        public static bool IsAdmin => ChucVu == "Admin";
        public static bool IsCashier => ChucVu == "Cashier";
        public static bool IsWarehouse => ChucVu == "Warehouse";
        public static bool IsShipper => ChucVu == "Shipper";

        public static void Clear()
        {
            CurrentUser = null;
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
                return CurrentUser?.ChucVuDisplay ?? ChucVu;
            }
        }
    }
}
