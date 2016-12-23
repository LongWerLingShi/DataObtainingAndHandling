import java.util.concurrent.atomic.AtomicBoolean;

public class Console implements Runnable{
	private DistributionThread dt;//分配线程
	private AtomicBoolean mark,single;//循环开关
	private databaseOperation dO;
	public Console()
	{
		mark = new AtomicBoolean(true);
		single  = new AtomicBoolean(false);
		dt = new DistributionThread();
		dO = new databaseOperationClass();
	}
	public int getThreadNumber()
	{
		return dt.getThreadNumber();
	}
	public String getActiveURL(int i)
	{
		return dt.getActiveURL(i);
	}
	public String getState(int i)
	{
		return dt.getState(i);
	}
	public void run()//后台主程序
	{
		mark.compareAndSet(false, true);
		single.compareAndSet(true, false);
		while(mark.compareAndSet(true, true))
		{
			//获取url
			String URL = dO.getnotdealed("10.2.28.78","crawler","aimashi2015");
			//System.out.println(URL);
			//try {
			//	Thread.sleep(200);
			//} catch (InterruptedException e) {
			//}
			int i = -1;
			while(i == -1)
			{
				i = dt.findEmptyThread();
				if(mark.compareAndSet(false, false))
				{
					break;
				}
			}
			if(mark.compareAndSet(false, false))
			{
				break;
			}
			dt.startThread(URL, i);
		}
		single.compareAndSet(false, true);
	}
	public int end()//关闭后台程序
	{
		mark.compareAndSet(true, false);
		return dt.isHaveRunThread();
	}
	public int activeThread()
	{
		return dt.isHaveRunThread();
	}
	
}
