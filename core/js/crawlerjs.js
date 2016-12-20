var CRAWLER = {};

CRAWLER.start = function () {
	var status = document.getElementById('crawler_status');
	status.innerText = '爬取中...';
}

CRAWLER.stop = function () {
	var status = document.getElementById('crawler_status');
	status.innerText = '停止'
}

CRAWLER.del = function(id){
	
}

CRAWLER.del2 = function(id){
	
}

function MyAutoRun2()
{
	xmlHttp=GetXmlHttpObject();
	if (xmlHttp==null)
	  {
		  alert ("Browser does not support HTTP Request")
		  return
	  } 
	var url="console/DataProcessing.php"
	url=url+"?q="+0;
	url=url+"&sid="+Math.random()
	xmlHttp.onreadystatechange=stateChanged
	xmlHttp.open("GET",url,true)
	xmlHttp.send(null)
}

function stateChanged()
{ 
	if (xmlHttp.readyState==4 || xmlHttp.readyState=="complete")
	{
		try
		{
			var strJson = xmlHttp.responseText;
			//alert(strJson);
			//var json = new Function("return" + strJson)();
			//alert(strJson);
			var jsonInfo = eval("("+strJson+")");
			document.getElementById("crawler_status").innerHTML = jsonInfo.state
			document.getElementById("crawler_starttime").innerHTML = jsonInfo.startTime
			document.getElementById("crawler_filecount").innerHTML = jsonInfo.fileNumber
			document.getElementById("htmlcount").innerHTML = jsonInfo.htmlNumber
			document.getElementById("pdfcount").innerHTML = jsonInfo.pdfNumber
			document.getElementById("doccount").innerHTML = jsonInfo.docNumber
			document.getElementById("picturecount").innerHTML = jsonInfo.pictureNumber
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
	var url="console/Crawler.php"
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
			var jsonInfo = eval("("+strJson+")");
			var textarea = document.getElementById("textList");
			var ul = document.getElementById("ulList");
			var htmlList = "";
			var htmlTemp = textarea.value;
			var i = 0;
			if(jsonInfo != null)
			{
				jsonInfo.web.forEach(function(object) {
					htmlList += htmlTemp.temp(object);
					htmlList=htmlList.replace("$id$",i.toString());  
					i += 1;
				});
			}
			ul.innerHTML = htmlList;
			
			var textarea2 = document.getElementById("textList2");
			var ul2 = document.getElementById("ulList2");
			htmlList = "";
			htmlTemp = textarea2.value;
			i = 0;
			if(jsonInfo != null)
			{
				jsonInfo.keyWord.forEach(function(object) {
					htmlList += htmlTemp.temp(object);
					htmlList=htmlList.replace("$id$",i.toString());  
					i += 1;
				});
			}
			ul2.innerHTML = htmlList;
		}
		catch(e){}
	}
}

String.prototype.temp = function(obj) {
	return this.replace(/\$\w+\$/gi, function(matchs) {
		var returns = obj[matchs.replace(/\$/g, "")];
		return (returns + "") == "undefined"? "": returns;
	});
};

