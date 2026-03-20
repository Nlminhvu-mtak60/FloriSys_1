using System;
using System.Data;
using System.Data.SqlClient;

namespace FloriSys.DataAccess
{
    public class DonHangDAO
    {
        public static DataTable LayDanhSach(string keyword = "", string trangThai = "", string maNV = "", DateTime? ngay = null)
        {
            string sql = @"SELECT dh.MaDon, dh.NgayTao, kh.HoTen AS TenKH, kh.SoDienThoai, 
                          dh.HinhThucNhanHang, dh.TongTien, dh.TrangThai, nv.HoTen AS TenNV, dh.GhiChu
                          FROM DON_HANG dh
                          INNER JOIN KHACH_HANG kh ON dh.MaKH = kh.MaKH
                          INNER JOIN NHAN_VIEN nv ON dh.MaNV_TaoDon = nv.MaNV
                          WHERE 1=1";
            var parms = new System.Collections.Generic.List<SqlParameter>();
            if (!string.IsNullOrEmpty(keyword))
            {
                sql += " AND (dh.MaDon LIKE @Key OR kh.HoTen LIKE @Key)";
                parms.Add(new SqlParameter("@Key", "%" + keyword + "%"));
            }
            if (!string.IsNullOrEmpty(trangThai))
            {
                sql += " AND dh.TrangThai = @TrangThai";
                parms.Add(new SqlParameter("@TrangThai", trangThai));
            }
            if (!string.IsNullOrEmpty(maNV))
            {
                sql += " AND dh.MaNV_TaoDon = @MaNV";
                parms.Add(new SqlParameter("@MaNV", maNV));
            }
            if (ngay.HasValue)
            {
                sql += " AND CAST(dh.NgayTao AS DATE) = @Ngay";
                parms.Add(new SqlParameter("@Ngay", ngay.Value.Date));
            }
            sql += " ORDER BY dh.NgayTao DESC";
            return DatabaseHelper.ExecuteRawQuery(sql, parms.ToArray());
        }

        public static DataTable LayChiTiet(string maDon)
        {
            string sql = @"SELECT ct.MaSP, sp.TenSP, ct.SoLuong, ct.DonGia, ct.ThanhTien
                          FROM CHI_TIET_DON_HANG ct
                          INNER JOIN SAN_PHAM sp ON ct.MaSP = sp.MaSP
                          WHERE ct.MaDon = @MaDon";
            return DatabaseHelper.ExecuteRawQuery(sql, new SqlParameter[] { new SqlParameter("@MaDon", maDon) });
        }

        public static DataTable LayThongTinDon(string maDon)
        {
            string sql = @"SELECT dh.*, kh.HoTen AS TenKH, kh.SoDienThoai, kh.DiaChi, kh.Email,
                          nv.HoTen AS TenNV
                          FROM DON_HANG dh
                          INNER JOIN KHACH_HANG kh ON dh.MaKH = kh.MaKH
                          INNER JOIN NHAN_VIEN nv ON dh.MaNV_TaoDon = nv.MaNV
                          WHERE dh.MaDon = @MaDon";
            return DatabaseHelper.ExecuteRawQuery(sql, new SqlParameter[] { new SqlParameter("@MaDon", maDon) });
        }

        public static string TaoDonHang(string maKH, string maNV, string hinhThuc, string ghiChu)
        {
            string maDon = DatabaseHelper.GenerateCode("DH", "DON_HANG", "MaDon");
            DatabaseHelper.ExecuteNonQuery("sp_TaoDonHang", new SqlParameter[]
            {
                new SqlParameter("@MaDon", maDon),
                new SqlParameter("@MaKH", maKH),
                new SqlParameter("@MaNV_TaoDon", maNV),
                new SqlParameter("@HinhThucNhanHang", hinhThuc),
                new SqlParameter("@GhiChu", (object)ghiChu ?? DBNull.Value)
            });
            return maDon;
        }

        public static void ThemChiTiet(string maDon, string maSP, int soLuong, decimal donGia)
        {
            DatabaseHelper.ExecuteNonQuery("sp_ThemChiTietDon", new SqlParameter[]
            {
                new SqlParameter("@MaDon", maDon),
                new SqlParameter("@MaSP", maSP),
                new SqlParameter("@SoLuong", soLuong),
                new SqlParameter("@DonGia", donGia)
            });
        }

        public static void CapNhatTrangThai(string maDon, string trangThai)
        {
            DatabaseHelper.ExecuteNonQuery("sp_CapNhatTrangThaiDon", new SqlParameter[]
            {
                new SqlParameter("@MaDon", maDon),
                new SqlParameter("@TrangThai", trangThai)
            });
        }

        public static DataTable LayDonChoXuatKho()
        {
            string sql = @"SELECT dh.MaDon, kh.HoTen AS TenKH, sp.TenSP, ct.SoLuong, sp.SoLuongTon,
                          CASE WHEN sp.SoLuongTon >= ct.SoLuong THEN N'DuHang' ELSE N'KhongDu' END AS TinhTrangKho
                          FROM DON_HANG dh
                          INNER JOIN KHACH_HANG kh ON dh.MaKH = kh.MaKH
                          INNER JOIN CHI_TIET_DON_HANG ct ON dh.MaDon = ct.MaDon
                          INNER JOIN SAN_PHAM sp ON ct.MaSP = sp.MaSP
                          WHERE dh.TrangThai = N'Moi'
                          ORDER BY dh.NgayTao";
            return DatabaseHelper.ExecuteRawQuery(sql);
        }
    }
}
