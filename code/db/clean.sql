-- http://www.devshed.com/c/a/MySQL/Error-Handling/
-- http://dev.mysql.com/doc/refman/5.0/en/commit.html
-- http://stackoverflow.com/questions/6908453/how-to-catch-any-exception-in-triggers-and-store-procedures-for-mysql
-- http://dev.mysql.com/doc/refman/5.1/en/declare-handler.html

DROP DATABASE IF EXISTS DISPOSABLE;
CREATE DATABASE DISPOSABLE /*!40100 DEFAULT CHARACTER SET utf8 */;

SOURCE Constants_DefineDynamic.sql

SOURCE ./procedures/build/Constants_DefineDynamic.sql
SOURCE ./procedures/utils/regex_replace.sql

SOURCE create_schema.sql;
--SOURCE constants.sql;
--SOURCE basedata.sql;