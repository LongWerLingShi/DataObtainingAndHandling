<?php

$hint = "";

function getState()//��ȡ���ݴ����������״̬
{
	$json_string = file_get_contents('state2.json');
	//$data = json_decode($json_string,true);
	return $json_string;
}
function restart()//�������ݴ������
{
	$json_string = file_get_contents('state.json');
	$data = json_decode($json_string,true);
	if($data["openState"] == 'close')
	{
		$data1 = array();
		$data1['allowOpen'] = 'true';
		$data1['allowClear'] = 'true';
		$json_string1 = json_encode($data1);
		file_put_contents('control.json', $json_string1);
		system('java -jar DataProcess.jar');
	}
	return $json_string;
}
function start()//�������ݴ������
{
	$json_string = file_get_contents('state.json');
	$data = json_decode($json_string,true);
	if($data["openState"] == 'close')
	{
		$data1 = array();
		$data1['allowOpen'] = 'true';
		$data1['allowClear'] = 'false';
		$json_string1 = json_encode($data1);
		file_put_contents('control.json', $json_string1);
		system('java -jar DataProcess.jar');
	}
	return $json_string;
}
function myEnd()//�ر����ݴ������
{
	$json_string = file_get_contents('state.json');
	$data = json_decode($json_string,true);
	//if($data["openState"] == 'open')
	{
		$data1 = array();
		$data1['allowOpen'] = 'false';
		$data1['allowClear'] = 'false';
		$json_string1 = json_encode($data1);
		file_put_contents('control.json', $json_string1);
	}
	return $json_string;
}

$q=$_GET["q"];

switch($q)
{
case 0:
	$hint = getState();
	break;
case 1:
	$hint = start();
	break;
case 2:
	$hint = myEnd();
	break;
case 3:
	$hint = restart();
	break;
default:
	;
}

$response=$hint;
echo $response;

?>
