DECLARE
	TYPE t_users IS TABLE OF VARCHAR2(30);
	v_users 	t_users;
	v_exists	NUMBER;
	v_sid	 	NUMBER;
	v_serial	NUMBER;
BEGIN
	
	v_users := t_users(
		'UPD'
	);	
	
	FOR i IN 1 .. v_users.COUNT LOOP
		
		SELECT COUNT(*)
		  INTO v_exists
		  FROM all_users
		 WHERE username = v_users(i);
		
		IF v_exists <> 0 THEN

			FOR r IN (
				SELECT sid, serial# FROM v$session WHERE UPPER(username) = v_users(i)
			) LOOP
				EXECUTE IMMEDIATE 'ALTER SYSTEM KILL SESSION '''||r.sid||','||r.serial#||'''';
			END LOOP;

			EXECUTE IMMEDIATE 'DROP USER '||v_users(i)||' CASCADE';
		END IF;
	
		EXECUTE IMMEDIATE 'CREATE USER '||v_users(i)||' IDENTIFIED BY '||LOWER(v_users(i))||' QUOTA UNLIMITED ON USERS';
		EXECUTE IMMEDIATE 'GRANT CREATE SESSION TO '||v_users(i);
	END LOOP;
END;
/

grant dba to upd;