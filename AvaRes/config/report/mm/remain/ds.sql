SELECT
ITM.NAME,
ITM.STGRPCODE,
ITM.STGRPCODESUB,
DATA.AMOUNT
FROM 
(
	SELECT SUM(AMOUNT) AMOUNT,STOCKREF STOCKREF
	FROM
	(
		SELECT
		SUM(
		( 
		CASE 
			WHEN LN.IOCODE IN (1,2) THEN +1 
			WHEN LN.IOCODE IN (3,4) THEN -1 
			ELSE 0 
		END 
		)*LN.AMOUNT) AMOUNT,
		LN.STOCKREF
		FROM
		LG_$FIRM$_$PERIOD$_STLINE LN
		WHERE
		LN.CANCELLED = 0
		GROUP BY LN.STOCKREF
		
		UNION
		
		SELECT 
		ITM.ONHAND,
		ITM.LOGICALREF
		FROM
		LG_$FIRM$_ITEMS ITM
		WHERE ITM.ONHAND > 0
	) DATA_
	GROUP BY STOCKREF	
) DATA
INNER JOIN
LG_$FIRM$_ITEMS ITM
ON DATA.STOCKREF = ITM.LOGICALREF
GO
