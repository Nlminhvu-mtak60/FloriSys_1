-- =====================================================
-- FloriSys - Co so du lieu Quan ly Cua hang Hoa
-- SQL Server 2022 - Developer Edition
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

-- 1. BANG NHAN VIEN
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

-- 2. BANG KHACH HANG
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

-- 3. BANG SAN PHAM
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

-- 4. BANG DON HANG
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

-- 5. BANG CHI TIET DON HANG
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

-- 6. BANG GIAO HANG
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
-- 7. Báº¢NG PHIáº¾U NHáº¬P KHO
-- =====================================================
CREATE TABLE PHIEU_NHAP_KHO (
    MaPhieu  NVARCHAR(20)  PRIMARY KEY,
    NgayNhap DATETIME      NOT NULL DEFAULT GETDATE(),
    MaNV     NVARCHAR(20)  NOT NULL REFERENCES NHAN_VIEN(MaNV),
    GhiChu   NVARCHAR(500)
);
GO

-- =====================================================
-- 8. Báº¢NG CHI TIáº¾T NHáº¬P KHO
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
-- 9. Báº¢NG PHáº¢N Há»’I
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
-- 10. Báº¢NG Cáº¢NH BĂO Tá»’N KHO
-- =====================================================
CREATE TABLE CANH_BAO_TON_KHO (
    MaSP       NVARCHAR(20)  PRIMARY KEY REFERENCES SAN_PHAM(MaSP),
    MucToiThieu INT          NOT NULL DEFAULT 10,
    NgayCapNhat DATETIME     NOT NULL DEFAULT GETDATE()
);
GO

-- =====================================================
-- 11. Báº¢NG HĂ€NG HÆ¯ (Lá»‹ch sá»­ há»§y hĂ ng)
-- =====================================================
CREATE TABLE HANG_HU (
    MaPhieuHuy  NVARCHAR(20)    PRIMARY KEY,
    MaSP        NVARCHAR(20)    NOT NULL REFERENCES SAN_PHAM(MaSP),
    SoLuong     INT             NOT NULL CHECK (SoLuong > 0),
    LyDo        NVARCHAR(200)   NOT NULL,
    NgayHuy     DATETIME        DEFAULT GETDATE(),
    GhiChu      NVARCHAR(500)   NULL
);
GO

-- =====================================================
-- 12. Báº¢NG PHĂ‚N QUYá»€N
-- =====================================================
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

-- =====================================================
-- 13. Báº¢NG TRáº¢ HĂ€NG
-- =====================================================
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

-- =====================================================
-- 14. Báº¢NG CHI TIáº¾T TRáº¢ HĂ€NG
-- =====================================================
CREATE TABLE CT_TRA_HANG (
    MaPhieuTra  NVARCHAR(20)  NOT NULL REFERENCES TRA_HANG(MaPhieuTra),
    MaSP        NVARCHAR(20)  NOT NULL REFERENCES SAN_PHAM(MaSP),
    SoLuong     INT           NOT NULL CHECK (SoLuong > 0),
    CoNhapKho   BIT           NOT NULL DEFAULT 0,
    PRIMARY KEY (MaPhieuTra, MaSP)
);
GO

-- =====================================================
-- TRIGGERS
-- =====================================================

-- Trigger: Tá»± Ä‘á»™ng tĂ­nh ThanhTien khi INSERT/UPDATE CHI_TIET_DON_HANG
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

-- Trigger: Tá»± Ä‘á»™ng cáº­p nháº­t TongTien cá»§a DON_HANG khi thay Ä‘á»•i CHI_TIET_DON_HANG
CREATE OR ALTER TRIGGER trg_CapNhatTongTien
ON CHI_TIET_DON_HANG
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    -- Cáº­p nháº­t cho Ä‘Æ¡n trong inserted
    UPDATE dh SET dh.TongTien = ISNULL((SELECT SUM(ThanhTien) FROM CHI_TIET_DON_HANG WHERE MaDon = dh.MaDon), 0)
    FROM DON_HANG dh
    WHERE dh.MaDon IN (SELECT MaDon FROM inserted UNION SELECT MaDon FROM deleted);
END;
GO

