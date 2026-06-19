-- =====================================================
-- FloriSys – Cơ sở dữ liệu Quản lý Cửa hàng Hoa
-- SQL Server 2022 – Developer Edition
-- Script có thể chạy lại an toàn (idempotent)
-- =====================================================

IF DB_ID('FloriSys') IS NULL CREATE DATABASE FloriSys;
GO
USE FloriSys;
GO

-- =====================================================
-- DROP tất cả bảng theo đúng thứ tự (con trước, cha sau)
-- để đảm bảo chạy lại an toàn
-- =====================================================
IF OBJECT_ID('CT_TRA_HANG', 'U') IS NOT NULL DROP TABLE CT_TRA_HANG;
IF OBJECT_ID('TRA_HANG', 'U') IS NOT NULL DROP TABLE TRA_HANG;
IF OBJECT_ID('LICH_SU_DON_HANG', 'U') IS NOT NULL DROP TABLE LICH_SU_DON_HANG;
IF OBJECT_ID('PHAN_HOI', 'U') IS NOT NULL DROP TABLE PHAN_HOI;
IF OBJECT_ID('CT_NHAP_KHO', 'U') IS NOT NULL DROP TABLE CT_NHAP_KHO;
IF OBJECT_ID('PHIEU_NHAP_KHO', 'U') IS NOT NULL DROP TABLE PHIEU_NHAP_KHO;
IF OBJECT_ID('GIAO_HANG', 'U') IS NOT NULL DROP TABLE GIAO_HANG;
IF OBJECT_ID('CHI_TIET_DON_HANG', 'U') IS NOT NULL DROP TABLE CHI_TIET_DON_HANG;
IF OBJECT_ID('DON_HANG', 'U') IS NOT NULL DROP TABLE DON_HANG;
IF OBJECT_ID('HANG_HU', 'U') IS NOT NULL DROP TABLE HANG_HU;
IF OBJECT_ID('PHAN_QUYEN', 'U') IS NOT NULL DROP TABLE PHAN_QUYEN;
IF OBJECT_ID('SAN_PHAM', 'U') IS NOT NULL DROP TABLE SAN_PHAM;
IF OBJECT_ID('KHACH_HANG', 'U') IS NOT NULL DROP TABLE KHACH_HANG;
IF OBJECT_ID('NHAN_VIEN', 'U') IS NOT NULL DROP TABLE NHAN_VIEN;
GO

-- 1. BẢNG NHÂN VIÊN
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

-- 2. BẢNG KHÁCH HÀNG
CREATE TABLE KHACH_HANG (
    MaKH        NVARCHAR(20)  PRIMARY KEY,
    HoTen       NVARCHAR(100) NOT NULL,
    SoDienThoai NVARCHAR(15)  NOT NULL UNIQUE,
    DiaChi      NVARCHAR(200),
    Email       NVARCHAR(100),
    NgayTao     DATETIME      NOT NULL DEFAULT GETDATE()
);
GO

-- 3. BẢNG SẢN PHẨM
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

-- 4. BẢNG ĐƠN HÀNG
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

-- 5. BẢNG CHI TIẾT ĐƠN HÀNG
CREATE TABLE CHI_TIET_DON_HANG (
    MaDon     NVARCHAR(20)   NOT NULL REFERENCES DON_HANG(MaDon),
    MaSP      NVARCHAR(20)   NOT NULL REFERENCES SAN_PHAM(MaSP),
    SoLuong   INT            NOT NULL CHECK (SoLuong > 0),
    DonGia    DECIMAL(18,0)  NOT NULL CHECK (DonGia >= 0),
    ThanhTien DECIMAL(18,0)  NOT NULL DEFAULT 0,
    PRIMARY KEY (MaDon, MaSP)
);
GO

-- 6. BẢNG GIAO HÀNG
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

-- 7. BẢNG PHIẾU NHẬP KHO
CREATE TABLE PHIEU_NHAP_KHO (
    MaPhieu  NVARCHAR(20)  PRIMARY KEY,
    NgayNhap DATETIME      NOT NULL DEFAULT GETDATE(),
    MaNV     NVARCHAR(20)  NOT NULL REFERENCES NHAN_VIEN(MaNV),
    GhiChu   NVARCHAR(500)
);
GO

-- 8. BẢNG CHI TIẾT NHẬP KHO
CREATE TABLE CT_NHAP_KHO (
    MaPhieu  NVARCHAR(20)   NOT NULL REFERENCES PHIEU_NHAP_KHO(MaPhieu),
    MaSP     NVARCHAR(20)   NOT NULL REFERENCES SAN_PHAM(MaSP),
    SoLuong  INT            NOT NULL CHECK (SoLuong > 0),
    GiaNhap  DECIMAL(18,0)  NOT NULL CHECK (GiaNhap >= 0),
    PRIMARY KEY (MaPhieu, MaSP)
);
GO

-- 9. BẢNG PHẢN HỒI
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

-- 11. BẢNG HÀNG HƯ (Lịch sử hủy hàng)
CREATE TABLE HANG_HU (
    MaPhieuHuy  NVARCHAR(20)    PRIMARY KEY,
    MaSP        NVARCHAR(20)    NOT NULL REFERENCES SAN_PHAM(MaSP),
    SoLuong     INT             NOT NULL CHECK (SoLuong > 0),
    LyDo        NVARCHAR(200)   NOT NULL,
    NgayHuy     DATETIME        DEFAULT GETDATE(),
    GhiChu      NVARCHAR(500)   NULL
);
GO

-- 12. BẢNG PHÂN QUYỀN
CREATE TABLE PHAN_QUYEN (
    ChucVu   NVARCHAR(20)  NOT NULL,
    Module   NVARCHAR(50)  NOT NULL,
    Xem      BIT           NOT NULL DEFAULT 0,
    Them     BIT           NOT NULL DEFAULT 0,
    Sua      BIT           NOT NULL DEFAULT 0,
    Xoa      BIT           NOT NULL DEFAULT 0,
    Export   BIT           NOT NULL DEFAULT 0,
    PRIMARY KEY (ChucVu, Module)
);
GO

