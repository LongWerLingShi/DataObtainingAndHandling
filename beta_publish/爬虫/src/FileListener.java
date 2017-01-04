import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.FileReader;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.Calendar;
import java.util.LinkedList;
//import java.util.List;

import net.sf.json.JSONArray;
import net.sf.json.JSONObject;
import net.sf.json.util.JSONStringer;
//import net.sf.json.util.JSONTokener;

public class FileListener extends Thread{
	
	boolean ifRunning;
	boolean ifClosing;
	Controller cont;
	LinkedList <String> listURLs;
	LinkedList <String> listkeyWords;
	int toBeNum;
	String startTime = "";
	public FileListener(){
		this.ifRunning = false;
		this.ifClosing = false;
		this.listURLs = new LinkedList<String>();
		this.listkeyWords = new LinkedList<String>();
	}
	public void run(){

        //
        long lastModified = 0;
        while(true){
        	String str = "";
        	try {
				Thread.sleep(1000);
			} catch (InterruptedException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
        	System.out.println("Next Round");
        	File fileControl = new File("E:\\AppServ\\www\\datahandler2\\console\\control2.json");
        	if(lastModified != fileControl.lastModified()){
        		lastModified = fileControl.lastModified();
        		System.out.println("Trigger changed : control.json");
        		
        		try {
        			FileReader fl = new FileReader(fileControl);
        			int a;
        			while((a=fl.read())!=-1){
        				str+=(char)a;
        			}
        			
        			fl.close();
        		} catch (FileNotFoundException e) {
        			// TODO Auto-generated catch block
        			e.printStackTrace();
        		} catch (IOException e) {
        			// TODO Auto-generated catch block
        			e.printStackTrace();
        		}
        		JSONObject ob = new JSONObject();
        		ob = JSONObject.fromObject(str);

        		toBeNum = ob.getInt("threadNumber");
        		if(toBeNum != 0 && ifRunning == true && ifClosing == false){ // 正在运行 重新载入参数
        			
        			System.out.println(toBeNum + ob.getJSONArray("keyWord").getJSONObject(0).getString("word"));
        		}
        		else if(toBeNum != 0 && ifRunning == false){ // 重新启动
        			Calendar now = Calendar.getInstance();
        			this.startTime = now.get(Calendar.YEAR) + "-"+now.get(Calendar.MONTH) + "-"+now.get(Calendar.DATE) + "-"+
        					now.get(Calendar.HOUR) + "-"+now.get(Calendar.MINUTE) + "-"+now.get(Calendar.SECOND);
        			System.out.println(this.startTime);
        			this.listURLs.clear();
        			JSONArray array = ob.getJSONArray("web");
        			cont = new Controller();
        			for(int i =0;i<array.size();i++){
        				//System.out.println("ADD: "+array.getJSONObject(i).getString("URL"));
        				cont.addSeed(array.getJSONObject(i).getString("URL"));
        				this.listURLs.add(array.getJSONObject(i).getString("URL")); //同时记录到list里面 用来写入state文件
        			}
        			ifRunning = true;
        			try {
						cont.setNumOfCrawler(toBeNum);
						
					} catch (Exception e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}
        		}
        		else if(toBeNum != 0 && ifRunning == true && ifClosing == false){
        			System.out.println("Warning: the thread is closing!");
        		}
        		else if(toBeNum == 0 && ifClosing == false){
        			System.out.println("Trigger changed : stop");
        			
        			this.ifClosing = true;
        			try {
						this.cont.setNumOfCrawler(0);
					} catch (Exception e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}
        			try {
						Thread.sleep(25000);
					} catch (InterruptedException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}
        			this.ifClosing = false;
        			this.ifRunning = false;
        		}else if(toBeNum == 0 && ifClosing == true){
        			System.out.println("Warning: the thread is closing!");
        		}

        		
        	}
        	//以下这部分是用来写state.json
        	JSONStringer js = new JSONStringer();  
        	String objState = "";  
        	String objStart = "";  
            int objTot;
            int objHtml;
            int objPdf;
            int objDoc;
            int objPic;
            int objCraw;
        	if(this.ifRunning == true && this.ifClosing == false){
        		objState="working";
        		objStart= this.startTime;
        		objTot =  cont.getTotNum();
        		objHtml =  cont.getHtmlNum();
        		objPdf = cont.getPdfNum();
        		objDoc = cont.getDocNum();
        		objPic = cont.getImageNum();
        		objCraw = this.toBeNum;
        	}
        	else{
        		if(this.ifClosing){
        			objState="closing";
        		}else{
        			objState="waiting";
        		}
        		objStart="";
        		objTot =  0;
        		objHtml =  0;
        		objPdf = 0;
        		objDoc = 0;
        		objPic = 0;
        		objCraw =0;
        	}
            
        	JSONArray objURL = new JSONArray();  
            for(int i=0;i<this.listURLs.size();i++){
            	JSONObject hi = new JSONObject();
            	hi.put("URL", this.listURLs.get(i));
            	objURL.add(hi);
            }
            
            JSONArray objKeyWord = new JSONArray();  
            for(int i=0;i<this.listkeyWords.size();i++){
            	JSONObject hi = new JSONObject();
            	hi.put("URL", this.listkeyWords.get(i));
            	objKeyWord.add(hi);
            }
              
            js.object().key("state").value(objState);
            js.key("startTime").value(objStart);
            js.key("fileNumber").value(objTot);
            js.key("htmlNumber").value(objHtml);
            js.key("pdfNumber").value(objPdf);
            js.key("docNumber").value(objDoc);
            js.key("pictureNumber").value(objPic);
            js.key("crawlerNumber").value(objCraw);
            js.key("web").value(objURL);
            js.key("keyWord").value(objKeyWord);
            js.endObject();  
              
            //System.out.println(js.toString());  
              
            try {
				PrintWriter out = new PrintWriter(new FileOutputStream("E:\\AppServ\\www\\datahandler2\\console\\state2.json"));
				out.println(js.toString());  
				out.close();
			} catch (FileNotFoundException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}  
            
        }
	}
}
