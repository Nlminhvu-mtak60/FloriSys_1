using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using FloriSys.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace FloriSys.Services
{
    /// <summary>
    /// Utility class to export reports to PDF using iTextSharp.
    /// Supports: Báo cáo Ngày, Tháng, Quý.
    /// </summary>
    public static class ReportPdfHelper
    {
        // ============================================================
        // FONTS — Sử dụng font Arial Unicode MS có sẵn trên Windows để hỗ trợ tiếng Việt
        // ============================================================
        private static BaseFont _baseFont;
        private static iTextSharp.text.Font _fontTitle;
        private static iTextSharp.text.Font _fontSubTitle;
        private static iTextSharp.text.Font _fontNormal;
        private static iTextSharp.text.Font _fontBold;
        private static iTextSharp.text.Font _fontSmall;
        private static iTextSharp.text.Font _fontKPIValue;
        private static iTextSharp.text.Font _fontHeader;

        static ReportPdfHelper()
        {
            // Tìm font hỗ trợ tiếng Việt
            string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
            if (!File.Exists(fontPath))
                fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "tahoma.ttf");

            _baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

            _fontTitle = new iTextSharp.text.Font(_baseFont, 18, iTextSharp.text.Font.BOLD, new BaseColor(31, 41, 55));
            _fontSubTitle = new iTextSharp.text.Font(_baseFont, 12, iTextSharp.text.Font.BOLD, new BaseColor(107, 114, 128));
            _fontNormal = new iTextSharp.text.Font(_baseFont, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            _fontBold = new iTextSharp.text.Font(_baseFont, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            _fontSmall = new iTextSharp.text.Font(_baseFont, 8, iTextSharp.text.Font.NORMAL, new BaseColor(107, 114, 128));
            _fontKPIValue = new iTextSharp.text.Font(_baseFont, 16, iTextSharp.text.Font.BOLD, new BaseColor(232, 57, 77));
            _fontHeader = new iTextSharp.text.Font(_baseFont, 10, iTextSharp.text.Font.BOLD, BaseColor.WHITE);
        }

        // ============================================================
        // PUBLIC METHODS
        // ============================================================

        /// <summary>
        /// Xuất báo cáo Ngày ra PDF
        /// </summary>
        public static void ExportBaoCaoNgay(DateTime ngay, BaoCaoDoanhThu dt,
            List<TopSanPhamNgay> topSP, int slSP, MemoryStream chartStream = null)
        {
            string defaultName = $"BaoCao_Ngay_{ngay:yyyyMMdd}.pdf";
            string filePath = ShowSaveDialog(defaultName);
            if (string.IsNullOrEmpty(filePath)) return;

            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    Document doc = new Document(PageSize.A4, 40, 40, 40, 40);
                    PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                    doc.Open();

                    // Header
                    AddReportHeader(doc, "BÁO CÁO DOANH THU NGÀY", ngay.ToString("dddd, dd/MM/yyyy"));

                    // KPI Section
                    PdfPTable kpiTable = new PdfPTable(3);
                    kpiTable.WidthPercentage = 100;
                    kpiTable.SpacingBefore = 15;
                    kpiTable.SpacingAfter = 15;

                    AddKPICell(kpiTable, "TỔNG ĐƠN HÀNG", dt != null ? dt.TongDon.ToString() : "0");
                    AddKPICell(kpiTable, "DOANH THU", dt != null ? dt.TongDoanhThu.ToString("N0") + "đ" : "0đ");
                    AddKPICell(kpiTable, "SỐ LƯỢNG SP BÁN", slSP.ToString());
                    doc.Add(kpiTable);

                    // Top Products Table
                    if (topSP != null && topSP.Count > 0)
                    {
                        AddSectionTitle(doc, "TOP SẢN PHẨM BÁN CHẠY");

                        PdfPTable table = new PdfPTable(3);
                        table.WidthPercentage = 100;
                        table.SetWidths(new float[] { 50, 20, 30 });
                        table.SpacingBefore = 5;

                        AddTableHeader(table, "Tên sản phẩm");
                        AddTableHeader(table, "SL bán");
                        AddTableHeader(table, "Doanh thu");

                        foreach (var sp in topSP)
                        {
                            AddTableCell(table, sp.TenSP);
                            AddTableCell(table, sp.SLBan.ToString(), Element.ALIGN_CENTER);
                            AddTableCell(table, sp.DoanhThu.ToString("N0") + "đ", Element.ALIGN_RIGHT);
                        }
                        doc.Add(table);
                    }

                    // Chart image
                    AddChartImage(doc, chartStream);

                    // Footer
                    AddReportFooter(doc);

                    doc.Close();
                }

                ShowSuccessMessage(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất PDF: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xuất báo cáo Tháng ra PDF
        /// </summary>
        public static void ExportBaoCaoThang(int thang, int nam, BaoCaoDoanhThu dt,
            BaoCaoDoanhThu dtTruoc, List<SanPhamBanChay> topSP, MemoryStream chartStream = null)
        {
            string defaultName = $"BaoCao_Thang{thang}_{nam}.pdf";
            string filePath = ShowSaveDialog(defaultName);
            if (string.IsNullOrEmpty(filePath)) return;

            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    Document doc = new Document(PageSize.A4, 40, 40, 40, 40);
                    PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                    doc.Open();

                    // Header
                    AddReportHeader(doc, "BÁO CÁO DOANH THU THÁNG", $"Tháng {thang}/{nam}");

                    // KPI Section
                    PdfPTable kpiTable = new PdfPTable(2);
                    kpiTable.WidthPercentage = 100;
                    kpiTable.SpacingBefore = 15;
                    kpiTable.SpacingAfter = 10;

                    AddKPICell(kpiTable, "TỔNG ĐƠN HÀNG", dt != null ? dt.TongDon.ToString() : "0");
                    AddKPICell(kpiTable, "DOANH THU", dt != null ? dt.TongDoanhThu.ToString("N0") + "đ" : "0đ");
                    doc.Add(kpiTable);

                    // So sánh tháng trước
                    if (dt != null && dtTruoc != null && dtTruoc.TongDoanhThu > 0)
                    {
                        decimal phanTram = ((dt.TongDoanhThu - dtTruoc.TongDoanhThu) / dtTruoc.TongDoanhThu) * 100;
                        string soSanh = (phanTram >= 0 ? "▲ +" : "▼ ") + phanTram.ToString("N1") + "% so với tháng trước";
                        BaseColor color = phanTram >= 0 ? new BaseColor(45, 106, 79) : new BaseColor(220, 38, 38);
                        var pCompare = new Paragraph(soSanh, new iTextSharp.text.Font(_baseFont, 10, iTextSharp.text.Font.ITALIC, color));
                        pCompare.SpacingAfter = 15;
                        doc.Add(pCompare);
                    }

                    // Top Products Table
                    if (topSP != null && topSP.Count > 0)
                    {
                        AddSectionTitle(doc, "TOP SẢN PHẨM BÁN CHẠY");

                        PdfPTable table = new PdfPTable(3);
                        table.WidthPercentage = 100;
                        table.SetWidths(new float[] { 50, 20, 30 });
                        table.SpacingBefore = 5;

                        AddTableHeader(table, "Sản phẩm");
                        AddTableHeader(table, "SL bán");
                        AddTableHeader(table, "Doanh thu");

                        foreach (var sp in topSP)
                        {
                            AddTableCell(table, sp.TenSP);
                            AddTableCell(table, sp.TongSoLuong.ToString(), Element.ALIGN_CENTER);
                            AddTableCell(table, sp.TongDoanhThu.ToString("N0") + "đ", Element.ALIGN_RIGHT);
                        }
                        doc.Add(table);
                    }

                    // Chart image
                    AddChartImage(doc, chartStream);

                    // Footer
                    AddReportFooter(doc);

                    doc.Close();
                }

                ShowSuccessMessage(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất PDF: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xuất báo cáo Quý ra PDF
        /// </summary>
        public static void ExportBaoCaoQuy(int quy, int nam, BaoCaoDoanhThu dt,
            BaoCaoDoanhThu dtTruoc, List<DoanhThuThang> dsThang, List<SanPhamBanChay> topSP,
            MemoryStream chartStream = null)
        {
            string defaultName = $"BaoCao_Quy{quy}_{nam}.pdf";
            string filePath = ShowSaveDialog(defaultName);
            if (string.IsNullOrEmpty(filePath)) return;

            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    Document doc = new Document(PageSize.A4, 40, 40, 40, 40);
                    PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                    doc.Open();

                    // Header
                    int thangDau = (quy - 1) * 3 + 1;
                    int thangCuoi = quy * 3;
                    AddReportHeader(doc, $"BÁO CÁO DOANH THU QUÝ {quy}",
                        $"Từ tháng {thangDau} đến tháng {thangCuoi}/{nam}");

                    // KPI Section
                    PdfPTable kpiTable = new PdfPTable(3);
                    kpiTable.WidthPercentage = 100;
                    kpiTable.SpacingBefore = 15;
                    kpiTable.SpacingAfter = 10;

                    AddKPICell(kpiTable, "TỔNG DOANH THU", dt != null ? dt.TongDoanhThu.ToString("N0") + "đ" : "0đ");
                    AddKPICell(kpiTable, "TỔNG ĐƠN HÀNG", dt != null ? dt.TongDon.ToString() : "0");
                    decimal tbThang = dt != null ? dt.TongDoanhThu / 3 : 0;
                    AddKPICell(kpiTable, "TRUNG BÌNH/THÁNG", tbThang.ToString("N0") + "đ");
                    doc.Add(kpiTable);

                    // So sánh quý trước
                    if (dt != null && dtTruoc != null && dtTruoc.TongDoanhThu > 0)
                    {
                        decimal phanTram = ((dt.TongDoanhThu - dtTruoc.TongDoanhThu) / dtTruoc.TongDoanhThu) * 100;
                        int quyTruoc = quy == 1 ? 4 : quy - 1;
                        int namTruoc = quy == 1 ? nam - 1 : nam;
                        string soSanh = (phanTram >= 0 ? "▲ +" : "▼ ") + phanTram.ToString("N1") +
                            $"% so với quý trước (Q{quyTruoc}/{namTruoc})";
                        BaseColor color = phanTram >= 0 ? new BaseColor(45, 106, 79) : new BaseColor(220, 38, 38);
                        var pCompare = new Paragraph(soSanh, new iTextSharp.text.Font(_baseFont, 10, iTextSharp.text.Font.ITALIC, color));
                        pCompare.SpacingAfter = 15;
                        doc.Add(pCompare);
                    }

                    // Bảng doanh thu theo tháng
                    if (dsThang != null && dsThang.Count > 0)
                    {
                        AddSectionTitle(doc, "DOANH THU THEO THÁNG");

                        PdfPTable table = new PdfPTable(3);
                        table.WidthPercentage = 100;
                        table.SetWidths(new float[] { 30, 40, 30 });
                        table.SpacingBefore = 5;
                        table.SpacingAfter = 15;

                        AddTableHeader(table, "Tháng");
                        AddTableHeader(table, "Doanh thu");
                        AddTableHeader(table, "Số đơn");

                        foreach (var item in dsThang)
                        {
                            AddTableCell(table, "Tháng " + item.Thang, Element.ALIGN_CENTER);
                            AddTableCell(table, item.DoanhThu.ToString("N0") + "đ", Element.ALIGN_RIGHT);
                            AddTableCell(table, item.SoDon.ToString(), Element.ALIGN_CENTER);
                        }
                        doc.Add(table);
                    }

                    // Chart image
                    AddChartImage(doc, chartStream);

                    // Top Products Table
                    if (topSP != null && topSP.Count > 0)
                    {
                        AddSectionTitle(doc, "TOP SẢN PHẨM BÁN CHẠY TRONG QUÝ");

                        PdfPTable table = new PdfPTable(3);
                        table.WidthPercentage = 100;
                        table.SetWidths(new float[] { 50, 20, 30 });
                        table.SpacingBefore = 5;

                        AddTableHeader(table, "Sản phẩm");
                        AddTableHeader(table, "SL bán");
                        AddTableHeader(table, "Doanh thu");

                        foreach (var sp in topSP)
                        {
                            AddTableCell(table, sp.TenSP);
                            AddTableCell(table, sp.TongSoLuong.ToString(), Element.ALIGN_CENTER);
                            AddTableCell(table, sp.TongDoanhThu.ToString("N0") + "đ", Element.ALIGN_RIGHT);
                        }
                        doc.Add(table);
                    }

                    // Footer
                    AddReportFooter(doc);

                    doc.Close();
                }

                ShowSuccessMessage(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất PDF: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ============================================================
        // PRIVATE HELPERS
        // ============================================================

        private static string ShowSaveDialog(string defaultName)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PDF File|*.pdf";
                sfd.FileName = defaultName;
                sfd.Title = "Chọn nơi lưu báo cáo PDF";
                if (sfd.ShowDialog() == DialogResult.OK)
                    return sfd.FileName;
            }
            return null;
        }

        private static void AddReportHeader(Document doc, string title, string subtitle)
        {
            // Store name
            var pStoreName = new Paragraph("🌸 FLORISYS — CỬA HÀNG HOA", _fontSubTitle);
            pStoreName.Alignment = Element.ALIGN_CENTER;
            doc.Add(pStoreName);

            // Title
            var pTitle = new Paragraph(title, _fontTitle);
            pTitle.Alignment = Element.ALIGN_CENTER;
            pTitle.SpacingBefore = 5;
            doc.Add(pTitle);

            // Subtitle (date/period)
            var pSub = new Paragraph(subtitle,
                new iTextSharp.text.Font(_baseFont, 11, iTextSharp.text.Font.NORMAL, new BaseColor(107, 114, 128)));
            pSub.Alignment = Element.ALIGN_CENTER;
            pSub.SpacingAfter = 5;
            doc.Add(pSub);

            // Separator line
            var separator = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(
                1f, 100f, new BaseColor(229, 231, 235), Element.ALIGN_CENTER, -1)));
            doc.Add(separator);
        }

        private static void AddSectionTitle(Document doc, string title)
        {
            var p = new Paragraph(title, _fontSubTitle);
            p.SpacingBefore = 15;
            p.SpacingAfter = 5;
            doc.Add(p);
        }

        private static void AddKPICell(PdfPTable table, string label, string value)
        {
            PdfPCell cell = new PdfPCell();
            cell.Border = PdfPCell.NO_BORDER;
            cell.Padding = 10;
            cell.BackgroundColor = new BaseColor(249, 250, 251);

            var pLabel = new Paragraph(label, _fontSmall);
            pLabel.Alignment = Element.ALIGN_CENTER;
            cell.AddElement(pLabel);

            var pValue = new Paragraph(value, _fontKPIValue);
            pValue.Alignment = Element.ALIGN_CENTER;
            cell.AddElement(pValue);

            table.AddCell(cell);
        }

        private static void AddTableHeader(PdfPTable table, string text)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, _fontHeader));
            cell.BackgroundColor = new BaseColor(232, 57, 77);
            cell.Padding = 8;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);
        }

        private static void AddTableCell(PdfPTable table, string text, int align = Element.ALIGN_LEFT)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text ?? "", _fontNormal));
            cell.Padding = 6;
            cell.HorizontalAlignment = align;
            cell.BorderColor = new BaseColor(229, 231, 235);
            table.AddCell(cell);
        }

        private static void AddChartImage(Document doc, MemoryStream chartStream)
        {
            if (chartStream != null && chartStream.Length > 0)
            {
                try
                {
                    chartStream.Position = 0;
                    iTextSharp.text.Image chartImg = iTextSharp.text.Image.GetInstance(chartStream.ToArray());
                    chartImg.ScaleToFit(doc.PageSize.Width - 80, 250);
                    chartImg.Alignment = Element.ALIGN_CENTER;
                    chartImg.SpacingBefore = 15;
                    chartImg.SpacingAfter = 10;
                    doc.Add(chartImg);
                }
                catch { /* Ignore chart render errors */ }
            }
        }

        private static void AddReportFooter(Document doc)
        {
            var separator = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(
                0.5f, 100f, new BaseColor(229, 231, 235), Element.ALIGN_CENTER, -1)));
            separator.SpacingBefore = 20;
            doc.Add(separator);

            var pFooter = new Paragraph(
                $"Xuất ngày: {DateTime.Now:dd/MM/yyyy HH:mm}  |  FloriSys — Hệ thống quản lý cửa hàng hoa",
                _fontSmall);
            pFooter.Alignment = Element.ALIGN_CENTER;
            pFooter.SpacingBefore = 5;
            doc.Add(pFooter);
        }

        public static void ExportBaoCaoSanPham(int? thang, int? nam, List<SanPhamBanChay> dsBanChay, List<SanPhamBanChay> dsE, MemoryStream chartStream = null)
        {
            string timeStr = thang.HasValue ? $"Tháng {thang}/{nam}" : (nam.HasValue ? $"Năm {nam}" : "Tất cả thời gian");
            
            string defaultName = $"BaoCao_SanPham_{DateTime.Now:yyyyMMdd}.pdf";
            string filePath = ShowSaveDialog(defaultName);
            if (string.IsNullOrEmpty(filePath)) return;

            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    Document doc = new Document(PageSize.A4, 40, 40, 40, 40);
                    PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                    doc.Open();

                    // Header
                    AddReportHeader(doc, "BÁO CÁO SẢN PHẨM", timeStr);

                    // Danh sách sản phẩm Bán Chạy
                    if (dsBanChay != null && dsBanChay.Count > 0)
                    {
                        AddSectionTitle(doc, "TOP SẢN PHẨM BÁN CHẠY");

                        PdfPTable table = new PdfPTable(3);
                        table.WidthPercentage = 100;
                        table.SetWidths(new float[] { 50f, 20f, 30f });
                        table.SpacingBefore = 10;
                        table.SpacingAfter = 15;

                        AddTableHeader(table, "Tên sản phẩm");
                        AddTableHeader(table, "SL bán");
                        AddTableHeader(table, "Doanh thu");

                        foreach (var sp in dsBanChay)
                        {
                            AddTableCell(table, sp.TenSP);
                            AddTableCell(table, sp.TongSoLuong.ToString(), Element.ALIGN_CENTER);
                            AddTableCell(table, sp.TongDoanhThu.ToString("N0") + "đ", Element.ALIGN_RIGHT);
                        }
                        doc.Add(table);
                    }
                    
                    // Danh sách sản phẩm Ế
                    if (dsE != null && dsE.Count > 0)
                    {
                        AddSectionTitle(doc, "SẢN PHẨM Ế (DƯỚI 15 SP/THÁNG)");

                        PdfPTable table = new PdfPTable(3);
                        table.WidthPercentage = 100;
                        table.SetWidths(new float[] { 50f, 20f, 30f });
                        table.SpacingBefore = 10;
                        table.SpacingAfter = 15;

                        AddTableHeader(table, "Tên sản phẩm");
                        AddTableHeader(table, "SL bán");
                        AddTableHeader(table, "Doanh thu");

                        foreach (var sp in dsE)
                        {
                            AddTableCell(table, sp.TenSP);
                            AddTableCell(table, sp.TongSoLuong.ToString(), Element.ALIGN_CENTER);
                            AddTableCell(table, sp.TongDoanhThu.ToString("N0") + "đ", Element.ALIGN_RIGHT);
                        }
                        doc.Add(table);
                    }

                    // Chart image
                    if (chartStream != null && chartStream.Length > 0)
                    {
                        AddChartImage(doc, chartStream);
                    }

                    // Footer
                    AddReportFooter(doc);
                    doc.Close();
                }

                ShowSuccessMessage(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất PDF: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void ShowSuccessMessage(string filePath)
        {
            DialogResult result = MessageBox.Show(
                "Xuất PDF thành công!\nFile đã được lưu tại:\n" + filePath + "\n\nBạn có muốn mở file không?",
                "Thành công", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(filePath);
            }
        }
    }
}
