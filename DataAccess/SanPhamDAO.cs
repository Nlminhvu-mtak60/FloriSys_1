using System.Data;
using System.Data.SqlClient;

namespace FloriSys.DataAccess
{
    public class SanPhamDAO
    {
        public static DataTable LayDanhSach(string keyword = "", string loai = "", string trangThai = "")
        {
            string sql = @"SELECT MaSP, TenSP, LoaiHoa, GiaBan, GiaNhap, SoLuongTon, MucTonToiThieu, TrangThai 
                          FROM SAN_PHAM WHERE 1=1";
            var parms = new System.Collections.Generic.List<SqlParameter>();
            if (!string.IsNullOrEmpty(keyword))
            {
                sql += " AND TenSP LIKE @Key";
                parms.Add(new SqlParameter("@Key", "%" + keyword + "%"));
            }
            if (!string.IsNullOrEmpty(loai))
            {
                sql += " AND LoaiHoa = @Loai";
                parms.Add(new SqlParameter("@Loai", loai));
            }
            if (!string.IsNullOrEmpty(trangThai))
            {
                sql += " AND TrangThai = @TrangThai";
                parms.Add(new SqlParameter("@TrangThai", trangThai));
            }
            sql += " ORDER BY MaSP";
            return DatabaseHelper.ExecuteRawQuery(sql, parms.ToArray());
        }

        public static DataTable LaySanPhamDangBan(string keyword = "")
        {
            string sql = @"SELECT MaSP, TenSP, LoaiHoa, GiaBan, SoLuongTon 
                          FROM SAN_PHAM WHERE TrangThai=N'DangBan'";
            var parms = new System.Collections.Generic.List<SqlParameter>();
            if (!string.IsNullOrEmpty(keyword))
            {
                sql += " AND TenSP LIKE @Key";
                parms.Add(new SqlParameter("@Key", "%" + keyword + "%"));
            }
            sql += " ORDER BY TenSP";
            return DatabaseHelper.ExecuteRawQuery(sql, parms.ToArray());
        }

        public static void ThemSanPham(string maSP, string tenSP, string loaiHoa, decimal giaBan, decimal giaNhap, int mucTonToiThieu)
        {
            string sql = @"INSERT INTO SAN_PHAM (MaSP, TenSP, LoaiHoa, GiaBan, GiaNhap, MucTonToiThieu) 
                          VALUES (@MaSP, @TenSP, @LoaiHoa, @GiaBan, @GiaNhap, @MucTon)";
            DatabaseHelper.ExecuteRawNonQuery(sql, new SqlParameter[]
            {
                new SqlParameter("@MaSP", maSP),
                new SqlParameter("@TenSP", tenSP),
                new SqlParameter("@LoaiHoa", loaiHoa),
                new SqlParameter("@GiaBan", giaBan),
                new SqlParameter("@GiaNhap", giaNhap),
                new SqlParameter("@MucTon", mucTonToiThieu)
            });
        }

        public static void CapNhatSanPham(string maSP, string tenSP, string loaiHoa, decimal giaBan, decimal giaNhap, int mucTonToiThieu, string trangThai)
        {
            string sql = @"UPDATE SAN_PHAM SET TenSP=@TenSP, LoaiHoa=@LoaiHoa, GiaBan=@GiaBan, 
                          GiaNhap=@GiaNhap, MucTonToiThieu=@MucTon, TrangThai=@TrangThai WHERE MaSP=@MaSP";
            DatabaseHelper.ExecuteRawNonQuery(sql, new SqlParameter[]
            {
                new SqlParameter("@MaSP", maSP),
                new SqlParameter("@TenSP", tenSP),
                new SqlParameter("@LoaiHoa", loaiHoa),
                new SqlParameter("@GiaBan", giaBan),
                new SqlParameter("@GiaNhap", giaNhap),
                new SqlParameter("@MucTon", mucTonToiThieu),
                new SqlParameter("@TrangThai", trangThai)
            });
        }

        public static void CapNhatMucTonToiThieu(string maSP, int mucTon)
        {
            string sql = "UPDATE SAN_PHAM SET MucTonToiThieu=@MucTon WHERE MaSP=@MaSP";
            DatabaseHelper.ExecuteRawNonQuery(sql, new SqlParameter[]
            {
                new SqlParameter("@MaSP", maSP),
                new SqlParameter("@MucTon", mucTon)
            });
        }

        public static DataTable LayCanhBaoTonKho()
        {
            return DatabaseHelper.ExecuteQuery("sp_CanhBaoTonKho");
        }
    }
}
