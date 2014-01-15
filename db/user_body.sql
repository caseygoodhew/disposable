CREATE OR REPLACE PACKAGE BODY disposable.user_pkg
AS

PROCEDURE Authenticate (
	in_username			IN  VARCHAR2,
	in_password			IN  VARCHAR2,
	out_result			OUT NUMBER
)
AS
BEGIN
	IF LOWER(in_username) = 'goodhew' AND in_password = 'ggg' THEN
		out_result := 1;
	ELSE
		out_result := 0;
	END IF;
END;

FUNCTION Authenticate (
	in_username			IN  VARCHAR2,
	in_password			IN  VARCHAR2
) RETURN NUMBER
AS
BEGIN
	IF LOWER(in_username) = 'casey' AND in_password = 'ccc' THEN
		RETURN 1;
	ELSE
		RETURN 0;
	END IF;
END;


END user_pkg;
/