# Kế Hoạch Đọc Code & Ôn Thi Bảo Vệ Project FloriSys

Tài liệu này hướng dẫn bạn cách đọc mã nguồn của dự án FloriSys theo thứ tự từ gốc đến ngọn, giúp bạn nắm vững luồng chạy, hiểu sâu cấu trúc 3 lớp, tự tay viết thêm nghiệp vụ và đối phó với các câu hỏi hóc búa từ giảng viên.

---

## 1. Lộ Trình Đọc Code (Đọc từ file nào trước?)

Để không bị ngợp, bạn cần đọc theo luồng dữ liệu (Data Flow) và kiến trúc 3 tầng (3-Layer Architecture). Hãy đọc theo thứ tự sau:

### Bước 1: Khởi động & Cấu hình
- `Program.cs`: Đây là điểm bắt đầu của ứng dụng. Bạn sẽ thấy nó gọi `frmDangNhap` trước, nếu OK mới mở `frmMain`.
- `App.config`: Xem thẻ `<connectionStrings>`. Đây là nơi chứa chuỗi kết nối tới SQL Server.

### Bước 2: Tầng Data Access & ORM Tự Chế (QUAN TRỌNG NHẤT)
*Giảng viên rất hay hỏi phần này để xem bạn có copy code hay không.*
- **`DataAccess/DatabaseHelper.cs`**: Đọc kỹ các hàm `ExecuteList<T>`, `ExecuteSingle<T>`. Chú ý đoạn code sử dụng **Reflection** (`PropertyInfo`) để tự động map dữ liệu từ `SqlDataReader` vào các Object C#. 
- **`DataAccess/BaseRepository.cs`**: Đây là lớp cha (Generic). Nó định nghĩa các hàm CRUD cơ bản (Thêm, lấy danh sách, lấy theo mã).
- **`DataAccess/DonHangRepository.cs`**: Đọc một Repository cụ thể để xem nó kế thừa `BaseRepository` và gọi các Stored Procedure (`sp_TaoDonHang`, `sp_ThemChiTietDon`) như thế nào.

### Bước 3: Tầng Models (Thực thể)
- **`Models/SanPham.cs`** & **`Models/DonHang.cs`**: Chú ý rằng **tên property trong class phải giống hệt tên cột trong DB** (để `DatabaseHelper` dùng Reflection map được). Một số thuộc tính có chữ `Display` phía sau dùng để hiển thị trên UI.

### Bước 4: Tầng Services (Xử lý nghiệp vụ & Phiên làm việc)
- **`Services/SessionManager.cs`**: Đọc kỹ. Nó dùng **Singleton Pattern** để lưu người dùng đang đăng nhập (`CurrentUser`). Mọi form đều gọi class này để biết user có quyền gì (`HasPermission`).
- **`Services/TraHangService.cs`**: Đọc để xem một luồng nghiệp vụ phức tạp (kiểm tra đơn hàng, lưu phiếu trả, hoàn tồn kho) được viết ở Service thay vì viết trực tiếp trên UI.

### Bước 5: Tầng Giao Diện (UI - WinForms)
- **`1_DangNhap/frmDangNhap.cs`**: Xem cách hash mật khẩu SHA-256 trước khi truyền xuống Database.
- **`2_QuanLy/frmMain.cs`**: Rất quan trọng. Xem cách ứng dụng hoạt động theo kiểu **SPA (Single Page Application)** — xóa controls cũ trong `panel1` và `Controls.Add(uc)` UserControl mới vào, thay vì mở nhiều cửa sổ Form.
- **`3_BanHang/ucTaoDon.cs`**: Đọc luồng thêm sản phẩm vào giỏ hàng, gọi tính tiền và gọi `DonHangRepository.TaoDonHangHoanChinh`.

---

## 2. Hướng Dẫn Tự Viết Thêm Nghiệp Vụ Mới

Khi giảng viên yêu cầu: *"Em hãy thêm tính năng X (ví dụ: Quản lý nhà cung cấp, hoặc Khuyến mãi)"*. Bạn làm đúng theo 5 bước sau:

1. **Database (Tầng Đáy):**
   - Mở SQL Server, tạo bảng mới (ví dụ `NHA_CUNG_CAP`).
   - Viết các Stored Procedures cơ bản: `sp_ThemNCC`, `sp_SuaNCC`, `sp_DanhSachNCC`.
2. **Models (Thực thể):**
   - Tạo file `Models/NhaCungCap.cs`. 
   - Khai báo các property C# khớp 100% với tên cột vừa tạo.
3. **DataAccess (Truy xuất dữ liệu):**
   - Tạo file `DataAccess/NhaCungCapRepository.cs` kế thừa `BaseRepository<NhaCungCap>`.
   - Bổ sung tên bảng: `public override string TableName => "NHA_CUNG_CAP";`.
