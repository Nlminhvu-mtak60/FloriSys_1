using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using FloriSys.DataAccess;
using FloriSys.Shared;
using FloriSys.Models;

namespace FloriSys._6_BaoCao
{
    public partial class ucBaoCaoNhanVien : BaseUserControl
    {
        private readonly BaoCaoRepository _bcRepo = new BaoCaoRepository();
        public ucBaoCaoNhanVien()
        {
            InitializeComponent();
        }

        private void ucBaoCaoNhanVien_Load(object sender, EventArgs e)
        {
            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;
            for (int i = 1; i <= 12; i++) cboThang.Items.Add(i);
            for (int i = currentYear - 1; i <= currentYear; i++) cboNam.Items.Add(i);
            cboThang.SelectedItem = currentMonth;
            cboNam.SelectedItem = currentYear;
            LoadData();
        }

        public override void LoadData()
        {
            try
            {
                int? thang = cboThang.SelectedItem as int?;
                int? nam = cboNam.SelectedItem as int?;
                List<HieuSuatNhanVien> dsNV = _bcRepo.HieuSuatNhanVien(thang, nam);
                dgvNhanVien.DataSource = dsNV;

                if (dgvNhanVien.Columns.Count > 0)
                {
                    var visibleCols = new List<string> { "HoTen", "SoDonTao", "TongDoanhThu" };
                    foreach (DataGridViewColumn col in dgvNhanVien.Columns) { if (!visibleCols.Contains(col.Name)) col.Visible = false; }

                    dgvNhanVien.Columns["HoTen"].HeaderText = "Họ tên nhân viên";
                    dgvNhanVien.Columns["SoDonTao"].HeaderText = "Số đơn hàng";
                    dgvNhanVien.Columns["TongDoanhThu"].HeaderText = "Tổng doanh thu";
                    dgvNhanVien.Columns["TongDoanhThu"].DefaultCellStyle.Format = "N0";
                }

                // Draw bar chart
                DrawBarChart(dsNV);
            }
            catch (Exception ex)
            {
                ShowError("Lỗi tải hiệu suất nhân viên: " + ex.Message);
            }
        }

        private void DrawBarChart(List<HieuSuatNhanVien> dsNV)
        {
            chartHieuSuat.Series["Doanh thu"].Points.Clear();
            chartHieuSuat.Series["Số đơn"].Points.Clear();

            if (dsNV.Count == 0) return;

            int count = 0;
            foreach (HieuSuatNhanVien nv in dsNV)
            {
                if (count >= 8) break;
                string name = nv.HoTen;
                if (name.Length > 12) name = name.Substring(0, 12) + "…";
                chartHieuSuat.Series["Doanh thu"].Points.AddXY(name, nv.TongDoanhThu);
                chartHieuSuat.Series["Số đơn"].Points.AddXY(name, nv.SoDonTao * 100000); // scale for visibility
                count++;
            }
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnXuatPDF_Click(object sender, EventArgs e)
        {
            int? thang = cboThang.SelectedItem as int?;
            int? nam = cboNam.SelectedItem as int?;
            List<HieuSuatNhanVien> dsNV = _bcRepo.HieuSuatNhanVien(thang, nam);

            System.IO.MemoryStream chartStream = null;
            if (chartHieuSuat.Series["Doanh thu"].Points.Count > 0)
            {
                chartStream = new System.IO.MemoryStream();
                chartHieuSuat.SaveImage(chartStream, ChartImageFormat.Png);
            }

            FloriSys.Services.ReportPdfHelper.ExportBaoCaoHieuSuatNhanVien(thang, nam, dsNV, "Quản trị viên", chartStream);
        }
    }
}
