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
-- 11. BẢNG HÀNG HƯ (Lịch sử hủy hàng)
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
-- 12. BẢNG PHÂN QUYỀN
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
-- 13. BẢNG TRẢ HÀNG
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
-- 14. BẢNG CHI TIẾT TRẢ HÀNG
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
    -- Khi Hủy từ DangXuLy → hoàn lại tồn kho (đã trừ khi xuất kho)
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
    -- HoanHang: KHÔNG hoàn kho tại đây. Tồn kho được quản lý bởi phiếu TRA_HANG (CT_TRA_HANG.CoNhapKho)
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

-- SP: Ghi nhận hàng hư (giảm tồn kho + lưu lịch sử)
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
    SET @SQL = N'SELECT @Num = ISNULL(MAX(CAST(SUBSTRING(' + QUOTENAME(@Column) + N', LEN(''' + @Prefix + N''')+1, 10) AS INT)), 0) FROM ' + QUOTENAME(@Table);
    EXEC sp_executesql @SQL, N'@Num INT OUTPUT', @Num = @MaxNum OUTPUT;
    SET @NewCode = @Prefix + RIGHT('000000' + CAST(@MaxNum + 1 AS NVARCHAR), 6);
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
(N'SP000001', N'Hoa hồng đỏ (bó 10)',    N'Hoa tươi', 180000, 120000, 42, 10, N'DangBan'),
(N'SP000002', N'Tulip hồng (bó 5)',       N'Hoa tươi', 220000, 160000, 28, 10, N'DangBan'),
(N'SP000003', N'Cúc vàng (bó 10)',        N'Hoa tươi', 80000,  45000,  3,  10, N'DangBan'),
(N'SP000004', N'Ly trắng (bó 5)',         N'Hoa tươi', 150000, 100000, 8,  10, N'DangBan'),
(N'SP000005', N'Hoa anh đào (bó 7)',      N'Hoa tươi', 160000, 110000, 15, 5,  N'DangBan'),
(N'SP000006', N'Lá decor xanh',           N'Phụ kiện', 30000,  20000,  0,  5,  N'DangBan'),
(N'SP000007', N'Ruy băng trang trí',      N'Phụ kiện', 15000,  8000,   200,50, N'DangBan'),
(N'SP000008', N'Hoa lay ơn (bó 10)',      N'Hoa tươi', 90000,  55000,  0,  10, N'NgungBan');

-- Khách hàng
INSERT INTO KHACH_HANG VALUES
(N'KH000001', N'Nguyễn Thị Lan',  N'0901234567', N'12 Lê Lợi, Q.1, TP.HCM',       N'lan.nt@gmail.com',  '2025-06-15'),
(N'KH000002', N'Trần Văn Hùng',   N'0912345678', N'45 Nguyễn Huệ, Q.1, TP.HCM',   N'hung.tv@gmail.com', '2025-09-01'),
(N'KH000003', N'Lê Thị Mai',      N'0923456789', NULL,                              NULL,                 '2026-01-20'),
(N'KH000004', N'Phạm Văn An',     N'0934567890', N'78 Trần Phú, Q.5, TP.HCM',      N'an.pv@email.com',   '2025-03-10');

-- Đơn hàng mẫu
INSERT INTO DON_HANG VALUES
(N'DH000001', '2026-03-11 09:15', N'KH001', N'NV002', N'GiaoTanNoi', N'Moi',      0, N'Giao trước 10h sáng'),
(N'DH000002', '2026-03-11 08:40', N'KH002', N'NV002', N'GiaoTanNoi', N'DangXuLy', 0, N'Gói quà đẹp, có thiệp'),
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

-- Chi tiết đơn hàng
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

-- Cập nhật TongTien dựa trên chi tiết
UPDATE dh SET dh.TongTien = (SELECT ISNULL(SUM(ThanhTien),0) FROM CHI_TIET_DON_HANG WHERE MaDon = dh.MaDon)
FROM DON_HANG dh;

-- Giao hàng
INSERT INTO GIAO_HANG VALUES
(N'GH000001', N'DH000001', N'NV004', NULL,                  N'ChoPhanCong', NULL),
(N'GH000002', N'DH000002', N'NV004', '2026-03-11 09:30',    N'DangGiao',    N'Có thiệp sinh nhật'),
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

-- Phiếu nhập kho
INSERT INTO PHIEU_NHAP_KHO VALUES
(N'PN000001', '2026-03-10 07:30', N'NV003', N'Hàng từ vựa Bình Điền');

-- Tạm tắt trigger để tránh xung đột tồn kho khi nhập data mẫu
DISABLE TRIGGER trg_NhapKho_TangTon ON CT_NHAP_KHO;
GO

INSERT INTO CT_NHAP_KHO VALUES
(N'PN000001', N'SP001', 50, 120000),
(N'PN000001', N'SP002', 30, 160000);

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

-- Phân quyền mẫu
INSERT INTO PHAN_QUYEN VALUES
-- Admin: toàn quyền
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
-- Cashier: bán hàng, khách hàng
(N'Cashier', N'Dashboard',  1, 0, 0, 0, 0),
(N'Cashier', N'DonHang',    1, 1, 1, 0, 0),
(N'Cashier', N'KhachHang',  1, 1, 1, 0, 0),
(N'Cashier', N'SanPham',    1, 0, 0, 0, 0),
(N'Cashier', N'TraHang',    1, 1, 0, 0, 0),
(N'Cashier', N'PhanHoi',    1, 1, 0, 0, 0),
-- Warehouse: kho hàng
(N'Warehouse', N'Dashboard',1, 0, 0, 0, 0),
(N'Warehouse', N'KhoHang',  1, 1, 1, 0, 1),
(N'Warehouse', N'SanPham',  1, 1, 1, 0, 0),
-- Shipper: giao hàng
(N'Shipper', N'Dashboard',  1, 0, 0, 0, 0),
(N'Shipper', N'GiaoHang',   1, 0, 1, 0, 0);

-- =====================================================
-- SP: Doanh thu theo ngày trong tháng (biểu đồ báo cáo tháng)
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

PRINT N'✅ Database FloriSys đã được tạo thành công!';
GO


-- =====================================================
-- PH?N B? SUNG: L?CH S? ��N H�NG V� D? LI?U M?U
-- =====================================================

-- =====================================================
-- BẢNG LỊCH SỬ TRẠNG THÁI ĐƠN HÀNG
-- Tự động ghi log mỗi khi đơn hàng thay đổi trạng thái
-- =====================================================

-- 1. Tạo bảng lịch sử
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'LICH_SU_DON_HANG')
BEGIN
    CREATE TABLE LICH_SU_DON_HANG (
        Id           INT IDENTITY(1,1) PRIMARY KEY,
        MaDon        NVARCHAR(20)  NOT NULL REFERENCES DON_HANG(MaDon),
        TrangThai    NVARCHAR(20)  NOT NULL,
        ThoiGian     DATETIME      NOT NULL DEFAULT GETDATE(),
        GhiChu       NVARCHAR(500)
    );
END
GO

-- 2. Trigger: Tự động ghi log khi INSERT đơn mới
IF EXISTS (SELECT * FROM sys.triggers WHERE name = 'trg_DonHang_Insert_Log')
    DROP TRIGGER trg_DonHang_Insert_Log;
GO

CREATE TRIGGER trg_DonHang_Insert_Log
ON DON_HANG
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO LICH_SU_DON_HANG (MaDon, TrangThai, ThoiGian, GhiChu)
    SELECT MaDon, TrangThai, GETDATE(), N'Tạo đơn hàng mới'
    FROM inserted;
END
GO

-- 3. Trigger: Tự động ghi log khi UPDATE trạng thái
IF EXISTS (SELECT * FROM sys.triggers WHERE name = 'trg_DonHang_Update_Log')
    DROP TRIGGER trg_DonHang_Update_Log;
GO

CREATE TRIGGER trg_DonHang_Update_Log
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
END
GO

-- 4. Backfill: Ghi log cho các đơn hàng đã tồn tại (chưa có lịch sử)
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

PRINT N'✅ Tạo bảng LICH_SU_DON_HANG + trigger thành công!';
GO




-- Phan bo sung: Du lieu mau mo rong
USE FloriSys;
GO

-- 1. THÊM PHIẾU NHẬP KHO (Để có hàng bán)
DECLARE @MaNV_Kho NVARCHAR(20) = (SELECT TOP 1 MaNV FROM NHAN_VIEN WHERE ChucVu = 'Warehouse');
IF @MaNV_Kho IS NULL SET @MaNV_Kho = 'NV003';

IF NOT EXISTS (SELECT 1 FROM PHIEU_NHAP_KHO WHERE MaPhieu = 'PN000002')
BEGIN
    INSERT INTO PHIEU_NHAP_KHO (MaPhieu, NgayNhap, MaNV, GhiChu) VALUES
    ('PN000002', '2026-01-05 08:00', @MaNV_Kho, N'Nhập hàng đầu năm'),
    ('PN000003', '2026-03-15 09:00', @MaNV_Kho, N'Nhập hàng bổ sung tháng 3'),
    ('PN000004', '2026-04-25 14:00', @MaNV_Kho, N'Nhập hàng chuẩn bị lễ');

    INSERT INTO CT_NHAP_KHO (MaPhieu, MaSP, SoLuong, GiaNhap)
    SELECT 'PN000002', MaSP, 100, GiaNhap FROM SAN_PHAM;
    
    INSERT INTO CT_NHAP_KHO (MaPhieu, MaSP, SoLuong, GiaNhap)
    SELECT 'PN000003', MaSP, 50, GiaNhap FROM SAN_PHAM WHERE MaSP LIKE '%1%';
END

-- 2. TẠO ĐƠN HÀNG VÀ CHIA ĐỀU CHO NHÂN VIÊN
DECLARE @StartDate DATETIME = '2026-01-10';
DECLARE @EndDate DATETIME = '2026-05-07'; -- Trước ngày 8/5
DECLARE @CurrentOrderNum INT = (SELECT ISNULL(MAX(CAST(SUBSTRING(MaDon, 3, 6) AS INT)), 0) FROM DON_HANG);
DECLARE @GHNum INT = (SELECT ISNULL(MAX(CAST(SUBSTRING(MaGiaoHang, 3, 6) AS INT)), 0) FROM GIAO_HANG);

-- Lấy danh sách nhân viên vào bảng tạm để chia đều
SELECT MaNV, ROW_NUMBER() OVER (ORDER BY MaNV) as ID INTO #Cashiers FROM NHAN_VIEN WHERE ChucVu IN ('Cashier', 'Admin');
SELECT MaNV, ROW_NUMBER() OVER (ORDER BY MaNV) as ID INTO #Shippers FROM NHAN_VIEN WHERE ChucVu = 'Shipper';

DECLARE @NumCashiers INT = (SELECT COUNT(*) FROM #Cashiers);
DECLARE @NumShippers INT = (SELECT COUNT(*) FROM #Shippers);

IF @NumCashiers > 0 AND @NumShippers > 0
BEGIN
    DECLARE @i INT = 1;
    DECLARE @TotalOrders INT = 120; 

    DECLARE @CurrentDate DATETIME = @StartDate;
    WHILE @i <= @TotalOrders
    BEGIN
        SET @CurrentDate = DATEADD(MINUTE, 30 + ABS(CHECKSUM(NEWID())) % 1440, @CurrentDate);
        DECLARE @OrderDate DATETIME = @CurrentDate;
        
        SET @CurrentOrderNum = @CurrentOrderNum + 1;
        DECLARE @MaDon NVARCHAR(20) = 'DH' + RIGHT('000000' + CAST(@CurrentOrderNum AS NVARCHAR), 6);
        
        DECLARE @CashierID INT = (@i % @NumCashiers) + 1;
        DECLARE @MaNV_Tao NVARCHAR(20) = (SELECT MaNV FROM #Cashiers WHERE ID = @CashierID);
        
        DECLARE @MaKH NVARCHAR(20) = (SELECT TOP 1 MaKH FROM KHACH_HANG ORDER BY NEWID());
        DECLARE @TrangThaiDon NVARCHAR(20) = CASE WHEN (@i % 10 = 0) THEN N'HoanHang' ELSE N'HoanThanh' END;

        INSERT INTO DON_HANG (MaDon, NgayTao, MaKH, MaNV_TaoDon, HinhThucNhanHang, TrangThai, TongTien)
        VALUES (@MaDon, @OrderDate, @MaKH, @MaNV_Tao, N'GiaoTanNoi', @TrangThaiDon, 0);

        INSERT INTO CHI_TIET_DON_HANG (MaDon, MaSP, SoLuong, DonGia)
        SELECT TOP 1 @MaDon, MaSP, (1 + ABS(CHECKSUM(NEWID())) % 2), GiaBan 
        FROM SAN_PHAM ORDER BY NEWID();

        UPDATE DON_HANG SET TongTien = (SELECT SUM(ThanhTien) FROM CHI_TIET_DON_HANG WHERE MaDon = @MaDon) WHERE MaDon = @MaDon;

        SET @GHNum = @GHNum + 1;
        DECLARE @MaGH NVARCHAR(20) = 'GH' + RIGHT('000000' + CAST(@GHNum AS NVARCHAR), 6);
        DECLARE @ShipperID INT = (@i % @NumShippers) + 1;
        DECLARE @MaNV_Ship NVARCHAR(20) = (SELECT MaNV FROM #Shippers WHERE ID = @ShipperID);
        
        INSERT INTO GIAO_HANG (MaGiaoHang, MaDon, MaNV_Shipper, NgayGiao, TrangThai)
        VALUES (@MaGH, @MaDon, @MaNV_Ship, DATEADD(MINUTE, 30 + (ABS(CHECKSUM(NEWID())) % 120), @OrderDate), 
                CASE WHEN @TrangThaiDon = N'HoanHang' THEN N'HoanHang' ELSE N'GiaoThanhCong' END);

        SET @i = @i + 1;
    END
END

DROP TABLE #Cashiers;
DROP TABLE #Shippers;

PRINT N'Done';
GO


-- =====================================================
-- 12. TỐI ƯU HÓA HIỆU SUẤT (INDEXES)
-- Cải thiện tốc độ tìm kiếm và phân trang cho dữ liệu lớn
-- =====================================================

-- Index hỗ trợ phân trang danh sách đơn hàng (Sắp xếp theo ngày tạo)
CREATE NONCLUSTERED INDEX idx_DonHang_NgayTao 
ON DON_HANG(NgayTao DESC) 
INCLUDE (TrangThai, TongTien);
GO

-- Index hỗ trợ tìm kiếm đơn hàng theo Trạng thái & Nhân viên tạo
CREATE NONCLUSTERED INDEX idx_DonHang_TrangThai_MaNV 
ON DON_HANG(TrangThai, MaNV_TaoDon);
GO

-- Index hỗ trợ lọc dữ liệu Giao hàng (cho màn hình Shipper)
CREATE NONCLUSTERED INDEX idx_GiaoHang_TrangThai_MaNV 
ON GIAO_HANG(MaNV_Shipper, TrangThai)
INCLUDE (NgayGiao);
GO

-- Index hỗ trợ tìm kiếm sản phẩm theo Trạng thái (để nhanh chóng lấy danh sách đang bán)
CREATE NONCLUSTERED INDEX idx_SanPham_TrangThai 
ON SAN_PHAM(TrangThai) 
INCLUDE (TenSP, GiaBan, SoLuongTon);
GO

-- =====================================================
-- SP: Báo cáo doanh thu theo Quý
-- =====================================================
CREATE OR ALTER PROCEDURE sp_BaoCaoDoanhThuQuy
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

-- =====================================================
-- SP: Doanh thu theo từng tháng trong Quý (biểu đồ)
-- =====================================================
CREATE OR ALTER PROCEDURE sp_DoanhThuTheoThangTrongQuy
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
