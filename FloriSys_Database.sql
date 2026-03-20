-- =====================================================
-- FloriSys – Cơ sở dữ liệu Quản lý Cửa hàng Hoa
-- SQL Server 2022 – Developer Edition
-- =====================================================

USE master;
GO
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'FloriSys')
    ALTER DATABASE FloriSys SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
GO
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'FloriSys')
    DROP DATABASE FloriSys;
GO
CREATE DATABASE FloriSys;
GO
USE FloriSys;
GO

-- =====================================================
-- 1. BẢNG NHÂN VIÊN
-- =====================================================
CREATE TABLE NHAN_VIEN (
    MaNV        NVARCHAR(20)  PRIMARY KEY,
    HoTen       NVARCHAR(100) NOT NULL,
    ChucVu      NVARCHAR(20)  NOT NULL CHECK (ChucVu IN (N'Admin', N'Cashier', N'Warehouse', N'Shipper')),
    SoDienThoai NVARCHAR(15),
    TaiKhoan    NVARCHAR(50)  NOT NULL UNIQUE,
    MatKhau     NVARCHAR(256) NOT NULL, -- SHA-256 hash
    TrangThai   NVARCHAR(20)  NOT NULL DEFAULT N'DangLam' CHECK (TrangThai IN (N'DangLam', N'DaNghi'))
);
GO

-- =====================================================
-- 2. BẢNG KHÁCH HÀNG
-- =====================================================
CREATE TABLE KHACH_HANG (
    MaKH        NVARCHAR(20)  PRIMARY KEY,
    HoTen       NVARCHAR(100) NOT NULL,
    SoDienThoai NVARCHAR(15)  NOT NULL UNIQUE,
    DiaChi      NVARCHAR(200),
    Email       NVARCHAR(100),
    NgayTao     DATETIME      NOT NULL DEFAULT GETDATE()
);
GO

-- =====================================================
-- 3. BẢNG SẢN PHẨM
-- =====================================================
CREATE TABLE SAN_PHAM (
    MaSP          NVARCHAR(20)   PRIMARY KEY,
    TenSP         NVARCHAR(100)  NOT NULL,
    LoaiHoa       NVARCHAR(50),
    GiaBan        DECIMAL(18,0)  NOT NULL CHECK (GiaBan >= 0),
    GiaNhap       DECIMAL(18,0)  NOT NULL CHECK (GiaNhap >= 0),
    SoLuongTon    INT            NOT NULL DEFAULT 0 CHECK (SoLuongTon >= 0),
    MucTonToiThieu INT           NOT NULL DEFAULT 10,
    TrangThai     NVARCHAR(20)   NOT NULL DEFAULT N'DangBan' CHECK (TrangThai IN (N'DangBan', N'NgungBan'))
);
GO

-- =====================================================
-- 4. BẢNG ĐƠN HÀNG
-- =====================================================
CREATE TABLE DON_HANG (
    MaDon            NVARCHAR(20)   PRIMARY KEY,
    NgayTao          DATETIME       NOT NULL DEFAULT GETDATE(),
    MaKH             NVARCHAR(20)   NOT NULL REFERENCES KHACH_HANG(MaKH),
    MaNV_TaoDon      NVARCHAR(20)   NOT NULL REFERENCES NHAN_VIEN(MaNV),
    HinhThucNhanHang NVARCHAR(30)   NOT NULL CHECK (HinhThucNhanHang IN (N'TaiQuay', N'GiaoTanNoi')),
    TrangThai        NVARCHAR(20)   NOT NULL DEFAULT N'Moi' 
        CHECK (TrangThai IN (N'Moi', N'DangXuLy', N'DaGiao', N'HoanThanh', N'Huy', N'HoanHang')),
    TongTien         DECIMAL(18,0)  NOT NULL DEFAULT 0,
    GhiChu           NVARCHAR(500)
);
GO

-- =====================================================
-- 5. BẢNG CHI TIẾT ĐƠN HÀNG
-- =====================================================
CREATE TABLE CHI_TIET_DON_HANG (
    MaDon     NVARCHAR(20)   NOT NULL REFERENCES DON_HANG(MaDon),
    MaSP      NVARCHAR(20)   NOT NULL REFERENCES SAN_PHAM(MaSP),
    SoLuong   INT            NOT NULL CHECK (SoLuong > 0),
    DonGia    DECIMAL(18,0)  NOT NULL CHECK (DonGia >= 0),
    ThanhTien DECIMAL(18,0)  NOT NULL DEFAULT 0,
    PRIMARY KEY (MaDon, MaSP)
);
GO

