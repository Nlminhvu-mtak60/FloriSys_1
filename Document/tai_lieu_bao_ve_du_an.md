# TÀI LIỆU BẢO VỆ DỰ ÁN
## Tên đề tài: FloriSys - Hệ Thống Quản Lý Cửa Hàng Hoa

---

### MỤC LỤC
1. [Tổng quan dự án](#1-tổng-quan-dự-án)
2. [Công nghệ sử dụng](#2-công-nghệ-sử-dụng)
3. [Kiến trúc phần mềm](#3-kiến-trúc-phần-mềm)
4. [Thiết kế Cơ sở dữ liệu](#4-thiết-kế-cơ-sở-dữ-liệu)
5. [Các chức năng chính](#5-các-chức-năng-chính)
6. [Các quy trình nghiệp vụ trọng tâm](#6-các-quy-trình-nghiệp-vụ-trọng-tâm)
7. [Điểm nổi bật về kỹ thuật](#7-điểm-nổi-bật-về-kỹ-thuật)

---

### 1. TỔNG QUAN DỰ ÁN
**FloriSys** là một ứng dụng Desktop (Windows Forms) được xây dựng để phục vụ công tác quản lý toàn diện cho một cửa hàng kinh doanh hoa. Hệ thống bao quát các quy trình từ bán hàng (Point-of-Sale), quản lý đơn hàng, vận hành kho bãi, theo dõi giao hàng cho đến báo cáo thống kê kinh doanh.
- **Mục tiêu:** Tự động hóa và tối ưu hóa quy trình quản lý cửa hàng hoa, giảm thiểu sai sót trong khâu tính toán, theo dõi chặt chẽ lượng hàng tồn kho và cung cấp các báo cáo doanh thu trực quan.
- **Đối tượng sử dụng:** Quản lý (Admin), Thu ngân (Cashier), Nhân viên kho (Warehouse), Nhân viên giao hàng (Shipper).

---

### 2. CÔNG NGHỆ SỬ DỤNG
Hệ thống được phát triển dựa trên nền tảng .NET và SQL Server với các công nghệ và thư viện như sau:
- **Ngôn ngữ lập trình:** C# (.NET Framework 4.7.2)
- **Framework Giao diện:** Windows Forms (WinForms)
- **Hệ quản trị Cơ sở dữ liệu:** SQL Server 2022 (Developer Edition)
- **Công nghệ truy xuất dữ liệu:** ADO.NET (SqlClient) kết hợp Reflection ORM tự xây dựng và Repository Pattern.
- **Báo cáo và biểu đồ:**
  - `System.Windows.Forms.DataVisualization` (Vẽ biểu đồ doanh thu).
  - Thư viện `EPPlus` (v4.5.3.3) (Xuất báo cáo ra file Excel).
- **Bảo mật:** Mã hóa mật khẩu SHA-256 (`System.Security.Cryptography`).

---

### 3. KIẾN TRÚC PHẦN MỀM
Hệ thống tuân thủ chặt chẽ **Kiến trúc 3 lớp (3-Tier Architecture)**:
1. **Lớp Giao diện (UI Layer - WinForms):** Trình bày dữ liệu và tiếp nhận thao tác người dùng. Hệ thống áp dụng mô hình **SPA (Single Page Application) trên WinForms**, sử dụng một Form chính (`frmMain`) làm container và chuyển đổi qua lại giữa các `UserControl` mà không cần mở nhiều cửa sổ nhỏ.
2. **Lớp Truy xuất dữ liệu (Repository Layer):** 
   - Kế thừa từ `BaseRepository<T>` theo chuẩn OOP để tái sử dụng mã nguồn.
   - Quản lý các thao tác Create, Read, Update, Delete (CRUD).
3. **Lớp Cơ sở dữ liệu (Database Layer - SQL Server):** Lưu trữ dữ liệu, thực hiện các tính toán logic tự động thông qua Stored Procedures và Triggers (như cập nhật tồn kho, tính tổng tiền đơn hàng).

**Giao tiếp qua Event-Driven:** Sự kiện click từ thanh Menu sẽ thông báo cho `frmMain` để tự động khởi tạo và hiển thị `UserControl` tương ứng lên màn hình.

---

### 4. THIẾT KẾ CƠ SỞ DỮ LIỆU
Hệ thống sử dụng **14 bảng** có mối quan hệ logic chặt chẽ với nhau.

**Các nhóm bảng chính:**
- **Quản lý con người:** `NHAN_VIEN` (Nhân viên), `KHACH_HANG` (Khách hàng), `PHAN_QUYEN` (Phân quyền).
- **Sản phẩm & Danh mục:** `SAN_PHAM` (Sản phẩm hoa, phụ kiện).
- **Quản lý Bán hàng:** `DON_HANG` (Đơn hàng), `CHI_TIET_DON_HANG` (Chi tiết), `LICH_SU_DON_HANG` (Lịch sử trạng thái).
- **Giao hàng:** `GIAO_HANG` (Phiếu giao hàng).
- **Kho hàng:** `PHIEU_NHAP_KHO` (Phiếu nhập), `CT_NHAP_KHO` (Chi tiết nhập), `HANG_HU` (Hàng hư hỏng).
- **Sau bán hàng:** `PHAN_HOI` (Phản hồi), `TRA_HANG` (Phiếu trả), `CT_TRA_HANG` (Chi tiết trả).

---

### 5. CÁC CHỨC NĂNG CHÍNH
Hệ thống được chia thành các phân hệ tương ứng với các nghiệp vụ thực tế của cửa hàng hoa:

1. **Phân hệ Đăng nhập và Phân quyền:**
   - Xác thực người dùng bằng tài khoản và mật khẩu (mã hóa SHA-256).
   - Phân quyền chi tiết (Xem, Thêm, Sửa, Xóa, Export) theo từng module cho 4 chức vụ: Admin, Thu ngân, Thủ kho, Shipper.

2. **Phân hệ Bán hàng (Sales):**
   - **Tạo đơn hàng:** Lên đơn theo dạng giỏ hàng, xác thực số lượng tồn kho theo thời gian thực (Real-time).
   - **Quản lý đơn:** Tìm kiếm, lọc đơn hàng theo từ khóa, trạng thái, ngày tháng, nhân viên. Xem chi tiết thông tin đơn hàng, khách hàng, giao hàng.
   - **Xử lý sau bán:** Theo dõi phản hồi khách hàng, quy trình xử lý trả hàng và tùy chọn nhập lại kho đối với hàng hóa còn nguyên vẹn.

3. **Phân hệ Kho hàng (Inventory):**
   - **Kiểm soát Tồn kho:** Xem danh sách hàng tồn, cảnh báo khi hàng sắp hết (dưới mức tối thiểu).
   - **Nhập/Xuất kho:** Ghi nhận phiếu nhập hàng (tự động cộng tồn kho). Xuất kho tương ứng với đơn hàng (tự động trừ tồn kho).
   - **Hàng hư hỏng:** Ghi nhận hàng lỗi, hàng hỏng và tự động giảm số lượng trong kho.

4. **Phân hệ Giao hàng (Delivery):**
   - Phân công shipper cho từng đơn hàng giao tận nơi.
   - Cập nhật trạng thái giao hàng theo luồng: Đang chờ -> Đang giao -> Đã giao -> Hoàn hàng.

5. **Phân hệ Báo cáo Thống kê (Reports):**
   - Doanh thu theo Ngày (kèm số lượng đơn).
   - Doanh thu theo Tháng (có biểu đồ minh họa trực quan).
   - Báo cáo Sản phẩm Bán chạy (Top 10 sản phẩm).
   - Đánh giá hiệu suất Nhân viên (Tổng đơn, doanh thu mang lại, tỷ lệ hủy đơn).
   - Cảnh báo Tồn kho.
   - *(Hỗ trợ xuất tất cả báo cáo ra định dạng Excel).*

6. **Phân hệ Quản lý Danh mục (Master Data):**
   - Quản lý thông tin Nhân viên, Khách hàng, Sản phẩm. 
   - Tự động điền/tra cứu khách hàng bằng Số điện thoại.

---

### 6. CÁC QUY TRÌNH NGHIỆP VỤ TRỌNG TÂM

#### 6.1. Quy trình Tạo và Xử lý Đơn Hàng (Sử dụng Transaction)
Đây là quy trình quan trọng nhất, kết hợp nhiều thao tác CSDL đảm bảo tính toàn vẹn dữ liệu:
1. Thu ngân thêm hoa/phụ kiện vào giỏ, hệ thống kiểm tra tồn kho (`SoLuongTon > 0`).
2. Nhập số điện thoại khách hàng, nếu chưa có sẽ tự động thêm mới vào bảng `KHACH_HANG`.
3. Khi bấm "Thanh toán", hệ thống khởi tạo **SqlTransaction**:
   - Tự động sinh `MaDon` và `INSERT` vào bảng `DON_HANG`.
   - Lặp qua từng sản phẩm trong giỏ để `INSERT` vào `CHI_TIET_DON_HANG`.
   - Các thao tác này sẽ kích hoạt Trigger trên SQL Server để tính Thành tiền (`Thành Tiền = Số Lượng * Đơn Giá`) và cập nhật Tổng tiền đơn hàng.
   - Nếu là đơn cần giao hàng, hệ thống tự động sinh `MaGiaoHang` và `INSERT` vào bảng `GIAO_HANG`.
4. Nếu tất cả thành công, Commit Transaction. Nếu có bất kỳ lỗi nào (như hết hàng giữa chừng), Rollback Transaction.

#### 6.2. Vòng đời Trạng Thái Đơn Hàng (State Machine)
- **Mới tạo:** Đơn hàng vừa lập.
- **Đang xử lý:** Đơn hàng được xác nhận, hệ thống **tự động trừ số lượng tồn kho** trong bảng `SAN_PHAM`.
- **Đã giao:** Shipper đã giao thành công.
- **Hoàn thành:** Khách hàng xác nhận không có vấn đề.
- **Hoàn hàng:** Khách hàng trả lại hàng (Hệ thống sẽ **cộng lại tồn kho** cho các sản phẩm hợp lệ).
- **Hủy:** Đơn hàng bị hủy trước khi xử lý (Không thay đổi tồn kho).

---

### 7. ĐIỂM NỔI BẬT VỀ KỸ THUẬT

1. **Auto Code Generation (Tự động sinh mã ID):** 
   - Mọi thực thể trong hệ thống đều tự động sinh mã định danh (Ví dụ: `NV000006`, `SP000009`, `DH000006`) dựa vào Stored Procedure `sp_SinhMa`, giúp quản lý ID thống nhất và chuyên nghiệp.
2. **Reflection ORM Tự Xây Dựng:** 
   - Thay vì dùng Entity Framework nặng nề, dự án tự code một bộ Generic ORM (`MapDataTable<T>`) sử dụng Reflection trong C# để ánh xạ kết quả từ SQL (`DataTable`) trực tiếp vào List các object C#, đảm bảo ứng dụng chạy cực nhẹ, nhanh và dễ bảo trì.
3. **Mô hình SPA trên WinForms:** 
   - Chuyển đổi giữa các màn hình bằng cách thêm/xóa `UserControl` vào trong một Panel duy nhất (`frmMain.Panel.Controls.Clear()`), mang lại trải nghiệm mượt mà không bị "nháy" cửa sổ như WinForms truyền thống.
4. **Xử lý Logic dữ liệu dưới Database (Trigger/Stored Procedure):** 
   - **19 Stored Procedures** và **3 Triggers** đảm nhận các công việc tính toán an toàn (như cập nhật tự động số lượng tồn kho, tính tổng tiền đơn hàng). Tránh sai sót và đồng bộ dữ liệu hoàn hảo dù có nhiều Client truy cập cùng lúc.
5. **BaseRepository & OOP:**
   - Ứng dụng nhuần nhuyễn tính Kế thừa (Inheritance) và Đa hình (Polymorphism) bằng cách xây dựng class `BaseRepository<T>`. 11 Repository khác nhau (như `DonHangRepository`, `SanPhamRepository`) chỉ việc kế thừa và tận dụng lại các hàm dùng chung (Thêm, Sửa, Xóa, Lấy danh sách).
6. **Bảo mật và Phân quyền động:** 
   - Mật khẩu lưu ở DB là mã Hash (SHA-256). Bảng `PHAN_QUYEN` cho phép tùy biến linh hoạt việc "đóng/mở" từng nút Thêm/Sửa/Xóa tùy theo chức vụ đăng nhập.

---

### 8. ĐÁNH GIÁ ƯU - NHƯỢC ĐIỂM & HƯỚNG PHÁT TRIỂN

#### 8.1. Ưu điểm (Kết quả đạt được)
1. **Kiến trúc rõ ràng, chuyên nghiệp:** Áp dụng chặt chẽ mô hình 3 lớp (3-Tier) và Repository Pattern, giúp code dễ đọc, dễ bảo trì và dễ mở rộng.
2. **Hiệu năng cao, nhẹ mượt:** Sử dụng Custom Reflection ORM tự thiết kế thay vì các framework nặng nề, kết hợp với cơ chế SPA (Single Page Application) trên WinForms giúp ứng dụng chạy cực kỳ nhanh và không bị giật lag giao diện.
3. **Tính toàn vẹn dữ liệu cao:** Sử dụng SqlTransaction trong quá trình thanh toán và tận dụng tối đa sức mạnh của SQL Server (Stored Procedures, Triggers) để xử lý logic phức tạp (tính tiền, trừ/cộng tồn kho), hạn chế tối đa sai sót số liệu.
4. **Có cơ chế phân quyền động cơ bản:** Hệ thống đã xây dựng được bảng phân quyền động (Xem, Thêm, Sửa, Xóa, Xuất file) theo từng module cho các chức vụ quản lý.
5. **Nghiệp vụ đầy đủ, thực tế:** Bao quát được toàn bộ vòng đời kinh doanh: từ bán hàng, xuất/nhập/hủy kho, giao hàng, đến xử lý phản hồi và hoàn trả.

#### 8.2. Nhược điểm (Hạn chế còn tồn tại)
1. **Phân quyền chưa tối ưu:** Mặc dù có cơ chế phân quyền, nhưng vẫn bị gộp chung vào "Chức vụ" (Role-based) thay vì chi tiết đến từng "Người dùng" (User-based) cụ thể. Đồng thời hệ thống thiếu cơ chế phân quyền theo dòng dữ liệu (Row-level Security - VD: Thu ngân A có thể xem được tất cả đơn hàng của Thu ngân B, thay vì chỉ xem được đơn do chính mình tạo).
2. **Thiếu module Khuyến mãi/Giảm giá:** Đặc thù hoa tươi là mặt hàng có thời hạn sử dụng ngắn, cần các chương trình giảm giá (voucher, chiết khấu % cho hoa sắp héo) để kích cầu và xả hàng nhanh, nhưng hệ thống hiện tại chưa hỗ trợ ghi nhận giảm giá.
3. **Chưa có hệ thống Chăm sóc Khách hàng (CRM):** Chưa có tính năng tích điểm hội viên, phân hạng khách hàng (VIP, Thường) dựa trên tổng chi tiêu để giữ chân khách hàng cũ.
4. **Hoạt động Offline:** Vì là ứng dụng Desktop kết nối mạng LAN/Cục bộ, khách hàng không thể tự đặt hàng Online hay theo dõi trạng thái đơn hàng từ xa qua điện thoại. Mọi thao tác đều phụ thuộc vào nhân viên tại cửa hàng.
5. **Quản lý kho chưa chi tiết theo lô (Batch/Lot):** Hoa nhập về các ngày khác nhau sẽ có độ tươi khác nhau, nhưng hệ thống hiện gộp chung vào tổng số lượng tồn kho (SoLuongTon), gây khó khăn cho việc quản lý xuất hàng theo nguyên tắc FIFO (Nhập trước Xuất trước).

#### 8.3. Hướng phát triển tương lai
1. **Tối ưu hóa Phân quyền:** Cho phép phân quyền chi tiết xuống từng tài khoản nhân viên ngoại lệ. Áp dụng Row-level Security để giới hạn phạm vi truy cập dữ liệu (VD: nhân viên chỉ thấy/sửa đơn hàng do chính mình phụ trách) để tăng tính bảo mật.
2. **Tích hợp tính năng Khuyến mãi & Tích điểm:** Thêm các trường dữ liệu xử lý Voucher/Mã giảm giá và tính điểm thưởng cho thẻ thành viên.
3. **Nâng cấp quản lý kho theo Lô (Lot-tracking):** Tách biệt số lượng tồn kho theo từng đợt nhập để dễ dàng theo dõi hạn sử dụng (độ tươi) của hoa.
4. **Mở rộng đa nền tảng:** Xây dựng thêm một Website hoặc Mobile App (sử dụng chung Database API) dành riêng cho khách hàng để họ tự xem menu hoa, đặt mua và tra cứu đơn hàng trực tuyến.

---
*Tài liệu này tổng hợp toàn bộ các ý tưởng, kiến trúc, giải pháp kỹ thuật và những đánh giá khách quan về dự án FloriSys. Dùng làm tài liệu tham khảo để viết báo cáo và thuyết trình bảo vệ đề tài.*
