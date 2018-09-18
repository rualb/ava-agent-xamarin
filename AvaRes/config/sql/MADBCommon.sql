CREATE TABLE [L_FIRMPARAMS] (
	LOGICALREF NVARCHAR(40) NOT NULL COLLATE NOCASE,
	CODE NVARCHAR(50) NOT NULL DEFAULT '',
	VALUE TEXT NULL COLLATE NOCASE DEFAULT '',
	CONSTRAINT IFIRMPARAMS_I1 PRIMARY KEY (LOGICALREF),
	CONSTRAINT IFIRMPARAMS_I2 UNIQUE (CODE)
	)
GO

CREATE TABLE L_CAPIWHOUSE (
	LOGICALREF NVARCHAR(40) NOT NULL COLLATE NOCASE,
	NR NVARCHAR(40) NOT NULL COLLATE NOCASE DEFAULT '',
	NAME NVARCHAR(100) NULL COLLATE NOCASE DEFAULT '',
	CONSTRAINT ICAPIWHOUSE_I1 PRIMARY KEY (LOGICALREF),
	CONSTRAINT ICAPIWHOUSE_I2 UNIQUE (NR)
	)
GO
 


 ---------------------------------------------FIRM --------------------


 CREATE TABLE LG_$FIRM$_CLCARD (
	LOGICALREF NVARCHAR(40) NOT NULL COLLATE NOCASE,
	CODE NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	DEFINITION_ NVARCHAR(50) NULL COLLATE NOCASE DEFAULT '',
	SPECODE NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	CYPHCODE NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	CLGRPCODE NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	CLGRPCODESUB NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	PRCLIST SMALLINT NULL DEFAULT 0,
	BALANCE FLOAT NULL DEFAULT 0,
	BALANCEIO FLOAT NULL DEFAULT 0,
	BALANCELIMIT FLOAT NULL DEFAULT 0,
	ORDDAY NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	SPE_ORDDAY NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	DISCPER FLOAT NULL DEFAULT 0,
	BARCODE NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	FILTERPROMOCL NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	RECVERS SMALLINT NULL DEFAULT 0,
	CREATEDDATE DATETIME NULL DEFAULT '1900-01-01',
	LASTTRANS VARCHAR(500) NULL COLLATE NOCASE DEFAULT '', 
	UNIONCODE NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	CONSTRAINT I$FIRM$_CLCARD_I1 PRIMARY KEY (LOGICALREF),
	CONSTRAINT I$FIRM$_CLCARD_I2 UNIQUE (CODE)
	)
GO

CREATE INDEX I$FIRM$_CLCARD_I3 ON LG_$FIRM$_CLCARD (BARCODE)
GO
CREATE INDEX I$FIRM$_CLCARD_I4 ON LG_$FIRM$_CLCARD (CLGRPCODE,CLGRPCODESUB)
GO
CREATE TABLE LG_$FIRM$_ITEMS (
	LOGICALREF NVARCHAR(40) NOT NULL COLLATE NOCASE,
	CODE NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	NAME NVARCHAR(50) NULL COLLATE NOCASE DEFAULT '',
	STGRPCODE NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	STGRPCODESUB NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	UNIT1 NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	UNIT2 NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	UNIT3 NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	UNITREF1 NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	UNITREF2 NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	UNITREF3 NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	UNITCF1 FLOAT NULL DEFAULT 0,
	UNITCF2 FLOAT NULL DEFAULT 0,
	UNITCF3 FLOAT NULL DEFAULT 0,
	PRICE0 FLOAT NULL DEFAULT 0,
	PRICE1 FLOAT NULL DEFAULT 0,
	PRICE2 FLOAT NULL DEFAULT 0,
	PRICE3 FLOAT NULL DEFAULT 0,
	PRICE4 FLOAT NULL DEFAULT 0,
	PRICE5 FLOAT NULL DEFAULT 0,
	COSTPRC FLOAT NULL DEFAULT 0,
	BARCODE1 NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	BARCODE2 NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	BARCODE3 NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	VOLUME_ FLOAT NULL DEFAULT 0,
	WEIGHT FLOAT NULL DEFAULT 0,
	ONHAND FLOAT NULL DEFAULT 0,
	ONHANDIO FLOAT NULL DEFAULT 0,
	ONMAIN FLOAT NULL DEFAULT 0,
	SPE_REMAIN FLOAT NULL DEFAULT 0,
	SPE_PRICE FLOAT NULL DEFAULT 0,
	PROMO SMALLINT NULL DEFAULT 0,
	RECVERS SMALLINT NULL DEFAULT 0,
	CREATEDDATE DATETIME NULL DEFAULT '1900-01-01',
	CONSTRAINT I$FIRM$_ITEMS_I1 PRIMARY KEY (LOGICALREF),
	CONSTRAINT I$FIRM$_ITEMS_I2 UNIQUE (CODE)
	)