-- =====================================================
-- 6. BẢNG GIAO HÀNG
-- =====================================================
CREATE TABLE GIAO_HANG (
    MaGiaoHang     NVARCHAR(20)  PRIMARY KEY,
    MaDon          NVARCHAR(20)  NOT NULL REFERENCES DON_HANG(MaDon),
    MaNV_Shipper   NVARCHAR(20)  REFERENCES NHAN_VIEN(MaNV),
    NgayGiao       DATETIME,
    TrangThai      NVARCHAR(20)  NOT NULL DEFAULT N'ChoPhanCong' 
        CHECK (TrangThai IN (N'ChoPhanCong', N'DangGiao', N'GiaoThanhCong', N'HoanHang', N'GiaoLai')),
    GhiChuGiaoHang NVARCHAR(500)
);
GO

-- =====================================================
-- 7. BẢNG PHIẾU NHẬP KHO
-- =====================================================
CREATE TABLE PHIEU_NHAP_KHO (
    MaPhieu  NVARCHAR(20)  PRIMARY KEY,
    NgayNhap DATETIME      NOT NULL DEFAULT GETDATE(),
    MaNV     NVARCHAR(20)  NOT NULL REFERENCES NHAN_VIEN(MaNV),
    GhiChu   NVARCHAR(500)
);
GO

-- =====================================================
-- 8. BẢNG CHI TIẾT NHẬP KHO
-- =====================================================
CREATE TABLE CT_NHAP_KHO (
    MaPhieu  NVARCHAR(20)   NOT NULL REFERENCES PHIEU_NHAP_KHO(MaPhieu),
    MaSP     NVARCHAR(20)   NOT NULL REFERENCES SAN_PHAM(MaSP),
    SoLuong  INT            NOT NULL CHECK (SoLuong > 0),
    GiaNhap  DECIMAL(18,0)  NOT NULL CHECK (GiaNhap >= 0),
    PRIMARY KEY (MaPhieu, MaSP)
);
GO

-- =====================================================
-- 9. BẢNG PHẢN HỒI
-- =====================================================
CREATE TABLE PHAN_HOI (
    MaPH          NVARCHAR(20)  PRIMARY KEY,
    MaDon         NVARCHAR(20)  NOT NULL REFERENCES DON_HANG(MaDon),
    NoiDung       NVARCHAR(1000) NOT NULL,
    NgayGhi       DATETIME      NOT NULL DEFAULT GETDATE(),
    TrangThaiXuLy NVARCHAR(30)  NOT NULL DEFAULT N'ChuaXuLy' 
        CHECK (TrangThaiXuLy IN (N'ChuaXuLy', N'DangXuLy', N'DaXuLy')),
    KetQuaXuLy    NVARCHAR(500)
);
GO

-- =====================================================
-- 10. BẢNG CẢNH BÁO TỒN KHO
-- =====================================================
CREATE TABLE CANH_BAO_TON_KHO (
    MaSP       NVARCHAR(20)  PRIMARY KEY REFERENCES SAN_PHAM(MaSP),
    MucToiThieu INT          NOT NULL DEFAULT 10,
    NgayCapNhat DATETIME     NOT NULL DEFAULT GETDATE()
);
GO

-- =====================================================
-- TRIGGERS
-- =====================================================

-- Trigger: Tự động tính ThanhTien khi INSERT/UPDATE CHI_TIET_DON_HANG
CREATE OR ALTER TRIGGER trg_TinhThanhTien
ON CHI_TIET_DON_HANG
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE ct SET ct.ThanhTien = ct.SoLuong * ct.DonGia
    FROM CHI_TIET_DON_HANG ct
    INNER JOIN inserted i ON ct.MaDon = i.MaDon AND ct.MaSP = i.MaSP;
END;
GO

