/*************************************************************************************************/
/*		PERMISSIONS																				 */
/*************************************************************************************************/
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

/*************************************************************************************************/
/*		PERMISSION TYPES																		 */
/*************************************************************************************************/
INSERT INTO disposable.permission_type (permission_type_id, description) VALUES (1, 'Allow');
INSERT INTO disposable.permission_type (permission_type_id, description) VALUES (2, 'Deny');

INSERT INTO disposable.constant (section, name, id)
SELECT 'PERMISSION', UPPER(REPLACE(description, ' ', '_')), permission_type_id
FROM disposable.permission_type;

/*************************************************************************************************/
/*		CLASS TYPE																				 */
/*************************************************************************************************/
INSERT INTO disposable.class_type (class_type_id, description) VALUES (1, 'Application');
INSERT INTO disposable.class_type (class_type_id, description) VALUES (2, 'User');
INSERT INTO disposable.class_type (class_type_id, description) VALUES (3, 'Group');

INSERT INTO disposable.constant (section, name, id)
SELECT 'SO_CLASS', UPPER(REPLACE(description, ' ', '_')), permission_id
FROM disposable.permission;


/*************************************************************************************************/
/*		INITIALIZE SECURABLE_OBJECT SEQUENCE TO 10000											 */
/*************************************************************************************************/
INSERT INTO disposable.securable_object (object_sid, class_type_id) VALUES (9990, 1);
DELETE FROM disposable.securable_object;