GO

CREATE INDEX I$FIRM$_ITEMS_I3 ON LG_$FIRM$_ITEMS (BARCODE1)
GO
CREATE INDEX I$FIRM$_ITEMS_I4 ON LG_$FIRM$_ITEMS (BARCODE2)
GO
CREATE INDEX I$FIRM$_ITEMS_I5 ON LG_$FIRM$_ITEMS (BARCODE3)
GO
CREATE INDEX I$FIRM$_ITEMS_I6 ON LG_$FIRM$_ITEMS (STGRPCODE,STGRPCODESUB)
GO

CREATE TABLE LG_$FIRM$_INFOFIRM (
	LOGICALREF NVARCHAR(40) NOT NULL COLLATE NOCASE,
	CODE NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	VALUE_ NVARCHAR(50) NULL COLLATE NOCASE DEFAULT '',
	CONSTRAINT I$FIRM$_INFOFIRM_I1 PRIMARY KEY (LOGICALREF)
	)
GO




----------------------------------PERIOD -----------------------



CREATE TABLE LG_$FIRM$_$PERIOD$_INVOICE (
	LOGICALREF NVARCHAR(40) NOT NULL COLLATE NOCASE ,
	FICHENO NVARCHAR(40) NOT NULL COLLATE NOCASE DEFAULT '',
	GRPCODE SMALLINT NULL DEFAULT 0,
	TRCODE SMALLINT NULL DEFAULT 0,
	IOCODE SMALLINT NULL DEFAULT 0,
	DATE_ DATETIME NULL DEFAULT '1900-01-01',
	SPECODE NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	CYPHCODE NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	CLIENTREF NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	SOURCEINDEX NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	DESTINDEX NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	ADDDISCOUNTS FLOAT NULL DEFAULT 0,
	TOTALDISCOUNTS FLOAT NULL DEFAULT 0,
	TOTALDISCOUNTED FLOAT NULL DEFAULT 0,
	ADDEXPENSES FLOAT NULL DEFAULT 0,
	TOTALEXPENSES FLOAT NULL DEFAULT 0,
	TOTALDEPOZITO FLOAT NULL DEFAULT 0,
	TOTALPROMOTIONS FLOAT NULL DEFAULT 0,
	TOTALVAT FLOAT NULL DEFAULT 0,
	GROSSTOTAL FLOAT NULL DEFAULT 0,
	NETTOTAL FLOAT NULL DEFAULT 0,
	DISCPER FLOAT NULL DEFAULT 0,
	GENEXP1 NVARCHAR(50) NULL COLLATE NOCASE DEFAULT '',
	GENEXP2 NVARCHAR(50) NULL COLLATE NOCASE DEFAULT '',
	GENEXP3 NVARCHAR(50) NULL COLLATE NOCASE DEFAULT '',
	GENEXP4 NVARCHAR(50) NULL COLLATE NOCASE DEFAULT '',
	FLOATF1 FLOAT NULL DEFAULT 0,
	FLOATF2 FLOAT NULL DEFAULT 0,
	FLOATF3 FLOAT NULL DEFAULT 0,
	PRCLIST SMALLINT NULL DEFAULT 0,
	CANCELLED SMALLINT NULL DEFAULT 0,
	IMPSTAT SMALLINT NULL DEFAULT 0,
	READONLY SMALLINT NULL DEFAULT 0,
	RECVERS SMALLINT NULL DEFAULT 0,
	CREATEDDATE DATETIME NULL DEFAULT '1900-01-01',
	CONSTRAINT I$FIRM$_$PERIOD$_INVOICE_I1 PRIMARY KEY (LOGICALREF)
	)
