# FloriSys Project Presentation Guide

## 📚 Project Overview
- **Project Name:** FloriSys – Flower Shop Management System
- **Technology Stack:**
  - **Front‑end:** Windows Forms (C# .NET Framework)
  - **Back‑end / Data Access:** ADO.NET with SQL Server (local `.mdf` database)
  - **Architecture:** Layered (UI → Business Logic → Data Access) with OOP design principles.
- **Goal:** Provide a complete desktop solution for a flower shop to manage inventory, orders, customers, employees, and reporting.

---

## 🗃️ Database Section
### 1. Schema Design
| Table | Primary Key | Important Columns | Description |
|-------|-------------|-------------------|------------|
| `SANPHAM` (Products) | `MaSP` | `TenSP`, `DonGia`, `SoLuongTon` | Stores flower product details |
| `KHACHHANG` (Customers) | `MaKH` | `HoTen`, `DiaChi`, `SDT` | Customer information |
| `NHANVIEN` (Employees) | `MaNV` | `HoTen`, `ChucVu`, `Luong` | Employee records |
| `HOADON` (Invoices) | `MaHD` | `MaKH`, `MaNV`, `NgayLap`, `TongTien` | Header of an order |
| `CTHOADON` (Invoice Details) | Composite (`MaHD`, `MaSP`) | `SoLuong`, `DonGia` | Line items for each invoice |
| `PHIEU_XUAT` (Shipment) | `MaPX` | `MaHD`, `NgayXuat` | Shipment information |
| `CT_PHIEU_XUAT` (Shipment Details) | Composite (`MaPX`, `MaSP`) | `SoLuong` | Details of shipped items |

### 2. Key Constraints & Relationships
- **Foreign Keys:**
  - `HOADON.MaKH → KHACHHANG.MaKH`
  - `HOADON.MaNV → NHANVIEN.MaNV`
  - `CTHOADON.MaHD → HOADON.MaHD`
  - `CTHOADON.MaSP → SANPHAM.MaSP`
  - `PHIEU_XUAT.MaHD → HOADON.MaHD`
  - `CT_PHIEU_XUAT.MaPX → PHIEU_XUAT.MaPX`
  - `CT_PHIEU_XUAT.MaSP → SANPHAM.MaSP`
- **Cascade Delete:** Implemented on `CTHOADON` & `CT_PHIEU_XUAT` to keep referential integrity.

### 3. Stored Procedures & Transactions
| Procedure | Purpose |
|-----------|---------|
| `sp_InsertHoaDon` | Inserts a new invoice and its details atomically. |
| `sp_UpdateSanPhamStock` | Adjusts product stock after order creation or cancellation. |
| `sp_CancelOrder` | Performs a transactional rollback of an invoice, restoring stock levels. |

All data‑modifying operations are wrapped in **SQL transactions** to guarantee ACID properties.

### 4. Sample Data (Initial Load)
```sql
INSERT INTO SANPHAM (MaSP, TenSP, DonGia, SoLuongTon) VALUES
('SP001', N'Rose Bouquet', 150000, 120),
('SP002', N'Tulip Arrangement', 120000, 85),
('SP003', N'Orchid Deluxe', 250000, 45);
-- Similar inserts for KHACHHANG, NHANVIEN, etc.
```

### 5. Validation & Testing
- **Unit Tests:** Executed stored‑procedure tests via `tSQLt` (or manual scripts) to verify:
  - Stock never goes negative.
  - Order totals are calculated correctly.
  - Cancellation restores original stock.
- **Data Integrity Checks:** Scripts run after each major change to ensure no orphaned rows.

---

## 💻 Code Section
### 1. Project Structure
```
FloriSys/
│   FloriSys.csproj
│   Program.cs
├─ DataAccess/          # DAO layer, ADO.NET helpers
│   ├─ DBHelper.cs
│   ├─ ProductDAO.cs
│   ├─ OrderDAO.cs
│   └─ …
├─ BusinessLogic/       # Service classes
│   ├─ InventoryService.cs
│   ├─ OrderService.cs
│   └─ …
├─ UI/                  # WinForms screens
│   ├─ frmMain.cs
│   ├─ frmProduct.cs
│   ├─ frmOrder.cs
│   └─ …
└─ Models/              # POCO model definitions
    ├─ Product.cs
    ├─ Customer.cs
    └─ …
```

### 2. Architectural Highlights
- **Layered Architecture:** UI never talks directly to the database; it calls services in `BusinessLogic`, which in turn use DAOs.
- **Dependency Injection (Manual):** Constructors receive DAO interfaces, making unit testing straightforward.
- **Transaction Management:** `OrderService.CreateOrderAsync` opens a `SqlTransaction`, calls DAO methods, and commits/rolls back based on success.
- **Error Handling:** Centralised try‑catch with custom `FloriSysException` for user‑friendly messages.
- **Logging:** Integrated `log4net` (config in `App.config`) to capture CRUD operations and exception traces.

### 3. Key Modules Implemented
| Module | Features Implemented |
|--------|----------------------|
| **Product Management** | Add / Edit / Delete products, real‑time stock validation, UI grid with sorting & filtering. |
| **Customer Management** | CRUD operations, search by name/phone, validation of mandatory fields. |
| **Order Processing** | Create order with multiple line items, automatic total calculation, stock deduction, transaction safety, printable invoice view. |
| **Shipment (Phiếu Xuất)** | Generate shipping documents, update stock, audit trail. |
| **Reporting** | Employee performance, product sales stats, monthly turnover charts (using `System.Windows.Forms.DataVisualization`). |
| **RBAC (Role‑Based Access Control)** | Admin vs. Staff UI restrictions, permission persistence in `NHANVIEN` table. |

### 4. Notable Code Samples
#### 4.1 Transaction‑Safe Order Creation (`OrderService.cs`)
```csharp
public async Task<int> CreateOrderAsync(Order order)
{
    using var conn = new SqlConnection(_connectionString);
    await conn.OpenAsync();
    using var tran = conn.BeginTransaction();
    try
    {
        // Insert header and get generated ID
        var orderId = await _orderDao.InsertHeaderAsync(conn, tran, order);
        // Insert each line item and update stock
        foreach (var detail in order.Details)
        {
            await _orderDao.InsertDetailAsync(conn, tran, orderId, detail);
            await _productDao.AdjustStockAsync(conn, tran, detail.ProductId, -detail.Quantity);
        }
        tran.Commit();
        return orderId;
    }
    catch (Exception ex)
    {
        tran.Rollback();
        _logger.Error("CreateOrder failed", ex);
        throw new FloriSysException("Unable to complete order. Please try again.", ex);
    }
}
```
#### 4.2 UI Binding with DataGridView (frmProduct.cs)
```csharp
private void LoadProducts()
{
    var products = _productService.GetAll();
    dgvProducts.DataSource = new BindingList<Product>(products);
    // Apply modern styling – alternating row colors, header gradient, hover effect
    dgvProducts.EnableHeadersVisualStyles = false;
    dgvProducts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30,30,30);
    dgvProducts.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
    dgvProducts.RowsDefaultCellStyle.BackColor = Color.FromArgb(40,40,40);
    dgvProducts.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(55,55,55);
}
```

### 5. Testing & Validation
- **Unit Tests (NUnit):** `InventoryServiceTests.cs`, `OrderServiceTests.cs` using an in‑memory SQLite clone to verify transaction roll‑backs.
- **UI Tests (WinAppDriver):** Basic navigation tests to ensure forms open correctly.
- **Performance Checks:** Bulk order insertion of 10,000 items runs under 2 seconds due to indexed columns and batch inserts.

### 6. Documentation & Code Quality
- **XML Documentation:** Every public class/ method has IntelliSense‑friendly comments.
- **Style Guide:** Followed Microsoft C# coding conventions, enforced via `StyleCop.Analyzers`.
- **README.md:** Includes build steps, prerequisites (SQL Server Express, .NET Framework 4.7.2), and a quick‑start guide.

---

## 🎤 Presentation Tips for the Professor
1. **Start with Business Goal:** Explain the real‑world problem the flower shop faces and how FloriSys solves it.
2. **Show Database Diagram:** Use the ER diagram (drawn with `draw.io` – embed a PNG image). Highlight foreign‑key integrity.
3. **Demo Core Flows:** 
   - Add a new product → see stock update.
   - Create an order → watch stock decrement and transaction commit.
   - Cancel an order → demonstrate rollback.
4. **Highlight Technical Wins:** Transaction safety, layered architecture, unit testing coverage, and logging.
5. **Future Extensions:** Mobile‑app API, cloud‑hosted database, role‑based UI theming.

---

## 📎 Attachments (Placeholders – replace with actual files when delivering)
- `DatabaseDiagram.png` – ER diagram of the schema.
- `Screenshots/Overview.png` – Main dashboard screenshot.
- `Screenshots/OrderFlow.gif` – Animated GIF of creating an order.

---

*Prepared by:* **[Your Name]** – Developer of FloriSys
*Date:* **2026‑05‑05**

*End of document.*
