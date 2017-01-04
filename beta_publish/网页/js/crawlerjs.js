var CRAWLER = {};
var crawlerThreadNumber = 0;
var jsonInfo;
var xmlHttp2;

function isExistWeb(website,websites){
	var mark = false;
	websites.web.forEach(function(object) {
		if(website["URL"] == object["URL"])
			mark = true;
	})
	return mark;
}
function isExistKW(keyword,keywords){
	var mark = false;
	keywords.keyWord.forEach(function(object) {
		if(keyword["word"] == object["word"])
			mark = true;
	})
	return mark;
}
function webFileSelect() {
	document.getElementById("webFileToUpload").click();
}
function webFileSelected(th) {
	var strJson =$.ajax({url:getFullPath(th),async:false}).responseText;
	//alert(strJson);
	try
	{
		var newJson = eval("("+strJson+")");
		//alert(newJson.a);
		xmlHttp=GetXmlHttpObject();
		if (xmlHttp==null)
		{
			alert ("Browser does not support HTTP Request");
			return;
		} 
		var json = 
			"{\"threadNumber\":"+crawlerThreadNumber+",";
			var mark = true;
			json+="\"web\":[";
			newJson.web.forEach(function(object) {
				if(!isExistWeb(object,jsonInfo))
				{
					if(mark)
					{
						json+="{\"URL\":\""+object["URL"]+"\"}";
						mark = false;
					}
					else
						json+=",{\"URL\":\""+object["URL"]+"\"}";
				}
			});
			jsonInfo.web.forEach(function(object) {
				if(mark)
				{
					json+="{\"URL\":\""+object["URL"]+"\"}";
					mark = false;
				}
				else
					json+=",{\"URL\":\""+object["URL"]+"\"}";
			});
			mark = true;
			json+="],\"keyWord\":[";
			jsonInfo.keyWord.forEach(function(object) {
				if(mark)
				{
					json+="{\"word\":\""+object["word"]+"\"}";
					mark = false;
				}
				else
					json+=",{\"word\":\""+object["word"]+"\"}";
			});
			json+="]}";
		var url="console/setCrawler.php"
		//alert(json);
		url=url+"?q="+json;
		url=url+"&sid="+Math.random();
		xmlHttp.onreadystatechange=stateChanged_WS;
		xmlHttp.open("GET",url,true);
		xmlHttp.send(null);
	}
	catch(e){
		alert("文件格式有误，请仔细检查！！！");
	};
}
function kWFileSelect() {
	document.getElementById("kWFileToUpload").click();
}
function kWFileSelected(th) {
	var strJson =$.ajax({url:getFullPath(th),async:false}).responseText;
	//alert(strJson);
	try
	{
		var newJson = eval("("+strJson+")");
		//alert(newJson.a);
		xmlHttp=GetXmlHttpObject();
		if (xmlHttp==null)
		{
			alert ("Browser does not support HTTP Request");
			return;
		} 
		var json = 
			"{\"threadNumber\":"+crawlerThreadNumber+",";
			var mark = true;
			json+="\"web\":[";
			jsonInfo.web.forEach(function(object) {
				if(mark)
				{
					json+="{\"URL\":\""+object["URL"]+"\"}";
					mark = false;
				}
				else
					json+=",{\"URL\":\""+object["URL"]+"\"}";
			});
			mark = true;
			json+="],\"keyWord\":[";
			newJson.keyWord.forEach(function(object) {
				if(!isExistKW(object,jsonInfo))
				{
					if(mark)
					{
						json+="{\"word\":\""+object["word"]+"\"}";
						mark = false;
					}
					else
						json+=",{\"word\":\""+object["word"]+"\"}";
				}
			});
			jsonInfo.keyWord.forEach(function(object) {
				if(mark)
				{
					json+="{\"word\":\""+object["word"]+"\"}";
					mark = false;
				}
				else
					json+=",{\"word\":\""+object["word"]+"\"}";
			});
			json+="]}";
		var url="console/setCrawler.php"
		//alert(json);
		url=url+"?q="+json;
		url=url+"&sid="+Math.random();
		xmlHttp.onreadystatechange=stateChanged_WS;
		xmlHttp.open("GET",url,true);
		xmlHttp.send(null);
	}
	catch(e){
		alert("文件格式有误，请仔细检查！！！");
	};
}
function getFullPath(obj){ 
	if(obj) 
	{ 
	 //ie 
	 if (window.navigator.userAgent.indexOf("MSIE")>=1) 
	 { 
	 obj.select(); 
	 return document.selection.createRange().text; 
	 } 
	 //firefox 
	 else if(window.navigator.userAgent.indexOf("Firefox")>=1) 
	 { 
	 if(obj.files) 
	 { 
	 return window.URL.createObjectURL(obj.files[0]);
	 } 
	 return obj.value; 
	 } 
	 return obj.value; 
	} 
	} 

