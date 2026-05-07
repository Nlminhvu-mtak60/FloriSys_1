using System;
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
        private event Action _onUserChanged;

        // Private constructor = singleton
        private SessionManager() { }

        // ============================================================
        // Instance methods for OOP usage
        // ============================================================

        public NhanVien GetCurrentUser() => _currentUser;

        /// <summary>Login: sets current user and fires event.</summary>
        public void Login(NhanVien nv)
        {
            _currentUser = nv;
            _onUserChanged?.Invoke();
        }

        /// <summary>Logout: clears current user and fires event.</summary>
        public void Logout()
        {
            _currentUser = null;
            _onUserChanged?.Invoke();
        }

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
        // These keep the EXACT same names as the old static SessionManager
        // so existing UI code continues to compile without changes.
        // ============================================================

        public static NhanVien CurrentUser
        {
            get => Instance._currentUser;
            set { Instance._currentUser = value; Instance._onUserChanged?.Invoke(); }
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
