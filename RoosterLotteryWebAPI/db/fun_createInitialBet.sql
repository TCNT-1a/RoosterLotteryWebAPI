

CREATE PROCEDURE CreateInitialBet
AS
BEGIN
    DECLARE @CurrentTime DATETIME
    DECLARE @DrawTime DATETIME

    -- Get the current time
    SET @CurrentTime = GETDATE()

    -- Calculate the next hour
    SET @DrawTime = DATEADD(HOUR, DATEPART(HOUR, @CurrentTime) + 1, CAST(CAST(@CurrentTime AS DATE) AS DATETIME))

    -- Check if the DrawTime already exists in the BET table
    IF NOT EXISTS (SELECT 1 FROM BET WHERE DrawTime = @DrawTime)
    BEGIN
        -- Insert the new bet into the BET table with the calculated DrawTime
        INSERT INTO BET (DrawTime)
        VALUES (@DrawTime)

        -- Return the ID and DrawTime of the newly created bet
        SELECT SCOPE_IDENTITY() AS NewBetID, @DrawTime AS DrawTime
    END
    ELSE
    BEGIN
        -- DrawTime already exists, return a message
        SELECT 'DrawTime already exists. No new bet created.' AS Message, @DrawTime AS DrawTime
    END
END
GO
