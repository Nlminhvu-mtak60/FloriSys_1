using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using FloriSys.Models;

namespace FloriSys.Services
{
    /// <summary>
    /// Quản lý phiên làm việc - theo mẫu Singleton chuẩn với tính đóng gói.
    /// Thể hiện: TÍNH ĐÓNG GÓI (setter private, kiểm soát truy cập),
    /// TÍNH TRỪU TƯỢNG (IsAdmin/IsCashier ẩn đi phép so sánh chuỗi ChucVu),
    /// TÍNH KẾ THỪA (sealed ngăn chặn việc kế thừa tiếp theo).
    /// 
    /// API static tương thích ngược được giữ nguyên để code UI hiện tại vẫn biên dịch được.
    /// Code mới có thể dùng Instance.Login(), Instance.Logout(), Instance.OnUserChanged.
    /// </summary>
    public sealed class SessionManager
    {
        // ============================================================
        // SINGLETON: Khởi tạo lười (lazy) an toàn với thread
        // ============================================================

        private static readonly Lazy<SessionManager> _instance =
            new Lazy<SessionManager>(() => new SessionManager());

        public static SessionManager Instance => _instance.Value;

        private NhanVien _currentUser;
        private List<PhanQuyen> _permissions = new List<PhanQuyen>();
        private event Action _onUserChanged;

        // Constructor private = singleton
        private SessionManager() { }

        // ============================================================
        // Các phương thức instance để sử dụng theo hướng đối tượng (OOP)
        // ============================================================

        public NhanVien GetCurrentUser() => _currentUser;

        /// <summary>Đăng nhập: thiết lập người dùng hiện tại, quyền và kích hoạt sự kiện.</summary>
        public void Login(NhanVien nv, List<PhanQuyen> permissions)
        {
            _currentUser = nv;
            _permissions = permissions ?? new List<PhanQuyen>();
            _onUserChanged?.Invoke();
        }

        /// <summary>Đăng xuất: xóa trạng thái và kích hoạt sự kiện.</summary>
        public void Logout()
        {
            _currentUser = null;
            _permissions.Clear();
            _onUserChanged?.Invoke();
        }

        /// <summary>Cập nhật quyền cho phiên làm việc của người dùng hiện tại.</summary>
        public void UpdatePermissions(List<PhanQuyen> permissions)
        {
            _permissions = permissions ?? new List<PhanQuyen>();
            _onUserChanged?.Invoke();
        }

        /// <summary>
        /// Kiểm tra xem người dùng hiện tại có quyền cụ thể cho một chức năng (module) hay không.
        /// TÍNH ĐÓNG GÓI: Tập trung hóa logic phân quyền.
        /// </summary>
        public bool HasPermission(string module, string action)
        {
            var p = _permissions.Find(x => x.Module.Equals(module, StringComparison.OrdinalIgnoreCase));
            if (p == null) return false;

            switch (action.ToLower())
            {
                case "xem": return p.Xem;
                case "them": return p.Them;
                case "sua": return p.Sua;
                case "xoa": return p.Xoa;
                case "export": return p.Export;
                default: return false;
            }
        }

        public List<PhanQuyen> GetPermissions() => _permissions;

        /// <summary>Sự kiện được kích hoạt khi người dùng đăng nhập hoặc đăng xuất.</summary>
        public event Action OnUserChanged
        {
            add { _onUserChanged += value; }
            remove { _onUserChanged -= value; }
        }

        // ============================================================
        // Tiện ích băm static (không trạng thái - stateless)
        // ============================================================

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


        /// <summary>
        /// FIX: Getter giữ nguyên để tương thích ngược.
        /// Setter đã được bảo vệ – chỉ nên dùng Instance.Login() để đảm bảo
        /// permissions cũng được load đồng thời với user.
        /// Setter này vẫn hoạt động nhưng sẽ ghi cảnh báo debug.
        /// </summary>
        public static NhanVien CurrentUser
        {
            get => Instance._currentUser;
            set
            {
                // FIX: Cảnh báo khi dùng setter thẳng (bỏ qua Login flow → permissions sẽ trống)
                System.Diagnostics.Debug.WriteLine(
                    "[SessionManager] CẢNH BÁO: Set CurrentUser trực tiếp có thể bỏ qua việc load permissions. " +
                    "Dùng Instance.Login(nv, permissions) thay thế.");
                Instance._currentUser = value;
                Instance._onUserChanged?.Invoke();
            }
        }

        public static string MaNV => Instance._currentUser?.MaNV;
        public static string HoTen => Instance._currentUser?.HoTen;
        public static string ChucVu => Instance._currentUser?.ChucVu;
        public static string TaiKhoan => Instance._currentUser?.TaiKhoan;
        public static string SoDienThoai => Instance._currentUser?.SoDienThoai;

        // TÍNH TRỪU TƯỢNG: Các kiểm tra vai trò kiểu Boolean ẩn đi phép so sánh chuỗi
        public static bool IsAdmin => ChucVu == "Admin";
        public static bool IsCashier => ChucVu == "Cashier";
        public static bool IsWarehouse => ChucVu == "Warehouse";
        public static bool IsShipper => ChucVu == "Shipper";

        public static string AvatarChar
        {
            get
            {
                if (string.IsNullOrEmpty(HoTen)) return "?";
                string[] parts = HoTen.Split(' ');
                return parts[parts.Length - 1].Substring(0, 1).ToUpper();
            }
        }

        public static string ChucVuDisplay => Instance._currentUser?.ChucVuDisplay ?? ChucVu;

        /// <summary>Tương thích ngược - cũ: SessionManager.Clear()</summary>
        public static void Clear() => Instance.Logout();
    }
}