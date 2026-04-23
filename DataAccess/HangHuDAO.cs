using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using FloriSys.Models;

namespace FloriSys.DataAccess
{
    public class HangHuDAO
    {
        public static void GhiNhan(HangHu hh)
        {
            string maPhieu = DatabaseHelper.GenerateCode("PHH", "HANG_HU", "MaPhieuHuy");
            DatabaseHelper.ExecuteNonQuery("sp_GhiNhanHangHu", new SqlParameter[]
            {
                new SqlParameter("@MaPhieuHuy", maPhieu),
                new SqlParameter("@MaSP", hh.MaSP),
                new SqlParameter("@SoLuong", hh.SoLuong),
                new SqlParameter("@LyDo", hh.LyDo),
                new SqlParameter("@GhiChu", (object)hh.GhiChu ?? DBNull.Value)
            });
        }

        public static List<HangHu> LayLichSu(int thang = 0, int nam = 0)
        {
            string sql = @"SELECT h.MaPhieuHuy, s.TenSP, h.SoLuong, h.LyDo, h.NgayHuy, h.GhiChu 
                          FROM HANG_HU h JOIN SAN_PHAM s ON h.MaSP = s.MaSP";
            if (thang > 0 && nam > 0)
            {
                sql += " WHERE MONTH(NgayHuy) = @Thang AND YEAR(NgayHuy) = @Nam";
            }
            sql += " ORDER BY NgayHuy DESC";
            return DatabaseHelper.ExecuteRawList<HangHu>(sql, new SqlParameter[] {
                new SqlParameter("@Thang", thang),
                new SqlParameter("@Nam", nam)
            });
        }
    }
}
