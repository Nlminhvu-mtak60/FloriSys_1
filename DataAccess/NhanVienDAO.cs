using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using FloriSys.Models;

namespace FloriSys.DataAccess
{
    public class NhanVienDAO
    {
        public static NhanVien DangNhap(string taiKhoan, string matKhauHash)
        {
            return DatabaseHelper.ExecuteSingle<NhanVien>("sp_DangNhap", new SqlParameter[]
            {
                new SqlParameter("@TaiKhoan", taiKhoan),
                new SqlParameter("@MatKhau", matKhauHash)
            });
        }

        public static bool DoiMatKhau(string maNV, string matKhauCuHash, string matKhauMoiHash)
        {
            DataTable dt = DatabaseHelper.ExecuteQuery("sp_DoiMatKhau", new SqlParameter[]
            {
                new SqlParameter("@MaNV", maNV),
                new SqlParameter("@MatKhauCu", matKhauCuHash),
                new SqlParameter("@MatKhauMoi", matKhauMoiHash)
            });
            return dt.Rows.Count > 0 && dt.Rows[0]["KetQua"].ToString() == "1";
        }

        public static List<NhanVien> LayDanhSach(string keyword = "", string chucVu = "", string trangThai = "")
        {
            string sql = @"SELECT MaNV, HoTen, ChucVu, SoDienThoai, TaiKhoan, TrangThai 
                          FROM NHAN_VIEN WHERE 1=1";
            var parms = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(keyword))
            {
                sql += " AND (HoTen LIKE @Key OR SoDienThoai LIKE @Key OR TaiKhoan LIKE @Key)";
                parms.Add(new SqlParameter("@Key", "%" + keyword + "%"));
            }
            if (!string.IsNullOrEmpty(chucVu))
            {
                sql += " AND ChucVu = @ChucVu";
                parms.Add(new SqlParameter("@ChucVu", chucVu));
            }
            if (!string.IsNullOrEmpty(trangThai))
            {
                sql += " AND TrangThai = @TrangThai";
                parms.Add(new SqlParameter("@TrangThai", trangThai));
            }
            sql += " ORDER BY MaNV";
            return DatabaseHelper.ExecuteRawList<NhanVien>(sql, parms.ToArray());
        }

        public static void ThemNhanVien(NhanVien nv)
        {
            string sql = @"INSERT INTO NHAN_VIEN (MaNV, HoTen, ChucVu, SoDienThoai, TaiKhoan, MatKhau) 
                          VALUES (@MaNV, @HoTen, @ChucVu, @SoDienThoai, @TaiKhoan, @MatKhau)";
            DatabaseHelper.ExecuteRawNonQuery(sql, new SqlParameter[]
            {
                new SqlParameter("@MaNV", nv.MaNV),
                new SqlParameter("@HoTen", nv.HoTen),
                new SqlParameter("@ChucVu", nv.ChucVu),
                new SqlParameter("@SoDienThoai", nv.SoDienThoai),
                new SqlParameter("@TaiKhoan", nv.TaiKhoan),
                new SqlParameter("@MatKhau", nv.MatKhau)
            });
        }

        public static void CapNhatNhanVien(NhanVien nv)
        {
            string sql = @"UPDATE NHAN_VIEN SET HoTen=@HoTen, ChucVu=@ChucVu, SoDienThoai=@SoDienThoai WHERE MaNV=@MaNV";
            DatabaseHelper.ExecuteRawNonQuery(sql, new SqlParameter[]
            {
                new SqlParameter("@MaNV", nv.MaNV),
                new SqlParameter("@HoTen", nv.HoTen),
                new SqlParameter("@ChucVu", nv.ChucVu),
                new SqlParameter("@SoDienThoai", nv.SoDienThoai)
            });
        }

        public static void CapNhatTrangThai(string maNV, string trangThai)
        {
            string sql = "UPDATE NHAN_VIEN SET TrangThai=@TrangThai WHERE MaNV=@MaNV";
            DatabaseHelper.ExecuteRawNonQuery(sql, new SqlParameter[]
            {
                new SqlParameter("@MaNV", maNV),
                new SqlParameter("@TrangThai", trangThai)
            });
        }

        public static List<NhanVien> LayShippers()
        {
            string sql = "SELECT MaNV, HoTen FROM NHAN_VIEN WHERE ChucVu=N'Shipper' AND TrangThai=N'DangLam'";
            return DatabaseHelper.ExecuteRawList<NhanVien>(sql);
        }
    }
}
