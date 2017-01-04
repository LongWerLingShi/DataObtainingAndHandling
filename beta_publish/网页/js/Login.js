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
	try
	{
		if(getCookie("isLog")=="true")
		{
			delCookie("isLog");
		}
		else
		{
			$('#myModal').modal({
			backdrop: 'static',
			keyboard: false});
		}
	}
	catch(e)
	{
		$('#myModal').modal({
		backdrop: 'static',
		keyboard: false});
	}
	
}

USER.gotoDataHandler = function()
{
	setCookie("isLog","true");
	window.location.href="datahandler.html";
}

USER.gotoCrawler = function()
{
	setCookie("isLog","true");
	window.location.href="crawler.html";
}

//写cookies

function setCookie(name,value)
{
	var Days = 30;
	var exp = new Date();
	exp.setTime(exp.getTime() + Days*24*60*60*1000);
	document.cookie = name + "="+ escape (value) + ";expires=" + exp.toGMTString();
}

//读取cookies
function getCookie(name)
{
	var arr,reg=new RegExp("(^| )"+name+"=([^;]*)(;|$)");
 
	if(arr=document.cookie.match(reg))
 
		return unescape(arr[2]);
	else
		return null;
}

//删除cookies
function delCookie(name)
{
	var exp = new Date();
	exp.setTime(exp.getTime() - 1);
	var cval=getCookie(name);
	if(cval!=null)
		document.cookie= name + "="+cval+";expires="+exp.toGMTString();
} 

