DELIMITER $$ 
DROP FUNCTION IF EXISTS disposable.Constants$$
CREATE FUNCTION disposable.Constants(in_section VARCHAR(30), in_name VARCHAR(100)) 
RETURNS INT 
READS SQL DATA 
BEGIN

	DECLARE v_id INT;
	
	SELECT id
	  INTO v_id
	  FROM disposable.constant
     WHERE section = in_section
       AND name = in_name;
    
    IF v_id IS NULL THEN
    	SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Constant Not Found';
    END IF;
    
    RETURN v_id;
END$$
DELIMITER ;