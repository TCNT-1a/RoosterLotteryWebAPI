
CREATE PROCEDURE FindPlayerByPhoneNumber
    @PhoneNumber NVARCHAR(15)
AS
BEGIN
    -- Ensure no SQL injection
    SET @PhoneNumber = LTRIM(RTRIM(@PhoneNumber))

    SELECT 
        ID,
        FullName,
        DateOfBirth,
        PhoneNumber
    FROM 
        dbo.PLAYER
    WHERE 
        PhoneNumber = @PhoneNumber
END
GO