CREATE OR REPLACE PACKAGE disposable.crypto_pkg
AS

FUNCTION Guid RETURN RAW;

FUNCTION Hash (
	in_value	IN  VARCHAR2
) RETURN RAW;

FUNCTION Matches (
	in_value		IN  VARCHAR2,
	in_expected 	IN  RAW
) RETURN BOOLEAN;

END crypto_pkg;
/