-- Trigger: Tá»± Ä‘á»™ng cáº­p nháº­t tá»“n kho khi nháº­p kho
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

-- SP: ÄÄƒng nháº­p
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

-- SP: Äá»•i máº­t kháº©u
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

-- SP: Táº¡o Ä‘Æ¡n hĂ ng
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

-- SP: ThĂªm chi tiáº¿t Ä‘Æ¡n hĂ ng
CREATE OR ALTER PROCEDURE sp_ThemChiTietDon
    @MaDon  NVARCHAR(20),
    @MaSP   NVARCHAR(20),
    @SoLuong INT,
    @DonGia  DECIMAL(18,0)
AS
BEGIN
    -- Kiá»ƒm tra tá»“n kho
    DECLARE @TonKho INT;
    SELECT @TonKho = SoLuongTon FROM SAN_PHAM WHERE MaSP = @MaSP;
    IF @TonKho < @SoLuong
    BEGIN
        RAISERROR(N'Tá»“n kho khĂ´ng Ä‘á»§! CĂ²n %d, yĂªu cáº§u %d.', 16, 1, @TonKho, @SoLuong);
        RETURN;
    END
    INSERT INTO CHI_TIET_DON_HANG (MaDon, MaSP, SoLuong, DonGia, ThanhTien)
    VALUES (@MaDon, @MaSP, @SoLuong, @DonGia, @SoLuong * @DonGia);
END;
GO

-- SP: Cáº­p nháº­t tráº¡ng thĂ¡i Ä‘Æ¡n hĂ ng
CREATE OR ALTER PROCEDURE sp_CapNhatTrangThaiDon
    @MaDon     NVARCHAR(20),
    @TrangThai NVARCHAR(20)
AS
BEGIN
    -- Khi chuyá»ƒn sang DangXuLy â†’ trá»« tá»“n kho
    IF @TrangThai = N'DangXuLy'
    BEGIN
        -- Kiá»ƒm tra tá»“n kho Ä‘á»§ khĂ´ng
        IF EXISTS (
            SELECT 1 FROM CHI_TIET_DON_HANG ct
            INNER JOIN SAN_PHAM sp ON ct.MaSP = sp.MaSP
            WHERE ct.MaDon = @MaDon AND sp.SoLuongTon < ct.SoLuong
        )
        BEGIN
            RAISERROR(N'KhĂ´ng thá»ƒ xá»­ lĂ½ Ä‘Æ¡n â€“ tá»“n kho khĂ´ng Ä‘á»§ cho má»™t sá»‘ sáº£n pháº©m.', 16, 1);
            RETURN;
        END
        -- Trá»« tá»“n kho
        UPDATE sp SET sp.SoLuongTon = sp.SoLuongTon - ct.SoLuong
        FROM SAN_PHAM sp
        INNER JOIN CHI_TIET_DON_HANG ct ON sp.MaSP = ct.MaSP
        WHERE ct.MaDon = @MaDon;
    END
    -- Khi Há»§y tá»« Moi â†’ khĂ´ng cáº§n hoĂ n kho (chÆ°a trá»«)
    -- Khi Há»§y tá»« DangXuLy â†’ hoĂ n láº¡i tá»“n kho (Ä‘Ă£ trá»« khi xuáº¥t kho)
    IF @TrangThai = N'Huy'
    BEGIN
        DECLARE @TrangThaiHienTai NVARCHAR(20);
        SELECT @TrangThaiHienTai = TrangThai FROM DON_HANG WHERE MaDon = @MaDon;
        IF @TrangThaiHienTai = N'DangXuLy'
        BEGIN
            UPDATE sp SET sp.SoLuongTon = sp.SoLuongTon + ct.SoLuong
            FROM SAN_PHAM sp
            INNER JOIN CHI_TIET_DON_HANG ct ON sp.MaSP = ct.MaSP
            WHERE ct.MaDon = @MaDon;
        END
    END
    -- HoanHang: KHĂ”NG hoĂ n kho táº¡i Ä‘Ă¢y. Tá»“n kho Ä‘Æ°á»£c quáº£n lĂ½ bá»Ÿi phiáº¿u TRA_HANG (CT_TRA_HANG.CoNhapKho)
    UPDATE DON_HANG SET TrangThai = @TrangThai WHERE MaDon = @MaDon;