-- Trigger: Tự động cập nhật TongTien của DON_HANG khi thay đổi CHI_TIET_DON_HANG
CREATE OR ALTER TRIGGER trg_CapNhatTongTien
ON CHI_TIET_DON_HANG
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    -- Cập nhật cho đơn trong inserted
    UPDATE dh SET dh.TongTien = ISNULL((SELECT SUM(ThanhTien) FROM CHI_TIET_DON_HANG WHERE MaDon = dh.MaDon), 0)
    FROM DON_HANG dh
    WHERE dh.MaDon IN (SELECT MaDon FROM inserted UNION SELECT MaDon FROM deleted);
END;
GO

-- Trigger: Tự động cập nhật tồn kho khi nhập kho
CREATE OR ALTER TRIGGER trg_NhapKho_TangTon
ON CT_NHAP_KHO
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE sp SET sp.SoLuongTon = sp.SoLuongTon + i.SoLuong
    FROM SAN_PHAM sp
    INNER JOIN inserted i ON sp.MaSP = i.MaSP;
END;
GO

-- =====================================================
-- STORED PROCEDURES
-- =====================================================

-- SP: Đăng nhập
CREATE OR ALTER PROCEDURE sp_DangNhap
    @TaiKhoan NVARCHAR(50),
    @MatKhau  NVARCHAR(256)
AS
BEGIN
    SELECT MaNV, HoTen, ChucVu, SoDienThoai, TaiKhoan, TrangThai
    FROM NHAN_VIEN
    WHERE TaiKhoan = @TaiKhoan AND MatKhau = @MatKhau AND TrangThai = N'DangLam';
END;
GO

-- SP: Đổi mật khẩu
CREATE OR ALTER PROCEDURE sp_DoiMatKhau
    @MaNV      NVARCHAR(20),
    @MatKhauCu NVARCHAR(256),
    @MatKhauMoi NVARCHAR(256)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM NHAN_VIEN WHERE MaNV = @MaNV AND MatKhau = @MatKhauCu)
    BEGIN
        UPDATE NHAN_VIEN SET MatKhau = @MatKhauMoi WHERE MaNV = @MaNV;
        SELECT 1 AS KetQua;
    END
    ELSE
        SELECT 0 AS KetQua;
END;
GO

-- SP: Tạo đơn hàng
CREATE OR ALTER PROCEDURE sp_TaoDonHang
    @MaDon            NVARCHAR(20),
    @MaKH             NVARCHAR(20),
    @MaNV_TaoDon      NVARCHAR(20),
    @HinhThucNhanHang NVARCHAR(30),
    @GhiChu           NVARCHAR(500) = NULL
AS
BEGIN
    INSERT INTO DON_HANG (MaDon, MaKH, MaNV_TaoDon, HinhThucNhanHang, GhiChu)
    VALUES (@MaDon, @MaKH, @MaNV_TaoDon, @HinhThucNhanHang, @GhiChu);
END;
GO

-- SP: Thêm chi tiết đơn hàng
CREATE OR ALTER PROCEDURE sp_ThemChiTietDon
    @MaDon  NVARCHAR(20),
    @MaSP   NVARCHAR(20),
    @SoLuong INT,
    @DonGia  DECIMAL(18,0)
AS
BEGIN
    -- Kiểm tra tồn kho
    DECLARE @TonKho INT;
    SELECT @TonKho = SoLuongTon FROM SAN_PHAM WHERE MaSP = @MaSP;
    IF @TonKho < @SoLuong
    BEGIN
        RAISERROR(N'Tồn kho không đủ! Còn %d, yêu cầu %d.', 16, 1, @TonKho, @SoLuong);
        RETURN;
    END
    INSERT INTO CHI_TIET_DON_HANG (MaDon, MaSP, SoLuong, DonGia, ThanhTien)
    VALUES (@MaDon, @MaSP, @SoLuong, @DonGia, @SoLuong * @DonGia);
END;
GO

-- SP: Cập nhật trạng thái đơn hàng
CREATE OR ALTER PROCEDURE sp_CapNhatTrangThaiDon
    @MaDon     NVARCHAR(20),
    @TrangThai NVARCHAR(20)
