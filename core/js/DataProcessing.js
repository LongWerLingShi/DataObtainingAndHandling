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
	var url="php/DataProcessing.php"
	url=url+"?q="+0;
	url=url+"&sid="+Math.random()
	xmlHttp.onreadystatechange=stateChanged_0
	xmlHttp.open("GET",url,true)
	xmlHttp.send(null)
}

function begin()//开启数据处理程序
{
	document.getElementById("dh_status").innerHTML="开启中...";
	mark = "opening";
	xmlHttp=GetXmlHttpObject();
	if (xmlHttp==null)
	  {
		  alert ("Browser does not support HTTP Request")
		  return
	  } 
	var url="php/DataProcessing.php"
	url=url+"?q="+1;
	url=url+"&sid="+Math.random()
	xmlHttp.onreadystatechange=stateChanged_1
	xmlHttp.open("GET",url,true)
	xmlHttp.send(null)
}

function rebegin()//开启数据处理程序
{
	document.getElementById("dh_status").innerHTML="开启中...";
	mark = "opening";
	xmlHttp=GetXmlHttpObject();
	if (xmlHttp==null)
	  {
		  alert ("Browser does not support HTTP Request")
		  return
	  } 
	var url="php/DataProcessing.php"
	url=url+"?q="+3;
	url=url+"&sid="+Math.random()
	xmlHttp.onreadystatechange=stateChanged_1
	xmlHttp.open("GET",url,true)
	xmlHttp.send(null)
}

function end()//关闭数据处理程序
{
	document.getElementById("dh_status").innerHTML="关闭中...";
	mark = "closing";
	xmlHttp=GetXmlHttpObject();
	if (xmlHttp==null)
	  {
		  alert ("Browser does not support HTTP Request")
		  return
	  } 
	var url="php/DataProcessing.php"
	url=url+"?q="+2;
	url=url+"&sid="+Math.random()
	xmlHttp.onreadystatechange=stateChanged_2
	xmlHttp.open("GET",url,true)
	xmlHttp.send(null)
}

function stateChanged_0()//监测程序回调函数
{ 
	if (xmlHttp.readyState==4 || xmlHttp.readyState=="complete")
	{
		var strJson = xmlHttp.responseText;
		//var state = new Function("return" + strJson)();
		try
		{
			var state = eval("("+strJson+")");
			//if((state.openState == 'close' && mark == 'closing')
			//	||(state.openState == 'open' && mark == 'opening'))
			//{
			//	document.getElementById("processing").innerHTML=state.openState;
			//}
			document.getElementById("dh_status").innerHTML=state.openState;
			for(i = 0;i < parseInt(state.threadNumber);i++)
			{
				if(state.thread[i].schedule == "waiting")
				{
					document.getElementById("thread"+i.toString()+"_pro_getting").style.display = "0%";
					document.getElementById("thread"+i.toString()+"_pro_dealing").style.display = "0%";
					document.getElementById("thread"+i.toString()+"_pro_sending").style.display = "0%";
				}
				else if(state.thread[i].schedule == "getting")
				{
					document.getElementById("thread"+i.toString()+"_pro_getting").style.display = "20%";
					document.getElementById("thread"+i.toString()+"_pro_dealing").style.display = "0%";
					document.getElementById("thread"+i.toString()+"_pro_sending").style.display = "0%";
				}
				else if(state.thread[i].schedule == "dealing")
				{
					document.getElementById("thread"+i.toString()+"_pro_getting").style.display = "0%";
					document.getElementById("thread"+i.toString()+"_pro_dealing").style.display = "50%";
					document.getElementById("thread"+i.toString()+"_pro_sending").style.display = "0%";
				}
				else if(state.thread[i].schedule == "sending")
				{
					document.getElementById("thread"+i.toString()+"_pro_getting").style.display = "0%";
					document.getElementById("thread"+i.toString()+"_pro_dealing").style.display = "0%";
					document.getElementById("thread"+i.toString()+"_pro_sending").style.display = "30%";
				}
			}
		}
		catch(Exception){}
	} 
}
function stateChanged_1() //开启程序回调函数
{ 
	if (xmlHttp.readyState==4 || xmlHttp.readyState=="complete")
	{
		var strJson = xmlHttp.responseText;
		//var state = eval(strJson);
		try
		{
			var state = new Function("return" + strJson)();
			document.getElementById("dh_status").innerHTML=state.openState;
			for(i = 0;i < parseInt(state.threadNumber);i++)
			{
				if(state.thread[i].schedule == "waiting")
				{
					document.getElementById("thread"+i.toString()+"_pro_getting").style.display = "0%";
					document.getElementById("thread"+i.toString()+"_pro_dealing").style.display = "0%";
					document.getElementById("thread"+i.toString()+"_pro_sending").style.display = "0%";
				}
				else if(state.thread[i].schedule == "getting")
				{
					document.getElementById("thread"+i.toString()+"_pro_getting").style.display = "20%";
					document.getElementById("thread"+i.toString()+"_pro_dealing").style.display = "0%";
					document.getElementById("thread"+i.toString()+"_pro_sending").style.display = "0%";
				}
				else if(state.thread[i].schedule == "dealing")
				{
					document.getElementById("thread"+i.toString()+"_pro_getting").style.display = "0%";
					document.getElementById("thread"+i.toString()+"_pro_dealing").style.display = "50%";
					document.getElementById("thread"+i.toString()+"_pro_sending").style.display = "0%";
				}
				else if(state.thread[i].schedule == "sending")
				{
					document.getElementById("thread"+i.toString()+"_pro_getting").style.display = "0%";
					document.getElementById("thread"+i.toString()+"_pro_dealing").style.display = "0%";
					document.getElementById("thread"+i.toString()+"_pro_sending").style.display = "30%";
				}
			}
		}
		catch(Exception){}
	} 
}
function stateChanged_2() //关闭程序回调函数
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
				var url="php/DataProcessing.php"
				url=url+"?q="+2;
				url=url+"&sid="+Math.random()
				xmlHttp.onreadystatechange=stateChanged_2
				xmlHttp.open("GET",url,true)
				xmlHttp.send(null)
			}
			document.getElementById("dh_status").innerHTML=state.openState;
			for(i = 0;i < parseInt(state.threadNumber);i++)
			{
				if(state.thread[i].schedule == "waiting")
				{
					document.getElementById("thread"+i.toString()+"_pro_getting").style.width = "0%";
					document.getElementById("thread"+i.toString()+"_pro_dealing").style.display = "0%";
					document.getElementById("thread"+i.toString()+"_pro_sending").style.display = "0%";
				}
				else if(state.thread[i].schedule == "getting")
				{
					document.getElementById("thread"+i.toString()+"_pro_getting").style.display = "20%";
					document.getElementById("thread"+i.toString()+"_pro_dealing").style.display = "0%";
					document.getElementById("thread"+i.toString()+"_pro_sending").style.display = "0%";
				}
				else if(state.thread[i].schedule == "dealing")
				{
					document.getElementById("thread"+i.toString()+"_pro_getting").style.display = "0%";
					document.getElementById("thread"+i.toString()+"_pro_dealing").style.display = "50%";
					document.getElementById("thread"+i.toString()+"_pro_sending").style.display = "0%";
				}
				else if(state.thread[i].schedule == "sending")
				{
					document.getElementById("thread"+i.toString()+"_pro_getting").style.display = "0%";
					document.getElementById("thread"+i.toString()+"_pro_dealing").style.display = "0%";
					document.getElementById("thread"+i.toString()+"_pro_sending").style.display = "30%";
				}
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