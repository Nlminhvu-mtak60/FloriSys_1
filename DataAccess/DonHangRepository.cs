using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using FloriSys.Models;

namespace FloriSys.DataAccess
{
    /// <summary>
    /// DonHang repository - inherits BaseRepository&lt;DonHang&gt;.
    /// Demonstrates: INHERITANCE, POLYMORPHISM (complex LayDanhSach with 4 filters + JOIN),
    /// ENCAPSULATION (TaoDonHangHoanChinh wraps full transaction).
    /// </summary>
    public class DonHangRepository : BaseRepository<DonHang>
    {
        public override string TableName => "DON_HANG";
        public override string IdColumn => "MaDon";
        public override string IdPrefix => "DH";

        // POLYMORPHISM: Override with complex 4-filter JOIN query
        public List<DonHang> LayDanhSach(string keyword = "", string trangThai = "", string maNV = "", DateTime? ngay = null)
        {
            return LayDanhSachPhanTrang(1, 1000000, keyword, trangThai, maNV, ngay).Data;
        }

        public (List<DonHang> Data, int TotalCount) LayDanhSachPhanTrang(int page, int pageSize, string keyword = "", string trangThai = "", string maNV = "", DateTime? ngay = null)
        {
            string baseSql = @" FROM DON_HANG dh
                               INNER JOIN KHACH_HANG kh ON dh.MaKH = kh.MaKH
                               INNER JOIN NHAN_VIEN nv ON dh.MaNV_TaoDon = nv.MaNV
                               WHERE 1=1";
            var parms = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(keyword))
            {
                baseSql += " AND (dh.MaDon LIKE @Key OR kh.HoTen LIKE @Key OR kh.SoDienThoai LIKE @Key)";
                parms.Add(new SqlParameter("@Key", "%" + keyword + "%"));
            }
            if (!string.IsNullOrEmpty(trangThai))
            {
                baseSql += " AND dh.TrangThai = @TrangThai";
                parms.Add(new SqlParameter("@TrangThai", trangThai));
            }
            if (!string.IsNullOrEmpty(maNV))
            {
                baseSql += " AND dh.MaNV_TaoDon = @MaNV";
                parms.Add(new SqlParameter("@MaNV", maNV));
            }
            if (ngay.HasValue)
            {
                baseSql += " AND CAST(dh.NgayTao AS DATE) = @Ngay";
                parms.Add(new SqlParameter("@Ngay", ngay.Value.Date));
            }

            // 1. Đếm tổng số dòng (để tính tổng số trang)
            string countSql = "SELECT COUNT(*) " + baseSql;
            int totalCount = (int)DatabaseHelper.ExecuteRawScalar(countSql, parms.ToArray());

            // 2. Lấy dữ liệu theo trang
            string dataSql = @"SELECT dh.MaDon, dh.NgayTao, kh.HoTen AS TenKH, kh.SoDienThoai, 
                             dh.HinhThucNhanHang, dh.TongTien, dh.TrangThai, nv.HoTen AS TenNV, dh.GhiChu " + 
                             baseSql + 
                             " ORDER BY dh.NgayTao DESC " + 
                             " OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY";
            
            var dataParms = new List<SqlParameter>(parms.ConvertAll(p => new SqlParameter(p.ParameterName, p.Value)));
            dataParms.Add(new SqlParameter("@Offset", (page - 1) * pageSize));
            dataParms.Add(new SqlParameter("@Limit", pageSize));

            return (GetList(dataSql, dataParms), totalCount);
        }

        public List<ChiTietDonHang> LayChiTiet(string maDon)
        {
            string sql = @"SELECT ct.MaSP, sp.TenSP, ct.SoLuong, ct.DonGia, ct.ThanhTien
                          FROM CHI_TIET_DON_HANG ct
                          INNER JOIN SAN_PHAM sp ON ct.MaSP = sp.MaSP
                          WHERE ct.MaDon = @MaDon";
            return DatabaseHelper.ExecuteRawList<ChiTietDonHang>(sql, new SqlParameter[] { new SqlParameter("@MaDon", maDon) });
        }

