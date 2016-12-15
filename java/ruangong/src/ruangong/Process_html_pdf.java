package ruangong;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.UnsupportedEncodingException;
import java.lang.management.ManagementFactory;

import javax.sound.sampled.AudioFormat.Encoding;


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
	public void run(){
		if(filepath==null||url==null||e==0){
			System.out.println("error!wrong fileinfo!\n");
		}else{
			File temp=new File(filepath);
			if(temp.exists()){
				csharppart.process(filepath, url, e);
				try {
					Thread.sleep(10);
				} catch (InterruptedException e1) {
					// TODO Auto-generated catch block
					e1.printStackTrace();
				}
			}
			else{
				System.out.println("error!no such file!\n");
				return ;
			}
			
		}
	}
	public String getAuthor(){
		File in=new File("temp_html_and_pdf_"+id+"_Author.txt");
		if(in.exists()){
			String s="";
			InputStreamReader read;
			String lineTxt="未初始化的作者，如看到此句说明程序未处理文件或者未在文件中找到作者信息";
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
			}//考虑到编码格式
			
			return s;
		}
		return "未匹配到作者，可能程序没有开始处理文件";
	}
	public String getTitle(){
		File in=new File("temp_html_and_pdf_"+id+"_Title.txt");
		if(in.exists()){
			String s="";
			InputStreamReader read;
			String lineTxt="未初始化的标题，如看到此句说明程序未处理文件或者未在文件中找到标题信息";
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
			}//考虑到编码格式
			
			return s;
		}
		return "未匹配到标题，可能程序没有开始处理文件";
	}
	public String getAnswer(){
		File in=new File("temp_html_and_pdf_"+id+"_Answer.txt");
		if(in.exists()){
			String s="";
			InputStreamReader read;
			String lineTxt="未初始化的答案，如看到此句说明程序未处理文件或者未在文件中找到答案信息";
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
			}//考虑到编码格式
			
			return s;
		}
		return "未匹配到答案，可能程序没有开始处理文件";
	}
	public String getQuestion(){
		File in=new File("temp_html_and_pdf_"+id+"_Question.txt");
		if(in.exists()){
			String s="";
			InputStreamReader read;
			String lineTxt="未初始化的问题，如看到此句说明程序未处理文件或者未在文件中找到问题信息";
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
			}//考虑到编码格式
			
			return s;
		}
		return "未匹配到问题，可能程序没有开始处理文件";
	}
	public String getDate(){
		File in=new File("temp_html_and_pdf_"+id+"_Date.txt");
		String s="";
		if(in.exists()){
			InputStreamReader read;
			String lineTxt="未初始化的日期，如看到此句说明程序未处理文件或者未在文件中找到日期信息";
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
			}//考虑到编码格式
			
			return s;
		}
		return "未匹配到日期，可能程序没有开始处理文件";
	}
}
