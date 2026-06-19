using System;
using System.Data;
using System.Data.SqlClient;
using FloriSys.Models;

namespace FloriSys.DataAccess
{
    /// <summary>
    /// TraHang repository - inherits BaseRepository&lt;TraHang&gt;.
    /// Demonstrates: ENCAPSULATION (ThemPhieuTraHoanChinh wraps full return transaction).
    /// </summary>
    public class TraHangRepository : BaseRepository<TraHang>
    {
        public override string TableName => "TRA_HANG";
        public override string IdColumn => "MaPhieuTra";
        public override string IdPrefix => "PTR";

        /// <summary>
        /// Lấy danh sách các đơn hàng ở trạng thái HoanHang nhưng chưa có phiếu trả
        /// </summary>
        public DataTable LayDanhSachDonChoTra()
        {
            string sql = @"SELECT dh.MaDon, kh.HoTen AS TenKH, dh.NgayTao, dh.TongTien
                          FROM DON_HANG dh
                          JOIN KHACH_HANG kh ON dh.MaKH = kh.MaKH
                          WHERE dh.TrangThai = N'HoanHang'
                            AND dh.MaDon NOT IN (SELECT MaDon FROM TRA_HANG)
                          ORDER BY dh.NgayTao DESC";
            return DatabaseHelper.ExecuteRawQuery(sql);
        }

        /// <summary>
        /// ENCAPSULATION: Full return processing in a single transaction.
        /// SinhMa → TRA_HANG → CT_TRA_HANG (N times) → Update tồn kho → Update trạng thái đơn.
        /// Auto-rollback on any error.
        /// </summary>
        public string ThemPhieuTraHoanChinh(string maDon, string lyDo, string hinhThuc,
                                             string ghiChu, DataTable chiTietTra)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    // 1. Generate return slip code
                    string maPhieu;
                    using (SqlCommand cmdSinhMa = new SqlCommand("sp_SinhMa", conn, tran))
                    {
                        cmdSinhMa.CommandType = CommandType.StoredProcedure;
                        cmdSinhMa.Parameters.AddWithValue("@Prefix", "PTR");
                        cmdSinhMa.Parameters.AddWithValue("@Table", "TRA_HANG");
                        cmdSinhMa.Parameters.AddWithValue("@Column", "MaPhieuTra");
                        SqlParameter outCode = new SqlParameter("@NewCode", SqlDbType.NVarChar, 20)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmdSinhMa.Parameters.Add(outCode);
                        cmdSinhMa.ExecuteNonQuery();
                        maPhieu = outCode.Value.ToString();
                    }

                    // 2. Create return slip
                    using (SqlCommand cmdTra = new SqlCommand(
                        @"INSERT INTO TRA_HANG (MaPhieuTra, MaDon, LyDo, HinhThucHoanTien, GhiChu) 
                          VALUES (@MaPT, @MaDon, @LyDo, @HinhThuc, @GhiChu)", conn, tran))
                    {
                        cmdTra.Parameters.AddWithValue("@MaPT", maPhieu);
                        cmdTra.Parameters.AddWithValue("@MaDon", maDon);
                        cmdTra.Parameters.AddWithValue("@LyDo", lyDo);
                        cmdTra.Parameters.AddWithValue("@HinhThuc", hinhThuc);
                        cmdTra.Parameters.AddWithValue("@GhiChu", (object)ghiChu ?? DBNull.Value);
                        cmdTra.ExecuteNonQuery();
                    }

                    // 3. Add each return detail + update stock if needed
                    foreach (DataRow row in chiTietTra.Rows)
                    {
                        int slTra = Convert.ToInt32(row["SoLuong"]);
                        if (slTra <= 0) continue;

                        string maSP = row["MaSP"].ToString();
                        bool coNhapKho = Convert.ToBoolean(row["CoNhapKho"]);

                        // Insert detail
                        using (SqlCommand cmdCT = new SqlCommand(
                            @"INSERT INTO CT_TRA_HANG (MaPhieuTra, MaSP, SoLuong, CoNhapKho) 
                              VALUES (@MaPT, @MaSP, @SoLuong, @CoNhapKho)", conn, tran))
                        {
                            cmdCT.Parameters.AddWithValue("@MaPT", maPhieu);
                            cmdCT.Parameters.AddWithValue("@MaSP", maSP);
                            cmdCT.Parameters.AddWithValue("@SoLuong", slTra);
                            cmdCT.Parameters.AddWithValue("@CoNhapKho", coNhapKho);
                            cmdCT.ExecuteNonQuery();
                        }

                        // Increase stock if restock
                        if (coNhapKho)
                        {
                            using (SqlCommand cmdStock = new SqlCommand(
                                "UPDATE SAN_PHAM SET SoLuongTon = SoLuongTon + @SoLuong WHERE MaSP = @MaSP", conn, tran))
                            {
                                cmdStock.Parameters.AddWithValue("@MaSP", maSP);
                                cmdStock.Parameters.AddWithValue("@SoLuong", slTra);
                                cmdStock.ExecuteNonQuery();
                            }
                        }
                    }

                    // 4. Update order status to HoanHang (if not already)
                    using (SqlCommand cmdDH = new SqlCommand(
                        "UPDATE DON_HANG SET TrangThai = N'HoanHang' WHERE MaDon = @MaDon", conn, tran))
                    {
                        cmdDH.Parameters.AddWithValue("@MaDon", maDon);
                        cmdDH.ExecuteNonQuery();
                    }

                    // 5. Thêm nhật ký xử lý (vì Trigger không bắt được nếu trạng thái không đổi)
                    using (SqlCommand cmdLog = new SqlCommand(
                        @"INSERT INTO LICH_SU_DON_HANG (MaDon, TrangThai, ThoiGian, GhiChu)
                          VALUES (@MaDon, N'HoanHang', GETDATE(), @GhiChuLog)", conn, tran))
                    {
                        cmdLog.Parameters.AddWithValue("@MaDon", maDon);
                        cmdLog.Parameters.AddWithValue("@GhiChuLog", "Nhân viên đã duyệt phiếu trả hàng & hoàn tiền (" + hinhThuc + ")");
                        cmdLog.ExecuteNonQuery();
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
