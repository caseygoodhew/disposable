CREATE OR REPLACE VIEW disposable.v$package_content
AS
	SELECT name,
			 CASE WHEN INSTR(stripped_text, ' ') = 0 THEN stripped_text ELSE SUBSTR(stripped_text, 1, INSTR(stripped_text, ' ')) END text,
			 CASE WHEN stripped_text LIKE 'T_%' THEN 1 ELSE 2 END type_order,
			 line
	  FROM (
		SELECT owner, name, type, line, 
			   CASE WHEN clean_text LIKE 'TYPE %' THEN TRIM(SUBSTR(clean_text, 5))
				    WHEN clean_text LIKE 'SUBTYPE %' THEN TRIM(SUBSTR(clean_text, 8))
					WHEN clean_text LIKE '% CONSTANT %' THEN TRIM(SUBSTR(clean_text, 1, INSTR(clean_text, ' CONSTANT ')))
			   END stripped_text
		  FROM (
				SELECT owner, name, type, line, TRIM(TRANSLATE(text, CHR(09), ' ')) clean_text
				  FROM all_source
				 WHERE owner = 'DISPOSABLE'
				   AND type = 'PACKAGE'
				 )
		 WHERE clean_text LIKE 'TYPE %' 
			OR clean_text LIKE 'SUBTYPE %' 
			OR clean_text LIKE '% CONSTANT %'
	   )
	 WHERE stripped_text NOT LIKE '--%'
;

 
CREATE OR REPLACE VIEW disposable.v$textpad_syn
AS
	-- PACKAGES
	SELECT owner||name||text result
	  FROM (
		SELECT x.type_order, o.owner, x.name||'.' name, x.text, x.line
		  FROM (
				SELECT NULL owner FROM DUAL
				 UNION 
				SELECT 'DISPOSABLE.' owner FROM DUAL
		  ) o, disposable.v$package_content x
	  	 UNION
		SELECT type_order, NULL owner, NULL name, text, line
		  FROM disposable.v$package_content
	  )
	 UNION ALL
	SELECT DISTINCT column_name result
	  FROM all_tab_cols
 	 WHERE owner = 'DISPOSABLE'
 	 UNION ALL
 	SELECT (CASE WHEN x.use_owner = 1 THEN ao.owner||'.' END)||ao.object_name result
	  FROM all_objects ao, (SELECT 1 use_owner FROM DUAL UNION SELECT 0 use_owner FROM DUAL) x
	 WHERE owner = 'DISPOSABLE' 
	   AND object_type NOT IN ('INDEX', 'PACKAGE')
;