import java.util.HashMap;
import java.util.Map;

public class ProcessThread implements Runnable{
	private String URL;
	private int id;
	private String WEB;
	private int threadNo;
	private DistributionThread distributionThread;
	private databaseOperation dO;
	public ProcessThread()
	{
		dO = new databaseOperationClass();
	}
	public void run()
	{
		try
		{
			System.out.println(WEB);
			//System.out.println(2222222);
			distributionThread.hs[threadNo] = "Getting";
			//线程处理函数
			String suffix = URL.substring(URL.lastIndexOf(".")+1);
			Map<String, Object> map = new HashMap<String,Object>();
			//System.out.println(suffix);
			if(suffix.equals("doc") || suffix.equals("docx"))
				map = dO.handleWord(URL,WEB,threadNo);
			else if(suffix.equals("pdf"))
				map = dO.handlePdfOrHTML(URL, WEB);
			else if(suffix.equals("html"))
				map = dO.handlePdfOrHTML(URL, WEB);
			else if(suffix.equals("png") || suffix.equals("jpg") || suffix.equals("bmp"))
				;
			map.put("id", id);
			//System.out.println(2333333);
			distributionThread.hs[threadNo] = "Dealing";
			//处理完上传到solr
			dO.update(id,map,"10.2.28.78","crawler","aimashi2015");
			//System.out.println(44444444);
			dO.insert(map);
			//System.out.println(5555555);
			distributionThread.hs[threadNo] = "Sending";
			try {
				Thread.sleep(2000);
			} catch (InterruptedException e) {
				// TODO Auto-generated catch block
				System.out.println("44");
			}
			distributionThread.hs[threadNo] = "Waiting";
			try {
				Thread.sleep(2000);
			} catch (InterruptedException e) {
				// TODO Auto-generated catch block
				System.out.println("44");
			}
			//dO.DeleteFileFromLocal(URL);
		}
		catch (Exception e) {
			e.printStackTrace();
		}
	}
	public void setURL(String URL)
	{
		this.URL = URL;
	}
	public void setId(int id)
	{
		this.id = id;
		this.WEB = dO.querybyid(this.id, "10.2.28.78","crawler","aimashi2015");
	}
	public void setNo(int threadNo)
	{
		this.threadNo = threadNo;
	}
	public void setDt(DistributionThread distributionThread)
	{
		this.distributionThread = distributionThread;
	}
	public String getURL()
	{
		return WEB;
	}
}