-- 13. BẢNG TRẢ HÀNG
CREATE TABLE TRA_HANG (
    MaPhieuTra      NVARCHAR(20)   PRIMARY KEY,
    MaDon           NVARCHAR(20)   NOT NULL REFERENCES DON_HANG(MaDon),
    LyDo            NVARCHAR(500)  NOT NULL,
    HinhThucHoanTien NVARCHAR(50)  NOT NULL DEFAULT N'TienMat'
        CHECK (HinhThucHoanTien IN (N'TienMat', N'ChuyenKhoan', N'DoiHang')),
    GhiChu          NVARCHAR(500),
    NgayTra         DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

-- 14. BẢNG CHI TIẾT TRẢ HÀNG
CREATE TABLE CT_TRA_HANG (
    MaPhieuTra  NVARCHAR(20)  NOT NULL REFERENCES TRA_HANG(MaPhieuTra),
    MaSP        NVARCHAR(20)  NOT NULL REFERENCES SAN_PHAM(MaSP),
    SoLuong     INT           NOT NULL CHECK (SoLuong > 0),
    CoNhapKho   BIT           NOT NULL DEFAULT 0,
    PRIMARY KEY (MaPhieuTra, MaSP)
);
GO

-- =====================================================
-- 15. BẢNG LỊCH SỬ TRẠNG THÁI ĐƠN HÀNG
-- =====================================================
CREATE TABLE LICH_SU_DON_HANG (
    Id           INT IDENTITY(1,1) PRIMARY KEY,
    MaDon        NVARCHAR(20)  NOT NULL REFERENCES DON_HANG(MaDon),
    TrangThai    NVARCHAR(20)  NOT NULL,
    ThoiGian     DATETIME      NOT NULL DEFAULT GETDATE(),
    GhiChu       NVARCHAR(500)
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

-- Trigger: Tự động ghi log khi INSERT đơn mới
CREATE OR ALTER TRIGGER trg_DonHang_Insert_Log
ON DON_HANG
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO LICH_SU_DON_HANG (MaDon, TrangThai, ThoiGian, GhiChu)
    SELECT MaDon, TrangThai, GETDATE(), N'Tạo đơn hàng mới'
    FROM inserted;
END;
GO

-- Trigger: Tự động ghi log khi UPDATE trạng thái
CREATE OR ALTER TRIGGER trg_DonHang_Update_Log
ON DON_HANG
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    -- Chỉ ghi log khi TrangThai thay đổi
    IF UPDATE(TrangThai)
    BEGIN
        INSERT INTO LICH_SU_DON_HANG (MaDon, TrangThai, ThoiGian, GhiChu)
        SELECT i.MaDon, i.TrangThai, GETDATE(),
            CASE i.TrangThai
                WHEN N'Moi'       THEN N'Đơn hàng mới'
                WHEN N'DangXuLy'  THEN N'Đã xuất kho - đang xử lý'
                WHEN N'DaGiao'    THEN N'Đã giao cho shipper'
                WHEN N'HoanThanh' THEN N'Đơn hàng hoàn thành'
                WHEN N'Huy'       THEN N'Đơn hàng bị hủy'
                WHEN N'HoanHang'  THEN N'Khách yêu cầu hoàn hàng'
                ELSE N'Cập nhật trạng thái: ' + i.TrangThai
            END
        FROM inserted i
        INNER JOIN deleted d ON i.MaDon = d.MaDon
        WHERE i.TrangThai <> d.TrangThai;
    END
END;
GO

-- =====================================================
-- STORED PROCEDURES
-- =====================================================

-- SP: Đăng nhập
DROP PROCEDURE IF EXISTS sp_DangNhap;
GO
CREATE PROCEDURE sp_DangNhap
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
DROP PROCEDURE IF EXISTS sp_DoiMatKhau;
GO
CREATE PROCEDURE sp_DoiMatKhau
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
DROP PROCEDURE IF EXISTS sp_TaoDonHang;
GO
CREATE PROCEDURE sp_TaoDonHang
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
DROP PROCEDURE IF EXISTS sp_ThemChiTietDon;
GO
CREATE PROCEDURE sp_ThemChiTietDon
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

    -- TRỪ TỒN KHO NGAY LẬP TỨC (Phương án A)
    UPDATE SAN_PHAM 
    SET SoLuongTon = SoLuongTon - @SoLuong 
    WHERE MaSP = @MaSP;
END;
GO

-- SP: Cập nhật trạng thái đơn hàng
DROP PROCEDURE IF EXISTS sp_CapNhatTrangThaiDon;
GO
CREATE PROCEDURE sp_CapNhatTrangThaiDon
    @MaDon     NVARCHAR(20),
    @TrangThai NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    -- Khi Hủy đơn hàng (từ trạng thái Moi hoặc DangXuLy) -> Tiến hành hoàn trả lại tồn kho (Phương án A)
    IF @TrangThai = N'Huy'
    BEGIN
        DECLARE @TrangThaiHienTai NVARCHAR(20);
        SELECT @TrangThaiHienTai = TrangThai FROM DON_HANG WHERE MaDon = @MaDon;

        -- Cả hai trạng thái Moi và DangXuLy đều đã bị trừ tồn kho trước đó, nên đều phải hoàn kho
        IF @TrangThaiHienTai IN (N'Moi', N'DangXuLy')
        BEGIN
            UPDATE sp 
            SET sp.SoLuongTon = sp.SoLuongTon + ct.SoLuong
            FROM SAN_PHAM sp
            INNER JOIN CHI_TIET_DON_HANG ct ON sp.MaSP = ct.MaSP
            WHERE ct.MaDon = @MaDon;
        END
    END

    -- HoanHang: KHÔNG hoàn kho tại đây. Tồn kho được quản lý bởi phiếu TRA_HANG (CT_TRA_HANG.CoNhapKho)
    UPDATE DON_HANG SET TrangThai = @TrangThai WHERE MaDon = @MaDon;
END;
GO

-- SP: Tạo phiếu nhập kho
DROP PROCEDURE IF EXISTS sp_TaoPhieuNhap;
GO
CREATE PROCEDURE sp_TaoPhieuNhap
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
DROP PROCEDURE IF EXISTS sp_ThemChiTietNhap;
GO
CREATE PROCEDURE sp_ThemChiTietNhap
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

-- SP: Ghi nhận hàng hư (giảm tồn kho + lưu lịch sử)
DROP PROCEDURE IF EXISTS sp_GhiNhanHangHu;
GO
CREATE PROCEDURE sp_GhiNhanHangHu
    @MaPhieuHuy NVARCHAR(20),
    @MaSP       NVARCHAR(20),
    @SoLuong    INT,
    @LyDo       NVARCHAR(200),
    @GhiChu     NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @TonHienTai INT;
    SELECT @TonHienTai = SoLuongTon FROM SAN_PHAM WHERE MaSP = @MaSP;
    IF @TonHienTai IS NULL
    BEGIN
        RAISERROR(N'Sản phẩm không tồn tại.', 16, 1);
        RETURN;
    END
    IF @TonHienTai < @SoLuong
    BEGIN
        RAISERROR(N'Không thể hủy nhiều hơn số tồn kho hiện tại.', 16, 1);
        RETURN;
    END
    INSERT INTO HANG_HU (MaPhieuHuy, MaSP, SoLuong, LyDo, GhiChu)
    VALUES (@MaPhieuHuy, @MaSP, @SoLuong, @LyDo, @GhiChu);
    UPDATE SAN_PHAM SET SoLuongTon = SoLuongTon - @SoLuong WHERE MaSP = @MaSP;
END;
GO

-- SP: Tạo lệnh giao hàng
DROP PROCEDURE IF EXISTS sp_TaoGiaoHang;
GO
CREATE PROCEDURE sp_TaoGiaoHang
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
DROP PROCEDURE IF EXISTS sp_PhanCongShipper;
GO
CREATE PROCEDURE sp_PhanCongShipper
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
DROP PROCEDURE IF EXISTS sp_CapNhatTrangThaiGiao;
GO
CREATE PROCEDURE sp_CapNhatTrangThaiGiao
    @MaGiaoHang NVARCHAR(20),
    @TrangThai  NVARCHAR(20),
    @GhiChu     NVARCHAR(500) = NULL
AS
BEGIN
    UPDATE GIAO_HANG 
    SET TrangThai = @TrangThai, 
        GhiChuGiaoHang = ISNULL(@GhiChu, GhiChuGiaoHang),
        NgayGiao = CASE WHEN @TrangThai = N'GiaoThanhCong' THEN GETDATE() ELSE NgayGiao END
    WHERE MaGiaoHang = @MaGiaoHang;

    IF @TrangThai = N'GiaoThanhCong'
    BEGIN
        UPDATE DON_HANG 
        SET TrangThai = N'DaGiao' 
        WHERE MaDon = (SELECT MaDon FROM GIAO_HANG WHERE MaGiaoHang = @MaGiaoHang);
    END
    ELSE IF @TrangThai = N'HoanHang'
    BEGIN
        DECLARE @MaDonHoan NVARCHAR(20);
        SELECT @MaDonHoan = MaDon FROM GIAO_HANG WHERE MaGiaoHang = @MaGiaoHang;
        
        UPDATE DON_HANG 
        SET TrangThai = N'HoanHang' 
        WHERE MaDon = @MaDonHoan;
        
        -- KHÔNG hoàn kho tại đây. Tồn kho được quản lý bởi phiếu TRA_HANG (CT_TRA_HANG.CoNhapKho)
        -- Cashier sẽ tạo phiếu trả hàng với chi tiết từng SP + quyết định nhập lại kho hay không
    END
    ELSE IF @TrangThai = N'GiaoLai' OR @TrangThai = N'DangGiao'
    BEGIN
        -- Đổi lại thành 'DangXuLy' vì bảng DON_HANG không có trạng thái 'DangGiao'
        UPDATE DON_HANG 
        SET TrangThai = N'DangXuLy' 
        WHERE MaDon = (SELECT MaDon FROM GIAO_HANG WHERE MaGiaoHang = @MaGiaoHang);
    END
END;
GO

-- SP: Ghi nhận phản hồi
DROP PROCEDURE IF EXISTS sp_GhiNhanPhanHoi;
GO
CREATE PROCEDURE sp_GhiNhanPhanHoi
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
DROP PROCEDURE IF EXISTS sp_BaoCaoDoanhThuNgay;
GO
CREATE PROCEDURE sp_BaoCaoDoanhThuNgay
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
DROP PROCEDURE IF EXISTS sp_BaoCaoDoanhThuThang;
GO
CREATE PROCEDURE sp_BaoCaoDoanhThuThang
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
DROP PROCEDURE IF EXISTS sp_SanPhamBanChay;
GO
CREATE PROCEDURE sp_SanPhamBanChay
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

-- SP: Sản phẩm bán ế (ít bán nhất)
-- FIX: Stored procedure này bị thiếu trong script gốc nhưng code C# gọi nó
DROP PROCEDURE IF EXISTS sp_SanPhamE;
GO
CREATE PROCEDURE sp_SanPhamE
    @Thang INT = NULL,
    @Nam   INT = NULL
AS
BEGIN
    SELECT TOP 10
        sp.MaSP, sp.TenSP, sp.LoaiHoa,
        ISNULL(SUM(ct.SoLuong), 0) AS TongSoLuong,
        ISNULL(SUM(ct.ThanhTien), 0) AS TongDoanhThu
    FROM SAN_PHAM sp
    LEFT JOIN CHI_TIET_DON_HANG ct ON ct.MaSP = sp.MaSP
    LEFT JOIN DON_HANG dh ON ct.MaDon = dh.MaDon
        AND dh.TrangThai NOT IN (N'Huy', N'HoanHang')
        AND (@Thang IS NULL OR MONTH(dh.NgayTao) = @Thang)
        AND (@Nam IS NULL OR YEAR(dh.NgayTao) = @Nam)
    WHERE sp.TrangThai = N'DangBan'
    GROUP BY sp.MaSP, sp.TenSP, sp.LoaiHoa
    ORDER BY TongSoLuong ASC;
END;
GO

-- SP: Hiệu suất nhân viên
DROP PROCEDURE IF EXISTS sp_HieuSuatNhanVien;
GO
CREATE PROCEDURE sp_HieuSuatNhanVien
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
DROP PROCEDURE IF EXISTS sp_CanhBaoTonKho;
GO
CREATE PROCEDURE sp_CanhBaoTonKho
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
DROP PROCEDURE IF EXISTS sp_SinhMa;
GO
CREATE PROCEDURE sp_SinhMa
    @Prefix NVARCHAR(10),
    @Table  NVARCHAR(50),
    @Column NVARCHAR(50),
    @NewCode NVARCHAR(20) OUTPUT
AS
BEGIN
    DECLARE @SQL NVARCHAR(500);
    DECLARE @MaxNum INT;
    SET @SQL = N'SELECT @Num = ISNULL(MAX(CAST(SUBSTRING(' + QUOTENAME(@Column) + N', LEN(''' + @Prefix + N''')+1, 10) AS INT)), 0) FROM ' + QUOTENAME(@Table);
    EXEC sp_executesql @SQL, N'@Num INT OUTPUT', @Num = @MaxNum OUTPUT;
    SET @NewCode = @Prefix + RIGHT('000000' + CAST(@MaxNum + 1 AS NVARCHAR), 6);
END;
GO

-- SP: Doanh thu theo ngày trong tháng (biểu đồ báo cáo tháng)
DROP PROCEDURE IF EXISTS sp_DoanhThuTheoNgayTrongThang;
GO
CREATE PROCEDURE sp_DoanhThuTheoNgayTrongThang
    @Thang INT,
    @Nam   INT
AS
BEGIN
    SET NOCOUNT ON;
    ;WITH DaysInMonth AS (
        SELECT DATEFROMPARTS(@Nam, @Thang, 1) AS Ngay
        UNION ALL
        SELECT DATEADD(DAY, 1, Ngay)
        FROM DaysInMonth
        WHERE DATEADD(DAY, 1, Ngay) < DATEADD(MONTH, 1, DATEFROMPARTS(@Nam, @Thang, 1))
    )
    SELECT 
        d.Ngay,
        DAY(d.Ngay) AS NgayTrongThang,
        ISNULL(SUM(dh.TongTien), 0) AS DoanhThu,
        COUNT(dh.MaDon) AS SoDon
    FROM DaysInMonth d
    LEFT JOIN DON_HANG dh 
        ON CAST(dh.NgayTao AS DATE) = d.Ngay 
        AND dh.TrangThai NOT IN (N'Huy', N'HoanHang')
    GROUP BY d.Ngay
    ORDER BY d.Ngay ASC
    OPTION (MAXRECURSION 31);
END;
GO

-- SP: Báo cáo doanh thu theo Quý
DROP PROCEDURE IF EXISTS sp_BaoCaoDoanhThuQuy;
GO
CREATE PROCEDURE sp_BaoCaoDoanhThuQuy
    @Quy INT, -- 1..4
    @Nam INT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @ThangDau INT = (@Quy - 1) * 3 + 1;
    DECLARE @ThangCuoi INT = @Quy * 3;
    SELECT 
        COUNT(*) AS TongDon,
        ISNULL(SUM(TongTien), 0) AS TongDoanhThu
    FROM DON_HANG
    WHERE MONTH(NgayTao) BETWEEN @ThangDau AND @ThangCuoi 
      AND YEAR(NgayTao) = @Nam 
      AND TrangThai NOT IN (N'Huy');
END;
GO

-- SP: Doanh thu theo từng tháng trong Quý (biểu đồ)
DROP PROCEDURE IF EXISTS sp_DoanhThuTheoThangTrongQuy;
GO
CREATE PROCEDURE sp_DoanhThuTheoThangTrongQuy
    @Quy INT,
    @Nam INT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @ThangDau INT = (@Quy - 1) * 3 + 1;
    ;WITH Months AS (
        SELECT @ThangDau AS Thang
        UNION ALL
        SELECT Thang + 1 FROM Months WHERE Thang < @ThangDau + 2
    )
    SELECT 
        m.Thang,
        ISNULL(SUM(dh.TongTien), 0) AS DoanhThu,
        COUNT(dh.MaDon) AS SoDon
    FROM Months m
    LEFT JOIN DON_HANG dh 
        ON MONTH(dh.NgayTao) = m.Thang AND YEAR(dh.NgayTao) = @Nam
        AND dh.TrangThai NOT IN (N'Huy', N'HoanHang')
    GROUP BY m.Thang
    ORDER BY m.Thang;
END;
GO

-- =====================================================
-- DỮ LIỆU MẪU
-- =====================================================

-- Nhân viên (mật khẩu = SHA-256 của '123456' = '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92')
INSERT INTO NHAN_VIEN VALUES 
(N'NV000001', N'Nguyễn Lê Minh Vũ',  N'Admin',     N'0901111222', N'admin',     N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'DangLam'),
(N'NV000002', N'Trần Thu Hương',      N'Cashier',   N'0912333444', N'thuhuong',  N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'DangLam'),
(N'NV000003', N'Lê Minh Khoa',        N'Warehouse', N'0923555666', N'minhkho',   N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'DangLam'),
(N'NV000004', N'Nguyễn Văn Sơn',      N'Shipper',   N'0934777888', N'shipper1',  N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'DangLam'),
(N'NV000005', N'Hoàng Thị Xuân',      N'Cashier',   N'0945999000', N'xuanxuan',  N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'DangLam'),
(N'NV000006', N'Phạm Thu Thảo',       N'Cashier',   N'0956111222', N'thuthao',   N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'DangLam'),
(N'NV000007', N'Trần Tuấn Anh',       N'Shipper',   N'0967222333', N'shipper2',  N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'DangLam'),
(N'NV000008', N'Đặng Văn Hùng',       N'Shipper',   N'0978333444', N'shipper3',  N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'DangLam');

-- Sản phẩm
INSERT INTO SAN_PHAM VALUES
(N'SP000001', N'Hoa hồng đỏ (bó 10)',            N'Hoa tươi', 180000, 120000, 42, 10, N'DangBan'),
(N'SP000002', N'Hoa hồng trắng (bó 10)',         N'Hoa tươi', 180000, 120000, 35, 10, N'DangBan'),
(N'SP000003', N'Hoa tulip hồng (bó 5)',          N'Hoa tươi', 220000, 160000, 28, 10, N'DangBan'),
(N'SP000004', N'Hoa hướng dương (bó 5)',         N'Hoa tươi', 140000, 95000, 40, 10, N'DangBan'),
(N'SP000005', N'Hoa ly trắng (bó 5)',            N'Hoa tươi', 150000, 100000, 22, 10, N'DangBan'),
(N'SP000006', N'Hoa cúc vàng (bó 10)',           N'Hoa tươi', 90000, 60000, 55, 15, N'DangBan'),
(N'SP000007', N'Hoa baby trắng',                 N'Hoa tươi', 100000, 65000, 45, 10, N'DangBan'),
(N'SP000008', N'Hoa cẩm tú cầu xanh',            N'Hoa tươi', 250000, 180000, 18, 5, N'DangBan'),
(N'SP000009', N'Hoa lan hồ điệp tím',            N'Hoa tươi', 420000, 320000, 12, 3, N'DangBan'),
(N'SP000010', N'Hoa cẩm chướng đỏ (bó 10)',      N'Hoa tươi', 120000, 80000, 30, 10, N'DangBan'),
(N'SP000011', N'Bó hoa sinh nhật',               N'Hoa bó', 350000, 250000, 20, 5, N'DangBan'),
(N'SP000012', N'Bó hoa tình yêu',                N'Hoa bó', 500000, 360000, 15, 5, N'DangBan'),
(N'SP000013', N'Bó hoa tốt nghiệp',              N'Hoa bó', 280000, 190000, 18, 5, N'DangBan'),
(N'SP000014', N'Bó hoa cảm ơn',                  N'Hoa bó', 320000, 220000, 14, 5, N'DangBan'),
(N'SP000015', N'Giỏ hoa sinh nhật',              N'Giỏ hoa', 580000, 420000, 12, 3, N'DangBan'),
(N'SP000016', N'Giỏ hoa chúc mừng',              N'Giỏ hoa', 650000, 480000, 10, 3, N'DangBan'),
(N'SP000017', N'Kệ hoa khai trương',             N'Kệ hoa', 1200000, 900000, 5, 1, N'DangBan'),
(N'SP000018', N'Kệ hoa khai trương cao cấp',     N'Kệ hoa', 1800000, 1400000, 4, 1, N'DangBan'),
(N'SP000019', N'Lẵng hoa chia buồn',             N'Lẵng hoa', 1100000, 850000, 5, 1, N'DangBan'),
(N'SP000020', N'Lẵng hoa chia buồn cao cấp',     N'Lẵng hoa', 1800000, 1400000, 3, 1, N'DangBan'),
(N'SP000021', N'Hoa cưới cầm tay',               N'Hoa cưới', 550000, 400000, 8, 2, N'DangBan'),
(N'SP000022', N'Hoa cưới baby',                  N'Hoa cưới', 650000, 480000, 6, 2, N'DangBan'),
(N'SP000023', N'Giấy gói hoa kraft',             N'Phụ kiện', 15000, 8000, 200, 50, N'DangBan'),
(N'SP000024', N'Giấy gói hoa Hàn Quốc',          N'Phụ kiện', 25000, 15000, 150, 30, N'DangBan'),
(N'SP000025', N'Ruy băng lụa',                   N'Phụ kiện', 20000, 12000, 180, 50, N'DangBan'),
(N'SP000026', N'Nơ trang trí',                   N'Phụ kiện', 10000, 5000, 250, 50, N'DangBan'),
(N'SP000027', N'Xốp cắm hoa Oasis',              N'Phụ kiện', 35000, 22000, 100, 20, N'DangBan'),
(N'SP000028', N'Giỏ mây cắm hoa',                N'Phụ kiện', 80000, 55000, 40, 10, N'DangBan'),
(N'SP000029', N'Bình thủy tinh cắm hoa',         N'Phụ kiện', 120000, 85000, 25, 5, N'DangBan'),
(N'SP000030', N'Lá bạc trang trí',               N'Phụ kiện', 30000, 18000, 120, 20, N'DangBan');

-- Khách hàng
INSERT INTO KHACH_HANG VALUES
(N'KH000001', N'Nguyễn Thị Lan',  N'0901234567', N'12 Hàng Bông, Q.Hoàn Kiếm, Hà Nội',      N'lan.nt@gmail.com',   '2025-06-15'),
(N'KH000002', N'Trần Văn Hùng',   N'0912345678', N'45 Phố Huế, Q.Hai Bà Trưng, Hà Nội',    N'hung.tv@gmail.com',  '2025-09-01'),
(N'KH000003', N'Lê Thị Mai',      N'0923456789', N'78 Kim Mã, Q.Ba Đình, Hà Nội',           N'mai.lt@gmail.com',   '2026-01-20'),
(N'KH000004', N'Phạm Văn An',     N'0934567890', N'23 Trần Phú, Q.Hà Đông, Hà Nội',         N'an.pv@email.com',    '2025-03-10'),
(N'KH000005', N'Đỗ Minh Tuấn',    N'0941122334', N'56 Láng Hạ, Q.Đống Đa, Hà Nội',          N'tuan.dm@gmail.com',  '2025-11-05'),
(N'KH000006', N'Vũ Thị Hương',    N'0952233445', N'102 Nguyễn Trãi, Q.Thanh Xuân, Hà Nội',  N'huong.vt@gmail.com', '2025-12-18'),
(N'KH000007', N'Hoàng Đức Anh',   N'0963344556', N'8 Xuân Diệu, Q.Tây Hồ, Hà Nội',         N'anh.hd@gmail.com',   '2026-02-01'),
(N'KH000008', N'Ngô Thanh Thảo',  N'0974455667', N'31 Trần Duy Hưng, Q.Cầu Giấy, Hà Nội',  N'thao.nt@gmail.com',  '2026-03-05'),
(N'KH000009', N'Bùi Quang Hải',   N'0985566778', N'15 Lê Duẩn, Q.Hoàn Kiếm, Hà Nội',       N'hai.bq@gmail.com',   '2026-04-12'),
(N'KH000010', N'Đinh Thị Ngọc',   N'0996677889', N'67 Đội Cấn, Q.Ba Đình, Hà Nội',          N'ngoc.dt@gmail.com',  '2026-05-20');

-- Tắt trigger Insert Log tạm thời để tránh duplicate khi insert đơn mẫu
DISABLE TRIGGER trg_DonHang_Insert_Log ON DON_HANG;
GO

-- Đơn hàng mẫu
INSERT INTO DON_HANG VALUES
(N'DH000001', '2026-03-11 09:15', N'KH000001', N'NV000002', N'GiaoTanNoi', N'Moi',      0, N'Giao trước 10h sáng'),
(N'DH000002', '2026-03-11 08:40', N'KH000002', N'NV000002', N'GiaoTanNoi', N'DangXuLy', 0, N'Gói quà đẹp, có thiệp'),
(N'DH000003', '2026-03-11 08:20', N'KH000003', N'NV000002', N'TaiQuay',    N'HoanThanh',0, NULL),
(N'DH000004', '2026-03-10 17:55', N'KH000004', N'NV000002', N'GiaoTanNoi', N'DaGiao',   0, NULL),
(N'DH000005', '2026-03-12 10:00', N'KH000001', N'NV000002', N'GiaoTanNoi', N'DaGiao',   0, NULL),
(N'DH000006', '2026-03-12 11:30', N'KH000002', N'NV000005', N'GiaoTanNoi', N'DaGiao',   0, NULL),
(N'DH000007', '2026-03-13 09:20', N'KH000003', N'NV000006', N'GiaoTanNoi', N'DaGiao',   0, NULL),
(N'DH000008', '2026-03-13 14:10', N'KH000004', N'NV000002', N'GiaoTanNoi', N'DaGiao',   0, NULL),
(N'DH000009', '2026-03-14 08:45', N'KH000001', N'NV000005', N'GiaoTanNoi', N'DaGiao',   0, NULL),
(N'DH000010', '2026-03-14 15:00', N'KH000002', N'NV000006', N'TaiQuay',    N'HoanThanh',0, NULL),
(N'DH000011', '2026-03-15 10:30', N'KH000003', N'NV000002', N'GiaoTanNoi', N'DaGiao',   0, NULL),
(N'DH000012', '2026-03-15 16:20', N'KH000004', N'NV000005', N'GiaoTanNoi', N'DaGiao',   0, NULL),
(N'DH000013', '2026-03-16 09:15', N'KH000001', N'NV000006', N'GiaoTanNoi', N'DaGiao',   0, NULL),
(N'DH000014', '2026-03-16 11:40', N'KH000002', N'NV000002', N'TaiQuay',    N'HoanThanh',0, NULL),
(N'DH000015', '2026-03-17 14:05', N'KH000003', N'NV000005', N'GiaoTanNoi', N'DaGiao',   0, NULL),
(N'DH000016', '2026-03-17 17:30', N'KH000004', N'NV000006', N'GiaoTanNoi', N'DaGiao',   0, NULL),
(N'DH000017', '2026-03-18 08:50', N'KH000001', N'NV000002', N'GiaoTanNoi', N'DaGiao',   0, NULL),
(N'DH000018', '2026-03-18 10:15', N'KH000002', N'NV000005', N'TaiQuay',    N'HoanThanh',0, NULL),
(N'DH000019', '2026-03-19 13:45', N'KH000003', N'NV000006', N'GiaoTanNoi', N'DaGiao',   0, NULL),
(N'DH000020', '2026-03-19 15:20', N'KH000004', N'NV000002', N'GiaoTanNoi', N'DaGiao',   0, NULL),
(N'DH000021', '2026-03-20 09:00', N'KH000001', N'NV000005', N'TaiQuay',    N'HoanThanh',0, NULL),
(N'DH000022', '2026-03-20 11:30', N'KH000002', N'NV000006', N'GiaoTanNoi', N'DaGiao',   0, NULL),
(N'DH000023', '2026-03-21 14:10', N'KH000003', N'NV000002', N'GiaoTanNoi', N'DaGiao',   0, NULL),
(N'DH000024', '2026-03-21 16:45', N'KH000004', N'NV000005', N'TaiQuay',    N'HoanThanh',0, NULL);

-- Bật lại trigger Insert Log
ENABLE TRIGGER trg_DonHang_Insert_Log ON DON_HANG;
GO

-- Chi tiết đơn hàng (giá khớp với SAN_PHAM mới, đa dạng sản phẩm)
INSERT INTO CHI_TIET_DON_HANG (MaDon, MaSP, SoLuong, DonGia, ThanhTien) VALUES
(N'DH000001', N'SP000001', 2, 180000, 360000),
(N'DH000001', N'SP000007', 1, 100000, 100000),
(N'DH000002', N'SP000011', 1, 350000, 350000),
(N'DH000002', N'SP000023', 2, 15000,  30000),
(N'DH000003', N'SP000006', 2, 90000,  180000),
(N'DH000004', N'SP000012', 1, 500000, 500000),
(N'DH000004', N'SP000025', 1, 20000,  20000),
(N'DH000005', N'SP000003', 1, 220000, 220000),
(N'DH000006', N'SP000004', 3, 140000, 420000),
(N'DH000007', N'SP000002', 1, 180000, 180000),
(N'DH000008', N'SP000005', 2, 150000, 300000),
(N'DH000009', N'SP000008', 1, 250000, 250000),
(N'DH000010', N'SP000009', 1, 420000, 420000),
(N'DH000011', N'SP000010', 2, 120000, 240000),
(N'DH000012', N'SP000013', 1, 280000, 280000),
(N'DH000013', N'SP000014', 1, 320000, 320000),
(N'DH000014', N'SP000015', 1, 580000, 580000),
(N'DH000015', N'SP000003', 1, 220000, 220000),
(N'DH000016', N'SP000006', 2, 90000,  180000),
(N'DH000017', N'SP000001', 1, 180000, 180000),
(N'DH000018', N'SP000016', 1, 650000, 650000),
(N'DH000019', N'SP000004', 1, 140000, 140000),
(N'DH000020', N'SP000002', 2, 180000, 360000),
(N'DH000021', N'SP000017', 1, 1200000, 1200000),
(N'DH000022', N'SP000001', 2, 180000, 360000),
(N'DH000023', N'SP000005', 1, 150000, 150000),
(N'DH000024', N'SP000021', 1, 550000, 550000);

-- Cập nhật TongTien dựa trên chi tiết
UPDATE dh SET dh.TongTien = (SELECT ISNULL(SUM(ThanhTien),0) FROM CHI_TIET_DON_HANG WHERE MaDon = dh.MaDon)
FROM DON_HANG dh;

-- Giao hàng
INSERT INTO GIAO_HANG VALUES
(N'GH000001', N'DH000001', N'NV000004', NULL,                  N'ChoPhanCong', NULL),
(N'GH000002', N'DH000002', N'NV000004', '2026-03-11 09:30',    N'DangGiao',    N'Có thiệp sinh nhật'),
(N'GH000004', N'DH000004', N'NV000004', '2026-03-10 18:30',    N'GiaoThanhCong', NULL),
(N'GH000005', N'DH000005', N'NV000007', '2026-03-12 11:00',    N'GiaoThanhCong', NULL),
(N'GH000006', N'DH000006', N'NV000008', '2026-03-12 12:30',    N'GiaoThanhCong', NULL),
(N'GH000007', N'DH000007', N'NV000004', '2026-03-13 10:20',    N'GiaoThanhCong', NULL),
(N'GH000008', N'DH000008', N'NV000007', '2026-03-13 15:10',    N'GiaoThanhCong', NULL),
(N'GH000009', N'DH000009', N'NV000008', '2026-03-14 09:45',    N'GiaoThanhCong', NULL),
(N'GH000010', N'DH000011', N'NV000004', '2026-03-15 11:30',    N'GiaoThanhCong', NULL),
(N'GH000011', N'DH000012', N'NV000007', '2026-03-15 17:20',    N'GiaoThanhCong', NULL),
(N'GH000012', N'DH000013', N'NV000008', '2026-03-16 10:15',    N'GiaoThanhCong', NULL),
(N'GH000013', N'DH000015', N'NV000004', '2026-03-17 15:05',    N'GiaoThanhCong', NULL),
(N'GH000014', N'DH000016', N'NV000007', '2026-03-17 18:30',    N'GiaoThanhCong', NULL),
(N'GH000015', N'DH000017', N'NV000008', '2026-03-18 09:50',    N'GiaoThanhCong', NULL),
(N'GH000016', N'DH000019', N'NV000004', '2026-03-19 14:45',    N'GiaoThanhCong', NULL),
(N'GH000017', N'DH000020', N'NV000007', '2026-03-19 16:20',    N'GiaoThanhCong', NULL),
(N'GH000018', N'DH000022', N'NV000008', '2026-03-20 12:30',    N'GiaoThanhCong', NULL),
(N'GH000019', N'DH000023', N'NV000004', '2026-03-21 15:10',    N'GiaoThanhCong', NULL);

-- Phiếu nhập kho
INSERT INTO PHIEU_NHAP_KHO VALUES
(N'PN000001', '2026-03-10 07:30', N'NV000003', N'Hàng từ vựa Bình Điền');

-- Tạm tắt trigger để tránh xung đột tồn kho khi nhập data mẫu
DISABLE TRIGGER trg_NhapKho_TangTon ON CT_NHAP_KHO;
GO

INSERT INTO CT_NHAP_KHO VALUES
(N'PN000001', N'SP000001', 50, 120000),
(N'PN000001', N'SP000002', 30, 120000);

-- Bật lại trigger
ENABLE TRIGGER trg_NhapKho_TangTon ON CT_NHAP_KHO;
GO

-- Phản hồi
INSERT INTO PHAN_HOI VALUES
-- 10 Phản hồi tốt
(N'PH000001', N'DH000005', N'Hoa rất đẹp và tươi, giao hàng đúng giờ.', '2026-03-12 12:00', N'ChuaXuLy', NULL),
(N'PH000002', N'DH000006', N'Dịch vụ tuyệt vời, nhân viên tư vấn nhiệt tình.', '2026-03-12 14:00', N'ChuaXuLy', NULL),
(N'PH000003', N'DH000007', N'Rất hài lòng với bó hoa kỷ niệm, bạn gái tôi rất thích.', '2026-03-13 11:00', N'ChuaXuLy', NULL),
(N'PH000004', N'DH000008', N'Chất lượng hoa vượt mong đợi, sẽ tiếp tục ủng hộ.', '2026-03-13 16:00', N'ChuaXuLy', NULL),
(N'PH000005', N'DH000009', N'Hoa giống hệt hình mẫu trên web, gói ghém cẩn thận.', '2026-03-14 10:00', N'ChuaXuLy', NULL),
(N'PH000006', N'DH000010', N'Lấy hoa tại quầy rất nhanh, hoa được chuẩn bị sẵn rất đẹp.', '2026-03-14 16:00', N'ChuaXuLy', NULL),
(N'PH000007', N'DH000011', N'Shipper lịch sự, hoa giao tới không bị dập nát.', '2026-03-15 12:00', N'ChuaXuLy', NULL),
(N'PH000008', N'DH000012', N'Đặt hàng online rất dễ dàng và tiện lợi.', '2026-03-15 18:00', N'ChuaXuLy', NULL),
(N'PH000009', N'DH000013', N'Giá cả hợp lý so với chất lượng hoa nhận được.', '2026-03-16 11:00', N'ChuaXuLy', NULL),
(N'PH000010', N'DH000014', N'Giấy gói hoa rất sang trọng, tôi rất ưng ý.', '2026-03-16 13:00', N'ChuaXuLy', NULL),
-- 5 Phản hồi bình thường
(N'PH000011', N'DH000015', N'Hoa đẹp nhưng bó hơi nhỏ so với tưởng tượng.', '2026-03-17 16:00', N'DangXuLy', NULL),
(N'PH000012', N'DH000016', N'Giao hàng hơi trễ 15 phút nhưng hoa vẫn tươi.', '2026-03-17 19:00', N'ChuaXuLy', NULL),
(N'PH000013', N'DH000017', N'Giấy gói màu hơi nhạt hơn so với ảnh, nhưng nhìn chung ổn.', '2026-03-18 11:00', N'DangXuLy', NULL),
(N'PH000014', N'DH000018', N'Hoa tạm ổn, có 1 bông có vẻ hơi dập cánh ngoài.', '2026-03-18 12:00', N'ChuaXuLy', NULL),
(N'PH000015', N'DH000019', N'Không có gì đặc biệt, dịch vụ ở mức chấp nhận được.', '2026-03-19 15:00', N'ChuaXuLy', NULL),
-- 5 Phản hồi tệ
(N'PH000016', N'DH000020', N'Hoa bị héo khá nhiều, không đáng tiền chút nào.', '2026-03-19 17:00', N'DangXuLy', NULL),
(N'PH000017', N'DH000021', N'Nhân viên ở quầy không niềm nở, thái độ phục vụ kém.', '2026-03-20 11:00', N'DangXuLy', NULL),
(N'PH000018', N'DH000022', N'Nhân viên tư vấn sai loại hoa tôi yêu cầu.', '2026-03-20 13:00', N'DangXuLy', NULL),
(N'PH000019', N'DH000023', N'Hoa dập nát nhiều, yêu cầu hoàn tiền hoặc đổi trả.', '2026-03-21 16:00', N'DaXuLy', N'Đã đồng ý hoàn trả 100%'),
(N'PH000020', N'DH000024', N'Dịch vụ tệ, gói hoa rất cẩu thả.', '2026-03-21 18:00', N'ChuaXuLy', NULL);

-- Phân quyền mẫu (CHỈ INSERT 1 LẦN - FIX lỗi trùng khóa từ script gốc)
INSERT INTO PHAN_QUYEN VALUES
-- Admin: toàn quyền
(N'Admin', N'Dashboard',    1, 1, 1, 1, 1),
(N'Admin', N'DonHang',      1, 1, 1, 1, 1),
(N'Admin', N'KhoHang',      1, 1, 1, 1, 1),
(N'Admin', N'GiaoHang',     1, 1, 1, 1, 1),
(N'Admin', N'NhanVien',     1, 1, 1, 1, 1),
(N'Admin', N'KhachHang',    1, 1, 1, 1, 1),
(N'Admin', N'SanPham',      1, 1, 1, 1, 1),
(N'Admin', N'BaoCao',       1, 1, 1, 1, 1),
(N'Admin', N'TraHang',      1, 1, 1, 1, 1),
(N'Admin', N'PhanHoi',      1, 1, 1, 1, 1),
(N'Admin', N'PhanQuyen',    1, 1, 1, 1, 1),
-- Cashier: bán hàng, khách hàng
(N'Cashier', N'Dashboard',  1, 0, 0, 0, 0),
(N'Cashier', N'DonHang',    1, 1, 1, 0, 0),
(N'Cashier', N'KhoHang',    0, 0, 0, 0, 0),
(N'Cashier', N'GiaoHang',   0, 0, 0, 0, 0),
(N'Cashier', N'NhanVien',   0, 0, 0, 0, 0),
(N'Cashier', N'KhachHang',  1, 1, 1, 0, 0),
(N'Cashier', N'SanPham',    1, 0, 0, 0, 0),
(N'Cashier', N'BaoCao',     0, 0, 0, 0, 0),
(N'Cashier', N'TraHang',    1, 1, 0, 0, 0),
(N'Cashier', N'PhanHoi',    1, 1, 0, 0, 0),
-- Warehouse: kho hàng
(N'Warehouse', N'Dashboard',1, 0, 0, 0, 0),
(N'Warehouse', N'DonHang',  0, 0, 0, 0, 0),
(N'Warehouse', N'KhoHang',  1, 1, 1, 0, 1),
(N'Warehouse', N'GiaoHang', 0, 0, 0, 0, 0),
(N'Warehouse', N'NhanVien', 0, 0, 0, 0, 0),
(N'Warehouse', N'KhachHang',0, 0, 0, 0, 0),
(N'Warehouse', N'SanPham',  1, 1, 1, 0, 0),
(N'Warehouse', N'BaoCao',   0, 0, 0, 0, 0),
(N'Warehouse', N'TraHang',  0, 0, 0, 0, 0),
(N'Warehouse', N'PhanHoi',  0, 0, 0, 0, 0),
-- Shipper: giao hàng
(N'Shipper', N'Dashboard',  1, 0, 0, 0, 0),
(N'Shipper', N'DonHang',    0, 0, 0, 0, 0),
(N'Shipper', N'KhoHang',    0, 0, 0, 0, 0),
(N'Shipper', N'GiaoHang',   1, 0, 1, 0, 0),
(N'Shipper', N'NhanVien',   0, 0, 0, 0, 0),
(N'Shipper', N'KhachHang',  0, 0, 0, 0, 0),
(N'Shipper', N'SanPham',    0, 0, 0, 0, 0),
(N'Shipper', N'BaoCao',     0, 0, 0, 0, 0),
(N'Shipper', N'TraHang',    0, 0, 0, 0, 0),
(N'Shipper', N'PhanHoi',    0, 0, 0, 0, 0);

-- Backfill lịch sử đơn hàng
INSERT INTO LICH_SU_DON_HANG (MaDon, TrangThai, ThoiGian, GhiChu)
SELECT dh.MaDon, N'Moi', dh.NgayTao, N'Tạo đơn hàng mới (backfill)'
FROM DON_HANG dh
WHERE NOT EXISTS (
    SELECT 1 FROM LICH_SU_DON_HANG ls WHERE ls.MaDon = dh.MaDon
);
GO

-- Nếu đơn đã chuyển trạng thái khác Moi, thêm 1 record nữa cho trạng thái hiện tại
INSERT INTO LICH_SU_DON_HANG (MaDon, TrangThai, ThoiGian, GhiChu)
SELECT dh.MaDon, dh.TrangThai, DATEADD(MINUTE, 1, dh.NgayTao),
    CASE dh.TrangThai
        WHEN N'DangXuLy'  THEN N'Đã xuất kho - đang xử lý (backfill)'
        WHEN N'DaGiao'    THEN N'Đã giao cho shipper (backfill)'
        WHEN N'HoanThanh' THEN N'Đơn hàng hoàn thành (backfill)'
        WHEN N'Huy'       THEN N'Đơn hàng bị hủy (backfill)'
        WHEN N'HoanHang'  THEN N'Khách yêu cầu hoàn hàng (backfill)'
        ELSE N'Trạng thái: ' + dh.TrangThai + N' (backfill)'
    END
FROM DON_HANG dh
WHERE dh.TrangThai <> N'Moi'
AND NOT EXISTS (
    SELECT 1 FROM LICH_SU_DON_HANG ls 
    WHERE ls.MaDon = dh.MaDon AND ls.TrangThai = dh.TrangThai
);
GO

PRINT N'✅ Tạo bảng + Trigger + Dữ liệu mẫu cơ bản thành công!';
GO
-- =====================================================
-- PHẦN BỔ SUNG: DỮ LIỆU MẪU MỞ RỘNG
-- 10 đơn/ngày × 169 ngày (01/01 → 18/06/2026)
-- Chia đều cho Cashier, Shipper, Khách hàng
-- =====================================================

-- 1. THÊM PHIẾU NHẬP KHO HÀNG THÁNG (Để có đủ hàng bán)
DECLARE @MaNV_Kho NVARCHAR(20) = (SELECT TOP 1 MaNV FROM NHAN_VIEN WHERE ChucVu = 'Warehouse');
IF @MaNV_Kho IS NULL SET @MaNV_Kho = 'NV000003';

IF NOT EXISTS (SELECT 1 FROM PHIEU_NHAP_KHO WHERE MaPhieu = 'PN000002')
BEGIN
    INSERT INTO PHIEU_NHAP_KHO (MaPhieu, NgayNhap, MaNV, GhiChu) VALUES
    ('PN000002', '2026-01-02 08:00', @MaNV_Kho, N'Nhập hàng đầu năm'),
    ('PN000003', '2026-02-01 08:00', @MaNV_Kho, N'Nhập hàng tháng 2'),
    ('PN000004', '2026-03-01 08:00', @MaNV_Kho, N'Nhập hàng tháng 3'),
    ('PN000005', '2026-04-01 08:00', @MaNV_Kho, N'Nhập hàng tháng 4'),
    ('PN000006', '2026-05-01 08:00', @MaNV_Kho, N'Nhập hàng tháng 5'),
    ('PN000007', '2026-06-01 08:00', @MaNV_Kho, N'Nhập hàng tháng 6');

    -- Mỗi tháng nhập 200 đơn vị cho mỗi sản phẩm
    DECLARE @pn NVARCHAR(20);
    DECLARE pn_cursor CURSOR FOR 
        SELECT MaPhieu FROM PHIEU_NHAP_KHO WHERE MaPhieu IN ('PN000002','PN000003','PN000004','PN000005','PN000006','PN000007');
    OPEN pn_cursor;
    FETCH NEXT FROM pn_cursor INTO @pn;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        INSERT INTO CT_NHAP_KHO (MaPhieu, MaSP, SoLuong, GiaNhap)
        SELECT @pn, MaSP, 200, GiaNhap FROM SAN_PHAM;
        FETCH NEXT FROM pn_cursor INTO @pn;
    END
    CLOSE pn_cursor;
    DEALLOCATE pn_cursor;
END
GO

-- 2. TẠO ĐƠN HÀNG: 10 đơn/ngày từ 01/01/2026 → 18/06/2026
DECLARE @CurrentOrderNum INT = (SELECT ISNULL(MAX(CAST(SUBSTRING(MaDon, 3, 6) AS INT)), 0) FROM DON_HANG);
DECLARE @GHNum INT = (SELECT ISNULL(MAX(CAST(SUBSTRING(MaGiaoHang, 3, 6) AS INT)), 0) FROM GIAO_HANG);

-- Bảng tạm nhân viên
SELECT MaNV, ROW_NUMBER() OVER (ORDER BY MaNV) as ID INTO #Cashiers FROM NHAN_VIEN WHERE ChucVu IN ('Cashier', 'Admin');
SELECT MaNV, ROW_NUMBER() OVER (ORDER BY MaNV) as ID INTO #Shippers FROM NHAN_VIEN WHERE ChucVu = 'Shipper';
SELECT MaKH, ROW_NUMBER() OVER (ORDER BY MaKH) as ID INTO #Customers FROM KHACH_HANG;

DECLARE @NumCashiers INT = (SELECT COUNT(*) FROM #Cashiers);
DECLARE @NumShippers INT = (SELECT COUNT(*) FROM #Shippers);
DECLARE @NumCustomers INT = (SELECT COUNT(*) FROM #Customers);

IF @NumCashiers > 0 AND @NumShippers > 0 AND @NumCustomers > 0
BEGIN
    DECLARE @DayDate DATE = '2026-01-01';
    DECLARE @EndDate DATE = '2026-06-18';
    DECLARE @j INT;
    DECLARE @OrderGlobalIdx INT = 0;

    -- Khai báo biến trước vòng lặp
    DECLARE @OrderTime DATETIME;
    DECLARE @MaDon NVARCHAR(20);
    DECLARE @CashierID INT;
    DECLARE @MaNV_Tao NVARCHAR(20);
    DECLARE @MaKH NVARCHAR(20);
    DECLARE @CustomerID INT;
    DECLARE @TrangThaiDon NVARCHAR(20);
    DECLARE @HinhThuc NVARCHAR(30);
    DECLARE @MaGH NVARCHAR(20);
    DECLARE @ShipperID INT;
    DECLARE @MaNV_Ship NVARCHAR(20);
    DECLARE @NumProducts INT;
    DECLARE @k INT;
    DECLARE @MaSP_Random NVARCHAR(20);
    DECLARE @GiaBan_Random DECIMAL(18,0);
    DECLARE @SoLuong_Random INT;
    DECLARE @DaysFromEnd INT;

    WHILE @DayDate <= @EndDate
    BEGIN
        SET @j = 1;
        WHILE @j <= 10 -- 10 đơn mỗi ngày
        BEGIN
            SET @OrderGlobalIdx = @OrderGlobalIdx + 1;

            -- Giờ tạo đơn ngẫu nhiên 8h-18h
            SET @OrderTime = CAST(@DayDate AS DATETIME) 
                + CAST(DATEADD(MINUTE, 480 + ABS(CHECKSUM(NEWID())) % 600, 0) AS DATETIME);

            -- Mã đơn
            SET @CurrentOrderNum = @CurrentOrderNum + 1;
            SET @MaDon = 'DH' + RIGHT('000000' + CAST(@CurrentOrderNum AS NVARCHAR), 6);

            -- Chia đều Cashier (round-robin)
            SET @CashierID = ((@OrderGlobalIdx - 1) % @NumCashiers) + 1;
            SET @MaNV_Tao = (SELECT MaNV FROM #Cashiers WHERE ID = @CashierID);

            -- Chia đều Khách hàng (round-robin)
            SET @CustomerID = ((@OrderGlobalIdx - 1) % @NumCustomers) + 1;
            SET @MaKH = (SELECT MaKH FROM #Customers WHERE ID = @CustomerID);

            -- Trạng thái: đơn cũ → HoanThanh/HoanHang/Huy, đơn gần đây → DaGiao/DangXuLy/Moi
            SET @DaysFromEnd = DATEDIFF(DAY, @DayDate, @EndDate);
            SET @TrangThaiDon = CASE
                WHEN @DaysFromEnd <= 1 AND @j <= 3  THEN N'Moi'        -- 3 đơn mới nhất
                WHEN @DaysFromEnd <= 3 AND @j <= 5  THEN N'DangXuLy'   -- đang xử lý
                WHEN @DaysFromEnd <= 5               THEN N'DaGiao'     -- vừa giao
                WHEN @OrderGlobalIdx % 20 = 0       THEN N'HoanHang'   -- 5% hoàn hàng
                WHEN @OrderGlobalIdx % 20 = 10      THEN N'Huy'        -- 5% hủy
                ELSE N'HoanThanh'                                       -- 90% hoàn thành
            END;

            -- Hình thức: 70% giao tận nơi, 30% tại quầy
            SET @HinhThuc = CASE WHEN @j <= 7 THEN N'GiaoTanNoi' ELSE N'TaiQuay' END;

            -- Tạo đơn hàng
            INSERT INTO DON_HANG (MaDon, NgayTao, MaKH, MaNV_TaoDon, HinhThucNhanHang, TrangThai, TongTien)
            VALUES (@MaDon, @OrderTime, @MaKH, @MaNV_Tao, @HinhThuc, @TrangThaiDon, 0);

            -- Thêm 1-3 sản phẩm ngẫu nhiên vào đơn
            SET @NumProducts = 1 + ABS(CHECKSUM(NEWID())) % 3; -- 1, 2 hoặc 3 SP
            SET @k = 1;
            WHILE @k <= @NumProducts
            BEGIN
                -- Chọn SP ngẫu nhiên chưa có trong đơn này
                SELECT TOP 1 @MaSP_Random = MaSP, @GiaBan_Random = GiaBan 
                FROM SAN_PHAM 
                WHERE MaSP NOT IN (SELECT MaSP FROM CHI_TIET_DON_HANG WHERE MaDon = @MaDon)
                ORDER BY NEWID();

                SET @SoLuong_Random = 1 + ABS(CHECKSUM(NEWID())) % 3; -- 1-3

                IF @MaSP_Random IS NOT NULL
                BEGIN
                    INSERT INTO CHI_TIET_DON_HANG (MaDon, MaSP, SoLuong, DonGia)
                    VALUES (@MaDon, @MaSP_Random, @SoLuong_Random, @GiaBan_Random);
                END

                SET @k = @k + 1;
            END

            -- Cập nhật tổng tiền
            UPDATE DON_HANG SET TongTien = (SELECT ISNULL(SUM(ThanhTien),0) FROM CHI_TIET_DON_HANG WHERE MaDon = @MaDon) WHERE MaDon = @MaDon;

            -- Tạo giao hàng (chỉ cho GiaoTanNoi)
            IF @HinhThuc = N'GiaoTanNoi'
            BEGIN
                SET @GHNum = @GHNum + 1;
                SET @MaGH = 'GH' + RIGHT('000000' + CAST(@GHNum AS NVARCHAR), 6);
                
                -- Chia đều Shipper (round-robin)
                SET @ShipperID = ((@GHNum - 1) % @NumShippers) + 1;
                SET @MaNV_Ship = (SELECT MaNV FROM #Shippers WHERE ID = @ShipperID);
                
                INSERT INTO GIAO_HANG (MaGiaoHang, MaDon, MaNV_Shipper, NgayGiao, TrangThai)
                VALUES (@MaGH, @MaDon, @MaNV_Ship, 
                    DATEADD(MINUTE, 30 + (ABS(CHECKSUM(NEWID())) % 120), @OrderTime),
                    CASE 
                        WHEN @TrangThaiDon = N'HoanHang'  THEN N'HoanHang'
                        WHEN @TrangThaiDon = N'Huy'       THEN N'ChoPhanCong'
                        WHEN @TrangThaiDon = N'Moi'       THEN N'ChoPhanCong'
                        WHEN @TrangThaiDon = N'DangXuLy'  THEN N'DangGiao'
                        WHEN @TrangThaiDon = N'DaGiao'    THEN N'GiaoThanhCong'
                        ELSE N'GiaoThanhCong'
                    END);
            END

            SET @j = @j + 1;
        END

        SET @DayDate = DATEADD(DAY, 1, @DayDate);
    END
END

DROP TABLE #Cashiers;
DROP TABLE #Shippers;
DROP TABLE #Customers;

PRINT N'✅ Dữ liệu mở rộng (10 đơn/ngày × 169 ngày) đã được tạo thành công!';
GO

-- =====================================================
-- TỐI ƯU HÓA HIỆU SUẤT (INDEXES)
-- =====================================================

-- Drop trước nếu tồn tại, rồi tạo lại
IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'idx_DonHang_NgayTao' AND object_id = OBJECT_ID('DON_HANG'))
    DROP INDEX idx_DonHang_NgayTao ON DON_HANG;
CREATE NONCLUSTERED INDEX idx_DonHang_NgayTao 
ON DON_HANG(NgayTao DESC) 
INCLUDE (TrangThai, TongTien);
GO

IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'idx_DonHang_TrangThai_MaNV' AND object_id = OBJECT_ID('DON_HANG'))
    DROP INDEX idx_DonHang_TrangThai_MaNV ON DON_HANG;
CREATE NONCLUSTERED INDEX idx_DonHang_TrangThai_MaNV 
ON DON_HANG(TrangThai, MaNV_TaoDon);
GO

IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'idx_GiaoHang_TrangThai_MaNV' AND object_id = OBJECT_ID('GIAO_HANG'))
    DROP INDEX idx_GiaoHang_TrangThai_MaNV ON GIAO_HANG;
CREATE NONCLUSTERED INDEX idx_GiaoHang_TrangThai_MaNV 
ON GIAO_HANG(MaNV_Shipper, TrangThai)
INCLUDE (NgayGiao);
GO

IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'idx_SanPham_TrangThai' AND object_id = OBJECT_ID('SAN_PHAM'))
    DROP INDEX idx_SanPham_TrangThai ON SAN_PHAM;
CREATE NONCLUSTERED INDEX idx_SanPham_TrangThai 
ON SAN_PHAM(TrangThai) 
INCLUDE (TenSP, GiaBan, SoLuongTon);
GO

PRINT N'✅ Database FloriSys đã được tạo lại hoàn chỉnh!';
GO