        public DonHang LayThongTinDon(string maDon)
        {
            string sql = @"SELECT dh.MaDon, dh.NgayTao, dh.MaKH, dh.MaNV_TaoDon, dh.HinhThucNhanHang,
                          dh.TrangThai, dh.TongTien, dh.GhiChu,
                          kh.HoTen AS TenKH, kh.SoDienThoai, kh.DiaChi, kh.Email,
                          nv.HoTen AS TenNV
                          FROM DON_HANG dh
                          INNER JOIN KHACH_HANG kh ON dh.MaKH = kh.MaKH
                          INNER JOIN NHAN_VIEN nv ON dh.MaNV_TaoDon = nv.MaNV
                          WHERE dh.MaDon = @MaDon";
            return GetSingle(sql, new SqlParameter[] { new SqlParameter("@MaDon", maDon) });
        }

        public string TaoDonHang(string maKH, string maNV, string hinhThuc, string ghiChu)
        {
            string maDon = TaoMoi();  // INHERITANCE: Uses base class method
            ExecuteSP("sp_TaoDonHang", new SqlParameter[]
            {
                new SqlParameter("@MaDon", maDon),
                new SqlParameter("@MaKH", maKH),
                new SqlParameter("@MaNV_TaoDon", maNV),
                new SqlParameter("@HinhThucNhanHang", hinhThuc),
                NullableParam("@GhiChu", ghiChu)
            });
            return maDon;
        }

        public void ThemChiTiet(string maDon, string maSP, int soLuong, decimal donGia)
        {
            ExecuteSP("sp_ThemChiTietDon", new SqlParameter[]
            {
                new SqlParameter("@MaDon", maDon),
                new SqlParameter("@MaSP", maSP),
                new SqlParameter("@SoLuong", soLuong),
                new SqlParameter("@DonGia", donGia)
            });
        }

        /// <summary>
        /// ENCAPSULATION: Full order creation in a single transaction.
        /// SinhMa → TaoDonHang → ThemChiTiet (N times) → TaoGiaoHang (if delivery).
        /// Auto-rollback on any error.
        /// </summary>
        public string TaoDonHangHoanChinh(string maKH, string maNV, string hinhThuc, string ghiChu, DataTable gioHang)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    // 1. Generate order code
                    string maDon;
                    using (SqlCommand cmdSinhMa = new SqlCommand("sp_SinhMa", conn, tran))
                    {
                        cmdSinhMa.CommandType = CommandType.StoredProcedure;
                        cmdSinhMa.Parameters.AddWithValue("@Prefix", "DH");
                        cmdSinhMa.Parameters.AddWithValue("@Table", "DON_HANG");
                        cmdSinhMa.Parameters.AddWithValue("@Column", "MaDon");
                        SqlParameter outMaDon = new SqlParameter("@NewCode", SqlDbType.NVarChar, 20)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmdSinhMa.Parameters.Add(outMaDon);
                        cmdSinhMa.ExecuteNonQuery();
                        maDon = outMaDon.Value.ToString();
                    }

                    // 2. Create order
                    using (SqlCommand cmdTaoDon = new SqlCommand("sp_TaoDonHang", conn, tran))
                    {
                        cmdTaoDon.CommandType = CommandType.StoredProcedure;
                        cmdTaoDon.Parameters.AddWithValue("@MaDon", maDon);
                        cmdTaoDon.Parameters.AddWithValue("@MaKH", maKH);
                        cmdTaoDon.Parameters.AddWithValue("@MaNV_TaoDon", maNV);
                        cmdTaoDon.Parameters.AddWithValue("@HinhThucNhanHang", hinhThuc);
                        cmdTaoDon.Parameters.AddWithValue("@GhiChu", (object)ghiChu ?? DBNull.Value);
                        cmdTaoDon.ExecuteNonQuery();
                    }