AS
BEGIN
    -- Khi chuyển sang DangXuLy → trừ tồn kho
    IF @TrangThai = N'DangXuLy'
    BEGIN
        -- Kiểm tra tồn kho đủ không
        IF EXISTS (
            SELECT 1 FROM CHI_TIET_DON_HANG ct
            INNER JOIN SAN_PHAM sp ON ct.MaSP = sp.MaSP
            WHERE ct.MaDon = @MaDon AND sp.SoLuongTon < ct.SoLuong
        )
        BEGIN
            RAISERROR(N'Không thể xử lý đơn – tồn kho không đủ cho một số sản phẩm.', 16, 1);
            RETURN;
        END
        -- Trừ tồn kho
        UPDATE sp SET sp.SoLuongTon = sp.SoLuongTon - ct.SoLuong
        FROM SAN_PHAM sp
        INNER JOIN CHI_TIET_DON_HANG ct ON sp.MaSP = ct.MaSP
        WHERE ct.MaDon = @MaDon;
    END
    -- Khi Hủy từ Moi → không cần hoàn kho (chưa trừ)
    -- Khi HoanHang từ DangXuLy → hoàn lại tồn kho
    IF @TrangThai = N'HoanHang'
    BEGIN
        DECLARE @TrangThaiHienTai NVARCHAR(20);
        SELECT @TrangThaiHienTai = TrangThai FROM DON_HANG WHERE MaDon = @MaDon;
        IF @TrangThaiHienTai IN (N'DangXuLy', N'DaGiao')
        BEGIN
            UPDATE sp SET sp.SoLuongTon = sp.SoLuongTon + ct.SoLuong
            FROM SAN_PHAM sp
            INNER JOIN CHI_TIET_DON_HANG ct ON sp.MaSP = ct.MaSP
            WHERE ct.MaDon = @MaDon;
        END
    END
    UPDATE DON_HANG SET TrangThai = @TrangThai WHERE MaDon = @MaDon;
END;
GO

-- SP: Tạo phiếu nhập kho
CREATE OR ALTER PROCEDURE sp_TaoPhieuNhap
    @MaPhieu NVARCHAR(20),
    @MaNV    NVARCHAR(20),
    @GhiChu  NVARCHAR(500) = NULL
AS
BEGIN
    INSERT INTO PHIEU_NHAP_KHO (MaPhieu, MaNV, GhiChu)
    VALUES (@MaPhieu, @MaNV, @GhiChu);
END;
GO

-- SP: Thêm chi tiết nhập kho (trigger sẽ tự tăng tồn kho)
CREATE OR ALTER PROCEDURE sp_ThemChiTietNhap
    @MaPhieu NVARCHAR(20),
    @MaSP    NVARCHAR(20),
    @SoLuong INT,
    @GiaNhap DECIMAL(18,0)
AS
BEGIN
    INSERT INTO CT_NHAP_KHO (MaPhieu, MaSP, SoLuong, GiaNhap)
    VALUES (@MaPhieu, @MaSP, @SoLuong, @GiaNhap);
END;
GO

-- SP: Ghi nhận hàng hư (giảm tồn kho)
CREATE OR ALTER PROCEDURE sp_GhiNhanHangHu
    @MaSP    NVARCHAR(20),
    @SoLuong INT
AS
BEGIN
    DECLARE @TonHienTai INT;
    SELECT @TonHienTai = SoLuongTon FROM SAN_PHAM WHERE MaSP = @MaSP;
    IF @TonHienTai < @SoLuong
    BEGIN
        RAISERROR(N'Không thể hủy nhiều hơn số tồn kho hiện tại.', 16, 1);
        RETURN;
    END
    UPDATE SAN_PHAM SET SoLuongTon = SoLuongTon - @SoLuong WHERE MaSP = @MaSP;
END;
GO

-- SP: Tạo lệnh giao hàng
CREATE OR ALTER PROCEDURE sp_TaoGiaoHang
    @MaGiaoHang NVARCHAR(20),
    @MaDon      NVARCHAR(20),
    @GhiChu     NVARCHAR(500) = NULL
AS
BEGIN
    INSERT INTO GIAO_HANG (MaGiaoHang, MaDon, GhiChuGiaoHang)
    VALUES (@MaGiaoHang, @MaDon, @GhiChu);
END;
GO

