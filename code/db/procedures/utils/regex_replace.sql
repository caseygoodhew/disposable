-- http://techras.wordpress.com/2011/06/02/regex-replace-for-mysql/

DELIMITER $$

DROP FUNCTION IF EXISTS disposable.regex_replace$$

CREATE FUNCTION disposable.regex_replace (
	original VARCHAR(1000), 
	pattern VARCHAR(1000), 
	replacement VARCHAR(1000)
)
RETURNS VARCHAR(1000)
DETERMINISTIC
BEGIN 
	
	DECLARE temp VARCHAR(1000); 
	DECLARE ch VARCHAR(1); 
	DECLARE i INT;
	
	SET i = 1;
	SET temp = '';
	
	IF original REGEXP pattern THEN 
		loop_label: LOOP 
			
			IF i > CHAR_LENGTH(original) THEN
				LEAVE loop_label;  
			END IF;
		
			SET ch = SUBSTRING(original,i,1);
			
			IF NOT ch REGEXP pattern THEN
				SET temp = CONCAT(temp, ch);
			ELSE
				SET temp = CONCAT(temp, replacement);
			END IF;
			
			SET i=i+1;
			
		END LOOP;
	ELSE
		SET temp = original;
	END IF;
	
	RETURN temp;
	
END$$
DELIMITER ;