                    // 3. Add each order detail
                    foreach (DataRow row in gioHang.Rows)
                    {
                        using (SqlCommand cmdCT = new SqlCommand("sp_ThemChiTietDon", conn, tran))
                        {
                            cmdCT.CommandType = CommandType.StoredProcedure;
                            cmdCT.Parameters.AddWithValue("@MaDon", maDon);
                            cmdCT.Parameters.AddWithValue("@MaSP", row["MaSP"].ToString());
                            cmdCT.Parameters.AddWithValue("@SoLuong", Convert.ToInt32(row["SoLuong"]));
                            cmdCT.Parameters.AddWithValue("@DonGia", Convert.ToDecimal(row["DonGia"]));
                            cmdCT.ExecuteNonQuery();
                        }
                    }

                    // 4. Create delivery record if needed
                    if (hinhThuc == "GiaoTanNoi")
                    {
                        string maGH;
                        using (SqlCommand cmdSinhGH = new SqlCommand("sp_SinhMa", conn, tran))
                        {
                            cmdSinhGH.CommandType = CommandType.StoredProcedure;
                            cmdSinhGH.Parameters.AddWithValue("@Prefix", "GH");
                            cmdSinhGH.Parameters.AddWithValue("@Table", "GIAO_HANG");
                            cmdSinhGH.Parameters.AddWithValue("@Column", "MaGiaoHang");
                            SqlParameter outMaGH = new SqlParameter("@NewCode", SqlDbType.NVarChar, 20)
                            {
                                Direction = ParameterDirection.Output
                            };
                            cmdSinhGH.Parameters.Add(outMaGH);
                            cmdSinhGH.ExecuteNonQuery();
                            maGH = outMaGH.Value.ToString();
                        }

                        using (SqlCommand cmdGH = new SqlCommand("sp_TaoGiaoHang", conn, tran))
                        {
                            cmdGH.CommandType = CommandType.StoredProcedure;
                            cmdGH.Parameters.AddWithValue("@MaGiaoHang", maGH);
                            cmdGH.Parameters.AddWithValue("@MaDon", maDon);
                            cmdGH.Parameters.AddWithValue("@GhiChu", DBNull.Value);
                            cmdGH.ExecuteNonQuery();
                        }
                    }

                    tran.Commit();
                    return maDon;
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        public void CapNhatTrangThai(string maDon, string trangThai)
        {
            ExecuteSP("sp_CapNhatTrangThaiDon", new SqlParameter[]
            {
                new SqlParameter("@MaDon", maDon),
                new SqlParameter("@TrangThai", trangThai)
            });
        }

        public List<DonChoXuatKho> LayDonChoXuatKho()
        {
            string sql = @"SELECT dh.MaDon, kh.HoTen AS TenKH, sp.TenSP, ct.SoLuong, sp.SoLuongTon,
                          CASE WHEN sp.SoLuongTon >= ct.SoLuong THEN N'DuHang' ELSE N'KhongDu' END AS TinhTrangKho,
                          dh.HinhThucNhanHang
                          FROM DON_HANG dh
                          INNER JOIN KHACH_HANG kh ON dh.MaKH = kh.MaKH
                          INNER JOIN CHI_TIET_DON_HANG ct ON dh.MaDon = ct.MaDon
                          INNER JOIN SAN_PHAM sp ON ct.MaSP = sp.MaSP
                          WHERE dh.TrangThai = N'Moi'
                          ORDER BY dh.NgayTao";
            return DatabaseHelper.ExecuteRawList<DonChoXuatKho>(sql);
        }

        public List<LichSuDonHang> LayLichSuDonHang(string maDon)
        {
            string sql = @"SELECT MaDon, TrangThai, ThoiGian, GhiChu
                          FROM LICH_SU_DON_HANG
                          WHERE MaDon = @MaDon
                          ORDER BY ThoiGian ASC";
            return DatabaseHelper.ExecuteRawList<LichSuDonHang>(sql,
                new SqlParameter[] { new SqlParameter("@MaDon", maDon) });
        }
    }
}
