

CREATE PROCEDURE RandomNumber
AS
BEGIN
    DECLARE @RandomNumber INT

    -- Generate a random number between 0 and 9
    SET @RandomNumber = FLOOR(RAND() * 10)

    -- Return the random number
    SELECT @RandomNumber AS RandomNumber
END
GO