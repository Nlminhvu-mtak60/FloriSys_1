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
                WHEN N'DangXuLy'  THEN N'Đã xuất kho – đang xử lý'
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
        WHEN N'DangXuLy'  THEN N'Đã xuất kho – đang xử lý (backfill)'
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