GO
DROP TRIGGER IF EXISTS INS_LG_$FIRM$_$PERIOD$_INVOICE
GO
CREATE TRIGGER INS_LG_$FIRM$_$PERIOD$_INVOICE AFTER INSERT 
ON LG_$FIRM$_$PERIOD$_INVOICE FOR EACH ROW 
BEGIN
UPDATE LG_$FIRM$_CLCARD SET BALANCEIO = ifnull(BALANCEIO,0) + ifnull((+1)*(CASE WHEN IFNULL(NEW.CANCELLED,0) IN (0) THEN + 1 ELSE 0 END)*(CASE WHEN IFNULL(NEW.TRCODE,0) IN (6,7,8) THEN + 1 WHEN NEW.TRCODE IN (1,2,3) THEN -1 ELSE 0 END)*NEW.NETTOTAL,0) WHERE LOGICALREF = NEW.CLIENTREF;
END

GO


DROP TRIGGER IF EXISTS DEL_LG_$FIRM$_$PERIOD$_INVOICE
GO
CREATE TRIGGER DEL_LG_$FIRM$_$PERIOD$_INVOICE AFTER DELETE 
ON LG_$FIRM$_$PERIOD$_INVOICE FOR EACH ROW
BEGIN
UPDATE LG_$FIRM$_CLCARD SET BALANCEIO = ifnull(BALANCEIO,0) + ifnull((-1)*(CASE WHEN IFNULL(OLD.CANCELLED,0) IN (0) THEN + 1 ELSE 0 END)*(CASE WHEN IFNULL(OLD.TRCODE,0) IN (6,7,8) THEN + 1 WHEN OLD.TRCODE IN (1,2,3) THEN -1 ELSE 0 END)*OLD.NETTOTAL,0) WHERE LOGICALREF = OLD.CLIENTREF;
END

GO

DROP TRIGGER IF EXISTS UPD_LG_$FIRM$_$PERIOD$_INVOICE
GO
CREATE TRIGGER UPD_LG_$FIRM$_$PERIOD$_INVOICE AFTER UPDATE 
ON LG_$FIRM$_$PERIOD$_INVOICE FOR EACH ROW
BEGIN
UPDATE LG_$FIRM$_CLCARD SET BALANCEIO = ifnull(BALANCEIO,0) + ifnull((-1)*(CASE WHEN IFNULL(OLD.CANCELLED,0) IN (0) THEN + 1 ELSE 0 END)*(CASE WHEN IFNULL(OLD.TRCODE,0) IN (6,7,8) THEN + 1 WHEN OLD.TRCODE IN (1,2,3) THEN -1 ELSE 0 END)*OLD.NETTOTAL,0) WHERE LOGICALREF = OLD.CLIENTREF;
UPDATE LG_$FIRM$_CLCARD SET BALANCEIO = ifnull(BALANCEIO,0) + ifnull((+1)*(CASE WHEN IFNULL(NEW.CANCELLED,0) IN (0) THEN + 1 ELSE 0 END)*(CASE WHEN IFNULL(NEW.TRCODE,0) IN (6,7,8) THEN + 1 WHEN NEW.TRCODE IN (1,2,3) THEN -1 ELSE 0 END)*NEW.NETTOTAL,0) WHERE LOGICALREF = NEW.CLIENTREF;
END
GO

