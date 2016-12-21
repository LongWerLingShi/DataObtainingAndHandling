package dbandsolr;

import static org.junit.Assert.*;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.HashMap;
import java.util.Map;

import org.junit.After;
import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;

public class DBHelperTest {
	static String ip = "10.2.28.78";
	static String solrip = "10.2.28.82";
	static String username = "crawler";
	static String basename = "XueBa";
	static String password="aimashi2015";
	static String port = "8080";
	static String table1 = "fileinfo";
	String table2 = "";
	DBHelper db;
	@BeforeClass
	public static void setUpBeforeClass() throws Exception {
	}

	@AfterClass
	public static void tearDownAfterClass() throws Exception {
	}

	@Before
	public void setUp() throws Exception {
	}

	@After
	public void tearDown() throws Exception {
		db.release();
	}

	@Test
	public void testQuery() {
		db = new DBHelper(ip,"Crawler","fileinfo",username,password);
		Map<String,Object> map = new HashMap<String,Object>();
		map.put("pagetype", "pdf");
		ResultSet rs = db.query(-1,map);		
		int count;
		int rowcount = 0;
		String line = "";
		try {
			rs.last();
			rowcount = rs.getRow();
			rs.first();
			if(rs != null) {
				rs.close();				
			}
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		assert(rowcount == 522);
	}

	@Test
	public void testUpdateAll() {
		db = new DBHelper(ip,"Crawler","Question",username,password);
		Map<String,Object> map = new HashMap<String,Object>();
		map.put("title", "QI");
		map.put("views", "2");
		db.updateAll(map);
		map.remove("views");
		ResultSet rs =  db.query(-1, map);
		int viewscount = 0;
		try {
			rs.next();
			viewscount = rs.getInt("views");
		} catch (SQLException e) {
			e.printStackTrace();
		}
		assert(viewscount == 2);	
	}

	@Test
	public void testInsertline() {
		db = new DBHelper(ip,"Crawler","Question",username,password);
		Map<String,Object> map = new HashMap<String,Object>();
		map.put("id", 10);
		map.put("title", "QI");
		map.put("views", 10);
		db.insertline(map);
		map.clear();
		map.put("id", 10);
		ResultSet rs = db.query(-1, map);
		int viewscount = 0;
		try {
			rs.next();
			viewscount = rs.getInt("views");
		} catch (SQLException e) {
			e.printStackTrace();
		}
		assert(viewscount == 2);
		db.delete(10);
		db.release();
	}

	@Test
	public void testDeleteMapOfStringObject() {
		db = new DBHelper(ip,"Crawler","Question",username,password);
		Map<String,Object> map = new HashMap<String,Object>();
		map.put("id", 10);
		map.put("title", "QI");
		map.put("views", 10);
		db.insertline(map);
		map.clear();
		map.put("id", 10);
		db.delete(map);
		ResultSet rs = db.query(-1, map);
		int count = 1;
		try {
			rs.last();
			count = rs.getRow();
		} catch (SQLException e) {
			e.printStackTrace();
		}
		System.out.println(count);
		assert(count <= 0);
		db.release();
		
	}

	@Test
	public void testDeleteInt() {
		db = new DBHelper(ip,"Crawler","Question",username,password);
		Map<String,Object> map = new HashMap<String,Object>();
		map.put("id", 10);
		map.put("title", "QI");
		map.put("views", 10);
		db.insertline(map);
		db.delete(10);
		map.clear();
		map.put("id", 10);
		ResultSet rs = db.query(-1, map);
		int count = 1;
		try {
			rs.last();
			count = rs.getRow();
		} catch (SQLException e) {
			e.printStackTrace();
		}
		System.out.println(count);
		assert(count <= 0);
		db.release();
	}

}
