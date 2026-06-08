using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using FloriSys.Models;

namespace FloriSys.Services
{
    public static class ReportExcelHelper
    {
        public static void ExportBaoCaoNgayExcel(DateTime ngay, BaoCaoDoanhThu dt, List<TopSanPhamNgay> topSP, int slSP)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx", FileName = $"BaoCaoNgay_{ngay:ddMMyyyy}.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (ExcelPackage excel = new ExcelPackage())
                        {
                            var sheet = excel.Workbook.Worksheets.Add("Báo Cáo Ngày");

                            // Header
                            sheet.Cells["A1:D1"].Merge = true;
                            sheet.Cells["A1"].Value = $"BÁO CÁO DOANH THU NGÀY {ngay:dd/MM/yyyy}";
                            sheet.Cells["A1"].Style.Font.Bold = true;
                            sheet.Cells["A1"].Style.Font.Size = 16;
                            sheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            sheet.Cells["A1"].Style.Font.Color.SetColor(Color.FromArgb(232, 57, 77));

                            // KPIs
                            sheet.Cells["A3"].Value = "Tổng đơn hàng:";
                            sheet.Cells["B3"].Value = dt.TongDon;
                            sheet.Cells["A4"].Value = "Tổng doanh thu:";
                            sheet.Cells["B4"].Value = dt.TongDoanhThu;
                            sheet.Cells["B4"].Style.Numberformat.Format = "#,##0 ₫";
                            sheet.Cells["A5"].Value = "Số sản phẩm bán:";
                            sheet.Cells["B5"].Value = slSP;

                            sheet.Cells["A3:A5"].Style.Font.Bold = true;

                            // Table
                            sheet.Cells["A7:D7"].Merge = true;
                            sheet.Cells["A7"].Value = "Top Sản Phẩm Bán Chạy Hôm Nay";
                            sheet.Cells["A7"].Style.Font.Bold = true;

                            sheet.Cells["A8"].Value = "STT";
                            sheet.Cells["B8"].Value = "Tên Sản Phẩm";
                            sheet.Cells["C8"].Value = "Số lượng bán";
                            sheet.Cells["D8"].Value = "Doanh Thu";
                            
                            var headerRange = sheet.Cells["A8:D8"];
                            headerRange.Style.Font.Bold = true;
                            headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            headerRange.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(45, 106, 79));
                            headerRange.Style.Font.Color.SetColor(Color.White);

                            int row = 9;
                            int stt = 1;
                            foreach (var sp in topSP)
                            {
                                sheet.Cells[$"A{row}"].Value = stt++;
                                sheet.Cells[$"B{row}"].Value = sp.TenSP;
                                sheet.Cells[$"C{row}"].Value = sp.SLBan;
                                sheet.Cells[$"D{row}"].Value = sp.DoanhThu;
                                sheet.Cells[$"D{row}"].Style.Numberformat.Format = "#,##0 ₫";
                                row++;
                            }

                            sheet.Cells.AutoFitColumns();

                            // Save
                            FileInfo excelFile = new FileInfo(sfd.FileName);
                            excel.SaveAs(excelFile);
                            
