using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using FloriSys.Models;

namespace FloriSys.DataAccess
{
    /// <summary>
    /// PhieuNhapKho repository - inherits BaseRepository&lt;PhieuNhapKho&gt;.
    /// </summary>
    public class PhieuNhapKhoRepository : BaseRepository<PhieuNhapKho>
    {
        public override string TableName => "PHIEU_NHAP_KHO";
        public override string IdColumn => "MaPhieu";
        public override string IdPrefix => "PN";

        public List<PhieuNhapKho> LayDanhSach(string keyword = "", string maNV = "", DateTime? fromDate = null, DateTime? toDate = null)
        {
            string sql = @"SELECT pn.MaPhieu, pn.NgayNhap, nv.HoTen AS TenNV, pn.GhiChu,
                          (SELECT COUNT(*) FROM CT_NHAP_KHO WHERE MaPhieu=pn.MaPhieu) AS SoLoaiSP,
                          (SELECT ISNULL(SUM(SoLuong),0) FROM CT_NHAP_KHO WHERE MaPhieu=pn.MaPhieu) AS TongSL,
                          (SELECT ISNULL(SUM(SoLuong*GiaNhap),0) FROM CT_NHAP_KHO WHERE MaPhieu=pn.MaPhieu) AS TongTien
                          FROM PHIEU_NHAP_KHO pn
                          INNER JOIN NHAN_VIEN nv ON pn.MaNV = nv.MaNV
                          WHERE 1=1";
            var parms = new List<SqlParameter>();
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
            return GetList(sql, parms);
        }

        public List<ChiTietNhapKho> LayChiTiet(string maPhieu)
        {
            string sql = @"SELECT ct.MaSP, sp.TenSP, ct.SoLuong, ct.GiaNhap, (ct.SoLuong*ct.GiaNhap) AS ThanhTien
                          FROM CT_NHAP_KHO ct
                          INNER JOIN SAN_PHAM sp ON ct.MaSP = sp.MaSP
                          WHERE ct.MaPhieu = @MaPhieu";
            return DatabaseHelper.ExecuteRawList<ChiTietNhapKho>(sql, new SqlParameter[] { new SqlParameter("@MaPhieu", maPhieu) });
        }

        public string TaoPhieuNhapHoanChinh(string maNV, string ghiChu, DataTable dsNhap)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    // 1. Generate code
                    string maPhieu;
                    using (SqlCommand cmdSinhMa = new SqlCommand("sp_SinhMa", conn, tran))
                    {
                        cmdSinhMa.CommandType = CommandType.StoredProcedure;
                        cmdSinhMa.Parameters.AddWithValue("@Prefix", "PN");
                        cmdSinhMa.Parameters.AddWithValue("@Table", "PHIEU_NHAP_KHO");
                        cmdSinhMa.Parameters.AddWithValue("@Column", "MaPhieu");
                        SqlParameter outMaPhieu = new SqlParameter("@NewCode", SqlDbType.NVarChar, 20) { Direction = ParameterDirection.Output };
                        cmdSinhMa.Parameters.Add(outMaPhieu);
                        cmdSinhMa.ExecuteNonQuery();
                        maPhieu = outMaPhieu.Value.ToString();
                    }

                    // 2. Create Phieu
                    using (SqlCommand cmdPN = new SqlCommand("sp_TaoPhieuNhap", conn, tran))
                    {
                        cmdPN.CommandType = CommandType.StoredProcedure;
                        cmdPN.Parameters.AddWithValue("@MaPhieu", maPhieu);
                        cmdPN.Parameters.AddWithValue("@MaNV", maNV);
                        cmdPN.Parameters.AddWithValue("@GhiChu", (object)ghiChu ?? DBNull.Value);
                        cmdPN.ExecuteNonQuery();
                    }

                    // 3. Add details
                    foreach (DataRow row in dsNhap.Rows)
                    {
                        using (SqlCommand cmdCT = new SqlCommand("sp_ThemChiTietNhap", conn, tran))
                        {
                            cmdCT.CommandType = CommandType.StoredProcedure;
                            cmdCT.Parameters.AddWithValue("@MaPhieu", maPhieu);
                            cmdCT.Parameters.AddWithValue("@MaSP", row["MaSP"].ToString());
                            cmdCT.Parameters.AddWithValue("@SoLuong", Convert.ToInt32(row["SoLuong"]));
                            cmdCT.Parameters.AddWithValue("@GiaNhap", Convert.ToDecimal(row["GiaNhap"]));
                            cmdCT.ExecuteNonQuery();
                        }
                    }

                    tran.Commit();
                    return maPhieu;
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }
    }
}