CREATE TABLE LG_$FIRM$_$PERIOD$_STLINE (
	LOGICALREF NVARCHAR(40) NOT NULL COLLATE NOCASE ,
	STOCKREF NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	LINETYPE SMALLINT NULL DEFAULT 0,
	TRCODE SMALLINT NULL DEFAULT 0,
	DATE_ DATETIME NULL DEFAULT '1900-01-01',
	GLOBTRANS SMALLINT NULL DEFAULT 0,
	SOURCEINDEX NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	DESTINDEX NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	IOCODE SMALLINT NULL DEFAULT 0,
	STDOCREF NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	STDOCLNNO SMALLINT NULL DEFAULT 0,
	SPECODE NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	AMOUNT FLOAT NULL DEFAULT 0,
	PRICE FLOAT NULL DEFAULT 0,
	TOTAL FLOAT NULL DEFAULT 0,
	UINFO1 FLOAT NULL DEFAULT 0,
	UINFO2 FLOAT NULL DEFAULT 0,
	UNITREF NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	UNIT NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	DISTDISC FLOAT NULL DEFAULT 0,
	DISTEXP FLOAT NULL DEFAULT 0,
	DISTPROM FLOAT NULL DEFAULT 0,
	DISCPER FLOAT NULL DEFAULT 0,
	LINEEXP NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	WEIGHT FLOAT NULL DEFAULT 0,
	VOLUME_ FLOAT NULL DEFAULT 0,
	CANCELLED SMALLINT NULL DEFAULT 0,
	PRCLIST SMALLINT NULL DEFAULT 0,
	ORFLINEREF NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	SCRIPTFLG SMALLINT NULL DEFAULT 0,
	SCRIPTEXP NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	RECVERS SMALLINT NULL DEFAULT 0,
	CREATEDDATE DATETIME NULL DEFAULT '1900-01-01',
	CONSTRAINT I$FIRM$_$PERIOD$_STLINE_I1 PRIMARY KEY (LOGICALREF)
	)
GO

CREATE UNIQUE INDEX I$FIRM$_$PERIOD$_STLINE_I4 ON LG_$FIRM$_$PERIOD$_STLINE (
	STDOCREF,
	STDOCLNNO,
	LOGICALREF
	)
GO


DROP TRIGGER IF EXISTS INS_LG_$FIRM$_$PERIOD$_STLINE
GO
CREATE TRIGGER INS_LG_$FIRM$_$PERIOD$_STLINE AFTER INSERT 
ON LG_$FIRM$_$PERIOD$_STLINE FOR EACH ROW 
BEGIN
UPDATE LG_$FIRM$_ITEMS SET ONHANDIO = ifnull(ONHANDIO,0) + ifnull((+1)*(CASE WHEN IFNULL(NEW.CANCELLED,0) IN (0) AND IFNULL(NEW.LINETYPE,0) IN (0,1) THEN + 1 ELSE 0 END)*(CASE WHEN IFNULL(NEW.IOCODE,0) IN (1, 2) THEN + 1 WHEN NEW.IOCODE IN (3, 4) THEN -1 ELSE 0 END)*NEW.AMOUNT,0) WHERE LOGICALREF = NEW.STOCKREF;
END

GO


DROP TRIGGER IF EXISTS DEL_LG_$FIRM$_$PERIOD$_STLINE
GO
CREATE TRIGGER DEL_LG_$FIRM$_$PERIOD$_STLINE AFTER DELETE 
ON LG_$FIRM$_$PERIOD$_STLINE FOR EACH ROW
BEGIN
UPDATE LG_$FIRM$_ITEMS SET ONHANDIO = ifnull(ONHANDIO,0) + ifnull((-1)*(CASE WHEN IFNULL(OLD.CANCELLED,0) IN (0) AND IFNULL(OLD.LINETYPE,0) IN (0,1) THEN + 1 ELSE 0 END)*(CASE WHEN IFNULL(OLD.IOCODE,0) IN (1, 2) THEN + 1 WHEN OLD.IOCODE IN (3, 4) THEN -1 ELSE 0 END)*OLD.AMOUNT,0) WHERE LOGICALREF = OLD.STOCKREF;
END

GO

