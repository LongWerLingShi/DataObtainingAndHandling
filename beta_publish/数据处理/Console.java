//import java.io.File;
import java.util.concurrent.atomic.AtomicBoolean;

/*
 * 后台主程序，负责查找未处理文件，分配线程进行处理
 */
public class Console implements Runnable{
	private DistributionThread dt;//分配线程
	private AtomicBoolean mark,single;//循环开关
	private databaseOperation dO;
	private int ErrorNo;
	/*
	 * 构造函数
	 */
	public Console()
	{
		mark = new AtomicBoolean(true);
		single  = new AtomicBoolean(false);
		dt = new DistributionThread();
		dO = new databaseOperationClass();
		ErrorNo = 0;
	}
	/*
	 * 一些获取状态的函数
	 */
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
	public String getErrNo()
	{
		return String.valueOf(ErrorNo);
	}
	/*
	 * 主程序负责搜索未处理文件以及分配线程
	 * @see java.lang.Runnable#run()
	 */
	public void run()//后台主程序
	{
		mark.compareAndSet(false, true);
		single.compareAndSet(true, false);
		while(mark.compareAndSet(true, true))
		{
			//String ip = "10.2.28.78";
			//String username = "crawler";
			//String password = "aimashi2015";
			//String relativelyPath=System.getProperty("user.dir"); 
			//System.out.println(relativelyPath);
			//String localpath = relativelyPath+"\\datahandlerTempFile";
			//String localpath = "F:\\\\作业\\第三学期\\软件工程\\DataProcessing\\datahandlerTempFile";
			//String localpath = "C:\\\\Users\\Public";
			//File lp = new File(localpath);
			//if(!lp.exists())lp.mkdir();
			//System.out.println(localpath);
			int id = dO.getnotdealed("10.2.28.78","crawler","aimashi2015");
			//System.out.println(id);
			if(id == -1)continue;
			String URL = dO.queryURlbyid(id,"10.2.28.78","crawler","aimashi2015");
			//System.out.println(localpath);
			//String localfilename = dO.getFromNetWorkConnection(URL, "10.2.28.78", localpath, "Crawler", "Ase12345678");
			//System.out.println(id);
			//if(localfilename == null)
			//{
			//	ErrorNo++;
			//	continue;
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
			//dt.startThread(id, i,localpath+"\\"+localfilename);
			dt.startThread(id, i, URL);
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
