CREATE OR REPLACE PACKAGE disposable.user_pkg
AS

PROCEDURE CreateUser (
	in_email				IN  constants.T_EMAIL,
	in_password				IN  constants.T_PASSWORD,
	in_approved				IN  constants.T_BOOLEAN,
	out_user_sid			OUT constants.T_SID,
	out_confirmation_guid	OUT constants.T_GUID
);

PROCEDURE Authenticate (
	in_email			IN  constants.T_EMAIL,
	in_password			IN  constants.T_PASSWORD,
	out_result			OUT constants.T_BOOLEAN
);

PROCEDURE CreateUser (
	in_username			IN  VARCHAR2,
	out_cur				OUT constants.T_OUTPUT_CUR
);

END user_pkg;
/