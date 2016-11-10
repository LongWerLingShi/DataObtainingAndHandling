package dbandsolr;

import java.util.HashMap;
import java.util.Map;
import java.sql.*;
public class Main {
	static String ip = "10.2.28.78";
	static String username = "crawler";
	static String basename = "XueBa";
	static String password="aimashi2015";
	public static void main(String[] args) {
		Main m = new Main();
		//m.testquery();
//		m.testupdate();
//		m.testinsert();
//		m.testquery();
		m.testdelete();
	}

	public void testdelete()
	{
		DBHelper db = new DBHelper(ip,basename,username,password);
		Map<String,Object> map = new HashMap<String,Object>();
		map.put("owner", new String("xzw"));
		db.delete(map);
		db.release();
	}
	
	public void testinsert()
	{
		DBHelper db = new DBHelper(ip,basename,username,password);
		Map<String,Object> map = new HashMap<String,Object>();
		map.put("owner", "xzw");
		map.put("view_count", 20);
		map.put("answer_count", 10);
		map.put("link", "www.baidu.com");
		map.put("title", "testinsert");
		db.insertline(map);
		db.release();
	}
	
	public void testquery()
	{
		DBHelper db = new DBHelper(ip,basename,username,password);
		Map<String,Object> map = new HashMap<String,Object>();
//		map.put("answer_count", 1);
//		map.put("view_count", 27);
		map.put("owner", "xzw");
		ResultSet rs = db.query(map);
		int count;
		String line = "";
		try {
			count = rs.getMetaData().getColumnCount();
			while(rs.next()) {
				line = "";
				for(int i = 1;i <= count;i ++) {
					if(i == rs.findColumn("body") || i == rs.findColumn("link") || i == rs.findColumn("title")){						
					}
					else {
						line += rs.getString(i) + '\t';	
					}			
				}
				System.out.println(line);
			}
			if(rs != null) {
				rs.close();				
			}
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		db.release();
	}
	
	public void testupdate() {
		DBHelper db = new DBHelper(ip,basename,username,password);
		Map<String,Object> map = new HashMap<String,Object>();
		map.put("answer_count", 1);
		map.put("view_count", 27);
		db.update(1, map);
		db.release();
	}
	
}
