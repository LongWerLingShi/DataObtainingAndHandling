package dbandsolr;

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
		int count = 0;
		SolrHelper solr = new SolrHelper(solrip,port);
		DBHelper db = new DBHelper(ip,basename,"Question",username,password);
		String line;
		ResultSet rs = db.query(new HashMap());
		try {
			count = rs.getMetaData().getColumnCount();
			while(rs.next()) {
				line = "";
//				for(int i = 1;i <= count;i ++) {
//					if(i == rs.findColumn("body") || i == rs.findColumn("link") || i == rs.findColumn("title")){						
//					}
//					else {
//						line += rs.getString(i) + '\t';	
//					}			
//				}
//				System.out.println(line);
				System.out.println(rs.getString("qid")+ " " + rs.getString("title")+ " " + rs.getString("link")+ " " + rs.getString("creation_date")+ " " + rs.getString("owner"));
				solr.insert(rs.getString("qid"), rs.getString("title"), rs.getString("link"), rs.getString("creation_date"), rs.getString("body"), rs.getString("owner"));
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

	public void testsolrinsert() {
		SolrHelper solr = new SolrHelper(solrip,port);
//		Map<String, Object> map = new HashMap();
//		map.put("wid", 1);
//		map.put("id", "webtest");
//		map.put("web_title", "baidu");
//		solr.insert(map);
//		solr.insert("webtest", "baidu", "www.baidu.com");
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
		map.put("id", "*");
		map.put("title", "baidu");
		SolrDocumentList list = solr.query(map, 100, "id", "title", "links");
		for(int i = 0;i < list.size();i ++) {
			SolrDocument doc = list.get(i);
			System.out.print(i + 1);
			System.out.print("id: " + doc.getFieldValue("id"));
			System.out.print("title: " + doc.getFieldValue("title"));
			System.out.println("links: " + doc.getFieldValue("links"));
		}
//		for(SolrDocument doc : list) {
//			num++;
//			System.out.print(num + " ");
//			System.out.print("id: " + doc.getFieldValue("id"));
//			System.out.print("title: " + doc.getFieldValue("title"));
//			System.out.println("links: " + doc.getFieldValue("links"));
//		}
	}
	
	public void testdelete()
	{
		DBHelper db = new DBHelper(ip,basename,table1,username,password);
		Map<String,Object> map = new HashMap<String,Object>();
		map.put("id", 2);
		db.delete(map);
		db.delete(1);
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
	
	public void testquery()
	{
		DBHelper db = new DBHelper(ip,basename,table1,username,password);
		Map<String,Object> map = new HashMap<String,Object>();
//		map.put("answer_count", 1);
//		map.put("view_count", 27);
		map.put("isDeal", 0);
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
		DBHelper db = new DBHelper(ip,basename,table1,username,password);
		Map<String,Object> map = new HashMap<String,Object>();
		map.put("answer_count", 1);
		map.put("view_count", 27);
		db.update(1, map);
		db.release();
	}
	
}
