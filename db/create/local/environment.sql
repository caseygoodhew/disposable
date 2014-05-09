DECLARE
	v_sid   constants.t_sid;
BEGIN
	v_sid := protected.user_pkg.createuser('goodhewc@gmail.com', 'password1');
END;
/