END;
GO

-- SP: Táº¡o phiáº¿u nháº­p kho
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

-- SP: ThĂªm chi tiáº¿t nháº­p kho (trigger sáº½ tá»± tÄƒng tá»“n kho)
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

-- SP: Ghi nháº­n hĂ ng hÆ° (giáº£m tá»“n kho + lÆ°u lá»‹ch sá»­)
CREATE OR ALTER PROCEDURE sp_GhiNhanHangHu
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
        RAISERROR(N'Sáº£n pháº©m khĂ´ng tá»“n táº¡i.', 16, 1);
        RETURN;
    END
    IF @TonHienTai < @SoLuong
    BEGIN
        RAISERROR(N'KhĂ´ng thá»ƒ há»§y nhiá»u hÆ¡n sá»‘ tá»“n kho hiá»‡n táº¡i.', 16, 1);
        RETURN;
    END
    INSERT INTO HANG_HU (MaPhieuHuy, MaSP, SoLuong, LyDo, GhiChu)
    VALUES (@MaPhieuHuy, @MaSP, @SoLuong, @LyDo, @GhiChu);
    UPDATE SAN_PHAM SET SoLuongTon = SoLuongTon - @SoLuong WHERE MaSP = @MaSP;
END;
GO

-- SP: Táº¡o lá»‡nh giao hĂ ng
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

-- SP: PhĂ¢n cĂ´ng shipper
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

-- SP: Cáº­p nháº­t tráº¡ng thĂ¡i giao hĂ ng
CREATE OR ALTER PROCEDURE sp_CapNhatTrangThaiGiao
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
        
        -- KHĂ”NG hoĂ n kho táº¡i Ä‘Ă¢y. Tá»“n kho Ä‘Æ°á»£c quáº£n lĂ½ bá»Ÿi phiáº¿u TRA_HANG (CT_TRA_HANG.CoNhapKho)
        -- Cashier sáº½ táº¡o phiáº¿u tráº£ hĂ ng vá»›i chi tiáº¿t tá»«ng SP + quyáº¿t Ä‘á»‹nh nháº­p láº¡i kho hay khĂ´ng
    END
    ELSE IF @TrangThai = N'GiaoLai' OR @TrangThai = N'DangGiao'
    BEGIN
        -- Äá»•i láº¡i thĂ nh 'DangXuLy' vĂ¬ báº£ng DON_HANG khĂ´ng cĂ³ tráº¡ng thĂ¡i 'DangGiao'
        UPDATE DON_HANG 
        SET TrangThai = N'DangXuLy' 
        WHERE MaDon = (SELECT MaDon FROM GIAO_HANG WHERE MaGiaoHang = @MaGiaoHang);
    END
END;
GO

-- SP: Ghi nháº­n pháº£n há»“i
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

-- SP: BĂ¡o cĂ¡o doanh thu theo ngĂ y
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

-- SP: BĂ¡o cĂ¡o doanh thu theo thĂ¡ng
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

-- SP: Sáº£n pháº©m bĂ¡n cháº¡y
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

-- SP: Hiá»‡u suáº¥t nhĂ¢n viĂªn
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

-- SP: Láº¥y cáº£nh bĂ¡o tá»“n kho
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

-- SP: Sinh mĂ£ tá»± Ä‘á»™ng
CREATE OR ALTER PROCEDURE sp_SinhMa
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

-- =====================================================
-- Dá»® LIá»†U MáºªU
-- =====================================================

