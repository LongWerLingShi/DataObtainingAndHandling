public class core {
	private Console con;
	private Thread console;
	public core()
	{
		con = new Console();
	}
	public boolean start()//��ʼ��̨����
	{
		GetFileInfo gf = new GetFileInfo();
		String file1 = "control.json";
		if(gf.getInfo2(file1))
		{
			databaseOperation dO = new databaseOperationClass();
			dO.cleardealedtag("10.2.28.78","crawler","aimashi2015");
		}
		console = new Thread(con);
		console.start();
		return true;
	}
    public int end()//�رպ�̨����
    {
    	return con.end();
    }
    public int activeThread()
    {
    	return con.activeThread();
    }
    public int getThreadNumber()
    {
    	return con.getThreadNumber();
    }
    public String getActiveURL(int i)
    {
    	return con.getActiveURL(i);
    }
    public String getState(int i)
    {
    	return con.getState(i);
    }
}
