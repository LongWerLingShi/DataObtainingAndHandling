var xmlHttp;
var mark = "processing";

function MyAutoRun()//自动运行实时监测数据处理程序运行状态
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

function begin()//开启数据处理程序
{
	mark = "opening";
	xmlHttp=GetXmlHttpObject();
	if (xmlHttp==null)
	  {
		  alert ("Browser does not support HTTP Request")
		  return
	  } 
	var url="console/DataProcessing.php"
	url=url+"?q="+1;
	url=url+"&sid="+Math.random()
	xmlHttp.onreadystatechange=stateChanged
	xmlHttp.open("GET",url,true)
	xmlHttp.send(null)
}

function rebegin()//开启数据处理程序
{
	mark = "opening";
	xmlHttp=GetXmlHttpObject();
	if (xmlHttp==null)
	  {
		  alert ("Browser does not support HTTP Request")
		  return
	  } 
	var url="console/DataProcessing.php"
	url=url+"?q="+3;
	url=url+"&sid="+Math.random()
	xmlHttp.onreadystatechange=stateChanged
	xmlHttp.open("GET",url,true)
	xmlHttp.send(null)
}

function end()//关闭数据处理程序
{
	mark = "closing";
	xmlHttp=GetXmlHttpObject();
	if (xmlHttp==null)
	  {
		  alert ("Browser does not support HTTP Request")
		  return
	  } 
	var url="console/DataProcessing.php"
	url=url+"?q="+2;
	url=url+"&sid="+Math.random()
	xmlHttp.onreadystatechange=stateChanged_end
	xmlHttp.open("GET",url,true)
	xmlHttp.send(null)
}

function stateChanged()//监测程序回调函数
{ 
	if (xmlHttp.readyState==4 || xmlHttp.readyState=="complete")
	{
		var strJson = xmlHttp.responseText;
		//var state = new Function("return" + strJson)();
		try
		{
			var state = eval("("+strJson+")");
			if(state.openState == 'close' && mark == 'opening')
			{
				document.getElementById("dh_status").innerHTML="开启中...";
			}
			else if(state.openState == 'open' && mark == 'closing')
			{
				document.getElementById("dh_status").innerHTML="关闭中...";
			}
			else if(state.openState == 'open')
			{
				document.getElementById("dh_status").innerHTML="已开启";
			}
			else if(state.openState == 'close')
			{
				document.getElementById("dh_status").innerHTML="已关闭";
			}
			
			for(i = 0;i < parseInt(state.threadNumber);i++)
			{
				if(state.thread[i].URL == 'null')
					document.getElementById("thread"+(i+1).toString()+"_fileName").innerHTML = "线程尚未处理文件";
				else
					document.getElementById("thread"+(i+1).toString()+"_fileName").innerHTML = state.thread[i].URL;
				if(state.thread[i].schedule == "Waiting")
				{
					document.getElementById("thread"+(i+1).toString()+"_progress").innerHTML = "线程空闲中...";
					
					document.getElementById("thread"+(i+1).toString()+"_pro_getting").style.width = "0%";
					document.getElementById("thread"+(i+1).toString()+"_pro_dealing").style.width = "0%";
					document.getElementById("thread"+(i+1).toString()+"_pro_sending").style.width = "0%";
				}
				else if(state.thread[i].schedule == "Getting")
				{
					document.getElementById("thread"+(i+1).toString()+"_progress").innerHTML = "获取待处理文件完成";
					
					document.getElementById("thread"+(i+1).toString()+"_pro_getting").style.width = "20%";
					document.getElementById("thread"+(i+1).toString()+"_pro_dealing").style.width = "0%";
					document.getElementById("thread"+(i+1).toString()+"_pro_sending").style.width = "0%";
				}
				else if(state.thread[i].schedule == "Dealing")
				{
					document.getElementById("thread"+(i+1).toString()+"_progress").innerHTML = "处理文件完成";
					
					document.getElementById("thread"+(i+1).toString()+"_pro_getting").style.width = "20%";
					document.getElementById("thread"+(i+1).toString()+"_pro_dealing").style.width = "50%";
					document.getElementById("thread"+(i+1).toString()+"_pro_sending").style.width = "0%";
				}
				else if(state.thread[i].schedule == "Sending")
				{
					document.getElementById("thread"+(i+1).toString()+"_progress").innerHTML = "上传文件完成";
					
					document.getElementById("thread"+(i+1).toString()+"_pro_getting").style.width = "20%";
					document.getElementById("thread"+(i+1).toString()+"_pro_dealing").style.width = "50%";
					document.getElementById("thread"+(i+1).toString()+"_pro_sending").style.width = "30%";
				}
				else if(state.thread[i].schedule == "Working")
				{
					document.getElementById("thread"+(i+1).toString()+"_progress").innerHTML = "线程忙绿中...";
					
					document.getElementById("thread"+(i+1).toString()+"_pro_getting").style.width = "20%";
					document.getElementById("thread"+(i+1).toString()+"_pro_dealing").style.width = "50%";
					document.getElementById("thread"+(i+1).toString()+"_pro_sending").style.width = "30%";
				}
			}
			document.getElementById("dh_starttime").innerHTML = state.handleTime;
			document.getElementById("dh_filecount").innerHTML = state.fileTitle;
			document.getElementById("dh_dealedcount").innerHTML = state.fileHaveDone;
			document.getElementById("dh_failedcount").innerHTML = state.fileHandleFail;
			
			document.getElementById("html_totalcount").innerHTML = state.htmlTitle;
			document.getElementById("html_dealedcount").innerHTML = state.htmlHaveDone;
			document.getElementById("html_pro").style.width = divide(state.htmlHaveDone,state.htmlTitle);
			
			document.getElementById("pdf_totalcount").innerHTML = state.pdfTitle;
			document.getElementById("pdf_dealedcount").innerHTML = state.pdfHaveDone;
			document.getElementById("pdf_pro").style.width = divide(state.pdfHaveDone,state.pdfTitle);
			
			document.getElementById("doc_totalcount").innerHTML = state.wordTitle;
			document.getElementById("doc_dealedcount").innerHTML = state.wordHaveDone;
			document.getElementById("doc_pro").style.width = divide(state.wordHaveDone,state.wordTitle);
			
			document.getElementById("picture_totalcount").innerHTML = state.pictureTitle;
			document.getElementById("picture_dealedcount").innerHTML = state.pictureHaveDone;
			document.getElementById("picture_pro").style.width = divide(state.pictureHaveDone,state.pictureTitle);
		}
		catch(Exception){}
	} 
}
function divide(a,b)
{
	if(b != 0)
		return parseInt((parseFloat(a)/parseFloat(b))*100).toString()+"%";
	else
		return "0%";
}
function stateChanged_end() //关闭程序回调函数
{ 
	if (xmlHttp.readyState==4 || xmlHttp.readyState=="complete")
	{
		var strJson = xmlHttp.responseText;
		//var state = eval(strJson);
		try
		{
			var state = new Function("return" + strJson)();
			if(state.openState == 'open')
			{
				end();
			}
		}
		catch(Exception){}
	} 
}

function GetXmlHttpObject()//获取xmlhttp对象
{
	var xmlHttp=null;
	try
	{
		// Firefox, Opera 8.0+, Safari
		xmlHttp=new XMLHttpRequest();
	}
	catch (e)
	{
		// Internet Explorer
		try
		{
			xmlHttp=new ActiveXObject("Msxml2.XMLHTTP");
		}
		catch (e)
		{
		xmlHttp=new ActiveXObject("Microsoft.XMLHTTP");
		}
	}
	return xmlHttp;
}