

CREATE TABLE DISPOSABLE.OBJECT_TYPE_PERMISSION(
    OBJECT_TYPE_ID    INT UNSIGNED  NOT NULL,
    PERMISSION_ID     INT UNSIGNED  NOT NULL,
    PRIMARY KEY (OBJECT_TYPE_ID, PERMISSION_ID)
)ENGINE=INNODB
;

CREATE TABLE DISPOSABLE.PERMISSION(
    PERMISSION_ID    INT UNSIGNED    NOT NULL,
    DESCRIPTION      VARCHAR(100)    NOT NULL,
    PRIMARY KEY (PERMISSION_ID)
)ENGINE=INNODB
;



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

/*************************************************************************************************/
/*		OBJECT TYPE																				 */
/*************************************************************************************************/
INSERT INTO disposable.object_type (object_type_id, description) VALUES (1, 'Application');
INSERT INTO disposable.object_type (object_type_id, description) VALUES (2, 'Container');
INSERT INTO disposable.object_type (object_type_id, description) VALUES (3, 'Web Resource');
INSERT INTO disposable.object_type (object_type_id, description) VALUES (4, 'Menu Resource');
INSERT INTO disposable.object_type (object_type_id, description) VALUES (5, 'User');
INSERT INTO disposable.object_type (object_type_id, description) VALUES (6, 'Role');
INSERT INTO disposable.object_type (object_type_id, description) VALUES (7, 'Device');

INSERT INTO disposable.constant (section, name, id)
SELECT 'SO_CLASS', UPPER(REPLACE(description, ' ', '_')), object_type_id
FROM disposable.object_type;

INSERT INTO disposable.object_type_relationship (primary_object_type_id, secondary_object_type_id)
SELECT p.object_type_id, s.object_type_id
  FROM disposable.object_type p, disposable.object_type s
 WHERE p.description = 'Role'
   AND s.description = 'Web Resource';

INSERT INTO disposable.object_type_relationship (primary_object_type_id, secondary_object_type_id)
SELECT p.object_type_id, s.object_type_id
  FROM disposable.object_type p, disposable.object_type s
 WHERE p.description = 'Role'
   AND s.description = 'Menu Resource';

INSERT INTO disposable.object_type_relationship (primary_object_type_id, secondary_object_type_id)
SELECT p.object_type_id, s.object_type_id
  FROM disposable.object_type p, disposable.object_type s
 WHERE p.description = 'User'
   AND s.description = 'User';

INSERT INTO disposable.object_type_relationship (primary_object_type_id, secondary_object_type_id)
SELECT p.object_type_id, s.object_type_id
  FROM disposable.object_type p, disposable.object_type s
 WHERE p.description = 'User'
   AND s.description = 'Device';
   
/*************************************************************************************************/
/*		PERMISSION POLICIES																		 */
/*************************************************************************************************/
INSERT INTO disposable.permission_policy (permission_policy_id, description) VALUES (1, 'Allow');
INSERT INTO disposable.permission_policy (permission_policy_id, description) VALUES (2, 'Deny');

INSERT INTO disposable.constant (section, name, id)
SELECT 'PERMISSION', UPPER(REPLACE(description, ' ', '_')), permission_policy_id
FROM disposable.permission_policy;

/*************************************************************************************************/
/*		PERMISSION TYPES																		 */
/*************************************************************************************************/
INSERT INTO disposable.permission_type (permission_type_id, description) VALUES (1, 'Object Type Permission');
INSERT INTO disposable.permission_type (permission_type_id, description) VALUES (2, 'Object Permission');
INSERT INTO disposable.permission_type (permission_type_id, description) VALUES (3, 'Object Or Object Type Permission');

INSERT INTO disposable.constant (section, name, id)
SELECT 'PERMISSION', UPPER(REPLACE(description, ' ', '_')), permission_type_id
FROM disposable.permission_type;

/*************************************************************************************************/
/*		OBJECT TYPE PERMISSION TYPE															  	 */
/*************************************************************************************************/
INSERT INTO disposable.object_type_permission_type (object_type_id, permission_type_id)
SELECT o.object_type_id, p.permission_type_id
  FROM disposable.object_type o, disposable.permission_type p
 WHERE o.description = 'Web Resource'
   AND p.description = 'Object Permission';

INSERT INTO disposable.object_type_permission_type (object_type_id, permission_type_id)
SELECT o.object_type_id, p.permission_type_id
  FROM disposable.object_type o, disposable.permission_type p
 WHERE o.description = 'Menu Resource'
   AND p.description = 'Object Permission';

INSERT INTO disposable.object_type_permission_type (object_type_id, permission_type_id)
SELECT o.object_type_id, p.permission_type_id
  FROM disposable.object_type o, disposable.permission_type p
 WHERE o.description = 'User Resource'
   AND p.description = 'Object Or Object Type Permission';

INSERT INTO disposable.object_type_permission_type (object_type_id, permission_type_id)
SELECT o.object_type_id, p.permission_type_id
  FROM disposable.object_type o, disposable.permission_type p
 WHERE o.description = 'Device'
   AND p.description = 'Object Type Permission';


/*************************************************************************************************/
/*		INITIALIZE SECURABLE_OBJECT SEQUENCE TO 10000											 */
/*************************************************************************************************/
INSERT INTO disposable.securable_object (object_sid, class_type_id) VALUES (9999, 1);
DELETE FROM disposable.securable_object;

