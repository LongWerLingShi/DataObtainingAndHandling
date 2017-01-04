public class DistributionThread {
	final private int threadNumber = 1;//����߳���
	private Thread[] t = new Thread[threadNumber];
	private ProcessThread[] p = new ProcessThread[threadNumber];
	public String[] hs = new String[threadNumber];//HandleState
	public DistributionThread()
	{
		init();
	}
	private void init()//��ʼ���߳�
	{
		for(int i = 0;i < threadNumber;i++)
		{
			p[i] = new ProcessThread();
			t[i] = new Thread(p[i]);
		}
	}
	public String getActiveURL(int i)
	{
		if(t[i].isAlive())
		{
			return p[i].getURL();
		}
		return "null";
	}
	public String getState(int i)
	{
		if(t[i].isAlive());
		else
		{
			hs[i] = "Waiting";
		}
		return hs[i];
	}
	public int getThreadNumber()
	{
		return threadNumber;
	}
	public int findEmptyThread()//Ѱ��һ�����߳�
	{
		for(int i = 0;i < threadNumber;i++)
		{
			if(!t[i].isAlive())
			{
				return i;
			}
		}
		return -1;
	}
	public int isHaveRunThread()//�ж��Ƿ����߳�û�н���
	{
		int aliveThreadNumber = 0;
		for(int i = 0;i < threadNumber;i++)
		{
			if(t[i].isAlive())
			{
				aliveThreadNumber++;
			}
		}
		return aliveThreadNumber;
	}
	public void startThread(int id,int i,String URL)//����һ���߳�
	{
		p[i].setId(id);
		p[i].setURL(URL);
		p[i].setNo(i);
		p[i].setDt(this);
		t[i] = new Thread(p[i]);
		t[i].start();
	}
}
