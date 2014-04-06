CREATE OR REPLACE PACKAGE disposable.so_pkg
AS

FUNCTION CreateSO (
	in_object_type			IN  constants.T_OBJECT_TYPE,
	in_parent_sid			IN  constants.T_SID
) RETURN constants.T_SID;

END so_pkg;
/