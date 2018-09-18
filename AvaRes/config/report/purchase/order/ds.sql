select
CL.CODE,
CL.DEFINITION_,
H.*
 from
LG_$FIRM$_$PERIOD$_ORFICHE H inner join LG_$FIRM$_CLCARD CL on CL.LOGICALREF = H.CLIENTREF
where
H.LOGICALREF = @lref
GO
select 
ITM.CODE,
ITM.NAME,
L.*
from
LG_$FIRM$_$PERIOD$_ORFLINE L inner join LG_$FIRM$_ITEMS ITM on ITM.LOGICALREF = L.STOCKREF
where
L.STDOCREF = @lref
order by L.STDOCLNNO asc
GO
