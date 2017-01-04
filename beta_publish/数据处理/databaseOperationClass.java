import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.HashMap;
import java.util.Map;

public class databaseOperationClass implements databaseOperation{
	public int queryHandleFail(String ip,String username,String password)
	{
		//失败函数
		return 0;
	}
	/*
	 * 查询失败返回null
	 */
	public String querybyid(int id,String ip,String username,String password)
	{
		DBHelper db = new DBHelper(ip,"XueBa","Fileinfo",username,password);
		Map<String,Object> map = new HashMap<String,Object>();
		map.put("id", id);
		ResultSet rs = db.query(-1, map);
		String url = null;
		try {
			rs.next();
			url = rs.getString("url");
			if(rs != null) {
				rs.close();
			}
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		db.release();
		return url;
	}
	public String getFromNetWorkConnection(String url, String ip,String localpath, String username, String password)
	{
		int lPos = url.lastIndexOf("\\");
		if(lPos < 0 || lPos > url.length())return null;
		String remotepath = url.substring(2, lPos);
		if(lPos >= url.length()-1)return null;
		String remotefilename = url.substring(lPos+1,url.length());
		DownloadFile down = new DownloadFile();
		//System.out.println("\\\\"+ip+remotepath);
		//System.out.println(localpath);
		//System.out.println(username);
		//System.out.println(password);
		//System.out.println(remotefilename);
		//System.out.println(remotefilename);
		down.getFromNetWorkConnection("\\\\"+ip+remotepath, localpath, username, password, remotefilename, remotefilename);
		return remotefilename; 
	}
	public boolean DeleteFileFromLocal(String localpath, String localfilename)
	{
		DownloadFile down = new DownloadFile();
		down.DeleteFileFromLocal(localpath, localfilename);
		return true;
	}
	public boolean DeleteFileFromLocal(String localfullpath)
	{
		int lPos = localfullpath.lastIndexOf("\\");
		if(lPos < 0 || lPos > localfullpath.length())return false;
		String localpath = localfullpath.substring(0, lPos);
		if(lPos >= localfullpath.length()-1)return false;
		String localfilename = localfullpath.substring(lPos+1,localfullpath.length());
		return DeleteFileFromLocal(localpath, localfilename);
	}
	public String queryURlbyid(int id,String ip,String username,String password)
	{
		DBHelper db = new DBHelper(ip,"XueBa","Fileinfo",username,password);
		Map<String,Object> map = new HashMap<String,Object>();
		map.put("id", id);
		ResultSet rs = db.query(-1, map);
		String url = null;
		try {
			rs.next();
			url = rs.getString("filepath");
			if(rs != null) {
				rs.close();
			}
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		db.release();
		return url;
	}
	public Map<String, Object> handleWord(String URL,String WEB,int threadNo)
	{
		Map<String, Object> map = new HashMap<String,Object>();
		TestJNI.processWord(URL, threadNo);
		//System.out.println(122222);
        Process_html_pdf proc=new Process_html_pdf(URL,"Anything",1);
        //System.out.println(1333333);
        
        String[] keywords=proc.getKey(String.valueOf(threadNo),1).split("\\s+");
        //System.out.println(144444);
        String content=proc.getContent(String.valueOf(threadNo),1);
        //System.out.println(155555);
        map.put("title","");
		map.put("links", WEB);
		map.put("date", "");
		map.put("content",content);
		map.put("keywords", keywords);
		map.put("author", "");
		map.put("doc_type", "word");
		map.put("question_content","");
		map.put("answers", "");
		return map;
	}
	public Map<String, Object> handlePdfOrHTML(String URL,String WEB)
	{
		Process_html_pdf proc=new Process_html_pdf(URL,WEB,1);
		return proc.run();
	}
	public int queryTitle(String ip,String username,String password)
	{
		DBHelper db = new DBHelper(ip,"XueBa","Fileinfo",username,password);
		Map<String,Object> map = new HashMap<String,Object>();
		ResultSet rs = db.query(-1, map);//-1说明返回所有满足的条数，如果是正数n说明返回前n条
		int count;
		try {
			rs.last();//指向最后一条
			count = rs.getRow();//得到行数
			rs.first();//指向第一条(如果还要用rs的话)
			return count;
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		db.release();
		return 0;
		
	}
	public int queryHaveDone(String ip,String username,String password)
	{
		DBHelper db = new DBHelper(ip,"XueBa","Fileinfo",username,password);
		Map<String,Object> map = new HashMap<String,Object>();
		map.put("isDeal", 1);
		ResultSet rs = db.query(-1, map);//-1说明返回所有满足的条数，如果是正数n说明返回前n条
		int count;
		rs = db.query(-1, map);
		try {
			rs.last();
			count = rs.getRow();
			rs.first();
			return count;
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		db.release();
		return 0;
	}
	public int queryTitle(String ip,String username,String password,String type)
	{
		DBHelper db = new DBHelper(ip,"XueBa","Fileinfo",username,password);
		Map<String,Object> map = new HashMap<String,Object>();
		map.put("filetype", type);
		ResultSet rs = db.query(-1, map);//-1说明返回所有满足的条数，如果是正数n说明返回前n条
		int count;
		try {
			rs.last();//指向最后一条
			count = rs.getRow();//得到行数
			rs.first();//指向第一条(如果还要用rs的话)
			return count;
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		db.release();
		return 0;
		
	}
	public int queryHaveDone(String ip,String username,String password,String type)
	{
		DBHelper db = new DBHelper(ip,"XueBa","Fileinfo",username,password);
		Map<String,Object> map = new HashMap<String,Object>();
		map.put("filetype", type);
		map.put("isDeal", 1);
		ResultSet rs = db.query(-1, map);//-1说明返回所有满足的条数，如果是正数n说明返回前n条
		int count;
		rs = db.query(-1, map);
		try {
			rs.last();
			count = rs.getRow();
			rs.first();
			return count;
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		db.release();
		return 0;
	}
	
	public void cleardealedtag(String ip,String username,String password)
	{
		DBHelper db = new DBHelper(ip,"XueBa","Fileinfo",username,password);
		Map<String, Object> map = new HashMap<String,Object>();
		map.put("isDeal", 0);
		db.updateAll(map);
		db.release();
	}

	public int getnotdealed(String ip,String username,String password) {
		DBHelper db = new DBHelper(ip,"XueBa","Fileinfo",username,password);
		Map<String, Object> map = new HashMap<String, Object>();
		map.put("isDeal", 0);
		int id = -1;
		ResultSet rs = db.query(1, map);
		try {
			if(rs.next()) {	
				id = Integer.parseInt(rs.getString("id"));
				Map<String,Object> updatemap = new HashMap<String,Object>();
				updatemap.put("isDeal", 1);
				db.update(id, updatemap);	
			}
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}		
		db.release();
		return id;
	}
	public void insert(Map<String, Object> map)
	{
		SolrHelper sh = new SolrHelper("10.2.28.82","8080");
		String id = String.valueOf((int) map.get("id"));
		String title = (String)map.get("title");
		String links = (String)map.get("links");
		String date  = (String)map.get("date");
		String content =(String)map.get("content");
		String[] keywords = (String[])map.get("keywords");
		String author = (String)map.get("author");
		String doc_type = (String)map.get("doc_type");
		String question_content = (String)map.get("questoin_content");
		String[] answer_content = (String[])map.get("answer_content");
		
		sh.insert(id, title, links, date, content, keywords, author, doc_type, question_content, answer_content);	
		sh.commitchange();
	}
	public void update(int id,Map<String,Object> map,String ip,String username,String password)
	{
		DBHelper db = new DBHelper(ip,"XueBa","Fileinfo",username,password);
		Map<String, Object> newMap = new HashMap<String,Object>();
		newMap.put("filetype", map.get("doc_type"));
		newMap.put("title", map.get("title"));
		newMap.put("date", map.get("date"));
		newMap.put("author", map.get("author"));
		String kW[] = (String[])map.get("keywords");
		String keywords = "";
		for(int i =0;i < kW.length;i++)
		{
			keywords += kW[i]+" ";
		}
		newMap.put("keywords", keywords);
		newMap.put("content", map.get("content"));
		db.update(id, newMap);	
		db.release();
	}
}
