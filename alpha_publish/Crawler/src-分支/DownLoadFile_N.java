import java.io.ByteArrayOutputStream;
import java.io.DataOutputStream;
import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.Set;

import javax.swing.JProgressBar;
import javax.swing.table.DefaultTableModel;

import org.apache.commons.httpclient.DefaultHttpMethodRetryHandler;
import org.apache.commons.httpclient.HttpClient;
import org.apache.commons.httpclient.HttpException;
import org.apache.commons.httpclient.HttpStatus;
import org.apache.commons.httpclient.cookie.CookiePolicy;
import org.apache.commons.httpclient.methods.GetMethod;
import org.apache.commons.httpclient.params.HttpMethodParams;

public class DownLoadFile_N extends Thread{
	private ConnectServer connectserver;
	private String url;
	private LinkFilter linkFilter;
	private KeywordFilter keywordFilter;
	private HttpClient httpClient;
	private GetMethod getMethod;
	private String contentTypeStr;
	private String contentType;
	private boolean htmlFlag, pdfFlag, pptFlag, docFlag;
	private int id;
	private String filePath;
	private String encode;
	private String pageType;
	private String lastCrawlerTime = "null";
	private String freshTime = "null";
	private String keywords;
	private String tag = "null";//第二组用
	private String host;
	private String use = "0";//第二组用
	private JProgressBar progressBar;
	
	public DownLoadFile_N(JProgressBar progressBar, ConnectServer connectserver, String url, LinkFilter linkFilter, KeywordFilter keywordFilter, boolean htmlFlag, boolean pdfFlag, boolean pptFlag, boolean docFlag){
		this.progressBar = progressBar;
		this.connectserver = connectserver;
		this.url = url;
		this.linkFilter = linkFilter;
		this.keywordFilter = keywordFilter;
		this.htmlFlag = htmlFlag;
		this.pdfFlag = pdfFlag;
		this.pptFlag = pptFlag;
		this.docFlag = docFlag;
	}
	
	public void run(){
		if(MyCrawler.succeed >= MyCrawler.pages){
			System.out.println("complete");
			MyCrawler.dtmHistory.addRow(new Object[]{MyCrawler.dtmSeeds.getValueAt(0, 0), MyCrawler.succeed, MyCrawler.failed, MyCrawler.pages});
			MyCrawler.dtmSeeds.removeRow(0);
			MyCrawler.completeCount++;
			MyCrawler.completeFlag = true;
		}
		httpClient = new HttpClient();
		httpClient.getHttpConnectionManager().getParams().setConnectionTimeout(5000);
		httpClient.getHttpConnectionManager().getParams().setSoTimeout(5000); 
		httpClient.getParams().setCookiePolicy(CookiePolicy.IGNORE_COOKIES);
		getMethod = new GetMethod(url);
		getMethod.getParams().setParameter(HttpMethodParams.SO_TIMEOUT, 5000);
		getMethod.getParams().setParameter(HttpMethodParams.RETRY_HANDLER, new DefaultHttpMethodRetryHandler());
		try {
			int statusCode = httpClient.executeMethod(getMethod);
			if(statusCode != HttpStatus.SC_OK){
				MyCrawler.failed++;
				return;
			}
			if(url.length() > 255){
				return;
			}
			else{
				getContentType();
				if((contentType.equals("pdf") && pdfFlag) 
						|| (contentType.equals("ppt") && pptFlag) 
						|| (contentType.equals("doc") && docFlag)){
					download();
					updateDB();
					MyCrawler.succeed++;
					progressBar.setValue(MyCrawler.succeed);
				}
				else if(contentType.equals("html")){
					if(htmlFlag){
						download();
						getChildLinks();
						updateDB();
						MyCrawler.succeed++;
						progressBar.setValue(MyCrawler.succeed);
					}
					else{
						getChildLinks();
						return;
					}
				}
				else{
					return;
				}
			}
		} catch (HttpException e) {
			System.out.println("failed connected:" + url);
		} catch (IOException e) {
			System.out.println(e);
		}
	}
	
	public void updateDB(){
		String values = "'" + id + "','" + url + "','" + filePath + "','" + encode + 
						"','" + pageType + "','" + lastCrawlerTime + "','" + freshTime +
						"','" + keywords + "','" + tag + "','" + host + "','" + use + "'";
		connectserver.execute("INSERT INTO fileinfo VALUES(" + values + ")");
	}
	
	public void getChildLinks(){
		Set<String> links = HtmlParserTool_N.extracLinks(url,linkFilter,keywordFilter, htmlFlag);
		for(String link : links){
			synchronized(LinkQueue.unVisitedUrl){
				LinkQueue.addUnvisitedUrl(link);
			}
		}
	}
	
	public void getContentType(){
		contentTypeStr = getMethod.getResponseHeader("Content-Type").getValue();
		if(contentTypeStr.contains("application/pdf")){
			contentType = "pdf";
		}
		else if(contentTypeStr.contains("applications-powerpoint") || contentTypeStr.contains("application/x-ppt") 
				|| contentTypeStr.contains("application/vnd.openxmlformats-officedocument.presentationml.presentation")){
			contentType = "ppt";
		}
		else if(contentTypeStr.contains("application/msword") 
				|| contentTypeStr.contains("application/vnd.openxmlformats-officedocument.wordprocessingml.document")){
			contentType = "doc";
		}
		else if(contentTypeStr.contains("text/html")){
			contentType = "html";
		}
		else{
			contentType = "";
		}
	}

	public void download(){
		try {
			ResultSet rs = null;
			synchronized(connectserver){
				rs = connectserver.getResultSet("SELECT COUNT(c_id) FROM fileinfo");
			}
			rs.next();
			id = rs.getInt(1);
			InputStream temp = getMethod.getResponseBodyAsStream();  
            ByteArrayOutputStream bAOut = new ByteArrayOutputStream();  
            int c;  
            while ((c = temp.read()) != -1) {  
                bAOut.write(c);  
            }  
            byte[] responseBody = bAOut.toByteArray();
			filePath = "D:\\XueBaResources\\" + getFileNameByID(id);
			saveToLocal(responseBody, filePath);
		} catch (SQLException e) {
			e.printStackTrace();
		} catch (IOException e) {
			e.printStackTrace();
		}
	}
	
	public String getFileNameByID(int id)
	{
		String res = String.valueOf(id);
		if(contentType.equals("html"))
		{
			res= res + ".html";
		}
		else if(contentType.equals("pdf"))
		{
			res= res + ".pdf";
		}
		else if(contentType.equals("ppt"))
		{
			res= res  + ".ppt";
		}
		else if(contentType.equals("doc")){
			res = res + ".doc";
		}
		return res;
	}
	
	private void saveToLocal(byte[] data, String filePath) {  
        try {  
            DataOutputStream out = new DataOutputStream(new FileOutputStream(new File(filePath)));  
            for (int i = 0; i < data.length; i++)  
                out.write(data[i]);
            out.flush();  
            out.close();  
        } catch (IOException e) {  
            e.printStackTrace();  
        }  
    }  
}
