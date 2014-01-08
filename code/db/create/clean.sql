whenever sqlerror exit failure rollback 
whenever oserror exit failure rollback	

set term off
set echo on

@prompt "Recreating users"

ALTER PROFILE DEFAULT LIMIT PASSWORD_LIFE_TIME UNLIMITED;

DECLARE
	TYPE t_users IS TABLE OF VARCHAR2(30);
	v_users t_users;
	v_exists NUMBER;
BEGIN
	v_users := t_users(
		'DISPOSABLE', 
		'WEB_USER'
	);	
	FOR i IN 1 .. v_users.COUNT LOOP
		SELECT COUNT(*)
		  INTO v_exists
		  FROM all_users
		 WHERE username = v_users(i);
		IF v_exists <> 0 THEN
			EXECUTE IMMEDIATE 'DROP USER '||v_users(i)||' CASCADE';
		END IF;
		EXECUTE IMMEDIATE 'CREATE USER '||v_users(i)||' IDENTIFIED BY '||LOWER(v_users(i))||' QUOTA UNLIMITED ON USERS';
	END LOOP;
	
	
	v_users := t_users(
		'WEB_USER'
	);	
	FOR i IN 1 .. v_users.COUNT LOOP
		EXECUTE IMMEDIATE 'GRANT CREATE SESSION TO '||v_users(i);
	END LOOP;
END;
/

@prompt "Creating DISPOSABLE"


set term on
@create_schema

