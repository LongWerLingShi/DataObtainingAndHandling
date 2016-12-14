package dbandsolr;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;
import java.sql.*;

import org.apache.solr.common.SolrDocument;
import org.apache.solr.common.SolrDocumentList;
public class Main {
	static String ip = "10.2.28.78";
	static String solrip = "10.2.28.82";
	static String username = "crawler";
	static String basename = "XueBa";
	static String password="aimashi2015";
	static String port = "8080";
	static String table1 = "fileinfo";
	String table2 = "";
	public static void main(String[] args) {
		Main m = new Main();
		//m.testquery();
//		m.testupdate();
//		m.testinsert();
//		m.testinsert();
//		m.testquery();
//		m.testdelete();
//		m.testsolrinsert();
//		m.testsolrquery();
//		m.testsolrdelete();
//		m.testsolrquery();
		m.cleardealedtag();
//		m.queryhtml();
	}

	public String getnotdealed() {
		DBHelper db = new DBHelper(ip,"Crawler","fileinfo",username,password);
		Map<String, Object> map = new HashMap<String, Object>();
		map.put("isDeal", 1);
		ResultSet rs = db.query(1, map);
		String path = null;
		try {
			if(rs.next()) {
				path = rs.getString("FilePath");				
			}
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}		
		db.release();
		return path;
	}	
	
	public void cleardealedtag()
	{
		DBHelper db = new DBHelper(ip,"Crawler","fileinfo",username,password);
		Map<String,Object> map = new HashMap<String,Object>();
		map.put("isDeal", 1);
		db.updateAll(map);
		db.release();
	}
	
//	public void cleardealedtag()
//	{
//		DBHelper db = new DBHelper(ip,"Crawler","fileinfo",username,password);
//		Map<String,Object> map = new HashMap<String,Object>();
//		ResultSet rs = db.query(-1,map);
//		int count = 0;
//		String line = "";
//		ArrayList<Integer> ids = new ArrayList<Integer>();
//		try {
//			while(rs.next()) {
//				int id = Integer.parseInt(rs.getString("id"));
//				ids.add(id);
//			}
//			for(int i = 0;i < ids.size();i++) {
//				Map<String,Object> updatemap = new HashMap<String,Object>();
//				updatemap.put("isDeal", 1);
//				db.update(ids.get(i), updatemap);							
//			}
//			if(rs != null) {
//				rs.close();				
//			}
//		} catch (SQLException e) {
//			// TODO Auto-generated catch block
//			e.printStackTrace();
//		} finally {
//			db.release();
//		}		
//	}
	
	public void testsolrinsert() {
		SolrHelper solr = new SolrHelper(solrip,port);
		Map<String, Object> map = new HashMap();
		map.put("wid", 1);
		map.put("id", "webtest");
		map.put("web_title", "baidu");
		solr.insert(map);
		solr.insert("webid", "title", "links", "date", "content", "author");
	}
	
	public void testsolrdelete() {
		SolrHelper solr = new SolrHelper(solrip,port);
		solr.delete("webtest");
	}
	
	public void testsolrquery()
	{
		SolrHelper solr = new SolrHelper(solrip,port);
		Map<String, Object> map = new HashMap();
		int num = 0;
		map.put("id", "web*");
		map.put("title", "baidu");
		SolrDocumentList list = solr.query(map, 100, "id", "title", "links");
		for(int i = 0;i < list.size();i ++) {
			SolrDocument doc = list.get(i);
			System.out.print(i + 1);
			System.out.print(" id: " + doc.getFieldValue("id"));
			System.out.print(" title: " + doc.getFieldValue("title"));
			System.out.println(" links: " + doc.getFieldValue("links"));
		}
	}
	
	public void testdelete()
	{
		DBHelper db = new DBHelper(ip,basename,table1,username,password);
		Map<String,Object> map = new HashMap<String,Object>();
		map.put("author", "QI");
		db.delete(map);
		db.delete(8);
		db.delete(9);
		db.release();
	}
	
	public void testinsert()
	{
		DBHelper db = new DBHelper(ip,basename,table1,username,password);
		Map<String,Object> map = new HashMap<String,Object>();
		map.put("filetype", "html");
		map.put("filepath", "d:\\1.html");
		map.put("url", "www.baidu.com");
		map.put("encode", "utf-8");
		map.put("isDeal", "0");
		db.insertline(map);
		db.release();
	}
	
	public void queryhtml()
	{
		DBHelper db = new DBHelper(ip,"XueBa","fileinfo",username,password);
		Map<String,Object> map = new HashMap<String,Object>();
		//map.put("isDeal", 1);
		map.put("filetype", "html");//filetype = 'html'
		ResultSet rs = db.query(-1, map);//-1说明返回所有满足的条数，如果是正数n说明返回前n条
		int count;
		try {
			rs.last();//指向最后一条
			count = rs.getRow();//得到行数
			rs.first();//指向第一条(如果还要用rs的话)
			System.out.println(count + " html");
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		map.put("isDeal", 1);//filetype = 'html' and isDeal = 1
		rs = db.query(-1, map);
		try {
			rs.last();
			count = rs.getRow();
			rs.first();
			System.out.println(count + " html isDeal");
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		db.release();
	}
	
	public void testquery()
	{
		DBHelper db = new DBHelper(ip,"Crawler","fileinfo",username,password);
		//DBHelper db = new DBHelper(ip,basename,table1,username,password);
		Map<String,Object> map = new HashMap<String,Object>();
		map.put("isDeal", 1);
		ResultSet rs = db.query(10,map);
		
		int count;
		String line = "";
		try {
			count = rs.getMetaData().getColumnCount();
			while(rs.next()) {
				line = "";
				for(int i = 1;i <= count;i ++) {				
					line += rs.getString(i) + '\t';					
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
		DBHelper db = new DBHelper(ip,basename,table1,username,password);
		Map<String,Object> map = new HashMap<String,Object>();
		map.put("title", "baidu");
		map.put("date", "2010-9-1");
		map.put("author", "QI");
		map.put("keywords", "test1,rt2");
		db.update(7, map);
		db.release();
	}
	
}