4. **Services (Nghiệp vụ - Nếu cần):**
   - Nếu chỉ thêm/sửa/xóa cơ bản, có thể gọi luôn Repository. Nếu logic phức tạp, tạo `NhaCungCapService.cs`.
5. **UI (Giao diện):**
   - Tạo UserControl mới (ví dụ `ucNhaCungCap.cs`).
   - Thiết kế GridView, TextBoxes.
   - Ở sự kiện nút "Thêm", thu thập dữ liệu từ TextBoxes gán vào object `NhaCungCap`, rồi gọi `repository.TaoMoi()`.

---

## 3. Các Điểm Chú Ý Bị Giảng Viên Khai Thác Hỏi

Đây là các điểm kỹ thuật cốt lõi trong code của bạn mà giảng viên hay "xoáy" vào:

### Khai thác 1: Cơ chế Reflection trong DatabaseHelper
- **Câu hỏi:** "Em map dữ liệu từ DB lên Class như thế nào mà code ngắn vậy? Tại sao không dùng Entity Framework?"
- **Trả lời:** "Dạ em viết hàm generic dùng **Reflection**. Code duyệt qua các cột trả về từ DB, tìm property trong Class C# có tên tương đương và dùng `SetValue` để gán. Nhờ vậy em không phải lặp lại việc gán thủ công `obj.Ten = reader["Ten"]` cho mọi class. Em không dùng EF vì môn học yêu cầu dùng ADO.NET thuần để hiểu sâu, và hệ thống của em gọi hoàn toàn bằng Stored Procedure."

### Khai thác 2: Tính tiền tự động bằng Trigger
- **Câu hỏi:** "Tại sao Thành Tiền và Tổng Tiền trong đơn hàng không được code C# tính mà để Database tự tính?"
- **Trả lời:** "Dạ để đảm bảo tính **toàn vẹn dữ liệu (Data Integrity)**. Nếu C# tính, có nguy cơ nhiều người cùng thao tác dẫn đến sai lệch. Việc dùng Trigger `trg_TinhThanhTien` và `trg_CapNhatTongTien` đảm bảo mỗi khi có dòng sản phẩm thêm vào chi tiết đơn, CSDL sẽ tự động nhân Số lượng * Đơn giá và sum lại lên đơn hàng gốc một cách an toàn nhất trong transaction."

### Khai thác 3: SPA Pattern trên WinForms
- **Câu hỏi:** "Sao app của em không mở nhiều Form (cửa sổ) mà chỉ có 1 Form chính?"
- **Trả lời:** "Dạ em thiết kế form chính (`frmMain`) đóng vai trò là một Container. Mọi màn hình chức năng đều là các `UserControl`. Khi người dùng bấm menu, em sẽ `panel1.Controls.Clear()` và `panel1.Controls.Add(userControlMoi)`. Thiết kế này giống các ứng dụng hiện đại, không làm rác màn hình với hàng chục cửa sổ Form chồng chéo."

### Khai thác 4: Mã tự sinh và Đồng thời (Concurrency)
- **Câu hỏi:** "Cái mã đơn hàng DH0001, DH0002 em sinh ra thế nào? Nếu 2 nhân viên cùng lúc bấm 'Tạo đơn', mã có bị trùng không?"
- **Trả lời (Quan trọng):** "Dạ em dùng Stored Procedure `sp_SinhMa`, nó lấy mã lớn nhất hiện tại rồi cộng thêm 1. Về mặt lý thuyết, nếu 2 người nhấn chính xác cùng 1 mili-giây, có thể xảy ra race condition trùng mã do chưa có transaction isolation level cao (Serializable). Hướng khắc phục là dùng cơ chế `SEQUENCE` của SQL Server 2022 hoặc bọc lock lại trong C#. (Trả lời được lỗi này sẽ được đánh giá cực cao)."

### Khai thác 5: Phân quyền (SessionManager)
- **Câu hỏi:** "Hệ thống phân quyền làm sao biết người này không được phép Xóa đơn hàng?"
- **Trả lời:** "Dạ em dùng bảng `PHAN_QUYEN` lưu ma trận các quyền (Xem, Thêm, Sửa, Xóa). Khi đăng nhập thành công, em lưu User vào class Singleton tên là `SessionManager`. Khi màn hình Danh sách đơn hàng mở lên, ở sự kiện Load, nó sẽ gọi `SessionManager.HasPermission(module, "Xoa")`, nếu trả về `false`, em sẽ `btnXoa.Enabled = false` (làm mờ nút Xóa)."

### Khai thác 6: Bảo mật mật khẩu
- **Câu hỏi:** "Mật khẩu em lưu trực tiếp xuống database à? Nếu DBA vào xem thì sao?"
- **Trả lời:** "Dạ không, mật khẩu được băm (hash) bằng thuật toán **SHA-256** ở tầng C# (trong AuthService/frmDangNhap) trước khi gửi xuống DB. DB chỉ lưu chuỗi băm 64 ký tự. Kể cả admin DB cũng không biết mật khẩu gốc là gì."
