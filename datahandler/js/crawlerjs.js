var CRAWLER = {};

CRAWLER.start = function () {
	var status = document.getElementById('crawler_status');
	status.innerText = '爬取中...';
}

CRAWLER.stop = function () {
	var status = document.getElementById('crawler_status');
	status.innerText = '停止'
}

