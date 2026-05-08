# Hướng Dẫn Trả Lời Câu Hỏi Giảng Viên – FloriSys

---

## PHẦN 1: CÂU HỎI VỀ DATABASE

---

### ❓ Q1: Database có bao nhiêu bảng? Liệt kê và giải thích mối quan hệ?

**Trả lời:**
> Dạ, database FloriSys gồm **14 bảng** chính:
>
> 1. `NHAN_VIEN` – Nhân viên (Admin, Cashier, Warehouse, Shipper)
> 2. `KHACH_HANG` – Khách hàng
> 3. `SAN_PHAM` – Sản phẩm (hoa, phụ kiện)
> 4. `DON_HANG` – Đơn hàng (header)
> 5. `CHI_TIET_DON_HANG` – Chi tiết đơn hàng (composite PK: MaDon + MaSP)
> 6. `GIAO_HANG` – Lệnh giao hàng
> 7. `PHIEU_NHAP_KHO` – Phiếu nhập kho (header)
> 8. `CT_NHAP_KHO` – Chi tiết nhập kho (composite PK: MaPhieu + MaSP)
> 9. `PHAN_HOI` – Phản hồi khách hàng
> 10. `CANH_BAO_TON_KHO` – Cảnh báo tồn kho
> 11. `HANG_HU` – Lịch sử hủy hàng hư
> 12. `PHAN_QUYEN` – Phân quyền theo chức vụ + module
> 13. `TRA_HANG` – Phiếu trả hàng (header)
> 14. `CT_TRA_HANG` – Chi tiết trả hàng (composite PK: MaPhieuTra + MaSP)
>
> **Mối quan hệ chính:**
> - `DON_HANG` → FK tới `KHACH_HANG` (MaKH) và `NHAN_VIEN` (MaNV_TaoDon)
> - `CHI_TIET_DON_HANG` → FK tới `DON_HANG` + `SAN_PHAM` (quan hệ N-N qua bảng trung gian)
> - `GIAO_HANG` → FK tới `DON_HANG` + `NHAN_VIEN` (shipper)
> - `TRA_HANG` → FK tới `DON_HANG`, `CT_TRA_HANG` → FK tới `SAN_PHAM`

---

### ❓ Q2: Tại sao dùng CHECK constraint thay vì bảng lookup riêng?

**Trả lời:**
> Dạ, em dùng CHECK constraint cho các giá trị cố định ít thay đổi (VD: ChucVu chỉ có 4 giá trị `Admin, Cashier, Warehouse, Shipper`; TrangThai đơn hàng có 6 giá trị). Vì số lượng giá trị ít và cố định nên dùng CHECK nhanh hơn và đơn giản hơn tạo bảng lookup riêng. Nếu sau này cần mở rộng linh hoạt hơn thì em sẽ chuyển sang bảng lookup.

---

### ❓ Q3: Giải thích Trigger trong database?

**Trả lời:**
> Dạ, em có **3 trigger** chính:
>
> 1. **`trg_TinhThanhTien`** – Khi INSERT/UPDATE vào `CHI_TIET_DON_HANG`, tự tính `ThanhTien = SoLuong × DonGia`. Đảm bảo dữ liệu luôn nhất quán.
>
> 2. **`trg_CapNhatTongTien`** – Sau khi thay đổi `CHI_TIET_DON_HANG`, tự cập nhật `TongTien` của `DON_HANG` = SUM(ThanhTien). Tránh phải tính lại từ code.
>
> 3. **`trg_NhapKho_TangTon`** – Khi INSERT vào `CT_NHAP_KHO`, tự động tăng `SoLuongTon` trong `SAN_PHAM`. Đảm bảo nhập kho là tồn kho tăng ngay.

---

### ❓ Q4: Stored Procedure nào quan trọng nhất? Giải thích logic?

**Trả lời:**
> Dạ, SP quan trọng nhất là **`sp_CapNhatTrangThaiDon`**. Logic:
>
> - Khi chuyển sang `DangXuLy`: Kiểm tra tồn kho đủ không → nếu đủ thì **trừ tồn kho** → cập nhật trạng thái.
> - Khi chuyển sang `HoanHang`: Kiểm tra trạng thái hiện tại, nếu đang ở `DangXuLy` hoặc `DaGiao` thì **hoàn lại tồn kho**.
> - Khi `Huy` từ `Moi`: Không cần hoàn kho vì chưa trừ.
>
> Ngoài ra còn có `sp_ThemChiTietDon` kiểm tra tồn kho trước khi thêm chi tiết, và `sp_GhiNhanHangHu` kiểm tra tồn kho đủ trước khi hủy.

---

### ❓ Q5: Mật khẩu lưu trữ thế nào? Có bảo mật không?

