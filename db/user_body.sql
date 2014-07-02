CREATE OR REPLACE PACKAGE BODY disposable.user_pkg
AS

PROCEDURE CreateUser (
	in_email				IN  constants.T_EMAIL,
	in_password				IN  constants.T_PASSWORD,
	in_approved				IN  constants.T_BOOLEAN,
	out_user_sid			OUT constants.T_SID,
	out_confirmation_guid	OUT constants.T_GUID
)
AS
	v_user_sid			constants.T_SID DEFAULT so_pkg.CreateSO(constants.OT_USER, NULL);
	v_salt				constants.T_GUID DEFAULT crypto_pkg.Guid;
	v_password			VARCHAR2(4000) DEFAULT NVL(in_password, crypto_pkg.Guid);
BEGIN
	BEGIN
		INSERT INTO system_user
		(user_sid, email, lower_email, password, salt)
		VALUES
		(v_user_sid, TRIM(in_email), LOWER(TRIM(in_email)), crypto_pkg.Hash(v_password||v_salt), v_salt);
	EXCEPTION WHEN DUP_VAL_ON_INDEX THEN   	
      	IF SQLERRM = 'ORA-00001: unique constraint (DISPOSABLE.UK_LOWER_EMAIL) violated' THEN
		   RAISE_APPLICATION_ERROR(exceptions.ERR_DUPLICATE_EMAIL, 'Duplicate email '||in_email);
		END IF;	
		RAISE;
    END;
	
	out_user_sid := v_user_sid;
END;

FUNCTION Authenticate (
	in_email			IN  constants.T_EMAIL,
	in_password			IN  constants.T_PASSWORD
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
	in_email			IN  constants.T_EMAIL,
	in_password			IN  constants.T_PASSWORD,
	out_result			OUT constants.T_BOOLEAN
)
AS
BEGIN
	out_result := constants.BOOL_FALSE;
	IF Authenticate(in_email, in_password) THEN
		out_result := constants.BOOL_TRUE;
	END IF;
END;

PROCEDURE CreateUser (
	in_username			IN  VARCHAR2,
	out_cur				OUT constants.T_OUTPUT_CUR
)
AS
BEGIN
	OPEN out_cur FOR
		SELECT * FROM system_user;

END;

END user_pkg;
/