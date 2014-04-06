CREATE OR REPLACE PACKAGE BODY disposable.so_pkg
AS

FUNCTION CreateSO (
	in_object_type			IN  constants.T_OBJECT_TYPE,
	in_parent_sid			IN  constants.T_SID
) RETURN constants.T_SID
AS
	v_object_sid			constants.T_SID;
BEGIN
	
	INSERT INTO object_pool
	(object_sid)
	VALUES
	(object_sid_seq.NEXTVAL)
	RETURNING object_sid INTO v_object_sid;
	
	INSERT INTO object_object_type
	(object_sid, object_type_id)
	VALUES
	(v_object_sid, in_object_type);

	INSERT INTO object
	(object_sid, object_type_id, parent_object_sid)
	VALUES
	(v_object_sid, in_object_type, in_parent_sid);
	
	RETURN v_object_sid;
END;

END so_pkg;
/