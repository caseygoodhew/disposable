CREATE OR REPLACE PACKAGE disposable.user_pkg
AS

PROCEDURE Authenticate (
	in_username			IN  VARCHAR2,
	in_password			IN  VARCHAR2,
	out_result			OUT NUMBER
);

FUNCTION Authenticate (
	in_username			IN  VARCHAR2,
	in_password			IN  VARCHAR2
) RETURN NUMBER;

END user_pkg;
/