DROP TRIGGER IF EXISTS UPD_LG_$FIRM$_$PERIOD$_STLINE
GO
CREATE TRIGGER UPD_LG_$FIRM$_$PERIOD$_STLINE AFTER UPDATE 
ON LG_$FIRM$_$PERIOD$_STLINE FOR EACH ROW
BEGIN
UPDATE LG_$FIRM$_ITEMS SET ONHANDIO = ifnull(ONHANDIO,0) + ifnull((-1)*(CASE WHEN IFNULL(OLD.CANCELLED,0) IN (0) AND IFNULL(OLD.LINETYPE,0) IN (0,1) THEN + 1 ELSE 0 END)*(CASE WHEN IFNULL(OLD.IOCODE,0) IN (1, 2) THEN + 1 WHEN OLD.IOCODE IN (3, 4) THEN -1 ELSE 0 END)*OLD.AMOUNT,0) WHERE LOGICALREF = OLD.STOCKREF;
UPDATE LG_$FIRM$_ITEMS SET ONHANDIO = ifnull(ONHANDIO,0) + ifnull((+1)*(CASE WHEN IFNULL(NEW.CANCELLED,0) IN (0) AND IFNULL(NEW.LINETYPE,0) IN (0,1) THEN + 1 ELSE 0 END)*(CASE WHEN IFNULL(NEW.IOCODE,0) IN (1, 2) THEN + 1 WHEN NEW.IOCODE IN (3, 4) THEN -1 ELSE 0 END)*NEW.AMOUNT,0) WHERE LOGICALREF = NEW.STOCKREF;
END
GO

CREATE TABLE LG_$FIRM$_$PERIOD$_ORFICHE (
	LOGICALREF NVARCHAR(40) NOT NULL COLLATE NOCASE ,
	FICHENO NVARCHAR(40) NOT NULL COLLATE NOCASE DEFAULT '',
	GRPCODE SMALLINT NULL DEFAULT 0,
	TRCODE SMALLINT NULL DEFAULT 0,
	IOCODE SMALLINT NULL DEFAULT 0,
	DATE_ DATETIME NULL DEFAULT '1900-01-01',
	SPECODE NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	CYPHCODE NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	CLIENTREF NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	SOURCEINDEX NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	DESTINDEX NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	ADDDISCOUNTS FLOAT NULL DEFAULT 0,
	TOTALDISCOUNTS FLOAT NULL DEFAULT 0,
	TOTALDISCOUNTED FLOAT NULL DEFAULT 0,
	ADDEXPENSES FLOAT NULL DEFAULT 0,
	TOTALEXPENSES FLOAT NULL DEFAULT 0,
	TOTALDEPOZITO FLOAT NULL DEFAULT 0,
	TOTALPROMOTIONS FLOAT NULL DEFAULT 0,
	TOTALVAT FLOAT NULL DEFAULT 0,
	GROSSTOTAL FLOAT NULL DEFAULT 0,
	NETTOTAL FLOAT NULL DEFAULT 0,
	DISCPER FLOAT NULL DEFAULT 0,
	GENEXP1 NVARCHAR(50) NULL COLLATE NOCASE DEFAULT '',
	GENEXP2 NVARCHAR(50) NULL COLLATE NOCASE DEFAULT '',
	GENEXP3 NVARCHAR(50) NULL COLLATE NOCASE DEFAULT '',
	GENEXP4 NVARCHAR(50) NULL COLLATE NOCASE DEFAULT '',
	FLOATF1 FLOAT NULL DEFAULT 0,
	FLOATF2 FLOAT NULL DEFAULT 0,
	FLOATF3 FLOAT NULL DEFAULT 0,
	PRCLIST SMALLINT NULL DEFAULT 0,
	CANCELLED SMALLINT NULL DEFAULT 0,
	IMPSTAT SMALLINT NULL DEFAULT 0,
	READONLY SMALLINT NULL DEFAULT 0,
	RECVERS SMALLINT NULL DEFAULT 0,
	CREATEDDATE DATETIME NULL DEFAULT '1900-01-01',
	CONSTRAINT I$FIRM$_$PERIOD$_ORFICHE_I1 PRIMARY KEY (LOGICALREF)
	)
GO