CRAWLER.start = function () {
	$('#setThreadModal').modal({
		backdrop: 'static',
		keyboard: false});
}

CRAWLER.confirm = function(){
	var threadNumber = document.getElementById("threadNumber");
	if(parseInt(threadNumber.value).toString() == threadNumber.value && parseInt(threadNumber.value)>0)
	{
		crawlerThreadNumber = parseInt(threadNumber.value);
		xmlHttp=GetXmlHttpObject();
		if (xmlHttp==null)
		{
			alert ("Browser does not support HTTP Request");
			return;
		} 
		var json = "{\"threadNumber\":"+crawlerThreadNumber+",";
		var mark = true;
		json+="\"web\":[";
		jsonInfo.web.forEach(function(object) {
			if(mark)
			{
				json+="{\"URL\":\""+object["URL"]+"\"}";
				mark = false;
			}
			else
				json+=",{\"URL\":\""+object["URL"]+"\"}";
		});
		mark = true;
		json+="],\"keyWord\":[";
		jsonInfo.keyWord.forEach(function(object) {
			if(mark)
			{
				json+="{\"word\":\""+object["word"]+"\"}";
				mark = false;
			}
			else
				json+=",{\"word\":\""+object["word"]+"\"}";
		});
		json+="]}";
		var url="console/setCrawler.php"
		//alert(json);
		url=url+"?q="+json;
		url=url+"&sid="+Math.random();
		xmlHttp.onreadystatechange=stateChanged_WS;
		xmlHttp.open("GET",url,true);
		xmlHttp.send(null);
		
		$('#setThreadModal').modal('hide');
	}
	else
	{
		var setThreadForm = document.getElementById('setThreadForm');
		var errorinfo = document.getElementById('errorinfo');
		if(!errorinfo)
		{
			errorinfo = document.createElement('p');
		}
		errorinfo.id = 'errorinfo';
		errorinfo.innerHTML = '<span style=\'color:red;font-size:12px;\'>爬虫线程数应为大于0的阿拉伯数字</span>';
		errorinfo.style = '';
		errorinfo.class = 'setThreadModal';
		setThreadForm.insertBefore(errorinfo,threadNumber);
		setTimeout("CRAWLER.removeErrorInfo(setThreadForm,errorinfo)",2000);
	}
}

CRAWLER.stop = function () {
	crawlerThreadNumber = 0;
	xmlHttp=GetXmlHttpObject();
	if (xmlHttp==null)
	{
		alert ("Browser does not support HTTP Request");
		return;
	} 
	var json = "{\"threadNumber\":"+crawlerThreadNumber+",";
	var mark = true;
	json+="\"web\":[";
	jsonInfo.web.forEach(function(object) {
		if(mark)
		{
			json+="{\"URL\":\""+object["URL"]+"\"}";
			mark = false;
		}
		else
			json+=",{\"URL\":\""+object["URL"]+"\"}";
	});
	mark = true;
	json+="],\"keyWord\":[";
	jsonInfo.keyWord.forEach(function(object) {
		if(mark)
		{
			json+="{\"word\":\""+object["word"]+"\"}";
			mark = false;
		}
		else
			json+=",{\"word\":\""+object["word"]+"\"}";
	});
	json+="]}";
	var url="console/setCrawler.php"
	//alert(json);
	url=url+"?q="+json;
	url=url+"&sid="+Math.random();
	xmlHttp.onreadystatechange=stateChanged_WS;
	xmlHttp.open("GET",url,true);
	xmlHttp.send(null);
}

CRAWLER.addWeb = function(){
	xmlHttp=GetXmlHttpObject();
	var webaddress = document.getElementById("webaddress");
	if(webaddress.value != '')
	{
		if (xmlHttp==null)
		{
			alert ("Browser does not support HTTP Request");
			return;
		} 
		var json = 
			"{\"threadNumber\":"+crawlerThreadNumber+","
			+"\"web\":[";
			var tempJson = {"URL":webaddress.value};
			var mark = true;
			if(!isExistWeb(tempJson,jsonInfo))
			{
				json+="{\"URL\":\""+webaddress.value+"\"}";
				mark = false;
			}
			jsonInfo.web.forEach(function(object) {
				if(mark)
				{
					json+="{\"URL\":\""+object["URL"]+"\"}";
					mark = false;
				}
				else
					json+=",{\"URL\":\""+object["URL"]+"\"}";
			});
			mark = true;
			json+="],\"keyWord\":[";
			jsonInfo.keyWord.forEach(function(object) {
				if(mark)
				{
					json+="{\"word\":\""+object["word"]+"\"}";
					mark = false;
				}
				else
					json+=",{\"word\":\""+object["word"]+"\"}";
			});
			json+="]}";
		var url="console/setCrawler.php"
		//alert(json);
		url=url+"?q="+json;
		url=url+"&sid="+Math.random();
		xmlHttp.onreadystatechange=stateChanged_WS;
		xmlHttp.open("GET",url,true);
		xmlHttp.send(null);
	}
	else
	{
		var webAdd = document.getElementById('webAdd');
		var errorinfo = document.getElementById('errorinfo');
		if(!errorinfo)
		{
			errorinfo = document.createElement('p');
		}
		errorinfo.id = 'errorinfo';
		errorinfo.innerHTML = '<span style=\'color:red;font-size:12px;\'>网址不能为空</span>';
		errorinfo.style = '';
		errorinfo.class = 'littleborder';
		webAdd.insertBefore(errorinfo,webaddress);
		setTimeout("CRAWLER.removeErrorInfo(webAdd,errorinfo)",2000);
	}
}

