import java.text.SimpleDateFormat;
import java.util.Date;

public class DataProcess {
	private static core c;
	private static databaseOperation dO;
	private static int threadNumber;
	private static Date sDate;
	private static Date eDate;
	
	public static void main(String args[]) 
	{
		String file1 = "control.json";
		String file2 = "state.json";
		GetFileInfo gf = new GetFileInfo();
		sDate = new Date();
		if(gf.getInfo(file1))
		{
			System.out.println("DataProcessing Begining!");
			c = new core();
			c.start();
			threadNumber  = c.getThreadNumber();
			do {
				gf.setInfo(file2,setString("open"));
			} while (gf.getInfo(file1));
			int finish;
			do
			{
				finish = c.end();
			}while(finish != 0);
			gf.setInfo(file2,setString("close"));
			System.out.println("DataProcessing finished!");
		}
	}
	public static String getSeconds(Date startdate, Date enddate) {
        //long time = enddate.getTime() - startdate.getTime();
        //int totalS = new Long(time / 1000).intValue();
        //Date date =new Date(totalS);
        SimpleDateFormat sdf=new SimpleDateFormat("yyyy-MM-dd");//设置转化格式
        String timeString=sdf.format(startdate);//将Date对象转化为yyyy-MM-dd形式的字符串
        return timeString;
    }
	public static String setString(String s)
	{
		eDate  = new Date();
		dO =  new databaseOperationClass();
		String string = "";
		string = "{\n\t\"openState\":\""+s+"\","
				+ "\n\t\"threadNumber\":\""+String.valueOf(threadNumber)+"\","
						+ "\n\t\"thread\":"
						+ "\n\t[";
		for(int i = 0;i < threadNumber;i++)
		{
			string += "\n\t\t{\"URL\":\""+c.getActiveURL(i) +"\",\"schedule\":\""+c.getState(i) +"\"}";
			if(i != threadNumber-1)
			{
				string += ",";
			}
		}
		string += "\n\t],";
		string += "\n\t\"htmlTitle\":\""+String.valueOf(dO.queryTitle("10.2.28.78","crawler","aimashi2015", "html"))+"\",";
		string += "\n\t\"htmlHaveDone\":\""+String.valueOf(dO.queryHaveDone("10.2.28.78","crawler","aimashi2015", "html"))+"\",";
		string += "\n\t\"pdfTitle\":\""+String.valueOf(dO.queryTitle("10.2.28.78","crawler","aimashi2015", "pdf"))+"\",";
		string += "\n\t\"pdfHaveDone\":\""+String.valueOf(dO.queryHaveDone("10.2.28.78","crawler","aimashi2015", "pdf"))+"\",";
		string += "\n\t\"wordTitle\":\""+String.valueOf(dO.queryTitle("10.2.28.78","crawler","aimashi2015", "word"))+"\",";
		string += "\n\t\"wordHaveDone\":\""+String.valueOf(dO.queryHaveDone("10.2.28.78","crawler","aimashi2015", "word"))+"\",";
		string += "\n\t\"pictureTitle\":\""+String.valueOf(dO.queryTitle("10.2.28.78","crawler","aimashi2015", "picture"))+"\",";
		string += "\n\t\"pictureHaveDone\":\""+String.valueOf(dO.queryHaveDone("10.2.28.78","crawler","aimashi2015", "picture"))+"\",";
		
		string += "\n\t\"fileTitle\":\""+String.valueOf(dO.queryTitle("10.2.28.78","crawler","aimashi2015"))+"\",";
		string += "\n\t\"fileHaveDone\":\""+String.valueOf(dO.queryHaveDone("10.2.28.78","crawler","aimashi2015"))+"\",";
		string += "\n\t\"fileHandleFail\":\""+String.valueOf(dO.queryHandleFail("10.2.28.78","crawler","aimashi2015"))+"\",";
		string += "\n\t\"handleTime\":\""+getSeconds(sDate, eDate)+"\"";
		string +="\n}";
		return string;
	}
}
