using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using FloriSys.Models;

namespace FloriSys.DataAccess
{
    /// <summary>
    /// HangHu repository - inherits BaseRepository&lt;HangHu&gt;.
    /// </summary>
    public class HangHuRepository : BaseRepository<HangHu>
    {
        public override string TableName => "HANG_HU";
        public override string IdColumn => "MaPhieuHuy";
        public override string IdPrefix => "PHH";

        public void GhiNhan(HangHu hh)
        {
            string maPhieu = TaoMoi();
            hh.MaPhieuHuy = maPhieu;
            ExecuteSP("sp_GhiNhanHangHu", new SqlParameter[]
            {
                new SqlParameter("@MaPhieuHuy", maPhieu),
                new SqlParameter("@MaSP", hh.MaSP),
                new SqlParameter("@SoLuong", hh.SoLuong),
                new SqlParameter("@LyDo", hh.LyDo),
                NullableParam("@GhiChu", hh.GhiChu)
            });
        }

        public List<HangHu> LayLichSu(int thang = 0, int nam = 0)
        {
            string sql = @"SELECT h.MaPhieuHuy, s.TenSP, h.SoLuong, h.LyDo, h.NgayHuy, h.GhiChu, s.GiaNhap 
                          FROM HANG_HU h JOIN SAN_PHAM s ON h.MaSP = s.MaSP";
            if (thang > 0 && nam > 0)
            {
                sql += " WHERE MONTH(NgayHuy) = @Thang AND YEAR(NgayHuy) = @Nam";
            }
            sql += " ORDER BY NgayHuy DESC";
            return GetList(sql, new List<SqlParameter>
            {
                new SqlParameter("@Thang", thang),
                new SqlParameter("@Nam", nam)
            });
        }
    }
}
