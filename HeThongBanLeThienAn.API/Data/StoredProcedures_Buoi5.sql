/*
================================================================================
  HỆ THỐNG BÁN LẺ THIÊN AN - BUỔI 5: STORED PROCEDURES (T-SQL / SQL SERVER)
  Mô tả: Toàn bộ kịch bản tạo Stored Procedure theo giáo trình Buổi 5
================================================================================
*/

USE MiniSupermarketDb;
GO

-- =============================================================================
-- TIẾT 3: STORED PROCEDURE CHÍNH
-- sp_GetRevenueByCashier: Thống kê tổng hợp doanh thu ca trực theo thu ngân
-- =============================================================================
IF OBJECT_ID('sp_GetRevenueByCashier', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetRevenueByCashier;
GO

CREATE PROCEDURE sp_GetRevenueByCashier
AS
BEGIN
    -- SET NOCOUNT ON: Ngăn SQL Server gửi số dòng bị ảnh hưởng (DoneInProc)
    -- Tối ưu tốc độ truyền gói tin qua mạng cho SqlDataReader
    SET NOCOUNT ON;

    -- Truy vấn gom nhóm theo tên đăng nhập thu ngân (CashierUsername)
    -- Tính tổng đơn hàng, tổng doanh thu và tổng điểm thưởng đã phát hành
    SELECT 
        CashierUsername,
        COUNT(OrderId) AS TotalOrders,
        SUM(FinalAmount) AS TotalRevenue,
        SUM(RewardPoints) AS TotalPointsAwarded
    FROM Orders
    GROUP BY CashierUsername
    ORDER BY TotalRevenue DESC; -- Sắp xếp doanh thu từ cao xuống thấp
END
GO

-- =============================================================================
-- BÀI TẬP 3.1: STORED PROCEDURE BÁO CÁO DOANH THU THEO KHOẢNG NGÀY (CÓ THAM SỐ)
-- sp_GetRevenueByDateRange: Thống kê doanh thu theo thu ngân trong khoảng ngày
-- =============================================================================
IF OBJECT_ID('sp_GetRevenueByDateRange', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetRevenueByDateRange;
GO

CREATE PROCEDURE sp_GetRevenueByDateRange
    @FromDate DATETIME,
    @ToDate DATETIME
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        CashierUsername,
        COUNT(OrderId) AS TotalOrders,
        SUM(FinalAmount) AS TotalRevenue,
        SUM(RewardPoints) AS TotalPointsAwarded
    FROM Orders
    WHERE CreatedDate >= @FromDate AND CreatedDate < @ToDate
    GROUP BY CashierUsername
    ORDER BY TotalRevenue DESC;
END
GO

-- =============================================================================
-- BÀI TẬP 3.2: STORED PROCEDURE TOP 5 SẢN PHẨM BÁN CHẠY NHẤT
-- sp_GetTop5SellingProducts: Kết hợp OrderDetails và Products để tìm top 5
-- =============================================================================
IF OBJECT_ID('sp_GetTop5SellingProducts', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetTop5SellingProducts;
GO

CREATE PROCEDURE sp_GetTop5SellingProducts
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 5
        p.ProductId,
        p.Barcode,
        p.ProductName,
        p.Price,
        SUM(od.Quantity) AS TotalSoldQuantity,
        SUM(od.LineTotal) AS TotalSalesAmount
    FROM OrderDetails od
    INNER JOIN Products p ON od.ProductId = p.ProductId
    GROUP BY p.ProductId, p.Barcode, p.ProductName, p.Price
    ORDER BY TotalSoldQuantity DESC, TotalSalesAmount DESC;
END
GO

-- =============================================================================
-- KIỂM THỬ CÁC THỦ TỤC VỪA TẠO
-- =============================================================================
-- EXEC sp_GetRevenueByCashier;
-- EXEC sp_GetRevenueByDateRange @FromDate = '2026-10-01', @ToDate = '2026-10-31';
-- EXEC sp_GetTop5SellingProducts;
