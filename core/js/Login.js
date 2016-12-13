var USER = {};

USER.login = function()
{
	var name = 'lwls';
	var pwd = '123456';	
	var pwdinput = document.getElementById('password');
	var nameinput = document.getElementById('username');
	var getname = nameinput.value;
	var getpwd = pwdinput.value;	
	if((name === getname) && (pwd === getpwd)) {
		$(function () { $('#myModal').modal('hide')});
	}
	else {
		pwdinput.value = '';
		var loginform = document.getElementById('login-form');
		var errorinfo = document.getElementById('errorinfo');
		if(!errorinfo)
		{
			errorinfo = document.createElement('p');
		}
		errorinfo.id = 'errorinfo';
		errorinfo.innerHTML = '<span style=\'color:red;font-size:15px;\'>账号或密码错误</span>';
		errorinfo.style = '';
		errorinfo.class = 'littleborder';
		loginform.insertBefore(errorinfo,nameinput);
		setTimeout("USER.removeErrorInfo()",2000);
		
	}		
}

USER.removeErrorInfo = function()
{
	var loginform = document.getElementById('login-form');
	var errorinfo = document.getElementById('errorinfo');
	loginform.removeChild(errorinfo);
}

USER.showlogin = function()
{
	$('#myModal').modal({
	backdrop: 'static',
	keyboard: false});
}