                            MessageBox.Show("Xuất file Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi xuất Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public static void ExportBaoCaoThangExcel(int thang, int nam, BaoCaoDoanhThu dtCurrent, BaoCaoDoanhThu dtPrev, List<SanPhamBanChay> topSP, List<DoanhThuNgay> dsNgay)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx", FileName = $"BaoCaoThang_{thang}_{nam}.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (ExcelPackage excel = new ExcelPackage())
                        {
                            var sheet = excel.Workbook.Worksheets.Add("Dữ liệu Báo Cáo");
                            sheet.View.ShowGridLines = true;

                            // Title
                            sheet.Cells["A1"].Value = $"BÁO CÁO DOANH THU THÁNG {thang}/{nam}";
                            sheet.Cells["A1"].Style.Font.Bold = true;
                            sheet.Cells["A1"].Style.Font.Size = 14;

                            sheet.Cells["A2"].Value = $"Ngày lập báo cáo: {DateTime.Now:dd/MM/yyyy HH:mm}";
                            sheet.Cells["A2"].Style.Font.Italic = true;

                            // Calculate Extra Metrics
                            int tongDon = 0;
                            if (dsNgay != null)
                            {
                                foreach (var dt in dsNgay) tongDon += dt.SoDon;
                            }
                            decimal tongDT = dtCurrent != null ? dtCurrent.TongDoanhThu : 0;
                            decimal tbDon = tongDon > 0 ? tongDT / tongDon : 0;
                            
                            string compare = "Chưa có dữ liệu";
                            if (dtCurrent != null && dtPrev != null && dtPrev.TongDoanhThu > 0)
                            {
                                decimal phanTram = ((tongDT - dtPrev.TongDoanhThu) / dtPrev.TongDoanhThu) * 100;
                                compare = $"{(phanTram >= 0 ? "Tăng" : "Giảm")} {Math.Abs(phanTram):N1}%";
                            }

                            // Summary Data
                            sheet.Cells["A4"].Value = "TỔNG QUAN";
                            sheet.Cells["A4"].Style.Font.Bold = true;
                            
                            sheet.Cells["A5"].Value = "Tổng doanh thu:";
                            sheet.Cells["B5"].Value = tongDT;
                            sheet.Cells["B5"].Style.Numberformat.Format = "#,##0 ₫";
                            
                            sheet.Cells["A6"].Value = "So với tháng trước:";
                            sheet.Cells["B6"].Value = compare;
                            
                            sheet.Cells["A7"].Value = "Tổng số đơn hàng:";
                            sheet.Cells["B7"].Value = tongDon;
                            
                            sheet.Cells["A8"].Value = "Giá trị TB / đơn:";
                            sheet.Cells["B8"].Value = tbDon;
                            sheet.Cells["B8"].Style.Numberformat.Format = "#,##0 ₫";

                            // Main Data Table (Daily)
                            int startRow = 11;
                            sheet.Cells[$"A{startRow}"].Value = "Ngày";
                            sheet.Cells[$"B{startRow}"].Value = "Số lượng đơn";
                            sheet.Cells[$"C{startRow}"].Value = "Tỷ trọng đơn";
                            sheet.Cells[$"D{startRow}"].Value = "Doanh thu";
                            sheet.Cells[$"E{startRow}"].Value = "Tỷ trọng DT";
                            sheet.Cells[$"F{startRow}"].Value = "Trung bình/Đơn";

                            int row = startRow + 1;
                            if (dsNgay != null)
                            {
                                foreach (var dt in dsNgay)
                                {
                                    sheet.Cells[$"A{row}"].Value = dt.Ngay.Day; // Just day number for better filtering
                                    sheet.Cells[$"B{row}"].Value = dt.SoDon;
                                    sheet.Cells[$"C{row}"].Value = tongDon > 0 ? (double)dt.SoDon / tongDon : 0;
                                    sheet.Cells[$"C{row}"].Style.Numberformat.Format = "0.0%";
                                    
                                    sheet.Cells[$"D{row}"].Value = dt.DoanhThu;
                                    sheet.Cells[$"D{row}"].Style.Numberformat.Format = "#,##0 ₫";
                                    
                                    sheet.Cells[$"E{row}"].Value = tongDT > 0 ? (double)dt.DoanhThu / (double)tongDT : 0;
                                    sheet.Cells[$"E{row}"].Style.Numberformat.Format = "0.0%";
                                    
                                    sheet.Cells[$"F{row}"].Value = dt.SoDon > 0 ? dt.DoanhThu / dt.SoDon : 0;
                                    sheet.Cells[$"F{row}"].Style.Numberformat.Format = "#,##0 ₫";
                                    row++;
                                }
                            }

                            if (row > startRow + 1)
                            {
                                var tableRange = sheet.Cells[$"A{startRow}:F{row - 1}"];
                                var table = sheet.Tables.Add(tableRange, "BangDoanhThuNgay");
                                table.TableStyle = OfficeOpenXml.Table.TableStyles.Medium2;
                            }

                            // Second Table: Product Details
                            int startRowSP = row + 2;
                            sheet.Cells[$"A{startRowSP}"].Value = "Tên Sản phẩm";
                            sheet.Cells[$"B{startRowSP}"].Value = "Số lượng bán";
                            sheet.Cells[$"C{startRowSP}"].Value = "Doanh thu";

                            int rowSP = startRowSP + 1;
                            if (topSP != null)
                            {
                                foreach (var sp in topSP)
                                {
                                    sheet.Cells[$"A{rowSP}"].Value = sp.TenSP;
                                    sheet.Cells[$"B{rowSP}"].Value = sp.TongSoLuong;
                                    sheet.Cells[$"C{rowSP}"].Value = sp.TongDoanhThu;
                                    sheet.Cells[$"C{rowSP}"].Style.Numberformat.Format = "#,##0 ₫";
                                    rowSP++;
                                }
                            }

                            if (rowSP > startRowSP + 1)
                            {
                                var tableRangeSP = sheet.Cells[$"A{startRowSP}:C{rowSP - 1}"];
                                var tableSP = sheet.Tables.Add(tableRangeSP, "BangSanPhamBanChay");
                                tableSP.TableStyle = OfficeOpenXml.Table.TableStyles.Medium14;
                            }

                            sheet.Cells.AutoFitColumns();

                            FileInfo excelFile = new FileInfo(sfd.FileName);
                            excel.SaveAs(excelFile);
                            
                            MessageBox.Show("Xuất file Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi xuất Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public static void ExportBaoCaoQuyExcel(int quy, int nam, BaoCaoDoanhThu dtCurrent, BaoCaoDoanhThu dtPrev, List<DoanhThuThang> dttList, List<SanPhamBanChay> topSP)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx", FileName = $"BaoCaoQuy_{quy}_{nam}.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (ExcelPackage excel = new ExcelPackage())
                        {
                            var sheet = excel.Workbook.Worksheets.Add("Dữ liệu Báo Cáo");
                            sheet.View.ShowGridLines = true; // Show gridlines for a data-centric feel

                            // Title
                            sheet.Cells["A1"].Value = $"BÁO CÁO DOANH THU QUÝ {quy} NĂM {nam}";
                            sheet.Cells["A1"].Style.Font.Bold = true;
                            sheet.Cells["A1"].Style.Font.Size = 14;

                            sheet.Cells["A2"].Value = $"Ngày lập báo cáo: {DateTime.Now:dd/MM/yyyy HH:mm}";
                            sheet.Cells["A2"].Style.Font.Italic = true;

                            // Calculate Extra Metrics
                            int tongDon = 0;
                            foreach (var dt in dttList) tongDon += dt.SoDon;
                            decimal tongDT = dtCurrent != null ? dtCurrent.TongDoanhThu : 0;
                            decimal tbDon = tongDon > 0 ? tongDT / tongDon : 0;
                            
                            string compare = "Chưa có dữ liệu";
                            if (dtCurrent != null && dtPrev != null && dtPrev.TongDoanhThu > 0)
                            {
                                decimal phanTram = ((tongDT - dtPrev.TongDoanhThu) / dtPrev.TongDoanhThu) * 100;
                                compare = $"{(phanTram >= 0 ? "Tăng" : "Giảm")} {Math.Abs(phanTram):N1}%";
                            }

                            // Summary Data
                            sheet.Cells["A4"].Value = "TỔNG QUAN";
                            sheet.Cells["A4"].Style.Font.Bold = true;
                            
                            sheet.Cells["A5"].Value = "Tổng doanh thu:";
                            sheet.Cells["B5"].Value = tongDT;
                            sheet.Cells["B5"].Style.Numberformat.Format = "#,##0 ₫";
                            
                            sheet.Cells["A6"].Value = "So với quý trước:";
                            sheet.Cells["B6"].Value = compare;
                            
                            sheet.Cells["A7"].Value = "Tổng số đơn hàng:";
                            sheet.Cells["B7"].Value = tongDon;
                            
                            sheet.Cells["A8"].Value = "Giá trị TB / đơn:";
                            sheet.Cells["B8"].Value = tbDon;
                            sheet.Cells["B8"].Style.Numberformat.Format = "#,##0 ₫";

                            // Main Data Table
                            int startRow = 11;
                            sheet.Cells[$"A{startRow}"].Value = "Tháng";
                            sheet.Cells[$"B{startRow}"].Value = "Số lượng đơn";
                            sheet.Cells[$"C{startRow}"].Value = "Tỷ trọng đơn";
                            sheet.Cells[$"D{startRow}"].Value = "Doanh thu";
                            sheet.Cells[$"E{startRow}"].Value = "Tỷ trọng DT";
                            sheet.Cells[$"F{startRow}"].Value = "Trung bình/Đơn";

                            int row = startRow + 1;
                            foreach (var dt in dttList)
                            {
                                sheet.Cells[$"A{row}"].Value = dt.Thang; // Just number for better filtering
                                sheet.Cells[$"B{row}"].Value = dt.SoDon;
                                sheet.Cells[$"C{row}"].Value = tongDon > 0 ? (double)dt.SoDon / tongDon : 0;
                                sheet.Cells[$"C{row}"].Style.Numberformat.Format = "0.0%";
                                
                                sheet.Cells[$"D{row}"].Value = dt.DoanhThu;
                                sheet.Cells[$"D{row}"].Style.Numberformat.Format = "#,##0 ₫";
                                
                                sheet.Cells[$"E{row}"].Value = tongDT > 0 ? (double)dt.DoanhThu / (double)tongDT : 0;
                                sheet.Cells[$"E{row}"].Style.Numberformat.Format = "0.0%";
                                
                                sheet.Cells[$"F{row}"].Value = dt.SoDon > 0 ? dt.DoanhThu / dt.SoDon : 0;
                                sheet.Cells[$"F{row}"].Style.Numberformat.Format = "#,##0 ₫";
                                row++;
                            }

                            // Convert to Excel Table (ListObject)
                            if (row > startRow + 1)
                            {
                                var tableRange = sheet.Cells[$"A{startRow}:F{row - 1}"];
                                var table = sheet.Tables.Add(tableRange, "BangDoanhThuThang");
                                table.TableStyle = OfficeOpenXml.Table.TableStyles.Medium2; // Standard Excel Table style
                            }

                            // Second Table: Product Details
                            int startRowSP = row + 2;
                            sheet.Cells[$"A{startRowSP}"].Value = "Tên Sản phẩm";
                            sheet.Cells[$"B{startRowSP}"].Value = "Số lượng bán";
                            sheet.Cells[$"C{startRowSP}"].Value = "Doanh thu";

                            int rowSP = startRowSP + 1;
                            if (topSP != null)
                            {
                                foreach (var sp in topSP)
                                {
                                    sheet.Cells[$"A{rowSP}"].Value = sp.TenSP;
                                    sheet.Cells[$"B{rowSP}"].Value = sp.TongSoLuong;
                                    sheet.Cells[$"C{rowSP}"].Value = sp.TongDoanhThu;
                                    sheet.Cells[$"C{rowSP}"].Style.Numberformat.Format = "#,##0 ₫";
                                    rowSP++;
                                }
                            }

                            if (rowSP > startRowSP + 1)
                            {
                                var tableRangeSP = sheet.Cells[$"A{startRowSP}:C{rowSP - 1}"];
                                var tableSP = sheet.Tables.Add(tableRangeSP, "BangSanPhamBanChay");
                                tableSP.TableStyle = OfficeOpenXml.Table.TableStyles.Medium14; // Give it a different color flavor
                            }

                            sheet.Cells.AutoFitColumns();

                            FileInfo excelFile = new FileInfo(sfd.FileName);
                            excel.SaveAs(excelFile);
                            
                            MessageBox.Show("Xuất file Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi xuất Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public static void ExportBaoCaoSanPhamExcel(int? thang, int? nam, List<SanPhamBanChay> dsBanChay, List<SanPhamBanChay> dsE)
        {
            string timeStr = thang.HasValue ? $"Tháng {thang}/{nam}" : (nam.HasValue ? $"Năm {nam}" : "Tất cả thời gian");
            
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx", FileName = $"BaoCao_SanPham_{DateTime.Now:ddMMyyyy}.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (ExcelPackage package = new ExcelPackage())
                        {
                            ExcelWorksheet ws = package.Workbook.Worksheets.Add("BaoCaoSP");

                            // Header
                            ws.Cells["A1:D1"].Merge = true;
                            ws.Cells["A1"].Value = "BÁO CÁO SẢN PHẨM";
                            ws.Cells["A1"].Style.Font.Size = 16;
                            ws.Cells["A1"].Style.Font.Bold = true;
                            ws.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                            ws.Cells["A2:D2"].Merge = true;
                            ws.Cells["A2"].Value = timeStr;
                            ws.Cells["A2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                            int row = 4;

                            // 1. Sản phẩm bán chạy
                            if (dsBanChay != null && dsBanChay.Count > 0)
                            {
                                ws.Cells[row, 1, row, 4].Merge = true;
                                ws.Cells[row, 1].Value = "TOP SẢN PHẨM BÁN CHẠY";
                                ws.Cells[row, 1].Style.Font.Bold = true;
                                ws.Cells[row, 1].Style.Font.Size = 14;
                                ws.Cells[row, 1].Style.Font.Color.SetColor(Color.FromArgb(232, 57, 77));
                                row++;

                                // Table header
                                ws.Cells[row, 1].Value = "STT";
                                ws.Cells[row, 2].Value = "Tên sản phẩm";
                                ws.Cells[row, 3].Value = "Số lượng bán";
                                ws.Cells[row, 4].Value = "Doanh thu";

                                using (var range = ws.Cells[row, 1, row, 4])
                                {
                                    range.Style.Font.Bold = true;
                                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    range.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(45, 106, 79));
                                    range.Style.Font.Color.SetColor(Color.White);
                                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                }
                                row++;

                                for (int i = 0; i < dsBanChay.Count; i++)
                                {
                                    ws.Cells[row, 1].Value = i + 1;
                                    ws.Cells[row, 2].Value = dsBanChay[i].TenSP;
                                    ws.Cells[row, 3].Value = dsBanChay[i].TongSoLuong;
                                    ws.Cells[row, 4].Value = dsBanChay[i].TongDoanhThu;
                                    ws.Cells[row, 4].Style.Numberformat.Format = "#,##0\"đ\"";

                                    using (var range = ws.Cells[row, 1, row, 4])
                                    {
                                        range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                        range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                        range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                        range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    }
                                    row++;
                                }
                                row += 2; // Empty rows between sections
                            }

                            // 2. Sản phẩm ế
                            if (dsE != null && dsE.Count > 0)
                            {
                                ws.Cells[row, 1, row, 4].Merge = true;
                                ws.Cells[row, 1].Value = "SẢN PHẨM Ế (DƯỚI 15 SP/THÁNG)";
                                ws.Cells[row, 1].Style.Font.Bold = true;
                                ws.Cells[row, 1].Style.Font.Size = 14;
                                ws.Cells[row, 1].Style.Font.Color.SetColor(Color.FromArgb(232, 57, 77));
                                row++;

                                // Table header
                                ws.Cells[row, 1].Value = "STT";
                                ws.Cells[row, 2].Value = "Tên sản phẩm";
                                ws.Cells[row, 3].Value = "Số lượng bán";
                                ws.Cells[row, 4].Value = "Doanh thu";

                                using (var range = ws.Cells[row, 1, row, 4])
                                {
                                    range.Style.Font.Bold = true;
                                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    range.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(45, 106, 79));
                                    range.Style.Font.Color.SetColor(Color.White);
                                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                }
                                row++;

                                for (int i = 0; i < dsE.Count; i++)
                                {
                                    ws.Cells[row, 1].Value = i + 1;
                                    ws.Cells[row, 2].Value = dsE[i].TenSP;
                                    ws.Cells[row, 3].Value = dsE[i].TongSoLuong;
                                    ws.Cells[row, 4].Value = dsE[i].TongDoanhThu;
                                    ws.Cells[row, 4].Style.Numberformat.Format = "#,##0\"đ\"";

                                    using (var range = ws.Cells[row, 1, row, 4])
                                    {
                                        range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                        range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                        range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                        range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    }
                                    row++;
                                }
                            }

                            ws.Column(1).Width = 10;
                            ws.Column(2).Width = 40;
                            ws.Column(3).Width = 15;
                            ws.Column(4).Width = 20;

                            FileInfo fi = new FileInfo(sfd.FileName);
                            package.SaveAs(fi);
                        }
                        MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi xuất Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
