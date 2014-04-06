CREATE OR REPLACE PACKAGE BODY disposable.user_pkg
AS

FUNCTION CreateUser (
	in_email			IN  SYSTEM_USER.EMAIL%TYPE,
	in_password			IN  VARCHAR2
) RETURN constants.T_SID
AS
	v_user_sid			constants.T_SID DEFAULT so_pkg.CreateSO(constants.OT_USER, NULL);
	v_salt				constants.T_GUID DEFAULT crypto_pkg.Guid;
	v_password			VARCHAR2(4000) DEFAULT NVL(in_password, crypto_pkg.Guid);
BEGIN
	INSERT INTO system_user
	(user_sid, email, password, salt)
	VALUES
	(v_user_sid, in_email, crypto_pkg.Hash(v_password||v_salt), v_salt);
	
	RETURN v_user_sid;
END;

FUNCTION Authenticate (
	in_email			IN  SYSTEM_USER.EMAIL%TYPE,
	in_password			IN  VARCHAR2
) RETURN BOOLEAN
AS
	v_password_hash		SYSTEM_USER.PASSWORD%TYPE;
	v_salt				SYSTEM_USER.SALT%TYPE;
BEGIN
	BEGIN
		SELECT password, salt
		  INTO v_password_hash, v_salt
		  FROM system_user
		 WHERE LOWER(email) = LOWER(TRIM(in_email));
	EXCEPTION
		WHEN NO_DATA_FOUND THEN
			RETURN FALSE;
	END;
	
	RETURN crypto_pkg.Matches(in_password||v_salt, v_password_hash);
END;

PROCEDURE Authenticate (
	in_email			IN  SYSTEM_USER.EMAIL%TYPE,
	in_password			IN  VARCHAR2,
	out_result			OUT constants.T_BOOLEAN
)
AS
BEGIN
	out_result := constants.BOOL_FALSE;
	IF Authenticate(in_email, in_password) THEN
		out_result := constants.BOOL_TRUE;
	END IF;
END;


END user_pkg;
/