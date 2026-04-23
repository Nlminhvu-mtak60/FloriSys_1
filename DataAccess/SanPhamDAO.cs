using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using FloriSys.Models;

namespace FloriSys.DataAccess
{
    public class SanPhamDAO
    {
        public static List<SanPham> LayDanhSach(string keyword = "", string loai = "", string trangThai = "")
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
            return DatabaseHelper.ExecuteRawList<SanPham>(sql, parms.ToArray());
        }

        public static List<SanPham> LaySanPhamDangBan(string keyword = "")
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
            return DatabaseHelper.ExecuteRawList<SanPham>(sql, parms.ToArray());
        }

        public static void ThemSanPham(SanPham sp)
        {
            string sql = @"INSERT INTO SAN_PHAM (MaSP, TenSP, LoaiHoa, GiaBan, GiaNhap, MucTonToiThieu) 
                          VALUES (@MaSP, @TenSP, @LoaiHoa, @GiaBan, @GiaNhap, @MucTon)";
            DatabaseHelper.ExecuteRawNonQuery(sql, new SqlParameter[]
            {
                new SqlParameter("@MaSP", sp.MaSP),
                new SqlParameter("@TenSP", sp.TenSP),
                new SqlParameter("@LoaiHoa", sp.LoaiHoa),
                new SqlParameter("@GiaBan", sp.GiaBan),
                new SqlParameter("@GiaNhap", sp.GiaNhap),
                new SqlParameter("@MucTon", sp.MucTonToiThieu)
            });
        }

        public static void CapNhatSanPham(SanPham sp)
        {
            string sql = @"UPDATE SAN_PHAM SET TenSP=@TenSP, LoaiHoa=@LoaiHoa, GiaBan=@GiaBan, 
                          GiaNhap=@GiaNhap, MucTonToiThieu=@MucTon, TrangThai=@TrangThai WHERE MaSP=@MaSP";
            DatabaseHelper.ExecuteRawNonQuery(sql, new SqlParameter[]
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

        public static void CapNhatMucTonToiThieu(string maSP, int mucTon)
        {
            string sql = "UPDATE SAN_PHAM SET MucTonToiThieu=@MucTon WHERE MaSP=@MaSP";
            DatabaseHelper.ExecuteRawNonQuery(sql, new SqlParameter[]
            {
                new SqlParameter("@MaSP", maSP),
                new SqlParameter("@MucTon", mucTon)
            });
        }

        public static List<SanPham> LayCanhBaoTonKho()
        {
            return DatabaseHelper.ExecuteList<SanPham>("sp_CanhBaoTonKho");
        }
    }
}
