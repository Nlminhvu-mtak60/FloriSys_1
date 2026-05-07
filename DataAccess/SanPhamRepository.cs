using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using FloriSys.Models;

namespace FloriSys.DataAccess
{
    /// <summary>
    /// SanPham repository - inherits BaseRepository&lt;SanPham&gt;.
    /// Demonstrates: INHERITANCE (uses base GenerateCode via TaoMoi),
    /// POLYMORPHISM (overrides LayDanhSach with 3 filters).
    /// </summary>
    public class SanPhamRepository : BaseRepository<SanPham>
    {
        public override string TableName => "SAN_PHAM";
        public override string IdColumn => "MaSP";
        public override string IdPrefix => "SP";

        // POLYMORPHISM: Override with custom 3-filter search
        public List<SanPham> LayDanhSach(string keyword = "", string loai = "", string trangThai = "")
        {
            string sql = @"SELECT MaSP, TenSP, LoaiHoa, GiaBan, GiaNhap, SoLuongTon, MucTonToiThieu, TrangThai 
                          FROM SAN_PHAM WHERE 1=1";
            var parms = new List<SqlParameter>();
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
            return GetList(sql, parms);
        }

        public List<SanPham> LaySanPhamDangBan(string keyword = "")
        {
            string sql = @"SELECT MaSP, TenSP, LoaiHoa, GiaBan, SoLuongTon 
                          FROM SAN_PHAM WHERE TrangThai=N'DangBan'";
            var parms = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(keyword))
            {
                sql += " AND TenSP LIKE @Key";
                parms.Add(new SqlParameter("@Key", "%" + keyword + "%"));
            }
            sql += " ORDER BY TenSP";
            return GetList(sql, parms);
        }

        public string ThemSanPham(SanPham sp)
        {
            string maSP = TaoMoi();  // INHERITANCE: Uses base class method
            sp.MaSP = maSP;
            string sql = @"INSERT INTO SAN_PHAM (MaSP, TenSP, LoaiHoa, GiaBan, GiaNhap, MucTonToiThieu) 
                          VALUES (@MaSP, @TenSP, @LoaiHoa, @GiaBan, @GiaNhap, @MucTon)";
            ExecuteSql(sql, new SqlParameter[]
            {
                new SqlParameter("@MaSP", sp.MaSP),
                new SqlParameter("@TenSP", sp.TenSP),
                new SqlParameter("@LoaiHoa", sp.LoaiHoa),
                new SqlParameter("@GiaBan", sp.GiaBan),
                new SqlParameter("@GiaNhap", sp.GiaNhap),
                new SqlParameter("@MucTon", sp.MucTonToiThieu)
            });
            return maSP;
        }

        public void CapNhatSanPham(SanPham sp)
        {
            string sql = @"UPDATE SAN_PHAM SET TenSP=@TenSP, LoaiHoa=@LoaiHoa, GiaBan=@GiaBan, 
                          GiaNhap=@GiaNhap, MucTonToiThieu=@MucTon, TrangThai=@TrangThai WHERE MaSP=@MaSP";
            ExecuteSql(sql, new SqlParameter[]
            {
                new SqlParameter("@MaSP", sp.MaSP),
                new SqlParameter("@TenSP", sp.TenSP),
                new SqlParameter("@LoaiHoa", sp.LoaiHoa),
                new SqlParameter("@GiaBan", sp.GiaBan),
                new SqlParameter("@GiaNhap", sp.GiaNhap),
                new SqlParameter("@MucTon", sp.MucTonToiThieu),
                new SqlParameter("@TrangThai", sp.TrangThai)
            });
        }

        public void CapNhatMucTonToiThieu(string maSP, int mucTon)
        {
            string sql = "UPDATE SAN_PHAM SET MucTonToiThieu=@MucTon WHERE MaSP=@MaSP";
            ExecuteSql(sql, new SqlParameter[]
            {
                new SqlParameter("@MaSP", maSP),
                new SqlParameter("@MucTon", mucTon)
            });
        }

        public List<SanPham> LayCanhBaoTonKho()
        {
            return GetListFromSP("sp_CanhBaoTonKho");
        }

        public void NgungBanSanPham(string maSP)
        {
            string sql = "UPDATE SAN_PHAM SET TrangThai = N'NgungBan' WHERE MaSP = @MaSP";
            ExecuteSql(sql, new SqlParameter[]
            {
                new SqlParameter("@MaSP", maSP)
            });
        }

        public string LayMaSPSinhTuDong()
        {
            try
            {
                return TaoMoi();  // INHERITANCE: Uses base class method
            }
            catch
            {
                return "SP999999";
            }
        }
    }
}
