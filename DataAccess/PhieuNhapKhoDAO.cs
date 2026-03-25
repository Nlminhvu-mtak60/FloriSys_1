using System.Data;
using System.Data.SqlClient;

namespace FloriSys.DataAccess
{
    public class PhieuNhapKhoDAO
    {
        public static DataTable LayDanhSach(string keyword = "", string maNV = "", System.DateTime? fromDate = null, System.DateTime? toDate = null)
        {
            string sql = @"SELECT pn.MaPhieu, pn.NgayNhap, nv.HoTen AS TenNV, pn.GhiChu,
                          (SELECT COUNT(*) FROM CT_NHAP_KHO WHERE MaPhieu=pn.MaPhieu) AS SoLoaiSP,
                          (SELECT ISNULL(SUM(SoLuong),0) FROM CT_NHAP_KHO WHERE MaPhieu=pn.MaPhieu) AS TongSL,
                          (SELECT ISNULL(SUM(SoLuong*GiaNhap),0) FROM CT_NHAP_KHO WHERE MaPhieu=pn.MaPhieu) AS TongTien
                          FROM PHIEU_NHAP_KHO pn
                          INNER JOIN NHAN_VIEN nv ON pn.MaNV = nv.MaNV
                          WHERE 1=1";
            var parms = new System.Collections.Generic.List<SqlParameter>();
            if (!string.IsNullOrEmpty(keyword))
            {
                sql += " AND (pn.MaPhieu LIKE @Key OR pn.GhiChu LIKE @Key)";
                parms.Add(new SqlParameter("@Key", "%" + keyword + "%"));
            }
            if (!string.IsNullOrEmpty(maNV))
            {
                sql += " AND pn.MaNV = @MaNV";
                parms.Add(new SqlParameter("@MaNV", maNV));
            }
            if (fromDate.HasValue)
            {
                sql += " AND pn.NgayNhap >= @FromDate";
                parms.Add(new SqlParameter("@FromDate", fromDate.Value.Date));
            }
            if (toDate.HasValue)
            {
                sql += " AND pn.NgayNhap <= @ToDate";
                parms.Add(new SqlParameter("@ToDate", toDate.Value.Date.AddDays(1).AddTicks(-1)));
            }
            sql += " ORDER BY pn.NgayNhap DESC";
            return DatabaseHelper.ExecuteRawQuery(sql, parms.ToArray());
        }

        public static DataTable LayChiTiet(string maPhieu)
        {
            string sql = @"SELECT ct.MaSP, sp.TenSP, ct.SoLuong, ct.GiaNhap, (ct.SoLuong*ct.GiaNhap) AS ThanhTien
                          FROM CT_NHAP_KHO ct
                          INNER JOIN SAN_PHAM sp ON ct.MaSP = sp.MaSP
                          WHERE ct.MaPhieu = @MaPhieu";
            return DatabaseHelper.ExecuteRawQuery(sql, new SqlParameter[] { new SqlParameter("@MaPhieu", maPhieu) });
        }

        public static string TaoPhieuNhap(string maNV, string ghiChu = null)
        {
            string maPhieu = DatabaseHelper.GenerateCode("PN", "PHIEU_NHAP_KHO", "MaPhieu");
            DatabaseHelper.ExecuteNonQuery("sp_TaoPhieuNhap", new SqlParameter[]
            {
                new SqlParameter("@MaPhieu", maPhieu),
                new SqlParameter("@MaNV", maNV),
                new SqlParameter("@GhiChu", (object)ghiChu ?? System.DBNull.Value)
            });
            return maPhieu;
        }

        public static void ThemChiTiet(string maPhieu, string maSP, int soLuong, decimal giaNhap)
        {
            DatabaseHelper.ExecuteNonQuery("sp_ThemChiTietNhap", new SqlParameter[]
            {
                new SqlParameter("@MaPhieu", maPhieu),
                new SqlParameter("@MaSP", maSP),
                new SqlParameter("@SoLuong", soLuong),
                new SqlParameter("@GiaNhap", giaNhap)
            });
        }

        public static void GhiNhanHangHu(string maSP, int soLuong)
        {
            DatabaseHelper.ExecuteNonQuery("sp_GhiNhanHangHu", new SqlParameter[]
            {
                new SqlParameter("@MaSP", maSP),
                new SqlParameter("@SoLuong", soLuong)
            });
        }
    }
}
