# TÀI LIỆU HƯỚNG DẪN BẢO VỆ ĐỒ ÁN CHI TIẾT (FLORISYS)

Tài liệu này bao gồm hai phần chính:
* **PHẦN I:** Hướng dẫn code chi tiết cho các dạng câu hỏi thực hành (Copy-Paste trực tiếp).
* **PHẦN II:** Hướng dẫn thuyết trình nghiệp vụ và trả lời phản biện với giảng viên để đạt điểm tối đa.

---

# PHẦN I: HƯỚNG DẪN CODE CHI TIẾT CÁC DẠNG CÂU HỎI THỰC HÀNH

<a name="dang-1-them-sua-thuoc-tinh-moi-3-tier-csdl"></a>
## 🛠️ Dạng 1: Thêm/Sửa thuộc tính mới (3-Tier & CSDL)
*Thầy yêu cầu: "Hãy thêm thuộc tính **Email** vào Khách hàng (hoặc **Đơn vị tính** cho Sản phẩm) và hiển thị lên giao diện."*

### Bước 1: Chạy câu lệnh SQL (Thêm cột vào CSDL)
Mở SSMS hoặc chạy Script bổ sung cột:
```sql
ALTER TABLE KHACH_HANG ADD Email NVARCHAR(100) NULL;
-- Hoặc đối với sản phẩm:
-- ALTER TABLE SAN_PHAM ADD DonViTinh NVARCHAR(50) DEFAULT N'Cành';
```

