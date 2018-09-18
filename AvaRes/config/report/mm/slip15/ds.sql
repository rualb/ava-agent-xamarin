select
D.*
from
LG_$FIRM$_$PERIOD$_INVOICE D 
where
D.LOGICALREF = @lref
GO
select 
ITM.CODE,
ITM.NAME,
L.*
from
LG_$FIRM$_$PERIOD$_STLINE L inner join LG_$FIRM$_ITEMS ITM on ITM.LOGICALREF = L.STOCKREF
where
L.STDOCREF = @lref
order by L.STDOCLNNO asc
GO
update LG_$FIRM$_$PERIOD$_INVOICE set READONLY = 1 where LOGICALREF = @lref
GO
