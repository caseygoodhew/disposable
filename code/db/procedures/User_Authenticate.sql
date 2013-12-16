DELIMITER $$

DROP PROCEDURE IF EXISTS disposable.User_Authenticate$$

CREATE PROCEDURE disposable.User_Authenticate(
	IN  in_username				VARCHAR(100), 
	IN  in_password				VARCHAR(100), 
	OUT in_result 				BIT
) 
BEGIN
	SET in_result = 0;

	IF in_username = 'james' AND in_password = BINARY 'Casey' THEN
		SET in_result = 1;
	END IF;

END$$

DELIMITER ;
