

/*************************************************************************************************/
/*		PERMISSIONS																				 */
/*************************************************************************************************/
/*
INSERT INTO disposable.permission (permission_id, description) VALUES (1, 'Create');
INSERT INTO disposable.permission (permission_id, description) VALUES (2, 'Read');
INSERT INTO disposable.permission (permission_id, description) VALUES (3, 'Update');
INSERT INTO disposable.permission (permission_id, description) VALUES (4, 'Delete');
INSERT INTO disposable.permission (permission_id, description) VALUES (5, 'Retire');
INSERT INTO disposable.permission (permission_id, description) VALUES (6, 'Change permission');
INSERT INTO disposable.permission (permission_id, description) VALUES (7, 'Add members');
INSERT INTO disposable.permission (permission_id, description) VALUES (8, 'Remove members');

INSERT INTO disposable.constant (section, name, id)
SELECT 'PERMISSION', UPPER(REPLACE(description, ' ', '_')), permission_id
FROM disposable.permission;
*/

BEGIN
	/*************************************************************************************************/
	/*		OBJECT TYPE																				 */
	/*************************************************************************************************/
	INSERT INTO disposable.object_type (object_type_id, description) VALUES (constants.OT_APPLICATION, 'Application');
	INSERT INTO disposable.object_type (object_type_id, description) VALUES (constants.OT_CONTAINER, 'Container');
	INSERT INTO disposable.object_type (object_type_id, description) VALUES (constants.OT_WEB_RESOURCE, 'Web Resource');
	INSERT INTO disposable.object_type (object_type_id, description) VALUES (constants.OT_MENU_RESOURCE, 'Menu Resource');
	INSERT INTO disposable.object_type (object_type_id, description) VALUES (constants.OT_USER, 'User');
	INSERT INTO disposable.object_type (object_type_id, description) VALUES (constants.OT_ROLE, 'Role');
	INSERT INTO disposable.object_type (object_type_id, description) VALUES (constants.OT_DEVICE, 'Device');

	INSERT INTO disposable.object_type_relationship (primary_object_type_id, secondary_object_type_id) VALUES (constants.OT_ROLE, constants.OT_WEB_RESOURCE);
	INSERT INTO disposable.object_type_relationship (primary_object_type_id, secondary_object_type_id) VALUES (constants.OT_ROLE, constants.OT_MENU_RESOURCE);
	INSERT INTO disposable.object_type_relationship (primary_object_type_id, secondary_object_type_id) VALUES (constants.OT_USER, constants.OT_USER);
	INSERT INTO disposable.object_type_relationship (primary_object_type_id, secondary_object_type_id) VALUES (constants.OT_USER, constants.OT_DEVICE);
	
	/*************************************************************************************************/
	/*		PERMISSION POLICIES																		 */
	/*************************************************************************************************/
	INSERT INTO disposable.permission_policy (permission_policy_id, description) VALUES (constants.PP_ALLOW, 'Allow');
	INSERT INTO disposable.permission_policy (permission_policy_id, description) VALUES (constants.PP_DENY, 'Deny');
	
	/*************************************************************************************************/
	/*		PERMISSION TYPES																		 */
	/*************************************************************************************************/
	INSERT INTO disposable.permission_type (permission_type_id, description) VALUES (constants.PT_OBJECT, 'Object Permission');
	INSERT INTO disposable.permission_type (permission_type_id, description) VALUES (constants.PT_OBJECT_TYPE, 'Object Type Permission');
	INSERT INTO disposable.permission_type (permission_type_id, description) VALUES (constants.PT_OBJECT + constants.PT_OBJECT_TYPE, 'Object Or Object Type Permission');

	/*************************************************************************************************/
	/*		OBJECT TYPE PERMISSION TYPE															  	 */
	/*************************************************************************************************/
	INSERT INTO disposable.object_type_permission_type (object_type_id, permission_type_id) VALUES (constants.OT_WEB_RESOURCE, constants.PT_OBJECT);
	INSERT INTO disposable.object_type_permission_type (object_type_id, permission_type_id) VALUES (constants.OT_MENU_RESOURCE, constants.PT_OBJECT);
	INSERT INTO disposable.object_type_permission_type (object_type_id, permission_type_id) VALUES (constants.OT_USER, constants.PT_OBJECT + constants.PT_OBJECT_TYPE);
	INSERT INTO disposable.object_type_permission_type (object_type_id, permission_type_id) VALUES (constants.OT_DEVICE, constants.PT_OBJECT_TYPE);
	
END;
/