-- NhĂ¢n viĂªn (máº­t kháº©u = SHA-256 cá»§a '123456' = '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92')
INSERT INTO NHAN_VIEN VALUES 
(N'NV001', N'Nguyá»…n LĂª Minh VÅ©',  N'Admin',     N'0901111222', N'admin',     N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'DangLam'),
(N'NV002', N'Tráº§n Thu HÆ°Æ¡ng',      N'Cashier',   N'0912333444', N'thuhuong',  N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'DangLam'),
(N'NV003', N'LĂª Minh Khoa',        N'Warehouse', N'0923555666', N'minhkho',   N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'DangLam'),
(N'NV004', N'Nguyá»…n VÄƒn SÆ¡n',      N'Shipper',   N'0934777888', N'shipper1',  N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'DangLam'),
(N'NV005', N'HoĂ ng Thá»‹ XuĂ¢n',      N'Cashier',   N'0945999000', N'xuanxuan',  N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'DangLam'),
(N'NV006', N'Pháº¡m Thu Tháº£o',       N'Cashier',   N'0956111222', N'thuthao',   N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'DangLam'),
(N'NV007', N'Tráº§n Tuáº¥n Anh',       N'Shipper',   N'0967222333', N'shipper2',  N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'DangLam'),
(N'NV008', N'Äáº·ng VÄƒn HĂ¹ng',       N'Shipper',   N'0978333444', N'shipper3',  N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'DangLam');

-- Sáº£n pháº©m
INSERT INTO SAN_PHAM VALUES
(N'SP001', N'Hoa há»“ng Ä‘á» (bĂ³ 10)',    N'Hoa tÆ°Æ¡i', 180000, 120000, 42, 10, N'DangBan'),
(N'SP002', N'Tulip há»“ng (bĂ³ 5)',       N'Hoa tÆ°Æ¡i', 220000, 160000, 28, 10, N'DangBan'),
(N'SP003', N'CĂºc vĂ ng (bĂ³ 10)',        N'Hoa tÆ°Æ¡i', 80000,  45000,  3,  10, N'DangBan'),
(N'SP004', N'Ly tráº¯ng (bĂ³ 5)',         N'Hoa tÆ°Æ¡i', 150000, 100000, 8,  10, N'DangBan'),
(N'SP005', N'Hoa anh Ä‘Ă o (bĂ³ 7)',      N'Hoa tÆ°Æ¡i', 160000, 110000, 15, 5,  N'DangBan'),
(N'SP006', N'LĂ¡ decor xanh',           N'Phá»¥ kiá»‡n', 30000,  20000,  0,  5,  N'DangBan'),
(N'SP007', N'Ruy bÄƒng trang trĂ­',      N'Phá»¥ kiá»‡n', 15000,  8000,   200,50, N'DangBan'),
(N'SP008', N'Hoa lay Æ¡n (bĂ³ 10)',      N'Hoa tÆ°Æ¡i', 90000,  55000,  0,  10, N'NgungBan');

-- KhĂ¡ch hĂ ng
INSERT INTO KHACH_HANG VALUES
(N'KH001', N'Nguyá»…n Thá»‹ Lan',  N'0901234567', N'12 LĂª Lá»£i, Q.1, TP.HCM',       N'lan.nt@gmail.com',  '2025-06-15'),
(N'KH002', N'Tráº§n VÄƒn HĂ¹ng',   N'0912345678', N'45 Nguyá»…n Huá»‡, Q.1, TP.HCM',   N'hung.tv@gmail.com', '2025-09-01'),
(N'KH003', N'LĂª Thá»‹ Mai',      N'0923456789', NULL,                              NULL,                 '2026-01-20'),
(N'KH004', N'Pháº¡m VÄƒn An',     N'0934567890', N'78 Tráº§n PhĂº, Q.5, TP.HCM',      N'an.pv@email.com',   '2025-03-10');