CREATE TABLE LG_$FIRM$_$PERIOD$_ORFLINE (
	LOGICALREF NVARCHAR(40) NOT NULL COLLATE NOCASE ,
	STOCKREF NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	LINETYPE SMALLINT NULL DEFAULT 0,
	TRCODE SMALLINT NULL DEFAULT 0,
	DATE_ DATETIME NULL DEFAULT '1900-01-01',
	GLOBTRANS SMALLINT NULL DEFAULT 0,
	SOURCEINDEX NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	DESTINDEX NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	IOCODE SMALLINT NULL DEFAULT 0,
	STDOCREF NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	STDOCLNNO SMALLINT NULL DEFAULT 0,
	SPECODE NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	AMOUNT FLOAT NULL DEFAULT 0,
	PRICE FLOAT NULL DEFAULT 0,
	TOTAL FLOAT NULL DEFAULT 0,
	UINFO1 FLOAT NULL DEFAULT 0,
	UINFO2 FLOAT NULL DEFAULT 0,
	UNITREF NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	UNIT NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	DISTDISC FLOAT NULL DEFAULT 0,
	DISTEXP FLOAT NULL DEFAULT 0,
	DISTPROM FLOAT NULL DEFAULT 0,
	DISCPER FLOAT NULL DEFAULT 0,
	LINEEXP NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	WEIGHT FLOAT NULL DEFAULT 0,
	VOLUME_ FLOAT NULL DEFAULT 0,
	CANCELLED SMALLINT NULL DEFAULT 0,
	PRCLIST SMALLINT NULL DEFAULT 0,
	SCRIPTFLG SMALLINT NULL DEFAULT 0,
	SCRIPTEXP NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	RECVERS SMALLINT NULL DEFAULT 0,
	CREATEDDATE DATETIME NULL DEFAULT '1900-01-01',
	CONSTRAINT I$FIRM$_$PERIOD$_ORFLINE_I1 PRIMARY KEY (LOGICALREF)
	)
GO

CREATE UNIQUE INDEX I$FIRM$_$PERIOD$_ORFLINE_I4 ON LG_$FIRM$_$PERIOD$_ORFLINE (
	STDOCREF,
	STDOCLNNO,
	LOGICALREF
	)
GO

CREATE TABLE LG_$FIRM$_$PERIOD$_KSLINES (
	LOGICALREF NVARCHAR(40) NOT NULL COLLATE NOCASE ,
	FICHENO NVARCHAR(40) NOT NULL COLLATE NOCASE DEFAULT '',
	CLIENTREF NVARCHAR(40) NOT NULL COLLATE NOCASE DEFAULT '',
	CARDREF NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	DATE_ DATETIME NULL DEFAULT '1900-01-01',
	TRCODE SMALLINT NULL DEFAULT 0,
	SPECODE NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	CYPHCODE NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	LINEEXP NVARCHAR(50) NULL COLLATE NOCASE DEFAULT '',
	GENEXP2 NVARCHAR(50) NULL COLLATE NOCASE DEFAULT '',
	GENEXP3 NVARCHAR(50) NULL COLLATE NOCASE DEFAULT '',
	FLOATF1 FLOAT NULL DEFAULT 0,
	FLOATF2 FLOAT NULL DEFAULT 0,
	FLOATF3 FLOAT NULL DEFAULT 0,
	AMOUNT FLOAT NULL DEFAULT 0,
	SIGN SMALLINT NULL DEFAULT 0,
	CANCELLED SMALLINT NULL DEFAULT 0,
	READONLY SMALLINT NULL DEFAULT 0,
	RECVERS SMALLINT NULL DEFAULT 0,
	CREATEDDATE DATETIME NULL DEFAULT '1900-01-01',
	CONSTRAINT I$FIRM$_$PERIOD$_KSLINES_I1 PRIMARY KEY (LOGICALREF)
	)
GO
DROP TRIGGER IF EXISTS INS_LG_$FIRM$_$PERIOD$_KSLINES
GO
CREATE TRIGGER INS_LG_$FIRM$_$PERIOD$_KSLINES AFTER INSERT 
ON LG_$FIRM$_$PERIOD$_KSLINES FOR EACH ROW 
BEGIN
UPDATE LG_$FIRM$_CLCARD SET BALANCEIO = ifnull(BALANCEIO,0) + ifnull((+1)*(CASE WHEN IFNULL(NEW.CANCELLED,0) IN (0) THEN + 1 ELSE 0 END)*(CASE WHEN IFNULL(NEW.TRCODE,0) IN (12) THEN + 1 WHEN NEW.TRCODE IN (11) THEN -1 ELSE 0 END)*NEW.AMOUNT,0) WHERE LOGICALREF = NEW.CLIENTREF;
END

GO


