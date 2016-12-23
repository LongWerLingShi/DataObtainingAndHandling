import java.util.HashMap;
import java.util.Map;

public class ProcessThread implements Runnable{
	private String URL;
	private int threadNo;
	private DistributionThread distributionThread;
	private databaseOperation dO;
	public ProcessThread()
	{
		dO = new databaseOperationClass();
	}
	public void run()
	{
		distributionThread.hs[threadNo] = "Getting";
		//�̴߳�����
		String suffix = URL.substring(URL.lastIndexOf(".")+1);
		Map<String, Object> map = new HashMap<String,Object>();;
		if(suffix.equals("doc") || suffix.equals("docx"))
			map = dO.handleWord(URL,threadNo);
		else if(suffix.equals("pdf"))
			;
		else if(suffix.equals("html"))
			;
		else if(suffix.equals("png") || suffix.equals("jpg") || suffix.equals("bmp"))
			;
		distributionThread.hs[threadNo] = "Dealing";
		//�������ϴ���solr
		dO.insert(map);
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
	}
	public void setURL(String URL)//����URL
	{
		this.URL = URL;
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
		return URL;
	}
}
