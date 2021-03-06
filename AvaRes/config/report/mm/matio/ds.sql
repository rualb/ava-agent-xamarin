SELECT
ITM.NAME,
ITM.STGRPCODE,
ITM.STGRPCODESUB,
DATA.QSTART,
DATA.QIN,
DATA.QOUT,
(DATA.QSTART+DATA.QIN-DATA.QOUT) QFINISH
FROM 
(
	SELECT SUM(QSTART) QSTART,SUM(QIN) QIN,SUM(QOUT) QOUT,STOCKREF STOCKREF
	FROM
	(
		SELECT
		0.0 QSTART,
		SUM(
		( 
		CASE 
			WHEN LN.IOCODE IN (1,2) THEN +1 
			ELSE 0 
		END 
		)*LN.AMOUNT) QIN,
		SUM(
		( 
		CASE  
			WHEN LN.IOCODE IN (3,4) THEN +1 
			ELSE 0 
		END 
		)*LN.AMOUNT) QOUT,
		LN.STOCKREF
		FROM
		LG_$FIRM$_$PERIOD$_STLINE LN
		WHERE
		LN.CANCELLED = 0
		GROUP BY LN.STOCKREF
		
		UNION
		
		SELECT 
		ITM.ONHAND QSTART,
		0.0 QIN,
		0.0 QOUT,
		ITM.LOGICALREF STOCKREF
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