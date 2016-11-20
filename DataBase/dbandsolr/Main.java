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
		m.testsolrquery();
//		m.testsolrdelete();
//		m.testsolrquery();

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
	
	public void testquery()
	{
		DBHelper db = new DBHelper(ip,basename,table1,username,password);
		Map<String,Object> map = new HashMap<String,Object>();
		map.put("filetype", "html");
		ResultSet rs = db.query(-1,map);
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