-- SP: Phân công shipper
CREATE OR ALTER PROCEDURE sp_PhanCongShipper
    @MaGiaoHang  NVARCHAR(20),
    @MaNV_Shipper NVARCHAR(20)
AS
BEGIN
    UPDATE GIAO_HANG 
    SET MaNV_Shipper = @MaNV_Shipper, TrangThai = N'DangGiao', NgayGiao = GETDATE()
    WHERE MaGiaoHang = @MaGiaoHang;
END;
GO

-- SP: Cập nhật trạng thái giao hàng
CREATE OR ALTER PROCEDURE sp_CapNhatTrangThaiGiao
    @MaGiaoHang NVARCHAR(20),
    @TrangThai  NVARCHAR(20),
    @GhiChu     NVARCHAR(500) = NULL
AS
BEGIN
    UPDATE GIAO_HANG 
    SET TrangThai = @TrangThai, 
        GhiChuGiaoHang = ISNULL(@GhiChu, GhiChuGiaoHang)
    WHERE MaGiaoHang = @MaGiaoHang;
END;
GO

-- SP: Ghi nhận phản hồi
CREATE OR ALTER PROCEDURE sp_GhiNhanPhanHoi
    @MaPH    NVARCHAR(20),
    @MaDon   NVARCHAR(20),
    @NoiDung NVARCHAR(1000)
AS
BEGIN
    INSERT INTO PHAN_HOI (MaPH, MaDon, NoiDung)
    VALUES (@MaPH, @MaDon, @NoiDung);
END;
GO

-- SP: Báo cáo doanh thu theo ngày
CREATE OR ALTER PROCEDURE sp_BaoCaoDoanhThuNgay
    @Ngay DATE
AS
BEGIN
    SELECT 
        COUNT(*) AS TongDon,
        ISNULL(SUM(TongTien), 0) AS TongDoanhThu,
        ISNULL(SUM(CASE WHEN TrangThai = N'HoanThanh' THEN TongTien ELSE 0 END), 0) AS DoanhThuHoanThanh
    FROM DON_HANG
    WHERE CAST(NgayTao AS DATE) = @Ngay AND TrangThai NOT IN (N'Huy');
END;
GO

-- SP: Báo cáo doanh thu theo tháng
CREATE OR ALTER PROCEDURE sp_BaoCaoDoanhThuThang
    @Thang INT,
    @Nam   INT
AS
BEGIN
    SELECT 
        COUNT(*) AS TongDon,
        ISNULL(SUM(TongTien), 0) AS TongDoanhThu
    FROM DON_HANG
    WHERE MONTH(NgayTao) = @Thang AND YEAR(NgayTao) = @Nam AND TrangThai NOT IN (N'Huy');
END;
GO

-- SP: Sản phẩm bán chạy
CREATE OR ALTER PROCEDURE sp_SanPhamBanChay
    @Thang INT = NULL,
    @Nam   INT = NULL
AS
BEGIN
    SELECT TOP 10
        sp.MaSP, sp.TenSP, sp.LoaiHoa,
        SUM(ct.SoLuong) AS TongSoLuong,
        SUM(ct.ThanhTien) AS TongDoanhThu
    FROM CHI_TIET_DON_HANG ct
    INNER JOIN SAN_PHAM sp ON ct.MaSP = sp.MaSP
    INNER JOIN DON_HANG dh ON ct.MaDon = dh.MaDon
    WHERE dh.TrangThai NOT IN (N'Huy', N'HoanHang')
        AND (@Thang IS NULL OR MONTH(dh.NgayTao) = @Thang)
        AND (@Nam IS NULL OR YEAR(dh.NgayTao) = @Nam)
    GROUP BY sp.MaSP, sp.TenSP, sp.LoaiHoa
    ORDER BY TongSoLuong DESC;
END;
GO

-- SP: Hiệu suất nhân viên
CREATE OR ALTER PROCEDURE sp_HieuSuatNhanVien
    @Thang INT = NULL,
    @Nam   INT = NULL
