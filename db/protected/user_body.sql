CREATE OR REPLACE PACKAGE BODY protected.user_pkg
AS

FUNCTION CreateUser (
	in_email			IN  constants.T_EMAIL,
	in_password			IN  constants.T_PASSWORD
) RETURN constants.T_SID
AS
	v_user_sid			constants.T_SID;
	v_confirmation_guid	constants.T_GUID;
BEGIN
	disposable.user_pkg.CreateUser(in_email, in_password, constants.BOOL_TRUE, v_user_sid, v_confirmation_guid);
	RETURN v_user_sid;
END;

END user_pkg;
/