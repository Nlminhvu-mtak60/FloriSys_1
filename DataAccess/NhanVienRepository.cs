using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using FloriSys.Models;

namespace FloriSys.DataAccess
{
    /// <summary>
    /// NhanVien repository - inherits BaseRepository&lt;NhanVien&gt;.
    /// Demonstrates: INHERITANCE (base helper methods),
    /// POLYMORPHISM (overrides LayDanhSach with custom JOIN + 3 filters).
    /// </summary>
    public class NhanVienRepository : BaseRepository<NhanVien>
    {
        public override string TableName => "NHAN_VIEN";
        public override string IdColumn => "MaNV";
        public override string IdPrefix => "NV";

        public NhanVien LayTheoTaiKhoan(string taiKhoan)
        {
            string sql = "SELECT MaNV, HoTen, ChucVu, SoDienThoai, TaiKhoan, MatKhau, TrangThai FROM NHAN_VIEN WHERE TaiKhoan = @TaiKhoan";
            return GetSingle(sql, new SqlParameter[] { new SqlParameter("@TaiKhoan", taiKhoan) });
        }

        // POLYMORPHISM: Override with custom 3-filter search
        public List<NhanVien> LayDanhSach(string keyword = "", string chucVu = "", string trangThai = "")
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
            return GetList(sql, parms);
        }

        public NhanVien DangNhap(string taiKhoan, string matKhauHash)
        {
            return GetSingleFromSP("sp_DangNhap", new SqlParameter[]
            {
                new SqlParameter("@TaiKhoan", taiKhoan),
                new SqlParameter("@MatKhau", matKhauHash)
            });
        }

        public bool DoiMatKhau(string maNV, string matKhauCuHash, string matKhauMoiHash)
        {
            DataTable dt = DatabaseHelper.ExecuteQuery("sp_DoiMatKhau", new SqlParameter[]
            {
                new SqlParameter("@MaNV", maNV),
                new SqlParameter("@MatKhauCu", matKhauCuHash),
                new SqlParameter("@MatKhauMoi", matKhauMoiHash)
            });
            return dt.Rows.Count > 0 && dt.Rows[0]["KetQua"].ToString() == "1";
        }

        public void ThemNhanVien(NhanVien nv)
        {
            string sql = @"INSERT INTO NHAN_VIEN (MaNV, HoTen, ChucVu, SoDienThoai, TaiKhoan, MatKhau) 
                          VALUES (@MaNV, @HoTen, @ChucVu, @SoDienThoai, @TaiKhoan, @MatKhau)";
            ExecuteSql(sql, new SqlParameter[]
            {
                new SqlParameter("@MaNV", nv.MaNV),
                new SqlParameter("@HoTen", nv.HoTen),
                new SqlParameter("@ChucVu", nv.ChucVu),
                new SqlParameter("@SoDienThoai", nv.SoDienThoai),
                new SqlParameter("@TaiKhoan", nv.TaiKhoan),
                new SqlParameter("@MatKhau", nv.MatKhau)
            });
        }

        public void CapNhatNhanVien(NhanVien nv)
        {
            string sql = @"UPDATE NHAN_VIEN SET HoTen=@HoTen, ChucVu=@ChucVu, SoDienThoai=@SoDienThoai WHERE MaNV=@MaNV";
            ExecuteSql(sql, new SqlParameter[]
            {
                new SqlParameter("@MaNV", nv.MaNV),
                new SqlParameter("@HoTen", nv.HoTen),
                new SqlParameter("@ChucVu", nv.ChucVu),
                new SqlParameter("@SoDienThoai", nv.SoDienThoai)
            });
        }

        public void CapNhatTrangThai(string maNV, string trangThai)
        {
            string sql = "UPDATE NHAN_VIEN SET TrangThai=@TrangThai WHERE MaNV=@MaNV";
            ExecuteSql(sql, new SqlParameter[]
            {
                new SqlParameter("@MaNV", maNV),
                new SqlParameter("@TrangThai", trangThai)
            });
        }

        public List<NhanVien> LayShippers()
        {
            string sql = "SELECT MaNV, HoTen FROM NHAN_VIEN WHERE ChucVu=N'Shipper' AND TrangThai=N'DangLam'";
            return GetList(sql);
        }

        public DataTable LayDanhSachShipperDePhanCong()
        {
            string sql = @"SELECT nv.MaNV, nv.HoTen, 
                    (SELECT COUNT(*) FROM GIAO_HANG gh WHERE gh.MaNV_Shipper = nv.MaNV AND gh.TrangThai = N'DangGiao') AS DangGiao,
                    (SELECT COUNT(*) FROM GIAO_HANG gh WHERE gh.MaNV_Shipper = nv.MaNV AND gh.TrangThai = N'GiaoThanhCong' AND CAST(gh.NgayGiao AS DATE) = CAST(GETDATE() AS DATE)) AS DaGiaoHomNay,
                    CASE 
                        WHEN (SELECT COUNT(*) FROM GIAO_HANG gh WHERE gh.MaNV_Shipper = nv.MaNV AND gh.TrangThai = N'DangGiao') = 0 THEN N'Rảnh'
                        ELSE N'Đang giao'
                    END AS TrangThai
                    FROM NHAN_VIEN nv
                    WHERE nv.ChucVu = N'Shipper' AND nv.TrangThai = N'DangLam'
                    ORDER BY DangGiao ASC";
            return DatabaseHelper.ExecuteRawQuery(sql);
        }

        public void ResetMatKhau(string maNV, string matKhauMoiHash)
        {
            string sql = "UPDATE NHAN_VIEN SET MatKhau=@MK WHERE MaNV=@Ma";
            ExecuteSql(sql, new SqlParameter[]
            {
                new SqlParameter("@MK", matKhauMoiHash),
                new SqlParameter("@Ma", maNV)
            });
        }
    }
}

