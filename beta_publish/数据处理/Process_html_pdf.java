import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.io.UnsupportedEncodingException;
import java.lang.management.ManagementFactory;
import java.util.HashMap;
import java.util.Map;
public class Process_html_pdf {
	private String filepath;
	private String url;
	private int e;
	private process_html_and_pdf csharppart;
	String path =System.getProperty("user.dir");
	
	String id;
	public Process_html_pdf(String filepath,String url,int e){
		this.filepath=filepath;
		this.url=url;
		this.e=e;
		csharppart=new process_html_and_pdf();
		
		String name = ManagementFactory.getRuntimeMXBean().getName();    
		// get pid    
		id = name.split("@")[0];   
		
	}
	public Map<String, Object> run(){
		Map<String, Object> map = new HashMap<String,Object>();
		if(filepath==null||url==null||e==0){
			System.out.println("error!wrong fileinfo!\n");
			return map;
		}else{
			File temp=new File(filepath);
			if(temp.exists()){
				csharppart.process(filepath, url, e);
			}
			else{
				System.out.println("error!no such file!\n");
				return map;
			}
			String title=getTitle();
			String links=url;
			String date=getDate();
			String content=getContent(id,0);
			String author=getAuthor();
			String doc_type;
			if(getisqa()){
				doc_type="Q&A";
			}else{
				String[] token = filepath.split("\\.");
				//System.out.println(filepath);
				
				String pf = token[1];
				
				if(pf.equals("html"))
					doc_type="html";
				else
					doc_type="pdf";
			}
			String question_content=getQuestion();
			String[] answers=getAnswer().split("201612212250328");
			String[] keywords=getKey(id,0).split("\\s+");
			
			map.put("title",title);
			map.put("links", links);
			map.put("date", date);
			map.put("content",content);
			map.put("keywords", keywords);
			map.put("author", author);
			map.put("doc_type", doc_type);
			map.put("question_content",question_content);
			map.put("answers", answers);
			return map;
			
		}
	}
	private String getAuthor(){
		File in=new File("temp_html_and_pdf_"+id+"_Author.txt");
		if(in.exists()){
			String s="";
			InputStreamReader read;
			String lineTxt="";
			try {
				read = new InputStreamReader(
						new FileInputStream(in),"UTF-8");
				try {
	            	BufferedReader bufferedReader = new BufferedReader(read);
	            	lineTxt= "";
					while((lineTxt = bufferedReader.readLine()) != null){
					    s+=lineTxt;
					}
					read.close();
	            } catch (Exception e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
			} catch (UnsupportedEncodingException | FileNotFoundException e) {
				// TODO Auto-generated catch block
				
				e.printStackTrace();
			}//¿¼ÂÇµ½±àÂë¸ñÊ½
			in.delete();
			return s;
		}
		return "";
	}
	private String getTitle(){
		File in=new File("temp_html_and_pdf_"+id+"_Title.txt");
		if(in.exists()){
			String s="";
			InputStreamReader read;
			String lineTxt="";
			try {
				read = new InputStreamReader(
						new FileInputStream(in),"UTF-8");
				try {
	            	BufferedReader bufferedReader = new BufferedReader(read);
	            	lineTxt= "";
					while((lineTxt = bufferedReader.readLine()) != null){
					    s+=lineTxt;
					}
					read.close();
	            } catch (Exception e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
			} catch (UnsupportedEncodingException | FileNotFoundException e) {
				// TODO Auto-generated catch block
				
				e.printStackTrace();
			}//¿¼ÂÇµ½±àÂë¸ñÊ½
			in.delete();
			return s;
		}
		return "";
	}
	public String getKey(String id,int mode){
		if(mode == 1)
		{
			return getContent(id, 3);
		}
		File in=new File("temp_html_and_pdf_"+id+"_Key.txt");
		if(in.exists()){
			String s="";
			InputStreamReader read;
			String lineTxt="";
			try {
				read = new InputStreamReader( 
						new FileInputStream(in),"UTF-8");
				try {
	            	BufferedReader bufferedReader = new BufferedReader(read);
	            	lineTxt= "";
					while((lineTxt = bufferedReader.readLine()) != null){
					    s+=lineTxt;
					}
					read.close();
	            } catch (Exception e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
			} catch (UnsupportedEncodingException | FileNotFoundException e) {
				// TODO Auto-generated catch block
				
				e.printStackTrace();
			}//¿¼ÂÇµ½±àÂë¸ñÊ½
			String data=getContent(id,0);
			File file =new File("temp"+id+".txt");
			try{
				if(!file.exists()){
					file.createNewFile();
				}
				OutputStreamWriter writter = new OutputStreamWriter(new FileOutputStream(file),"UTF-8");
	            BufferedWriter bufferWritter = new BufferedWriter(writter);
	            if(data==null)
	            	data="";
	            bufferWritter.write(data);
	            bufferWritter.close();
			}catch(SecurityException|IOException e){
				e.printStackTrace();
			}
			TestJNI pw = new TestJNI(); 
			pw.cutwords(Integer.parseInt(id), "Lucene.China.ChineseAnalyzer");
			pw.key("CorpusWordlist.xls", Integer.parseInt(id));
	        pw.translate(Integer.parseInt(id));
			try {
				read = new InputStreamReader( 
						new FileInputStream(file),"UTF-8");
				try {
	            	BufferedReader bufferedReader = new BufferedReader(read);
	            	lineTxt= "";
					while((lineTxt = bufferedReader.readLine()) != null){
					    s+=lineTxt;
					}
					read.close();
	            } catch (Exception e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
			} catch (UnsupportedEncodingException | FileNotFoundException e1) {
				// TODO Auto-generated catch block
				e1.printStackTrace();
			}
			in.delete();
			return s;
		}
		return "";
	}
	private String getAnswer(){
		File in=new File("temp_html_and_pdf_"+id+"_Answer.txt");
		if(in.exists()){
			String s="";
			InputStreamReader read;
			String lineTxt="";
			try {
				read = new InputStreamReader(
						new FileInputStream(in),"UTF-8");
				try {
	            	BufferedReader bufferedReader = new BufferedReader(read);
	            	lineTxt= "";
					while((lineTxt = bufferedReader.readLine()) != null){
					    s+=lineTxt;
					}
					read.close();
	            } catch (Exception e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
			} catch (UnsupportedEncodingException | FileNotFoundException e) {
				// TODO Auto-generated catch block
				
				e.printStackTrace();
			}//¿¼ÂÇµ½±àÂë¸ñÊ½
			in.delete();
			return s;
		}
		return "";
	}
	private String getQuestion(){
		File in=new File("temp_html_and_pdf_"+id+"_Question.txt");
		if(in.exists()){
			String s="";
			InputStreamReader read;
			String lineTxt="";
			try {
				read = new InputStreamReader(
						new FileInputStream(in),"UTF-8");
				try {
	            	BufferedReader bufferedReader = new BufferedReader(read);
	            	lineTxt= "";
					while((lineTxt = bufferedReader.readLine()) != null){
					    s+=lineTxt;
					}
					read.close();
	            } catch (Exception e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
			} catch (UnsupportedEncodingException | FileNotFoundException e) {
				// TODO Auto-generated catch block
				
				e.printStackTrace();
			}//¿¼ÂÇµ½±àÂë¸ñÊ½
			in.delete();
			return s;
		}
		return "";
	}
	private String getDate(){
		File in=new File("temp_html_and_pdf_"+id+"_Date.txt");
		String s="";
		if(in.exists()){
			InputStreamReader read;
			String lineTxt="";
			try {
				read = new InputStreamReader(
						new FileInputStream(in),"UTF-8");
				try {
	            	BufferedReader bufferedReader = new BufferedReader(read);
	            	lineTxt= "";
					while((lineTxt = bufferedReader.readLine()) != null){
					    s+=lineTxt;
					}
					read.close();
	            } catch (Exception e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
			} catch (UnsupportedEncodingException | FileNotFoundException e) {
				// TODO Auto-generated catch block
				
				e.printStackTrace();
			}//¿¼ÂÇµ½±àÂë¸ñÊ½
			in.delete();
			return s;
		}
		return "";
	}
	public String getContent(String id,int mode){
		File in;
		if(mode == 0)
			in=new File("temp_html_and_pdf_"+id+"_Content.txt");
		else if(mode == 1)
			in=new File("content"+id+".txt");
		else 
			in = new File("temp"+id+".txt");
		String s="";
		if(in.exists()){
			InputStreamReader read;
			String lineTxt="";
			try {
				read = new InputStreamReader(
						new FileInputStream(in),"UTF-8");
				try {
	            	BufferedReader bufferedReader = new BufferedReader(read);
	            	lineTxt= "";
					while((lineTxt = bufferedReader.readLine()) != null){
					    s+=lineTxt;
					}
					read.close();
	            } catch (Exception e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
			} catch (UnsupportedEncodingException | FileNotFoundException e) {
				// TODO Auto-generated catch block
				
				e.printStackTrace();
			}//¿¼ÂÇµ½±àÂë¸ñÊ½
			//in.delete();
			//System.out.println("233"+s);
			s = s.replaceAll("\\\\", "\\\\\\\\");
			s = s.replaceAll("'", "''");
			return s;
		}
		return "";
	}
	private boolean getisqa(){
		File in=new File("temp_html_and_pdf_"+id+"_isqa.txt");
		String s="";
		if(in.exists()){
			InputStreamReader read;
			String lineTxt="Î´³õÊ¼»¯µÄÎÊ´ðÐÅÏ¢";
			try {
				read = new InputStreamReader(
						new FileInputStream(in),"UTF-8");
				try {
	            	BufferedReader bufferedReader = new BufferedReader(read);
	            	lineTxt= "";
					while((lineTxt = bufferedReader.readLine()) != null){
					    s+=lineTxt;
					}
					read.close();
	            } catch (Exception e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
			} catch (UnsupportedEncodingException | FileNotFoundException e) {
				// TODO Auto-generated catch block
				
				e.printStackTrace();
			}//¿¼ÂÇµ½±àÂë¸ñÊ½
			in.delete();
			int i;
			for(i=0;i<s.length();i++){
				if(s.charAt(i)=='1')
					return true;
			}
			return false;
		}
		return false;
	}

}