CRAWLER.removeErrorInfo = function(father,child)
{
	father.removeChild(child);
}


CRAWLER.addKeyWord = function(){
	xmlHttp=GetXmlHttpObject();
	var keyword = document.getElementById("keyword");
	if(keyword.value != '')
	{
		if (xmlHttp==null)
		{
			alert ("Browser does not support HTTP Request");
			return;
		} 
		var json = 
			"{\"threadNumber\":"+crawlerThreadNumber+",";
			var mark = true;
			json+="\"web\":[";
			jsonInfo.web.forEach(function(object) {
				if(mark)
				{
					json+="{\"URL\":\""+object["URL"]+"\"}";
					mark = false;
				}
				else
					json+=",{\"URL\":\""+object["URL"]+"\"}";
			});
			json+="],\"keyWord\":[";
			mark = true;
			var tempJson = {"word":keyWord.value};
			if(!isExistKW(tempJson,jsonInfo))
			{
				json+="{\"word\":\""+keyword.value+"\"}";
				mark = false;
			}
			jsonInfo.keyWord.forEach(function(object) {
				if(mark)
				{
					json+="{\"word\":\""+object["word"]+"\"}";
					mark = false;
				}
				else
					json+=",{\"word\":\""+object["word"]+"\"}";
			});
			json+="]}";
		var url="console/setCrawler.php"
		//alert(json);
		url=url+"?q="+json;
		url=url+"&sid="+Math.random();
		xmlHttp.onreadystatechange=stateChanged_WS;
		xmlHttp.open("GET",url,true);
		xmlHttp.send(null);
	}
	else
	{
		var keyWordAdd = document.getElementById('keyWordAdd');
		var errorinfo = document.getElementById('errorinfo');
		if(!errorinfo)
		{
			errorinfo = document.createElement('p');
		}
		errorinfo.id = 'errorinfo';
		errorinfo.innerHTML = '<span style=\'color:red;font-size:12px;\'>关键词不能为空</span>';
		errorinfo.style = '';
		errorinfo.class = 'littleborder';
		keyWordAdd.insertBefore(errorinfo,keyword);
		setTimeout("CRAWLER.removeErrorInfo(keyWordAdd,errorinfo)",2000);
	}
}

CRAWLER.delWeb = function(id){
	xmlHttp=GetXmlHttpObject();
	if (xmlHttp==null)
	{
		alert ("Browser does not support HTTP Request");
		return;
	} 
	var json = "{\"threadNumber\":"+crawlerThreadNumber+",";
	var mark = true;
	var i = 0;
	json+="\"web\":[";
	jsonInfo.web.forEach(function(object) {
		if(i != id)
		{
			if(mark)
			{
				json+="{\"URL\":\""+object["URL"]+"\"}";
				mark = false;
			}
			else
				json+=",{\"URL\":\""+object["URL"]+"\"}";
		}
		i++;
	});
	mark = true;
	json+="],\"keyWord\":[";
	jsonInfo.keyWord.forEach(function(object) {
		if(mark)
		{
			json+="{\"word\":\""+object["word"]+"\"}";
			mark = false;
		}
		else
			json+=",{\"word\":\""+object["word"]+"\"}";
	});
	json+="]}";
	var url="console/setCrawler.php"
	//alert(json);
	url=url+"?q="+json;
	url=url+"&sid="+Math.random();
	xmlHttp.onreadystatechange=stateChanged_WS;
	xmlHttp.open("GET",url,true);
	xmlHttp.send(null);
}

