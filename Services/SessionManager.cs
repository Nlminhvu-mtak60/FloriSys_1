using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using FloriSys.Models;

namespace FloriSys.Services
{
    /// <summary>
    /// Session manager - proper Singleton pattern with encapsulation.
    /// Demonstrates: ENCAPSULATION (private setter, controlled access),
    /// ABSTRACTION (IsAdmin/IsCashier hide ChucVu string comparison),
    /// INHERITANCE (sealed prevents further inheritance).
    /// 
    /// Backward-compatible static API is preserved so existing UI code compiles.
    /// New code can use Instance.Login(), Instance.Logout(), Instance.OnUserChanged.
    /// </summary>
    public sealed class SessionManager
    {
        // ============================================================
        // SINGLETON: Lazy thread-safe initialization
        // ============================================================

        private static readonly Lazy<SessionManager> _instance =
            new Lazy<SessionManager>(() => new SessionManager());

        public static SessionManager Instance => _instance.Value;

        private NhanVien _currentUser;
        private List<PhanQuyen> _permissions = new List<PhanQuyen>();
        private event Action _onUserChanged;

        // Private constructor = singleton
        private SessionManager() { }

        // ============================================================
        // Instance methods for OOP usage
        // ============================================================

        public NhanVien GetCurrentUser() => _currentUser;

        /// <summary>Login: sets current user, permissions and fires event.</summary>
        public void Login(NhanVien nv, List<PhanQuyen> permissions)
        {
            _currentUser = nv;
            _permissions = permissions ?? new List<PhanQuyen>();
            _onUserChanged?.Invoke();
        }

        /// <summary>Logout: clears state and fires event.</summary>
        public void Logout()
        {
            _currentUser = null;
            _permissions.Clear();
            _onUserChanged?.Invoke();
        }

        /// <summary>Update permissions for the current user session.</summary>
        public void UpdatePermissions(List<PhanQuyen> permissions)
        {
            _permissions = permissions ?? new List<PhanQuyen>();
            _onUserChanged?.Invoke();
        }

        /// <summary>
        /// Check if current user has specific permission for a module.
        /// ENCAPSULATION: Centralizes permission logic.
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

        /// <summary>Event fired when user logs in or out.</summary>
        public event Action OnUserChanged
        {
            add { _onUserChanged += value; }
            remove { _onUserChanged -= value; }
        }

        // ============================================================
        // Static hashing utility (stateless)
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

        // ============================================================
        // Backward-compatible static API
        // ============================================================

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

        // ABSTRACTION: Boolean role checks hide string comparison
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

        /// <summary>Backward compat - old: SessionManager.Clear()</summary>
        public static void Clear() => Instance.Logout();
    }
}