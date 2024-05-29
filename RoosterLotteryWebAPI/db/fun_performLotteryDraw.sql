CREATE PROCEDURE PerformLotteryDraw
AS
BEGIN
    DECLARE @ResultNumber INT

    -- Step 1: Generate a random result number
    SET @ResultNumber = FLOOR(RAND() * 10)

    -- Step 2: Update the ResultNumber column for the latest bet
    DECLARE @LatestBetID INT

    SELECT @LatestBetID = MAX(ID) FROM BET

    UPDATE BET
    SET ResultNumber = @ResultNumber
    WHERE ID = @LatestBetID

    -- Step 3: Update the isWinner column for users with matching bets
    UPDATE PLAYER_BET
    SET isWinner = 1
    FROM PLAYER_BET pb
    INNER JOIN BET b ON pb.BET_ID = b.ID
    WHERE b.ID = @LatestBetID
    AND pb.BetNumber = @ResultNumber
END
GO