DROP TRIGGER IF EXISTS DEL_LG_$FIRM$_$PERIOD$_KSLINES
GO
CREATE TRIGGER DEL_LG_$FIRM$_$PERIOD$_KSLINES AFTER DELETE 
ON LG_$FIRM$_$PERIOD$_KSLINES FOR EACH ROW
BEGIN
UPDATE LG_$FIRM$_CLCARD SET BALANCEIO = ifnull(BALANCEIO,0) + ifnull((-1)*(CASE WHEN IFNULL(OLD.CANCELLED,0) IN (0) THEN + 1 ELSE 0 END)*(CASE WHEN IFNULL(OLD.TRCODE,0) IN (12) THEN + 1 WHEN OLD.TRCODE IN (11) THEN -1 ELSE 0 END)*OLD.AMOUNT,0) WHERE LOGICALREF = OLD.CLIENTREF;
END

GO

DROP TRIGGER IF EXISTS UPD_LG_$FIRM$_$PERIOD$_KSLINES
GO
CREATE TRIGGER UPD_LG_$FIRM$_$PERIOD$_KSLINES AFTER UPDATE 
ON LG_$FIRM$_$PERIOD$_KSLINES FOR EACH ROW
BEGIN
UPDATE LG_$FIRM$_CLCARD SET BALANCEIO = ifnull(BALANCEIO,0) + ifnull((-1)*(CASE WHEN IFNULL(OLD.CANCELLED,0) IN (0) THEN + 1 ELSE 0 END)*(CASE WHEN IFNULL(OLD.TRCODE,0) IN (12) THEN + 1 WHEN OLD.TRCODE IN (11) THEN -1 ELSE 0 END)*OLD.AMOUNT,0) WHERE LOGICALREF = OLD.CLIENTREF;
UPDATE LG_$FIRM$_CLCARD SET BALANCEIO = ifnull(BALANCEIO,0) + ifnull((+1)*(CASE WHEN IFNULL(NEW.CANCELLED,0) IN (0) THEN + 1 ELSE 0 END)*(CASE WHEN IFNULL(NEW.TRCODE,0) IN (12) THEN + 1 WHEN NEW.TRCODE IN (11) THEN -1 ELSE 0 END)*NEW.AMOUNT,0) WHERE LOGICALREF = NEW.CLIENTREF;
END
GO
CREATE TABLE LG_$FIRM$_$PERIOD$_INFOPERIOD (
	LOGICALREF NVARCHAR(40) NOT NULL COLLATE NOCASE DEFAULT '',
	CODE NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	VALUE_ NVARCHAR(50) NULL COLLATE NOCASE DEFAULT '',
	CONSTRAINT I$FIRM$_$PERIOD$_INFOPERIOD_I1 PRIMARY KEY (LOGICALREF)
	)
GO

CREATE TABLE LG_$FIRM$_$PERIOD$_INFODOCSAVE (
	LOGICALREF NVARCHAR(40) NOT NULL COLLATE NOCASE DEFAULT '',
	CODE NVARCHAR(40) NOT NULL COLLATE NOCASE DEFAULT '',
	TYPE NVARCHAR(40) NOT NULL COLLATE NOCASE DEFAULT '',
	STOCKREF NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	PROMOREF NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	CF1 FLOAT NULL DEFAULT 0,
	CF2 FLOAT NULL DEFAULT 0,
	CLCODE NVARCHAR(40) NULL COLLATE NOCASE DEFAULT '',
	TEXT1 NVARCHAR(50) NULL COLLATE NOCASE DEFAULT '',
	TEXT2 NVARCHAR(50) NULL COLLATE NOCASE DEFAULT '',
	FILTERPROMOCL NVARCHAR(250) NULL COLLATE NOCASE DEFAULT '',
	CONSTRAINT I$FIRM$_$PERIOD$_INFODOCSAVE_I1 PRIMARY KEY (LOGICALREF)
	)
GO

CREATE TABLE LG_$FIRM$_$PERIOD$_GENSEQ (
	ID INT NULL,
	LASTLREF INT NOT NULL
	)
GO

INSERT INTO LG_$FIRM$_$PERIOD$_GENSEQ
VALUES (
	1,
	0
	)
GO


