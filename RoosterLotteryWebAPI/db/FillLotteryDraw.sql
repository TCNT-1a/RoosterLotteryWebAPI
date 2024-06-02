-- T?o function m?i
CREATE FUNCTION dbo.Fn_CalculateNextTime (@CurrentTime DATETIME)
RETURNS DATETIME
AS
BEGIN
    DECLARE @NextHour DATETIME;
    DECLARE @NextMinute DATETIME;
    DECLARE @NextTime DATETIME;

    -- T�nh to�n gi? k? ti?p
    SET @NextHour = DATEADD(HOUR, DATEPART(HOUR, @CurrentTime) + 1, CAST(CAST(@CurrentTime AS DATE) AS DATETIME));
    SET @NextMinute = DATEADD(MINUTE, 1, @CurrentTime);

    -- Quy?t ??nh gi� tr? tr? v?
    SET @NextTime = @NextHour;
    -- SET @NextTime = @NextMinute; -- S? d?ng n?u b?n mu?n tr? v? ph�t k? ti?p thay v� gi? k? ti?p

    RETURN @NextTime;
END;
GO