### Bước 2: Sửa Model trong C#
Mở file [KhachHang.cs](file:///D:/Learning/C%23/BAI_TAP_LON/FloriSys/Models/KhachHang.cs) và thêm thuộc tính:
```csharp
public string Email { get; set; }
```

### Bước 3: Sửa DataAccess (Repository)
Mở file [KhachHangRepository.cs](file:///D:/Learning/C%23/BAI_TAP_LON/FloriSys/DataAccess/KhachHangRepository.cs):
* **Sửa hàm `LayDanhSach`**: Thêm cột `kh.Email` vào câu SELECT và `kh.Email` vào phần `GROUP BY`.
* **Sửa hàm `ThemKhachHang`**:
```csharp
string sql = @"INSERT INTO KHACH_HANG (MaKH, HoTen, SoDienThoai, DiaChi, Email) 
              VALUES (@MaKH, @HoTen, @SDT, @DiaChi, @Email)";
// Thêm tham số: NullableParam("@Email", kh.Email)
```
* **Sửa hàm `CapNhatKhachHang`**:
```csharp
string sql = @"UPDATE KHACH_HANG SET HoTen=@HoTen, SoDienThoai=@SDT, DiaChi=@DiaChi, Email=@Email 
              WHERE MaKH=@MaKH";
// Thêm tham số: NullableParam("@Email", kh.Email)
```

### Bước 4: Sửa giao diện Form Nhập liệu (UI)
Mở file Code-behind của form chỉnh sửa [frmThemSuaKhachHang.cs](file:///d:/Learning/C%23/BAI_TAP_LON/FloriSys/7_DanhMuc/frmThemSuaKhachHang.cs):
* **Khi hiển thị dữ liệu lên Form (Hàm Load dữ liệu / Edit):**
```csharp
txtEmail.Text = khachHang.Email;
```
* **Khi lưu dữ liệu (Hàm Save / Lưu):**
```csharp
khachHang.Email = txtEmail.Text.Trim();
```

---

<a name="dang-2-su-kien-click-dong-tren-datagridview-hien-thi-du-lieu-len-textbox"></a>
## 🖱️ Dạng 2: Sự kiện Click dòng trên DataGridView hiển thị dữ liệu lên TextBox
*Thầy yêu cầu: "Khi tôi click chuột vào một hàng trên bảng Sản phẩm/Khách hàng, hãy hiển thị thông tin hàng đó lên các TextBox ở bên cạnh."*

Mở file UI tương ứng (ví dụ: [ucKhachHang.cs](file:///D:/Learning/C%23/BAI_TAP_LON/FloriSys/7_DanhMuc/ucKhachHang.cs)), đăng ký sự kiện `CellClick` của DataGridView (ví dụ `dgvKhachHang`), sau đó viết code sau:

```csharp
private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
{
    // Kiểm tra để tránh lỗi khi người dùng click vào dòng tiêu đề (Header Row)
    if (e.RowIndex >= 0)
    {
        // Lấy dòng hiện tại đang được click
        DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];

        // Lấy giá trị từng ô gán lên các TextBox/Label bên cạnh
        txtMaKH.Text = row.Cells["MaKH"].Value?.ToString();
        txtHoTen.Text = row.Cells["HoTen"].Value?.ToString();
        txtSDT.Text = row.Cells["SoDienThoai"].Value?.ToString();
        txtDiaChi.Text = row.Cells["DiaChi"].Value?.ToString();
        
        // Nếu có thuộc tính mới (ví dụ Email)
        txtEmail.Text = row.Cells["Email"].Value?.ToString();
    }
}
```

---

<a name="dang-3-su-kien-cellformatting-doi-mau-dong-o-trong-datagridview-theo-dieu-kien"></a>
## 🎨 Dạng 3: Sự kiện CellFormatting (Đổi màu dòng/ô trong DataGridView theo điều kiện)
*Thầy yêu cầu: "Hãy tô màu ĐỎ các sản phẩm có số lượng tồn kho bằng 0 (hết hàng) và tô màu VÀNG cho sản phẩm sắp hết hàng (tồn <= mức tối thiểu)."*

Mở file code của UserControl (ví dụ: [ucSanPham.cs](file:///D:/Learning/C%23/BAI_TAP_LON/FloriSys/7_DanhMuc/ucSanPham.cs)), đăng ký sự kiện `CellFormatting` của DataGridView (`dgvSanPham`):

```csharp
private void dgvSanPham_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
{
    if (e.RowIndex >= 0 && dgvSanPham.Columns[e.ColumnIndex].Name == "SoLuongTon")
    {
        var sp = dgvSanPham.Rows[e.RowIndex].DataBoundItem as FloriSys.Models.SanPham;
        
        if (sp != null)
        {
            if (sp.SoLuongTon == 0) // Hết hàng hoàn toàn (Tô màu Đỏ nhạt)
            {
                e.CellStyle.BackColor = System.Drawing.Color.LightCoral;
                e.CellStyle.ForeColor = System.Drawing.Color.DarkRed;
            }
            else if (sp.SoLuongTon <= sp.MucTonToiThieu) // Sắp hết hàng (Tô màu Vàng nhạt)
            {
                e.CellStyle.BackColor = System.Drawing.Color.Khaki;
                e.CellStyle.ForeColor = System.Drawing.Color.DarkGoldenrod;
            }
        }
    }
}
```

---

<a name="dang-4-tim-kiem-loc-dong-tuc-thi-textchanged-selectedindexchanged"></a>
## 🔍 Dạng 4: Tìm kiếm / Lọc động tức thời (TextChanged & SelectedIndexChanged)
*Thầy yêu cầu: "Tôi muốn khi gõ bất cứ ký tự nào vào TextBox tìm kiếm là danh sách tự động lọc luôn."*

### 1. Lọc động qua sự kiện TextChanged của TextBox
Mở file [ucSanPham.cs](file:///D:/Learning/C%23/BAI_TAP_LON/FloriSys/7_DanhMuc/ucSanPham.cs), double-click vào `txtTimKiem`:
```csharp
private void txtTimKiem_TextChanged(object sender, EventArgs e)
{
    LoadData(txtTimKiem.Text.Trim());
}

private void LoadData(string keyword = "")
{
    var repo = new SanPhamRepository();
    dgvSanPham.DataSource = repo.LayDanhSach(keyword, "", "");
}
```

---

<a name="dang-5-rang-buoc-du-lieu-validation-truoc-khi-themsua"></a>
## ⚠️ Dạng 5: Ràng buộc dữ liệu (Validation) trước khi Thêm/Sửa
*Thầy yêu cầu: "Trước khi lưu thông tin Khách hàng, phải kiểm tra SĐT phải đúng 10 chữ số, tên không được rỗng."*

Mở hàm lưu dữ liệu ở Form [frmThemSuaKhachHang.cs](file:///d:/Learning/C%23/BAI_TAP_LON/FloriSys/7_DanhMuc/frmThemSuaKhachHang.cs):
```csharp
private bool ValidDuaLieu()
{
    if (string.IsNullOrWhiteSpace(txtHoTen.Text))
    {
        MessageBox.Show("Tên khách hàng không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        txtHoTen.Focus();
        return false;
    }

    string sdt = txtSDT.Text.Trim();
    if (string.IsNullOrEmpty(sdt) || sdt.Length != 10 || !long.TryParse(sdt, out _))
    {
        MessageBox.Show("Số điện thoại phải đúng 10 số!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        txtSDT.Focus();
        return false;
    }
    return true;
}

private void btnLuu_Click(object sender, EventArgs e)
{
    if (!ValidDuaLieu()) return; // Dừng lại nếu lỗi validation
    
    // Tiếp tục lưu dữ liệu...
}
```

---

# PHẦN II: HƯỚNG DẪN TRÌNH BÀY VÀ PHẢN BIỆN VỚI GIẢNG VIÊN

Phần này hướng dẫn bạn cách giới thiệu đề tài, giải thích kiến trúc phần mềm, và cách trả lời các câu hỏi phản biện lý thuyết khó từ thầy cô để khẳng định bạn tự làm đồ án 100%.

---

## 🎯 1. Kịch bản Thuyết trình Đồ án trong 3 Phút đầu
Khi bắt đầu, đừng chỉ click lung tung trên giao diện. Hãy nói theo kịch bản thông minh sau:

> *"Kính thưa thầy/cô, hệ thống **FloriSys** là phần mềm Quản lý Cửa hàng Hoa tươi được xây dựng trên nền tảng **C# WinForms** kết hợp cơ sở dữ liệu **SQL Server**. Phần mềm của em giải quyết 3 bài toán lớn:*
> 1. **Quản lý bán hàng & in hóa đơn**: Cho phép chọn hoa trực quan, tự động tạo hóa đơn và xuất file PDF chuyên nghiệp.
> 2. **Kiểm soát kho hàng chặt chẽ**: Có tính năng cảnh báo tồn kho tối thiểu, cảnh báo đơn hàng xuất kho bị trễ hạn.
> 3. **Hệ thống phân quyền chi tiết**: Chia rõ vai trò Admin, Thu ngân, Thủ kho, và Nhân viên giao hàng để bảo mật thông tin tối đa.
> 
> *Về mặt kỹ thuật, hệ thống được thiết kế theo kiến trúc **3-Tier (3 lớp)** chuẩn chỉ, giúp mã nguồn sạch sẽ, dễ bảo trì và có khả năng chống tấn công SQL Injection thông qua Parameterized Queries."*

---

## 🏗️ 2. Cách giải thích Kiến trúc 3-Tier (3 lớp) trong Dự án
Khi thầy yêu cầu: *"Em hãy giải thích kiến trúc 3 lớp của dự án nằm ở đâu trong code và nó hoạt động như thế nào?"*

Hãy chỉ vào cấu trúc thư mục của dự án và giải thích:
1. **Lớp Giao Diện (Presentation Layer / UI)**:
   * **Nơi lưu trữ:** Các thư mục như `2_QuanLy`, `3_BanHang`, `4_KhoHang`, `5_GiaoHang`, `7_DanhMuc` chứa các UserControl (`uc`) và Form (`frm`).
   * **Nhiệm vụ:** Chỉ lo việc hiển thị dữ liệu lên màn hình (DataGridView, TextBox, ComboBox) và bắt sự kiện của người dùng (Click nút, Gõ phím). Lớp này **không trực tiếp gọi xuống Database**.
2. **Lớp Nghiệp Vụ & Kết Nối (DataAccess Layer / Repository)**:
   * **Nơi lưu trữ:** Thư mục [DataAccess](file:///D:/Learning/C%23/BAI_TAP_LON/FloriSys/DataAccess/).
   * **Nhiệm vụ:** Chứa các class Repository như `SanPhamRepository`, `KhachHangRepository`, `DonHangRepository`. Đây là nơi viết các câu lệnh SQL truy vấn dữ liệu hoặc gọi Stored Procedure.
3. **Lớp Thực Thể (Business Object / Model)**:
   * **Nơi lưu trữ:** Thư mục [Models](file:///D:/Learning/C%23/BAI_TAP_LON/FloriSys/Models/).
   * **Nhiệm vụ:** Chứa cấu trúc dữ liệu mô phỏng các bảng trong Database dưới dạng các class C# (ví dụ: class `SanPham`, `KhachHang`, `DonHang`). Các tầng khác sẽ dùng các Model này để truyền dữ liệu qua lại.

### 🔄 Luồng Đi của Dữ Liệu (Ví dụ khi Thêm Khách hàng):
1. Người dùng nhập thông tin lên `frmThemSuaKhachHang` (tầng UI).
2. Khi bấm "Lưu", UI sẽ đóng gói dữ liệu vào một đối tượng Model `KhachHang` và gọi hàm của tầng DataAccess: `_khachHangRepository.ThemKhachHang(kh)`.
3. Tầng DataAccess nhận đối tượng `KhachHang`, thực hiện biên dịch thành câu lệnh SQL chứa các `SqlParameter` và gửi xuống SQL Server để thực thi.
4. Kết quả trả về từ DB sẽ được truyền ngược lại qua các tầng để UI hiển thị thông báo thành công cho người dùng.

---

## 🔒 3. Trả lời về Bảo mật và Phòng chống SQL Injection
Thầy cô cực kỳ thích hỏi về vấn đề bảo mật cơ sở dữ liệu: *"Làm thế nào để phần mềm của em không bị hack dữ liệu qua lỗi SQL Injection?"*

### Cách trả lời:
> *"Trong toàn bộ dự án, em tuyệt đối không sử dụng phương pháp cộng chuỗi trực tiếp để tạo câu lệnh SQL (Ví dụ: không viết `SELECT * FROM SAN_PHAM WHERE TenSP = '` + txtTen.Text + `'`). Cách viết đó rất nguy hiểm vì hacker có thể nhập các ký tự đặc biệt như `' OR '1'='1` để phá hủy cơ sở dữ liệu.
> 
> Thay vào đó, em sử dụng **Parameterized Query (Truy vấn có tham số)** thông qua đối tượng **SqlParameter** của ADO.NET."*

### Dẫn chứng trong code (Hãy mở [SanPhamRepository.cs](file:///D:/Learning/C%23/BAI_TAP_LON/FloriSys/DataAccess/SanPhamRepository.cs) hàm `CapNhatSanPham`):
```csharp
string sql = @"UPDATE SAN_PHAM SET TenSP=@TenSP WHERE MaSP=@MaSP";
ExecuteSql(sql, new SqlParameter[]
{
    new SqlParameter("@MaSP", sp.MaSP),
    new SqlParameter("@TenSP", sp.TenSP) // Giá trị truyền qua Parameter sẽ tự động được làm sạch (escape)
});
```
> *"Khi sử dụng `SqlParameter`, SQL Server sẽ xử lý giá trị truyền vào thuần túy là dữ liệu dạng text chứ không biên dịch nó thành lệnh thực thi, giúp loại bỏ hoàn toàn nguy cơ tấn công SQL Injection."*

---

## 👤 4. Giải thích cơ chế Phân quyền người dùng (Role-based)
Thầy hỏi: *"Hệ thống phân quyền như thế nào? Thủ kho vào có thấy màn hình bán hàng không?"*

### Cách trả lời:
1. Hệ thống quản lý tài khoản dựa trên cột `VaiTro` trong bảng `NHAN_VIEN` (Admin, Thu ngân, Thủ kho, Giao hàng).
2. Khi người dùng đăng nhập thành công, thông tin tài khoản và vai trò của họ được lưu trữ tập trung vào lớp tĩnh **[SessionManager.cs](file:///D:/Learning/C%23/BAI_TAP_LON/FloriSys/Services/SessionManager.cs)**.
3. Khi Form chính ([frmMain.cs](file:///D:/Learning/C%23/BAI_TAP_LON/FloriSys/frmMain.cs)) được tải lên, hệ thống sẽ kiểm tra vai trò hiện tại trong Session để ẩn/hiện hoặc vô hiệu hóa các menu chức năng tương ứng:

```csharp
// Ví dụ minh họa phân quyền trong frmMain:
private void ApDungPhanQuyen()
{
    string vaiTro = SessionManager.CurrentUser?.VaiTro;

    if (vaiTro == "ThuKho")
    {
        btnBanHang.Visible = false; // Thủ kho không nhìn thấy nút bán hàng
        btnBaoCao.Visible = false;  // Không xem được báo cáo doanh thu
        btnKhoHang.Visible = true;
    }
    else if (vaiTro == "ThuNgan")
    {
        btnBanHang.Visible = true;
        btnKhoHang.Visible = false; // Thu ngân không được vào quản lý kho
    }
}
```

---

## 📈 5. Trình bày Chức năng Báo cáo Thống kê & Xuất PDF
Thầy hỏi: *"Chức năng báo cáo của em hoạt động như thế nào? Xuất hóa đơn ra sao?"*

### Cách trả lời:
* **Vẽ biểu đồ**: Em sử dụng thư viện trực quan hóa dữ liệu **Microsoft Chart Control** có sẵn trong .NET (`System.Windows.Forms.DataVisualization.Charting`). Dữ liệu thống kê theo ngày/tháng được Repository truy vấn bằng câu lệnh `SUM`, `GROUP BY` rồi gán trực tiếp vào thuộc tính `Series.Points.AddXY()` để hiển thị biểu đồ cột hoặc hình tròn trực quan.
* **Xuất hóa đơn PDF**: Em sử dụng thư viện **iTextSharp**. Khi khách hàng thanh toán thành công, hệ thống sẽ lấy dữ liệu đơn hàng và chi tiết đơn hàng, gọi helper **[ReportPdfHelper.cs](file:///D:/Learning/C%23/BAI_TAP_LON/FloriSys/Services/ReportPdfHelper.cs)**. Hàm này sẽ khởi tạo một tài liệu PDF (định dạng hóa đơn nhiệt khổ 80mm), vẽ bảng danh sách sản phẩm, tự động tính tổng tiền, thuế VAT và ghi trực tiếp ra tệp PDF trên máy tính.

---

## 🚑 6. Mẹo cứu nguy khi chương trình bị Lỗi (Crash) khi đang Demo
Trong buổi bảo vệ, nếu phần mềm đột ngột báo lỗi đỏ (Crash) hoặc chạy sai dữ liệu, hãy bình tĩnh thực hiện theo các bước sau để ghi điểm "kỹ năng xử lý sự cố":

1. **Tuyệt đối không bối rối**. Hãy nói: *"Thưa thầy cô, hệ thống đang gặp lỗi bất ngờ do dữ liệu kiểm thử thực tế chưa đồng bộ. Em xin phép đặt Breakpoint (điểm dừng) để debug trực tiếp luồng chạy của chức năng này."*
2. **Đặt Breakpoint**: Bấm phím **F9** tại dòng đầu tiên của hàm xử lý sự kiện click nút bấm trên UI.
3. **Chạy Debug**: Nhấn nút Start (F5), thực hiện lại thao tác lỗi trên giao diện. Khi chương trình dừng lại tại dòng màu vàng:
   * Bấm **F10** để đi qua từng dòng lệnh độc lập.
   * Di chuột vào các biến để xem giá trị thực tế của chúng có bị `null` hoặc sai định dạng không.
   * Bấm **F11** để nhảy vào chi tiết bên trong các hàm của Repository.
4. **Giải thích nguyên nhân**: Khi phát hiện ra biến bị trống hoặc câu lệnh SQL sai tên cột, hãy sửa trực tiếp, bấm **F5** để chạy tiếp và nói: *"Lỗi xảy ra do dữ liệu chưa đồng bộ, em đã sửa lại biến này và hệ thống đã hoạt động bình thường."* Giảng viên sẽ đánh giá cực kỳ cao kỹ năng gỡ lỗi thực tế này của bạn.
