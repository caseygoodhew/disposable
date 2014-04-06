CREATE OR REPLACE PACKAGE disposable.user_pkg
AS

FUNCTION CreateUser (
	in_email			IN  SYSTEM_USER.EMAIL%TYPE,
	in_password			IN  VARCHAR2
) RETURN constants.T_SID;

PROCEDURE Authenticate (
	in_email			IN  SYSTEM_USER.EMAIL%TYPE,
	in_password			IN  VARCHAR2,
	out_result			OUT constants.T_BOOLEAN
);

END user_pkg;
/