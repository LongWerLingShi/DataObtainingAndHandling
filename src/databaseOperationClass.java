import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

public class databaseOperationClass implements databaseOperation{
	private native void denoiseWord(String path,int trd);  
	private native void cutwords(int trd, String analyzer);
	private native void key(String Path,int trd);
	private native void translate(int trd);
	public int queryHandleFail(String ip,String username,String password)
	{
		//茶轴失败函数
		return 0;
	}
	public Map<String, Object> handleWord(String URL,int threadNo)
	{
		Map<String, Object> map = new HashMap<String,Object>();;
		denoiseWord(URL,threadNo);
        cutwords(1, "Lucene.China.ChineseAnalyzer");
        key("CorpusWordlist.xls", threadNo);
        translate(threadNo);
		return map;
	}
	public int queryTitle(String ip,String username,String password)
	{
		DBHelper db = new DBHelper(ip,"XueBa","fileinfo",username,password);
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
		DBHelper db = new DBHelper(ip,"XueBa","fileinfo",username,password);
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
		DBHelper db = new DBHelper(ip,"XueBa","fileinfo",username,password);
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
		DBHelper db = new DBHelper(ip,"XueBa","fileinfo",username,password);
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
		DBHelper db = new DBHelper(ip,"Crawler","fileinfo",username,password);
		Map<String,Object> map = new HashMap<String,Object>();
		ResultSet rs = db.query(-1,map);
		//int count = 0;
		//String line = "";
		ArrayList<Integer> ids = new ArrayList<Integer>();
		try {
			while(rs.next()) {
				int id = Integer.parseInt(rs.getString("id"));
				ids.add(id);
				//System.out.println(id);
			}
			for(int i = 0;i < ids.size();i++) {
				Map<String,Object> updatemap = new HashMap<String,Object>();
				updatemap.put("isDeal", 1);
				db.update(ids.get(i), updatemap);		
				//System.out.println(ids.size());
			}
			if(rs != null) {
				rs.close();				
			}
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} finally {
			db.release();
		}		
	}

	public String getnotdealed(String ip,String username,String password) {
		DBHelper db = new DBHelper(ip,"Crawler","fileinfo",username,password);
		Map<String, Object> map = new HashMap<String, Object>();
		map.put("isDeal", 1);
		ResultSet rs = db.query(1, map);
		String path = null;
		try {
			if(rs.next()) {
				path = rs.getString("FilePath");		
				int id = Integer.parseInt(rs.getString("id"));
				Map<String,Object> updatemap = new HashMap<String,Object>();
				updatemap.put("isDeal", 0);
				db.update(id, updatemap);	
			}
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}		
		db.release();
		return path;
	}
	public void insert(String id,String title,String links,String date,String content,String author)
	{
		SolrHelper sh = new SolrHelper("ip:10.2.28.82","8080");
		sh.insert(id, title, links, date, content, author);
	}
	public void insert(Map<String, Object> map)
	{
		SolrHelper sh = new SolrHelper("ip:10.2.28.82","8080");
		sh.insert(map);
	}
}
