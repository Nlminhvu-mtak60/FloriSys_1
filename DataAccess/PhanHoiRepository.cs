using System.Collections.Generic;
using System.Data.SqlClient;
using FloriSys.Models;

namespace FloriSys.DataAccess
{
    /// <summary>
    /// PhanHoi repository - inherits BaseRepository&lt;PhanHoi&gt;.
    /// </summary>
    public class PhanHoiRepository : BaseRepository<PhanHoi>
    {
        public override string TableName => "PHAN_HOI";
        public override string IdColumn => "MaPH";
        public override string IdPrefix => "PH";

        public new List<PhanHoi> LayDanhSach(string maDon = "")
        {
            string sql = @"SELECT ph.MaPH, ph.MaDon, ph.NoiDung, ph.NgayGhi, ph.TrangThaiXuLy, ph.KetQuaXuLy,
                          kh.HoTen AS TenKH
                          FROM PHAN_HOI ph
                          LEFT JOIN DON_HANG dh ON ph.MaDon = dh.MaDon
                          LEFT JOIN KHACH_HANG kh ON dh.MaKH = kh.MaKH
                          WHERE 1=1";
            var parms = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(maDon))
            {
                string trimmedMaDon = maDon.Trim();
                sql += " AND ph.MaDon = @MaDon";
                parms.Add(new SqlParameter("@MaDon", trimmedMaDon));
            }
            sql += " ORDER BY ph.NgayGhi DESC";
            return GetList(sql, parms);
        }

        public string GhiNhan(string maDon, string noiDung)
        {
            string maPH = TaoMoi();
            ExecuteSP("sp_GhiNhanPhanHoi", new SqlParameter[]
            {
                new SqlParameter("@MaPH", maPH),
                new SqlParameter("@MaDon", maDon),
                new SqlParameter("@NoiDung", noiDung)
            });
            return maPH;
        }

        public void CapNhatXuLy(string maPH, string trangThai, string ketQua)
        {
            string sql = "UPDATE PHAN_HOI SET TrangThaiXuLy=@TrangThai, KetQuaXuLy=@KetQua WHERE MaPH=@MaPH";
            ExecuteSql(sql, new SqlParameter[]
            {
                new SqlParameter("@MaPH", maPH),
                new SqlParameter("@TrangThai", trangThai),
                new SqlParameter("@KetQua", ketQua)
            });
        }
    }
}
