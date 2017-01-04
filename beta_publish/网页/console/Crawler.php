<?php
function getState()//获取数据处理程序运行状态
{
	//$f=new Com("VisitRemoteServer.NetworkConnection");
	//$r = $f->getFromNetWorkConnection("\\\\10.2.28.78\\XueBaResources","E:\\\\AppServ\\www\\datahandler2\\console", "Crawler", "Ase12345678","state.json","state2.json");
	$json_string = "";
	//if($r == true)
	{
		$json_string = file_get_contents('state2.json');
	}
	//$data = json_decode($json_string,true);
	return $json_string;
}
function setState($q)
{
	file_put_contents('control2.json', $q);
	//system('java -jar DataProcess.jar');
	//$f=new Com("VisitRemoteServer.NetworkConnection");
	//$r = $f->setFromNetWorkConnection("\\\\10.2.28.78\\XueBaResources","E:\\\\AppServ\\www\\datahandler2\\console", "Crawler", "Ase12345678","control.json","control2.json");
	$json_string = "";
	//if($r == true)
	{
		$json_string = file_get_contents('control2.json');
	}
	//$data = json_decode($json_string,true);
	return $json_string;
}
?>
