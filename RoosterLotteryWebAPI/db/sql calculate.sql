USE [RoosterLottery]
GO
/****** Object:  StoredProcedure [dbo].[PerformLotteryDraw]    Script Date: 6/2/2024 10:29:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[PerformLotteryDraw]
AS
BEGIN
    -- Step 1: Generate a random result number
    DECLARE @ResultNumber INT
    SET @ResultNumber = FLOOR(RAND() * 10)

    -- Step 2: Declare necessary variables
    DECLARE @LatestBetID INT
    DECLARE @DrawTime DATETIME
    DECLARE @CurrentTime DATETIME
    DECLARE @NextTimeDraw DATETIME
    DECLARE @NextTimeCurrent DATETIME

    -- Get the current time
    SET @CurrentTime = GETDATE()

    -- Step 3: Select all bets with NULL ResultNumber
    DECLARE bet_cursor CURSOR FOR
    SELECT ID, DrawTime FROM BET WHERE ResultNumber IS NULL 
	 EXEC [dbo].[CalculateNextTime] @DrawTime, @NextTimeDraw OUTPUT
        EXEC [dbo].[CalculateNextTime] @CurrentTime, @NextTimeCurrent OUTPUT

    OPEN bet_cursor

    FETCH NEXT FROM bet_cursor INTO @LatestBetID, @DrawTime

    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Step 4: Calculate the next draw times
        EXEC [dbo].[CalculateNextTime] @DrawTime, @NextTimeDraw OUTPUT
        EXEC [dbo].[CalculateNextTime] @CurrentTime, @NextTimeCurrent OUTPUT

        -- Print statements for debugging


        -- Step 5: Update ResultNumber if the draw time condition is met
        IF @NextTimeDraw < @NextTimeCurrent
        BEGIN
            UPDATE BET
            SET ResultNumber = FLOOR(RAND() * 10)
            WHERE ID = @LatestBetID

            -- Step 6: Update the isWinner column for users with matching bets
            UPDATE PLAYER_BET
            SET isWinner = 1
            FROM PLAYER_BET pb
            INNER JOIN BET b ON pb.BET_ID = b.ID
            WHERE b.ID = @LatestBetID
            AND pb.BetNumber = @ResultNumber
        END

        FETCH NEXT FROM bet_cursor INTO @LatestBetID, @DrawTime
    END

    CLOSE bet_cursor
    DEALLOCATE bet_cursor
END
GO