**Trả lời:**
> Dạ, mật khẩu **không lưu plaintext** mà lưu dạng **hash SHA-256**. Khi đăng nhập, code C# sẽ hash mật khẩu người dùng nhập rồi so sánh với hash trong database. Ví dụ: mật khẩu `123456` được lưu thành `8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92`.

---

### ❓ Q6: Giải thích cơ chế sinh mã tự động (sp_SinhMa)?

**Trả lời:**
> SP `sp_SinhMa` nhận 3 tham số: `@Prefix` (tiền tố, VD "DH"), `@Table`, `@Column`. Nó lấy MAX số lớn nhất hiện tại từ bảng, +1, rồi ghép với prefix thành mã mới. VD: Đơn cuối là `DH000024` → tạo mới ra `DH000025`. Dùng dynamic SQL để tái sử dụng cho nhiều bảng.

---

### ❓ Q7: Tại sao dùng composite primary key cho CHI_TIET_DON_HANG?

**Trả lời:**
> Vì mỗi sản phẩm chỉ xuất hiện **một lần** trong một đơn hàng. Composite PK `(MaDon, MaSP)` đảm bảo ràng buộc này ở tầng database, không cần phải validate ở code. Tương tự cho `CT_NHAP_KHO` và `CT_TRA_HANG`.

---

### ❓ Q8: Bảng PHAN_QUYEN hoạt động thế nào?

**Trả lời:**
> Composite PK `(ChucVu, Module)`. Mỗi dòng quy định một chức vụ có quyền gì trên một module cụ thể. Có 5 cột quyền: `Xem, Them, Sua, Xoa, Export` (kiểu BIT). VD: Cashier có thể Xem + Thêm + Sửa `DonHang` nhưng không được Xóa. Code C# sẽ query bảng này khi load form để ẩn/hiện nút chức năng.

---

## PHẦN 2: CÂU HỎI VỀ CODE (C#)

---

### ❓ Q9: Kiến trúc project như thế nào? Có mấy lớp (layer)?

**Trả lời:**
> Dạ, project theo kiến trúc **3 lớp (3-layer)**:
>
> 1. **UI Layer** (WinForms): Các folder `1_DangNhap`, `2_QuanLy`, `3_BanHang`, `4_KhoHang`, `5_GiaoHang`, `6_BaoCao`, `7_DanhMuc` – chỉ lo hiển thị và nhận input.
> 2. **Service Layer** (`Services/`): `AuthService`, `DonHangService`, `GiaoHangService`, `KhoHangService`, `TraHangService` – chứa business logic.
> 3. **Data Access Layer** (`DataAccess/`): `BaseRepository<T>`, `DonHangRepository`, `SanPhamRepository`... – lo kết nối database.
>
> Ngoài ra có folder `Models/` chứa các model POCO.

---

### ❓ Q10: 4 tính chất OOP thể hiện ở đâu trong code?

**Trả lời:**

> **1. Tính đóng gói (Encapsulation):**
> - `SessionManager`: dùng `private` constructor, `private _currentUser`, chỉ truy cập qua `Instance` (Singleton). Password hash logic nằm bên trong, UI không cần biết cách hash.
> - `DonHang` model: `CanCancel`, `CanProcess`, `TongTienFormatted` – business logic gói trong model, UI chỉ gọi property.
>
> **2. Tính kế thừa (Inheritance):**
> - `BaseEntity` (abstract) → `DonHang`, `SanPham`, `KhachHang`... kế thừa.
> - `BaseRepository<T>` (abstract generic) → `DonHangRepository`, `SanPhamRepository`... kế thừa.
>
> **3. Tính đa hình (Polymorphism):**
> - `BaseEntity.DisplayText` là `abstract` → mỗi model override khác nhau (`DonHang` return `MaDon`, `SanPham` return `TenSP`...).
> - `BaseRepository.LayDanhSach()` là `virtual` → `DonHangRepository` override thành query JOIN 4 bảng với 4 filter.
>
> **4. Tính trừu tượng (Abstraction):**
> - `BaseRepository<T>` khai báo abstract `TableName`, `IdColumn`, `IdPrefix` → subclass phải định nghĩa.
> - `SessionManager.IsAdmin`, `IsCashier` ẩn đi việc so sánh chuỗi, code UI chỉ cần gọi `if (SessionManager.IsAdmin)`.

---

### ❓ Q11: BaseRepository<T> hoạt động thế nào? Tại sao dùng Generic?

