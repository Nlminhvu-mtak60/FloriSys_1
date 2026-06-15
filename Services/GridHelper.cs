using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FloriSys.Services
{
    public static class GridHelper
    {
        /// <summary>
        /// Định dạng DataGridView theo danh sách cột hiển thị, tên cột Việt hóa và định dạng dữ liệu.
        /// </summary>
        public static void FormatGrid(
            DataGridView dgv, 
            List<string> visibleCols, 
            Dictionary<string, string> headers, 
            Dictionary<string, string> formats = null)
        {
            if (dgv == null || dgv.Columns.Count == 0) return;

            // Thiết lập các thuộc tính mặc định chuẩn cho lưới
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;

            // Ẩn các cột không cần thiết
            if (visibleCols != null)
            {
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    col.Visible = visibleCols.Contains(col.Name);
                }
            }

            // Đổi tên tiêu đề cột
            if (headers != null)
            {
                foreach (var kvp in headers)
                {
                    if (dgv.Columns.Contains(kvp.Key))
                    {
                        dgv.Columns[kvp.Key].HeaderText = kvp.Value;
                    }
                }
            }

            // Cài đặt định dạng hiển thị số/ngày tháng
            if (formats != null)
            {
                foreach (var kvp in formats)
                {
                    if (dgv.Columns.Contains(kvp.Key))
                    {
                        dgv.Columns[kvp.Key].DefaultCellStyle.Format = kvp.Value;
                    }
                }
            }
        }

        /// <summary>
        /// Tạo nhanh cột TextBox nếu chưa tồn tại (tiện dùng cho cột tỷ trọng tự tính).
        /// </summary>
        public static void EnsureColumnExists(DataGridView dgv, string colName, string headerText, int width = 120)
        {
            if (dgv == null) return;
            if (!dgv.Columns.Contains(colName))
            {
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn
                {
                    Name = colName,
                    HeaderText = headerText,
                    Width = width
                };
                dgv.Columns.Add(col);
            }
        }
    }
}
