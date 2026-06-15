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
                // 1. Kiểm tra tài khoản tồn tại
                var userFound = _nvRepo.LayTheoTaiKhoan(taiKhoan);

                if (userFound == null)
                {
                    error = "Tên đăng nhập không tồn tại trong hệ thống.";
                    return false;
                }

                if (userFound.TrangThai == "DaNghi")
                {
                    error = "Tài khoản này đã bị khóa hoặc ngừng hoạt động.";
                    return false;
                }

                // 2. Kiểm tra mật khẩu
                string hash = SessionManager.HashSHA256(matKhau);
                if (userFound.MatKhau != hash)
                {
                    error = "Mật khẩu không chính xác. Vui lòng thử lại.";
                    return false;
                }

                // 3. FIX: Dùng trực tiếp userFound thay vì gọi DB lần 2
                // Trước đây: nv = _nvRepo.DangNhap(taiKhoan, hash);  <- gọi DB 2 lần không cần thiết
                nv = userFound;

                // 4. Load phân quyền cho vai trò của user
                var pqRepo = new PhanQuyenRepository();
                var permissions = pqRepo.LayPhanQuyen(nv.ChucVu);

                // Tự động seed/force quyền PhanQuyen cho Admin (Đảm bảo chuẩn 3 lớp, gọi qua Repo)
                if (nv.ChucVu == "Admin")
                {
                    var existingPq = permissions.Find(p => p.Module.Equals("PhanQuyen", StringComparison.OrdinalIgnoreCase));
                    if (existingPq == null)
                    {
                        var adminPq = new PhanQuyen { ChucVu = "Admin", Module = "PhanQuyen", Xem = true, Them = true, Sua = true, Xoa = true, Export = true };
                        pqRepo.CapNhatQuyen(adminPq);
                        permissions.Add(adminPq);
                    }
                    else if (!existingPq.Xem)
                    {
                        existingPq.Xem = true;
                        existingPq.Them = true;
                        existingPq.Sua = true;
                        existingPq.Xoa = true;
                        pqRepo.CapNhatQuyen(existingPq);
                    }
                }

                SessionManager.Instance.Login(nv, permissions);
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