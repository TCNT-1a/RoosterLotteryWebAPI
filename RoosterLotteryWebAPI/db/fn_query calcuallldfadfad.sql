use RoosterLottery;

SELECT 
    ID, 
    DrawTime, ResultNumber
FROM 
    BET 
WHERE 
    ResultNumber IS NULL
    AND dbo.Fn_CalculateNextTime (GetDate()) >dbo.Fn_CalculateNextTime (DrawTime)
	select * from BET

	DECLARE @D DATETIME
	DECLARE @N DATETIME
	SET @N = GETDATE();
	 EXEC [dbo].[CalculateNextTime] @N, @D output