**Trả lời:**
> `BaseRepository<T> where T : BaseEntity, new()` là class abstract generic. Nó cung cấp sẵn các method dùng chung: `LayDanhSach()`, `LayTheoMa()`, `TaoMoi()`, `ExecuteSP()`, `GetList()`.
>
> Dùng Generic vì tất cả Repository đều có cùng pattern: query database → map kết quả thành object. Generic giúp **không phải viết lại code giống nhau** cho từng entity. VD: `SanPhamRepository` chỉ cần khai báo `TableName = "SAN_PHAM"`, `IdColumn = "MaSP"`, `IdPrefix = "SP"` là đã có sẵn CRUD cơ bản.

---

### ❓ Q12: Transaction trong code xử lý thế nào?

**Trả lời:**
> Ở method `DonHangRepository.TaoDonHangHoanChinh()`:
>
> ```
> conn.Open() → BeginTransaction()
>   → sp_SinhMa (sinh mã đơn)
>   → sp_TaoDonHang (tạo header)
>   → sp_ThemChiTietDon (N lần, mỗi sản phẩm 1 lần)
>   → sp_TaoGiaoHang (nếu giao tận nơi)
> → Commit()
> ```
>
> Nếu **bất kỳ bước nào lỗi** (VD: tồn kho không đủ), catch block sẽ gọi `tran.Rollback()` → toàn bộ thao tác bị hủy, database không bị dữ liệu dở dang.

---

### ❓ Q13: SessionManager là gì? Singleton pattern hoạt động sao?

**Trả lời:**
> `SessionManager` quản lý phiên đăng nhập. Dùng **Singleton pattern** – chỉ có **duy nhất 1 instance** trong toàn bộ app:
>
> - Constructor `private` → không ai tạo được instance mới.
> - `Lazy<SessionManager>` đảm bảo thread-safe.
> - Truy cập qua `SessionManager.Instance`.
>
> Khi login thành công, `AuthService` gọi `SessionManager.Instance.Login(nv)` lưu user. Mọi form khác đều truy cập `SessionManager.MaNV`, `SessionManager.ChucVu`... để biết ai đang đăng nhập.

---

### ❓ Q14: AuthService xử lý đăng nhập như thế nào?

**Trả lời:**
> 1. Validate input (không trống).
> 2. Hash mật khẩu bằng `SessionManager.HashSHA256(matKhau)`.
> 3. Gọi `_nvRepo.DangNhap(taiKhoan, hash)` → gọi SP `sp_DangNhap` so sánh taiKhoan + hash + TrangThai = 'DangLam'.
> 4. Nếu tìm thấy → `SessionManager.Instance.Login(nv)` → return true.
> 5. Nếu lỗi kết nối → catch Exception → trả lỗi "Không thể kết nối CSDL".

---

### ❓ Q15: Enum dùng để làm gì? Tại sao không dùng string?

**Trả lời:**
> File `Enums.cs` định nghĩa: `ChucVu`, `TrangThaiDon`, `TrangThaiGiao`, `TrangThaiSP`, `HinhThucNhan`... Dùng enum thay string vì:
> - **Type-safe**: Compiler sẽ báo lỗi nếu gán sai giá trị.
> - **IntelliSense**: IDE gợi ý giá trị hợp lệ.
> - Extension method `ToDbString()` chuyển enum → string để lưu database, `ToChucVu()` chuyển ngược lại.

---

### ❓ Q16: Project có bao nhiêu module chức năng?

**Trả lời:**
> **7 module** chính tương ứng 7 folder:
>
> | # | Module | Chức năng |
> |---|--------|-----------|
> | 1 | `1_DangNhap` | Đăng nhập, đổi mật khẩu |
> | 2 | `2_QuanLy` | Quản lý nhân viên, khách hàng, phân quyền |
> | 3 | `3_BanHang` | Tạo đơn, danh sách đơn, chi tiết đơn, phản hồi, trả hàng, dashboard bán hàng |
> | 4 | `4_KhoHang` | Nhập kho, quản lý tồn kho, hàng hư |
> | 5 | `5_GiaoHang` | Phân công shipper, cập nhật trạng thái giao |
> | 6 | `6_BaoCao` | Báo cáo ngày, tháng, nhân viên, sản phẩm, tồn kho |
> | 7 | `7_DanhMuc` | Quản lý danh mục sản phẩm |

---

### ❓ Q17: Xử lý lỗi (Error Handling) trong code thế nào?

**Trả lời:**
> - Tầng **DataAccess**: try-catch quanh SqlConnection, Rollback transaction nếu lỗi, `throw` lại exception cho tầng trên.
> - Tầng **Service**: catch exception từ DataAccess, trả ra `out string error` với thông báo thân thiện.
> - Tầng **UI**: Nhận error string → hiển thị `MessageBox`. Không để exception crash app.

---

### ❓ Q18: Tại sao không dùng Entity Framework mà dùng ADO.NET?

