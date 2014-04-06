CREATE OR REPLACE PACKAGE BODY disposable.crypto_pkg
AS

FUNCTION Guid 
RETURN RAW
AS
BEGIN
	-- Guids are 32 byte
	-- SYS_GUID could result in sequential guids on some processors
	-- Hashing a sequential guid against MD5 removes obvious sequence
	--		issues and produces a coincidental 32 byte result
	RETURN DBMS_CRYPTO.HASH(SYS_GUID(), DBMS_CRYPTO.HASH_MD5);
END;

FUNCTION Hash (
	in_value		IN  VARCHAR2
) RETURN RAW
AS
BEGIN
	-- SHA2-512 hash preferred but is not natively available in ora11g
	RETURN DBMS_CRYPTO.HASH(UTL_RAW.CAST_TO_RAW(in_value), DBMS_CRYPTO.HASH_SH1);
END;

FUNCTION Matches (
	in_value		IN  VARCHAR2,
	in_expected 	IN  RAW
) RETURN BOOLEAN
AS
BEGIN
	RETURN in_expected = Hash(in_value);
END;

END crypto_pkg;
/