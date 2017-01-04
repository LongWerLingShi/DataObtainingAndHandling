<?php
include_once('Crawler.php');
$q=$_GET["q"];
$response = setState($q);
echo $response;
?>
