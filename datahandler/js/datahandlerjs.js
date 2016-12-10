var DH = {};

DH.restart = function() {
	begin();
}

DH.parse = function() {
	var status = document.getElementById('dh_status');
	status.innerText = '暂停中...';
}

DH.Stop = function() {
	end();
}
