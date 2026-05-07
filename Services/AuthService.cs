using System;
using FloriSys.DataAccess;
using FloriSys.Models;

namespace FloriSys.Services
{
    /// <summary>
    /// Authentication service - encapsulates login/logout/password change logic.
    /// Demonstrates: ENCAPSULATION (all auth logic in one place, UI only calls this).
    /// </summary>
    public class AuthService
    {
        private readonly NhanVienRepository _nvRepo;

        public AuthService()
        {
            _nvRepo = new NhanVienRepository();
        }

        /// <summary>
        /// Login with username and plain-text password.
        /// ENCAPSULATION: hashing + validation + session setup all handled internally.
        /// </summary>
        public bool DangNhap(string taiKhoan, string matKhau, out NhanVien nv, out string error)
        {
            nv = null;
            error = "";

            if (string.IsNullOrEmpty(taiKhoan) || string.IsNullOrEmpty(matKhau))
            {
                error = "Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.";
                return false;
            }

            try
            {
                string hash = SessionManager.HashSHA256(matKhau);
                nv = _nvRepo.DangNhap(taiKhoan, hash);

                if (nv == null)
                {
                    error = "Tên đăng nhập hoặc mật khẩu không đúng.\nHoặc tài khoản đã bị khóa.";
                    return false;
                }

                SessionManager.Instance.Login(nv);
                return true;
            }
            catch (Exception ex)
            {
                error = "Không thể kết nối cơ sở dữ liệu!\n\n" + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Change password with validation.
        /// </summary>
        public bool DoiMatKhau(string matKhauCu, string matKhauMoi, out string error)
        {
            error = "";

            if (string.IsNullOrEmpty(matKhauCu) || string.IsNullOrEmpty(matKhauMoi))
            {
                error = "Vui lòng nhập đầy đủ mật khẩu cũ và mới.";
                return false;
            }

            string maNV = SessionManager.MaNV;
            string hashCu = SessionManager.HashSHA256(matKhauCu);
            string hashMoi = SessionManager.HashSHA256(matKhauMoi);

            bool result = _nvRepo.DoiMatKhau(maNV, hashCu, hashMoi);
            if (!result)
            {
                error = "Mật khẩu cũ không đúng.";
            }
            return result;
        }

        /// <summary>
        /// Logout current user.
        /// </summary>
        public void DangXuat()
        {
            SessionManager.Instance.Logout();
        }
    }
}
