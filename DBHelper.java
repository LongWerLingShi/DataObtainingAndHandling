package dbandsolr;
import java.sql.*;
import java.util.Map;

/*
 * 数据库管理类，可以对数据库进行查询，更新，插入，删除操作
 * 食用需求：在src中导入sqljdbc4.jar,
 * 下载地址：https://www.microsoft.com/zh-cn/download/details.aspx?id=11774
 * 下载sqljdbc_4.0.2206.100_chs.tar.gz
 */
public class DBHelper {
	private static final String driverName="com.microsoft.sqlserver.jdbc.SQLServerDriver";
	private String ip;
	private String baseName;
	private String userName;
	private String userPwd;
	private Connection conn;
	private Statement sta;
	private String TableName = "Question";			//因为之前商量说数据库只有一张表，所以构造时不用传表名，在这里定义一下即可
	/*
	 * 构造方法
	 * @prama：服务器ip，数据库名称basename，用户名username，密码password
	 */
	public DBHelper(String ip,String basename,String username,String password) {
		this.ip = ip;
		this.baseName = basename;
		this.userName = username;
		this.userPwd = password;
		String dbURL="jdbc:sqlserver://" + ip + ":1433;DatabaseName=" + baseName;
		try
		{
			Class.forName(driverName);
			conn = DriverManager.getConnection(dbURL,userName,userPwd);
			sta = conn.createStatement(ResultSet.TYPE_SCROLL_SENSITIVE, ResultSet.CONCUR_UPDATABLE);
		    System.out.println("连接数据库成功");
		}
		catch(Exception e)
		{
			e.printStackTrace();
			System.out.print("连接失败");
		}     
	}
	
	/*
	 * 查询方法
	 * 根据传入的属性值查询，返回查询结果
	 * @prama:一个保存着属性名(String)，属性值(Object)的map
	 * @return:保存着查询结果的ResultSet，类似一个表，具体用法可以看我的例子或者查看java api(java.sql.ResultSet)
	 */
	public ResultSet query(Map<String, Object> map) {
		String attribute;
		Object value;
		String valuestr = "";
		ResultSet rs = null;
		int num = 0;
		String sqlquery = "Select * from " + TableName + " where ";
		for(Map.Entry<String, Object> pair : map.entrySet()) {
			attribute = pair.getKey();
			value = pair.getValue();
			if(value instanceof String) {
				valuestr = '\'' + value.toString() + '\'';
			}
			else {
				valuestr = value.toString();
			}
			if(num == 0) {
				sqlquery += attribute + " = " + valuestr;					
			}
			else {
				sqlquery += " and " + attribute + " = " + valuestr;	
			}
			num++;
		}
//		System.out.println(sqlquery);
		try {
			rs = sta.executeQuery(sqlquery);
		} catch (SQLException e) {
			e.printStackTrace();
		}		
		return rs;		
	}
	
	/*
	 * 更新数据
	 * 根据id，更新该表项数据
	 * @prama:要更新的表项的id，一个保存着属性名(String)，属性值(Object)的map
	 * @return：修改是否成功，成功为1，失败为0
	 */
	public int update(int id,Map<String,Object> map) {
		String attribute;
		Object value;
		String sqlupdate = "";
		int num = 0;
		for(Map.Entry<String, Object> pair : map.entrySet()) {
			attribute = pair.getKey();
			value = pair.getValue();
			sqlupdate = "update " + TableName + " set " + attribute + " = " + value.toString() + " where qid = " + id;
//			System.out.println(sqlupdate);
			try {
				num = sta.executeUpdate(sqlupdate);
			} catch (SQLException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}
		try {
			conn.commit();
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return num;
	}
	
	/*
	 * 插入新的一行
	 * 新建一行，并且根据map提供的属性名和属性值在该行的相应属性栏填入数据
	 * @prama:一个保存着属性名(String)，属性值(Object)的map
	 * @return:
	 */
	public void insertline(Map<String,Object> map) {
		String attribute;
		Object value;
		String sqlinsert = "";
		try {
			ResultSet rs = sta.executeQuery("Select * from " + TableName);
			rs.moveToInsertRow();
			for(Map.Entry<String, Object> pair : map.entrySet()) {
				attribute = pair.getKey();
				value = pair.getValue();
				rs.updateString(attribute, value.toString());
			}
			rs.insertRow();
		} catch (SQLException | NumberFormatException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
	/*
	 * 删除行
	 * 根据map提供的属性名和属性值删除符合条件的行
	 * @prama:一个保存着属性名(String)，属性值(Object)的map
	 * @return:删除的行数
	 */
	public int delete(Map<String,Object> map) {
		String attribute;
		Object value;
		String valuestr = "";
		int num = 0;
		int count = 0;
		String sqldelete = "delete from " + TableName + " where ";
		for(Map.Entry<String, Object> pair : map.entrySet()) {
			attribute = pair.getKey();
			value = pair.getValue();
			if(value instanceof String) {
				valuestr = '\'' + value.toString() + '\'';
			}
			else {
				valuestr = value.toString();
			}
			if(num == 0) {
				sqldelete += attribute + " = " + valuestr;					
			}
			else {
				sqldelete += " and " + attribute + " = " + valuestr;	
			}
			num++;
		}
		try {
//			System.out.println(sqldelete);
			count = sta.executeUpdate(sqldelete);
		} catch (SQLException e) {
			e.printStackTrace();
		}	
		return count;
	}
	
	/*
	 * 释放资源
	 * 若此DBHelper不再使用，记得一定要调用一次,不然用久了资源耗尽就会挂
	 */
	public void release() 
	{
		try {
			if(sta != null) {
				sta.close();				
			}
			if(conn != null) {
				conn.close();
			}
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
}