AS
BEGIN
    SELECT 
        nv.MaNV, nv.HoTen, nv.ChucVu,
        COUNT(dh.MaDon) AS SoDonTao,
        ISNULL(SUM(dh.TongTien), 0) AS TongDoanhThu,
        SUM(CASE WHEN dh.TrangThai = N'Huy' THEN 1 ELSE 0 END) AS DonHuy
    FROM NHAN_VIEN nv
    LEFT JOIN DON_HANG dh ON nv.MaNV = dh.MaNV_TaoDon
        AND (@Thang IS NULL OR MONTH(dh.NgayTao) = @Thang)
        AND (@Nam IS NULL OR YEAR(dh.NgayTao) = @Nam)
    WHERE nv.ChucVu = N'Cashier'
    GROUP BY nv.MaNV, nv.HoTen, nv.ChucVu
    ORDER BY TongDoanhThu DESC;
END;
GO

-- SP: Lấy cảnh báo tồn kho
CREATE OR ALTER PROCEDURE sp_CanhBaoTonKho
AS
BEGIN
    SELECT sp.MaSP, sp.TenSP, sp.LoaiHoa, sp.SoLuongTon, sp.MucTonToiThieu,
        CASE 
            WHEN sp.SoLuongTon = 0 THEN N'HetHang'
            WHEN sp.SoLuongTon <= sp.MucTonToiThieu THEN N'SapHet'
            ELSE N'DuHang'
        END AS TinhTrang
    FROM SAN_PHAM sp
    WHERE sp.TrangThai = N'DangBan'
    ORDER BY sp.SoLuongTon ASC;
END;
GO

-- SP: Sinh mã tự động
CREATE OR ALTER PROCEDURE sp_SinhMa
    @Prefix NVARCHAR(10),
    @Table  NVARCHAR(50),
    @Column NVARCHAR(50),
    @NewCode NVARCHAR(20) OUTPUT