-- ÄÆ¡n hĂ ng máº«u
INSERT INTO DON_HANG VALUES
(N'DH000001', '2026-03-11 09:15', N'KH001', N'NV002', N'GiaoTanNoi', N'Moi',      0, N'Giao trÆ°á»›c 10h sĂ¡ng'),
(N'DH000002', '2026-03-11 08:40', N'KH002', N'NV002', N'GiaoTanNoi', N'DangXuLy', 0, N'GĂ³i quĂ  Ä‘áº¹p, cĂ³ thiá»‡p'),
(N'DH000003', '2026-03-11 08:20', N'KH003', N'NV002', N'TaiQuay',    N'HoanThanh',0, NULL),
(N'DH000004', '2026-03-10 17:55', N'KH004', N'NV002', N'GiaoTanNoi', N'DaGiao',   0, NULL),
(N'DH000005', '2026-03-12 10:00', N'KH001', N'NV002', N'GiaoTanNoi', N'DaGiao',   0, NULL),
(N'DH000006', '2026-03-12 11:30', N'KH002', N'NV005', N'GiaoTanNoi', N'DaGiao',   0, NULL),
(N'DH000007', '2026-03-13 09:20', N'KH003', N'NV006', N'GiaoTanNoi', N'DaGiao',   0, NULL),
(N'DH000008', '2026-03-13 14:10', N'KH004', N'NV002', N'GiaoTanNoi', N'DaGiao',   0, NULL),
(N'DH000009', '2026-03-14 08:45', N'KH001', N'NV005', N'GiaoTanNoi', N'DaGiao',   0, NULL),
(N'DH000010', '2026-03-14 15:00', N'KH002', N'NV006', N'TaiQuay',    N'HoanThanh',0, NULL),
(N'DH000011', '2026-03-15 10:30', N'KH003', N'NV002', N'GiaoTanNoi', N'DaGiao',   0, NULL),
(N'DH000012', '2026-03-15 16:20', N'KH004', N'NV005', N'GiaoTanNoi', N'DaGiao',   0, NULL),
(N'DH000013', '2026-03-16 09:15', N'KH001', N'NV006', N'GiaoTanNoi', N'DaGiao',   0, NULL),
(N'DH000014', '2026-03-16 11:40', N'KH002', N'NV002', N'TaiQuay',    N'HoanThanh',0, NULL),
(N'DH000015', '2026-03-17 14:05', N'KH003', N'NV005', N'GiaoTanNoi', N'DaGiao',   0, NULL),
(N'DH000016', '2026-03-17 17:30', N'KH004', N'NV006', N'GiaoTanNoi', N'DaGiao',   0, NULL),
(N'DH000017', '2026-03-18 08:50', N'KH001', N'NV002', N'GiaoTanNoi', N'DaGiao',   0, NULL),
(N'DH000018', '2026-03-18 10:15', N'KH002', N'NV005', N'TaiQuay',    N'HoanThanh',0, NULL),
(N'DH000019', '2026-03-19 13:45', N'KH003', N'NV006', N'GiaoTanNoi', N'DaGiao',   0, NULL),
(N'DH000020', '2026-03-19 15:20', N'KH004', N'NV002', N'GiaoTanNoi', N'DaGiao',   0, NULL),
(N'DH000021', '2026-03-20 09:00', N'KH001', N'NV005', N'TaiQuay',    N'HoanThanh',0, NULL),
(N'DH000022', '2026-03-20 11:30', N'KH002', N'NV006', N'GiaoTanNoi', N'DaGiao',   0, NULL),
(N'DH000023', '2026-03-21 14:10', N'KH003', N'NV002', N'GiaoTanNoi', N'DaGiao',   0, NULL),
(N'DH000024', '2026-03-21 16:45', N'KH004', N'NV005', N'TaiQuay',    N'HoanThanh',0, NULL);

-- Chi tiáº¿t Ä‘Æ¡n hĂ ng
INSERT INTO CHI_TIET_DON_HANG (MaDon, MaSP, SoLuong, DonGia, ThanhTien) VALUES
(N'DH000001', N'SP001', 2, 180000, 360000),
(N'DH000001', N'SP002', 1, 220000, 220000),
(N'DH000002', N'SP001', 1, 180000, 180000),
(N'DH000002', N'SP002', 1, 220000, 220000),
(N'DH000003', N'SP003', 2, 80000,  160000),
(N'DH000004', N'SP001', 1, 180000, 180000),
(N'DH000004', N'SP004', 2, 150000, 300000),
(N'DH000005', N'SP002', 1, 220000, 220000),
(N'DH000006', N'SP003', 3, 80000,  240000),
(N'DH000007', N'SP001', 1, 180000, 180000),
(N'DH000008', N'SP005', 2, 160000, 320000),
(N'DH000009', N'SP004', 1, 150000, 150000),
(N'DH000010', N'SP002', 2, 220000, 440000),
(N'DH000011', N'SP003', 1, 80000,  80000),
(N'DH000012', N'SP001', 3, 180000, 540000),
(N'DH000013', N'SP005', 1, 160000, 160000),
(N'DH000014', N'SP004', 2, 150000, 300000),
(N'DH000015', N'SP002', 1, 220000, 220000),
(N'DH000016', N'SP003', 2, 80000,  160000),
(N'DH000017', N'SP001', 1, 180000, 180000),
(N'DH000018', N'SP005', 3, 160000, 480000),
(N'DH000019', N'SP004', 1, 150000, 150000),
(N'DH000020', N'SP002', 2, 220000, 440000),
(N'DH000021', N'SP003', 1, 80000,  80000),
(N'DH000022', N'SP001', 2, 180000, 360000),
(N'DH000023', N'SP005', 1, 160000, 160000),
(N'DH000024', N'SP004', 2, 150000, 300000);

