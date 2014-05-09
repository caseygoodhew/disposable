CREATE OR REPLACE PACKAGE protected.user_pkg
AS

FUNCTION CreateUser (
	in_email			IN  constants.T_EMAIL,
	in_password			IN  constants.T_PASSWORD
) RETURN constants.T_SID;

END user_pkg;
/