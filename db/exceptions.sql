CREATE OR REPLACE PACKAGE DISPOSABLE.exceptions
IS

/**********************************************************************
	ANY UNUSED CONSTANTS SHOULD BE MOVED TO THE "DEPRICATED"
	SECTION BELOW SO THAT OLD UPDATE SCRIPTS STILL RUN
**********************************************************************/

DUPLICATE_EMAIL					EXCEPTION;
ERR_DUPLICATE_EMAIL				CONSTANT NUMBER := 	-20100;
PRAGMA EXCEPTION_INIT(DUPLICATE_EMAIL,    			-20100);

END exceptions;
/