CRAWLER.delKeyWord = function(id){
	xmlHttp=GetXmlHttpObject();
	if (xmlHttp==null)
	{
		alert ("Browser does not support HTTP Request");
		return;
	} 
	var json = 
		"{\"threadNumber\":"+crawlerThreadNumber+",";
		var mark = true;
		json+="\"web\":[";
		jsonInfo.web.forEach(function(object) {
			if(mark)
			{
				json+="{\"URL\":\""+object["URL"]+"\"}";
				mark = false;
			}
			else
				json+=",{\"URL\":\""+object["URL"]+"\"}";
		});
		mark = true;
		var i = 0;
		json+="],\"keyWord\":[";
		jsonInfo.keyWord.forEach(function(object) {
			if(i != id)
			{
				if(mark)
				{
					json+="{\"word\":\""+object["word"]+"\"}";
					mark = false;
				}
				else
					json+=",{\"word\":\""+object["word"]+"\"}";
			}
			i++;
		});
		json+="]}";
	var url="console/setCrawler.php"
	//alert(json);
	url=url+"?q="+json;
	url=url+"&sid="+Math.random();
	xmlHttp.onreadystatechange=stateChanged_WS;
	xmlHttp.open("GET",url,true);
	xmlHttp.send(null);
}

function MyAutoRun2()
{
	xmlHttp2=GetXmlHttpObject();
	if (xmlHttp2==null)
	  {
		  alert ("Browser does not support HTTP Request")
		  return
	  } 
	var url="console/getCrawler.php"
	url=url+"?q="+0;
	url=url+"&sid="+Math.random()
	xmlHttp2.onreadystatechange=stateChanged_2
	xmlHttp2.open("GET",url,true)
	xmlHttp2.send(null)
}

function stateChanged_2()
{ 
	if (xmlHttp2.readyState==4 || xmlHttp2.readyState=="complete")
	{
		try
		{
			var strJson = xmlHttp2.responseText;
			//alert(strJson);
			//var json = new Function("return" + strJson)();
			//alert(strJson);
			var jsonInfo = eval("("+strJson+")");
			document.getElementById("crawler_status").innerHTML = jsonInfo.state
			document.getElementById("crawler_starttime").innerHTML = jsonInfo.startTime
			document.getElementById("crawler_filecount").innerHTML = jsonInfo.fileNumber.toString()
			document.getElementById("crawler_number").innerHTML = jsonInfo.crawlerNumber.toString()
			document.getElementById("htmlcount").innerHTML = jsonInfo.htmlNumber.toString()
			document.getElementById("pdfcount").innerHTML = jsonInfo.pdfNumber.toString()
			document.getElementById("doccount").innerHTML = jsonInfo.docNumber.toString()
			document.getElementById("picturecount").innerHTML = jsonInfo.pictureNumber.toString()
			
		}
		catch(e){}
	}
}

function LoadWS()
{
	xmlHttp=GetXmlHttpObject();
	if (xmlHttp==null)
	  {
		  alert ("Browser does not support HTTP Request")
		  return
	  } 
	var url="console/getCrawler.php"
	url=url+"?q="+0;
	url=url+"&sid="+Math.random()
	xmlHttp.onreadystatechange=stateChanged_WS
	xmlHttp.open("GET",url,true)
	xmlHttp.send(null)
}

function stateChanged_WS()
{ 
	if (xmlHttp.readyState==4 || xmlHttp.readyState=="complete")
	{
		try
		{
			var strJson = xmlHttp.responseText;
			//alert(strJson);
			//var json = new Function("return" + strJson)();
			//alert(strJson);
			jsonInfo = eval("("+strJson+")");
			var textShowWeb = document.getElementById("textShowWeb");
			var textAddWeb = document.getElementById("textAddWeb");
			var ulWeb = document.getElementById("ulWeb");
			var htmlList = "";
			var htmlTemp = textShowWeb.value;
			var i = 0;
			if(jsonInfo != null)
			{
				jsonInfo.web.forEach(function(object) {
					htmlList += htmlTemp.temp(object);
					var reg=new RegExp("#id#","g"); //创建正则RegExp对象 
					htmlList=htmlList.replace(reg,i.toString());  
					i += 1;
				});
			}
			ulWeb.innerHTML = textAddWeb.value + htmlList;
			
			var textShowKeyWord = document.getElementById("textShowKeyWord");
			var textAddKeyWord = document.getElementById("textAddKeyWord");
			var ulKeyWord = document.getElementById("ulKeyWord");
			htmlList = "";
			htmlTemp = textShowKeyWord.value;
			i = 0;
			if(jsonInfo != null)
			{
				jsonInfo.keyWord.forEach(function(object) {
					htmlList += htmlTemp.temp(object);
					var reg=new RegExp("#id#","g"); //创建正则RegExp对象 
					htmlList=htmlList.replace(reg,i.toString());  
					i += 1;
				});
			}
			ulKeyWord.innerHTML = textAddKeyWord.value + htmlList;
		}
		catch(e){}
	}
	else{}
}

String.prototype.temp = function(obj) {
	return this.replace(/\$\w+\$/gi, function(matchs) {
		var returns = obj[matchs.replace(/\$/g, "")];
		return (returns + "") == "undefined"? "": returns;
	});
};