-- Cáº­p nháº­t TongTien dá»±a trĂªn chi tiáº¿t
UPDATE dh SET dh.TongTien = (SELECT ISNULL(SUM(ThanhTien),0) FROM CHI_TIET_DON_HANG WHERE MaDon = dh.MaDon)
FROM DON_HANG dh;

-- Giao hĂ ng
INSERT INTO GIAO_HANG VALUES
(N'GH000001', N'DH000001', N'NV004', NULL,                  N'ChoPhanCong', NULL),
(N'GH000002', N'DH000002', N'NV004', '2026-03-11 09:30',    N'DangGiao',    N'CĂ³ thiá»‡p sinh nháº­t'),
(N'GH000004', N'DH000004', N'NV004', '2026-03-10 18:30',    N'GiaoThanhCong', NULL),
(N'GH000005', N'DH000005', N'NV007', '2026-03-12 11:00',    N'GiaoThanhCong', NULL),
(N'GH000006', N'DH000006', N'NV008', '2026-03-12 12:30',    N'GiaoThanhCong', NULL),
(N'GH000007', N'DH000007', N'NV004', '2026-03-13 10:20',    N'GiaoThanhCong', NULL),
(N'GH000008', N'DH000008', N'NV007', '2026-03-13 15:10',    N'GiaoThanhCong', NULL),
(N'GH000009', N'DH000009', N'NV008', '2026-03-14 09:45',    N'GiaoThanhCong', NULL),
(N'GH000010', N'DH000011', N'NV004', '2026-03-15 11:30',    N'GiaoThanhCong', NULL),
(N'GH000011', N'DH000012', N'NV007', '2026-03-15 17:20',    N'GiaoThanhCong', NULL),
(N'GH000012', N'DH000013', N'NV008', '2026-03-16 10:15',    N'GiaoThanhCong', NULL),
(N'GH000013', N'DH000015', N'NV004', '2026-03-17 15:05',    N'GiaoThanhCong', NULL),
(N'GH000014', N'DH000016', N'NV007', '2026-03-17 18:30',    N'GiaoThanhCong', NULL),
(N'GH000015', N'DH000017', N'NV008', '2026-03-18 09:50',    N'GiaoThanhCong', NULL),
(N'GH000016', N'DH000019', N'NV004', '2026-03-19 14:45',    N'GiaoThanhCong', NULL),
(N'GH000017', N'DH000020', N'NV007', '2026-03-19 16:20',    N'GiaoThanhCong', NULL),
(N'GH000018', N'DH000022', N'NV008', '2026-03-20 12:30',    N'GiaoThanhCong', NULL),
(N'GH000019', N'DH000023', N'NV004', '2026-03-21 15:10',    N'GiaoThanhCong', NULL);

-- Phiáº¿u nháº­p kho
INSERT INTO PHIEU_NHAP_KHO VALUES
(N'PN000001', '2026-03-10 07:30', N'NV003', N'HĂ ng tá»« vá»±a BĂ¬nh Äiá»n');

-- Táº¡m táº¯t trigger Ä‘á»ƒ trĂ¡nh xung Ä‘á»™t tá»“n kho khi nháº­p data máº«u
DISABLE TRIGGER trg_NhapKho_TangTon ON CT_NHAP_KHO;
GO

INSERT INTO CT_NHAP_KHO VALUES
(N'PN000001', N'SP001', 50, 120000),
(N'PN000001', N'SP002', 30, 160000);

-- Báº­t láº¡i trigger
ENABLE TRIGGER trg_NhapKho_TangTon ON CT_NHAP_KHO;
GO

