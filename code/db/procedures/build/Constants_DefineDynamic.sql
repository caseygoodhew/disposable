DELIMITER $$

DROP PROCEDURE IF EXISTS disposable.Constants_DefineDynamic$$

CREATE PROCEDURE disposable.Constants_DefineDynamic(
	in_section 				VARCHAR(100), 
	in_schema 				VARCHAR(100), 
	in_table 				VARCHAR(100), 
	in_name_column 			VARCHAR(100), 
	in_value_column 		VARCHAR(100)
) 
BEGIN
	DECLARE v_tidy_re VARCHAR(100) DEFAULT '[^0-9a-zA-Z_]+';
	DECLARE v_re_err_msg VARCHAR(100);
	DECLARE v_invalid_name VARCHAR(100);

	DELETE FROM constant 
	 WHERE section = UPPER(in_section);
	
	IF regex_replace(in_section, v_tidy_re, '') <> in_section THEN
		SET v_re_err_msg = CONCAT('Invalid section - must conform to ', v_tidy_re);
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = v_re_err_msg;
	END IF;
	
	SET @statement = CONCAT('INSERT INTO constant (section, name, id) ', 
							'SELECT ''', UPPER(in_section), ''', UPPER(', regex_replace(in_name_column, v_tidy_re, ''), '), ', regex_replace(in_value_column, v_tidy_re, ''), 
							'  FROM ', regex_replace(in_schema, v_tidy_re, ''), '.', regex_replace(in_table, v_tidy_re, ''));
	
	PREPARE stmt FROM @statement;
	EXECUTE stmt;
	DEALLOCATE PREPARE stmt;
	
	IF (SELECT COUNT(*)
		  FROM constant
		 WHERE section = UPPER(in_section)
		   AND name <> regex_replace(name, v_tidy_re, '')) > 0
	THEN
		SET v_re_err_msg = CONCAT('Invalid name - must conform to ', v_tidy_re);
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = v_re_err_msg; 
	END IF;
END$$

DELIMITER ;