**Trả lời:**
> Dạ, vì mục tiêu bài tập lớn là **hiểu rõ cách tương tác database ở mức thấp**: viết SQL thủ công, quản lý connection, xử lý transaction. ADO.NET giúp em nắm vững kiến thức nền tảng. Entity Framework tuy tiện hơn nhưng che giấu nhiều chi tiết quan trọng mà sinh viên cần hiểu.

---

### ❓ Q19: DatabaseHelper làm nhiệm vụ gì?

**Trả lời:**
> `DatabaseHelper` là class utility tập trung hóa kết nối database:
> - `GetConnection()` – tạo SqlConnection từ connection string trong App.config.
> - `ExecuteList<T>()` – chạy SP, map DataReader thành List<T> bằng reflection.
> - `ExecuteNonQuery()` – chạy SP INSERT/UPDATE/DELETE.
> - `ExecuteRawQuery()` – chạy SQL thuần trả về DataTable.
> - `GenerateCode()` – gọi sp_SinhMa sinh mã tự động.
>
> Mọi Repository đều gọi qua DatabaseHelper, **không tự mở connection** → tập trung quản lý, dễ bảo trì.

---

### ❓ Q20: Luồng tạo đơn hàng hoàn chỉnh từ UI → DB?

**Trả lời:**
> ```
> [UI: ucTaoDon] Nhân viên chọn khách, thêm sản phẩm vào giỏ, bấm "Tạo đơn"
>     ↓
> [Service: DonHangService] Validate dữ liệu, gọi Repository
>     ↓
> [DataAccess: DonHangRepository.TaoDonHangHoanChinh()]
>     ↓ BeginTransaction
>     → sp_SinhMa → sinh mã "DH000025"
>     → sp_TaoDonHang → INSERT DON_HANG
>     → sp_ThemChiTietDon (N lần) → INSERT CHI_TIET_DON_HANG
>       → Trigger trg_TinhThanhTien → tự tính ThanhTien
>       → Trigger trg_CapNhatTongTien → tự tính TongTien
>     → sp_TaoGiaoHang (nếu giao tận nơi) → INSERT GIAO_HANG
>     ↓ Commit
> [UI] Hiển thị "Tạo đơn thành công!", refresh danh sách
> ```

---

### ❓ Q21: Phân quyền (RBAC) hoạt động thế nào trong code?

**Trả lời:**
> 1. Khi đăng nhập → `SessionManager` lưu `ChucVu` của user.
> 2. Khi mở form chính → query bảng `PHAN_QUYEN` WHERE `ChucVu = @ChucVu`.
> 3. Dựa trên kết quả → ẩn/hiện menu, enable/disable nút Thêm/Sửa/Xóa.
> 4. VD: Shipper chỉ thấy module GiaoHang, không thấy KhoHang hay BaoCao.

---

### ❓ Q22: Khó khăn gặp phải và cách giải quyết?

**Trả lời:**
> 1. **Đồng bộ tồn kho**: Khi hủy đơn phải hoàn kho, trả hàng phải cộng lại → Giải quyết bằng SP `sp_CapNhatTrangThaiDon` xử lý tập trung tại database.
> 2. **Transaction**: Ban đầu không dùng transaction, dữ liệu bị dở dang khi lỗi giữa chừng → Refactor dùng `SqlTransaction` wrap toàn bộ thao tác.
> 3. **Generic Repository**: Phải hiểu Generics + Reflection để map DataReader → object tự động.
> 4. **Trigger conflict**: Khi insert data mẫu, trigger nhập kho chạy sai → Phải DISABLE trigger trước khi insert rồi ENABLE lại.

---

### ❓ Q23: Nếu có thêm thời gian, em sẽ cải thiện gì?

**Trả lời:**
> 1. Thêm **salt** cho password hash (hiện chỉ SHA-256 thuần).
> 2. Dùng **async/await** cho tất cả database call để UI không bị freeze.
> 3. Thêm **unit test** với NUnit/xUnit.
> 4. Xuất báo cáo ra **PDF/Excel**.
> 5. Tích hợp **barcode scanning** cho nhập kho.

---

## MẸO TRÌNH BÀY

1. **Mở app demo trước** – cho giảng viên thấy app chạy thật trước khi giải thích code.
2. **Demo flow chính**: Đăng nhập → Tạo đơn → Xem tồn kho giảm → Hủy đơn → Xem tồn kho hoàn lại.
3. **Mở SQL Server** song song để show dữ liệu thay đổi realtime.
4. **Trả lời tự tin**, nếu không biết thì nói "Em sẽ tìm hiểu thêm" thay vì đoán.
5. **Chuẩn bị sẵn** file `FloriSys_Database.sql` để show schema nếu được hỏi.