-- Pháº£n há»“i
INSERT INTO PHAN_HOI VALUES
-- 10 Pháº£n há»“i tá»‘t
(N'PH000001', N'DH000005', N'Hoa ráº¥t Ä‘áº¹p vĂ  tÆ°Æ¡i, giao hĂ ng Ä‘Ăºng giá».', '2026-03-12 12:00', N'ChuaXuLy', NULL),
(N'PH000002', N'DH000006', N'Dá»‹ch vá»¥ tuyá»‡t vá»i, nhĂ¢n viĂªn tÆ° váº¥n nhiá»‡t tĂ¬nh.', '2026-03-12 14:00', N'ChuaXuLy', NULL),
(N'PH000003', N'DH000007', N'Ráº¥t hĂ i lĂ²ng vá»›i bĂ³ hoa ká»· niá»‡m, báº¡n gĂ¡i tĂ´i ráº¥t thĂ­ch.', '2026-03-13 11:00', N'ChuaXuLy', NULL),
(N'PH000004', N'DH000008', N'Cháº¥t lÆ°á»£ng hoa vÆ°á»£t mong Ä‘á»£i, sáº½ tiáº¿p tá»¥c á»§ng há»™.', '2026-03-13 16:00', N'ChuaXuLy', NULL),
(N'PH000005', N'DH000009', N'Hoa giá»‘ng há»‡t hĂ¬nh máº«u trĂªn web, gĂ³i ghĂ©m cáº©n tháº­n.', '2026-03-14 10:00', N'ChuaXuLy', NULL),
(N'PH000006', N'DH000010', N'Láº¥y hoa táº¡i quáº§y ráº¥t nhanh, hoa Ä‘Æ°á»£c chuáº©n bá»‹ sáºµn ráº¥t Ä‘áº¹p.', '2026-03-14 16:00', N'ChuaXuLy', NULL),
(N'PH000007', N'DH000011', N'Shipper lá»‹ch sá»±, hoa giao tá»›i khĂ´ng bá»‹ dáº­p nĂ¡t.', '2026-03-15 12:00', N'ChuaXuLy', NULL),
(N'PH000008', N'DH000012', N'Äáº·t hĂ ng online ráº¥t dá»… dĂ ng vĂ  tiá»‡n lá»£i.', '2026-03-15 18:00', N'ChuaXuLy', NULL),
(N'PH000009', N'DH000013', N'GiĂ¡ cáº£ há»£p lĂ½ so vá»›i cháº¥t lÆ°á»£ng hoa nháº­n Ä‘Æ°á»£c.', '2026-03-16 11:00', N'ChuaXuLy', NULL),
(N'PH000010', N'DH000014', N'Giáº¥y gĂ³i hoa ráº¥t sang trá»ng, tĂ´i ráº¥t Æ°ng Ă½.', '2026-03-16 13:00', N'ChuaXuLy', NULL),
-- 5 Pháº£n há»“i bĂ¬nh thÆ°á»ng
(N'PH000011', N'DH000015', N'Hoa Ä‘áº¹p nhÆ°ng bĂ³ hÆ¡i nhá» so vá»›i tÆ°á»Ÿng tÆ°á»£ng.', '2026-03-17 16:00', N'DangXuLy', NULL),
(N'PH000012', N'DH000016', N'Giao hĂ ng hÆ¡i trá»… 15 phĂºt nhÆ°ng hoa váº«n tÆ°Æ¡i.', '2026-03-17 19:00', N'ChuaXuLy', NULL),
(N'PH000013', N'DH000017', N'Giáº¥y gĂ³i mĂ u hÆ¡i nháº¡t hÆ¡n so vá»›i áº£nh, nhÆ°ng nhĂ¬n chung á»•n.', '2026-03-18 11:00', N'DangXuLy', NULL),
(N'PH000014', N'DH000018', N'Hoa táº¡m á»•n, cĂ³ 1 bĂ´ng cĂ³ váº» hÆ¡i dáº­p cĂ¡nh ngoĂ i.', '2026-03-18 12:00', N'ChuaXuLy', NULL),
(N'PH000015', N'DH000019', N'KhĂ´ng cĂ³ gĂ¬ Ä‘áº·c biá»‡t, dá»‹ch vá»¥ á»Ÿ má»©c cháº¥p nháº­n Ä‘Æ°á»£c.', '2026-03-19 15:00', N'ChuaXuLy', NULL),
-- 5 Pháº£n há»“i tá»‡
(N'PH000016', N'DH000020', N'Hoa bá»‹ hĂ©o khĂ¡ nhiá»u, khĂ´ng Ä‘Ă¡ng tiá»n chĂºt nĂ o.', '2026-03-19 17:00', N'DangXuLy', NULL),
(N'PH000017', N'DH000021', N'NhĂ¢n viĂªn á»Ÿ quáº§y khĂ´ng niá»m ná»Ÿ, thĂ¡i Ä‘á»™ phá»¥c vá»¥ kĂ©m.', '2026-03-20 11:00', N'DangXuLy', NULL),
(N'PH000018', N'DH000022', N'NhĂ¢n viĂªn tÆ° váº¥n sai loáº¡i hoa tĂ´i yĂªu cáº§u.', '2026-03-20 13:00', N'DangXuLy', NULL),
(N'PH000019', N'DH000023', N'Hoa dáº­p nĂ¡t nhiá»u, yĂªu cáº§u hoĂ n tiá»n hoáº·c Ä‘á»•i tráº£.', '2026-03-21 16:00', N'DaXuLy', N'ÄĂ£ Ä‘á»“ng Ă½ hoĂ n tráº£ 100%'),
(N'PH000020', N'DH000024', N'Dá»‹ch vá»¥ tá»‡, gĂ³i hoa ráº¥t cáº©u tháº£.', '2026-03-21 18:00', N'ChuaXuLy', NULL);