AS
BEGIN
    DECLARE @SQL NVARCHAR(500);
    DECLARE @MaxNum INT;
    SET @SQL = N'SELECT @Num = ISNULL(MAX(CAST(SUBSTRING(' + @Column + N', LEN(''' + @Prefix + N''')+1, 10) AS INT)), 0) FROM ' + @Table;
    EXEC sp_executesql @SQL, N'@Num INT OUTPUT', @Num = @MaxNum OUTPUT;
    SET @NewCode = @Prefix + RIGHT('000000' + CAST(@MaxNum + 1 AS NVARCHAR), 6);
END;
GO

-- =====================================================
-- DỮ LIỆU MẪU
-- =====================================================

-- Nhân viên (mật khẩu = SHA-256 của '123456' = '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92')
INSERT INTO NHAN_VIEN VALUES 
(N'NV001', N'Nguyễn Lê Minh Vũ',  N'Admin',     N'0901111222', N'admin',     N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'DangLam'),
(N'NV002', N'Trần Thu Hương',      N'Cashier',   N'0912333444', N'thuhuong',  N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'DangLam'),
(N'NV003', N'Lê Minh Khoa',        N'Warehouse', N'0923555666', N'minhkho',   N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'DangLam'),
(N'NV004', N'Nguyễn Văn Sơn',      N'Shipper',   N'0934777888', N'shipper1',  N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'DangLam'),
(N'NV005', N'Hoàng Thị Xuân',      N'Cashier',   N'0945999000', N'xuanxuan',  N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'DaNghi');

-- Sản phẩm
INSERT INTO SAN_PHAM VALUES
(N'SP001', N'Hoa hồng đỏ (bó 10)',    N'Hoa tươi', 180000, 120000, 42, 10, N'DangBan'),
(N'SP002', N'Tulip hồng (bó 5)',       N'Hoa tươi', 220000, 160000, 28, 10, N'DangBan'),
(N'SP003', N'Cúc vàng (bó 10)',        N'Hoa tươi', 80000,  45000,  3,  10, N'DangBan'),
(N'SP004', N'Ly trắng (bó 5)',         N'Hoa tươi', 150000, 100000, 8,  10, N'DangBan'),
(N'SP005', N'Hoa anh đào (bó 7)',      N'Hoa tươi', 160000, 110000, 15, 5,  N'DangBan'),
(N'SP006', N'Lá decor xanh',           N'Phụ kiện', 30000,  20000,  0,  5,  N'DangBan'),
(N'SP007', N'Ruy băng trang trí',      N'Phụ kiện', 15000,  8000,   200,50, N'DangBan'),
(N'SP008', N'Hoa lay ơn (bó 10)',      N'Hoa tươi', 90000,  55000,  0,  10, N'NgungBan');

-- Khách hàng
INSERT INTO KHACH_HANG VALUES
(N'KH001', N'Nguyễn Thị Lan',  N'0901234567', N'12 Lê Lợi, Q.1, TP.HCM',       N'lan.nt@gmail.com',  '2025-06-15'),
(N'KH002', N'Trần Văn Hùng',   N'0912345678', N'45 Nguyễn Huệ, Q.1, TP.HCM',   N'hung.tv@gmail.com', '2025-09-01'),
(N'KH003', N'Lê Thị Mai',      N'0923456789', NULL,                              NULL,                 '2026-01-20'),
(N'KH004', N'Phạm Văn An',     N'0934567890', N'78 Trần Phú, Q.5, TP.HCM',      N'an.pv@email.com',   '2025-03-10');

-- Đơn hàng mẫu
INSERT INTO DON_HANG VALUES
(N'DH000001', '2026-03-11 09:15', N'KH001', N'NV002', N'GiaoTanNoi', N'Moi',      0, N'Giao trước 10h sáng'),
(N'DH000002', '2026-03-11 08:40', N'KH002', N'NV002', N'GiaoTanNoi', N'DangXuLy', 0, N'Gói quà đẹp, có thiệp'),
(N'DH000003', '2026-03-11 08:20', N'KH003', N'NV002', N'TaiQuay',    N'HoanThanh',0, NULL),
(N'DH000004', '2026-03-10 17:55', N'KH004', N'NV002', N'GiaoTanNoi', N'DaGiao',   0, NULL);

-- Chi tiết đơn hàng
INSERT INTO CHI_TIET_DON_HANG (MaDon, MaSP, SoLuong, DonGia, ThanhTien) VALUES
(N'DH000001', N'SP001', 2, 180000, 360000),
(N'DH000001', N'SP002', 1, 220000, 220000),
(N'DH000002', N'SP001', 1, 180000, 180000),
(N'DH000002', N'SP002', 1, 220000, 220000),
(N'DH000003', N'SP003', 2, 80000,  160000),
(N'DH000004', N'SP001', 1, 180000, 180000),
(N'DH000004', N'SP004', 2, 150000, 300000);

-- Cập nhật TongTien dựa trên chi tiết
UPDATE dh SET dh.TongTien = (SELECT ISNULL(SUM(ThanhTien),0) FROM CHI_TIET_DON_HANG WHERE MaDon = dh.MaDon)
FROM DON_HANG dh;

-- Giao hàng
INSERT INTO GIAO_HANG VALUES
(N'GH000001', N'DH000001', N'NV004', NULL,                  N'ChoPhanCong', NULL),
(N'GH000002', N'DH000002', N'NV004', '2026-03-11 09:30',    N'DangGiao',    N'Có thiệp sinh nhật'),
(N'GH000004', N'DH000004', N'NV004', '2026-03-10 18:30',    N'GiaoThanhCong', NULL);

-- Phiếu nhập kho
INSERT INTO PHIEU_NHAP_KHO VALUES
(N'PN000001', '2026-03-10 07:30', N'NV003', N'Hàng từ vựa Bình Điền');

INSERT INTO CT_NHAP_KHO VALUES
(N'PN000001', N'SP001', 50, 120000),
(N'PN000001', N'SP002', 30, 160000);

-- NOTE: Trigger sẽ tự động cộng tồn kho. Do data mẫu đã set sẵn SoLuongTon nên bỏ qua.
-- Trong thực tế, khi INSERT vào CT_NHAP_KHO trigger sẽ tự cập nhật.
-- Resetlại tồn kho đúng cho data mẫu:
UPDATE SAN_PHAM SET SoLuongTon = 42 WHERE MaSP = N'SP001';
UPDATE SAN_PHAM SET SoLuongTon = 28 WHERE MaSP = N'SP002';

-- Phản hồi
INSERT INTO PHAN_HOI VALUES
(N'PH000001', N'DH000004', N'Khách phản hồi hoa hồng bị héo sau 1 ngày', '2026-03-10 14:00', N'DangXuLy', NULL);

PRINT N'✅ Database FloriSys đã được tạo thành công!';
GO
