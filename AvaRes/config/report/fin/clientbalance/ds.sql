select
CL.CODE,
CL.DEFINITION_,
CL.BALANCE
from
LG_$FIRM$_CLCARD CL
where
CL.LOGICALREF = @lref 
GO
select
sum(DOC.NETTOTAL) TOTAL
from
LG_$FIRM$_$PERIOD$_INVOICE DOC
where
DOC.TRCODE = 8 and
DOC.CANCELLED = 0 and
DOC.CLIENTREF = @lref
GO
select
sum(DOC.NETTOTAL) TOTAL
from
LG_$FIRM$_$PERIOD$_INVOICE DOC
where
DOC.TRCODE = 3 and
DOC.CANCELLED = 0 and
DOC.CLIENTREF = @lref
GO
select
sum(DOC.AMOUNT) TOTAL
from
LG_$FIRM$_$PERIOD$_KSLINES DOC
where
DOC.TRCODE = 11 and
DOC.CANCELLED = 0 and
DOC.CLIENTREF = @lref