-- PhĂ¢n quyá»n máº«u
INSERT INTO PHAN_QUYEN VALUES
-- Admin: toĂ n quyá»n
(N'Admin', N'Dashboard',    1, 1, 1, 1, 1),
(N'Admin', N'DonHang',      1, 1, 1, 1, 1),
(N'Admin', N'KhoHang',      1, 1, 1, 1, 1),
(N'Admin', N'GiaoHang',     1, 1, 1, 1, 1),
(N'Admin', N'NhanVien',     1, 1, 1, 1, 1),
(N'Admin', N'KhachHang',    1, 1, 1, 1, 1),
(N'Admin', N'SanPham',      1, 1, 1, 1, 1),
(N'Admin', N'PhanQuyen',    1, 1, 1, 0, 0),
(N'Admin', N'BaoCao',       1, 0, 0, 0, 1),
(N'Admin', N'TraHang',      1, 1, 1, 0, 0),
(N'Admin', N'PhanHoi',      1, 1, 1, 0, 0),
-- Cashier: bĂ¡n hĂ ng, khĂ¡ch hĂ ng
(N'Cashier', N'Dashboard',  1, 0, 0, 0, 0),
(N'Cashier', N'DonHang',    1, 1, 1, 0, 0),
(N'Cashier', N'KhachHang',  1, 1, 1, 0, 0),
(N'Cashier', N'SanPham',    1, 0, 0, 0, 0),
(N'Cashier', N'TraHang',    1, 1, 0, 0, 0),
(N'Cashier', N'PhanHoi',    1, 1, 0, 0, 0),
-- Warehouse: kho hĂ ng
(N'Warehouse', N'Dashboard',1, 0, 0, 0, 0),
(N'Warehouse', N'KhoHang',  1, 1, 1, 0, 1),
(N'Warehouse', N'SanPham',  1, 1, 1, 0, 0),
-- Shipper: giao hĂ ng
(N'Shipper', N'Dashboard',  1, 0, 0, 0, 0),
(N'Shipper', N'GiaoHang',   1, 0, 1, 0, 0);

-- =====================================================
-- SP: Doanh thu theo ngĂ y trong thĂ¡ng (biá»ƒu Ä‘á»“ bĂ¡o cĂ¡o thĂ¡ng)
-- =====================================================
GO

CREATE OR ALTER PROCEDURE sp_DoanhThuTheoNgayTrongThang
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

PRINT N'âœ… Database FloriSys Ä‘Ă£ Ä‘Æ°á»£c táº¡o thĂ nh cĂ´ng!';
GO

