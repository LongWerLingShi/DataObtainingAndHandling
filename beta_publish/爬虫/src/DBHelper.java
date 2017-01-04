import java.sql.*;
import java.util.Map;

/*
 * ���ݿ�����࣬���Զ����ݿ���в�ѯ�����£����룬ɾ������
 * ʳ��������src�е���sqljdbc4.jar,
 * ���ص�ַ��https://www.microsoft.com/zh-cn/download/details.aspx?id=11774
 * ����sqljdbc_4.0.2206.100_chs.tar.gz
 */
public class DBHelper {
	private static final String driverName="com.microsoft.sqlserver.jdbc.SQLServerDriver";
	private String ip;
	private String baseName;
	private String userName;
	private String userPwd;
	private Connection conn;
	private Statement sta;
	private String tableName;			
	/*
	 * ���췽��
	 * @prama��������ip�����ݿ�����basename������tablename, �û���username������password
	 */
	public DBHelper(String ip,String basename, String tableName, String username,String password) {
		this.ip = ip;
		this.baseName = basename;
		this.userName = username;
		this.userPwd = password;
		this.tableName = tableName;
		String dbURL="jdbc:sqlserver://" + this.ip + ":1433;DatabaseName=" + baseName;
		try
		{
			Class.forName(driverName);
			conn = DriverManager.getConnection(dbURL,userName,userPwd);
			sta = conn.createStatement(ResultSet.TYPE_SCROLL_SENSITIVE, ResultSet.CONCUR_UPDATABLE);
		    System.out.println("�������ݿ�ɹ�");
		}
		catch(Exception e)
		{
			e.printStackTrace();
			System.out.print("����ʧ��");
		}     
	}
	
	/*
	 * ��ѯ����
	 * ���ݴ��������ֵ��ѯ�����ز�ѯ���
	 * @prama:һ��������������(String)������ֵ(Object)��map,countΪ���ص����������С�ڵ���0ʱ��Ϊ����ȫ����
	 * @return:�����Ų�ѯ�����ResultSet������һ���������÷����Կ��ҵ����ӻ��߲鿴java api(java.sql.ResultSet)
	 */
	public ResultSet query(int count, Map<String, Object> map) {
		String attribute;
		Object value;
		String valuestr = "";
		ResultSet rs = null;
		int num = 0;
		String sqlquery;
		if(count > 0) {
			sqlquery = "Select top " + count + " * from " + tableName + " where ";
		}
		else {
			sqlquery = "Select * from " + tableName + " where ";
		}		
		if(map != null) {			
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
		}
		if(num == 0) {
			if(count > 0) {
				sqlquery = "Select top " + count + " * from " + tableName;
			}
			else {
				sqlquery = "Select * from " + tableName;				
			}
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
	 * ��������
	 * ����id�����¸ñ�������
	 * @prama:Ҫ���µı����id��һ��������������(String)������ֵ(Object)��map
	 * @return���޸��Ƿ�ɹ����ɹ�Ϊ1��ʧ��Ϊ0
	 */
	public int update(int id,Map<String,Object> map) {
		String attribute;
		Object value;
		String sqlupdate = "";
		int num = 0;
		if(map == null) {
			return 0;
		}
		for(Map.Entry<String, Object> pair : map.entrySet()) {
			attribute = pair.getKey();
			value = pair.getValue();
			if(value instanceof String) {
				sqlupdate = "update " + tableName + " set " + attribute + " = '" + value.toString() + "' where id = " + id;
			}
			else {
				sqlupdate = "update " + tableName + " set " + attribute + " = " + value.toString() + " where id = " + id;				
			}
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
	 * �����µ�һ��
	 * �½�һ�У����Ҹ���map�ṩ��������������ֵ�ڸ��е���Ӧ��������������
	 * @prama:һ��������������(String)������ֵ(Object)��map
	 * @return:
	 */
	public void insertline(Map<String,Object> map) {
		String attribute;
		Object value;
		//String sqlinsert = "";
		if(map == null) {
			return ;
		}
		try {
			ResultSet rs = sta.executeQuery("Select * from " + tableName);
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
	 * ɾ����
	 * ����map�ṩ��������������ֵɾ��������������
	 * @prama:һ��������������(String)������ֵ(Object)��map
	 * @return:ɾ��������
	 */
	public int delete(Map<String,Object> map) {
		String attribute;
		Object value;
		String valuestr = "";
		int num = 0;
		int count = 0;
		String sqldelete = "delete from " + tableName + " where ";
		if(map == null) {
			return 0;
		}
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
		if(num == 0) {
			return 0;
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
	 * ����idɾ����
	 * @prama:Ҫɾ�����е�id
	 * @return:1��ʾ�ɹ���0��ʾʧ��
	 */
	public int delete(int id) {
		String sqldelete = "delete from " + tableName + " where id = " + id;
		int count = 0;
		try {
			count = sta.executeUpdate(sqldelete);
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return count;
	}
	
	/*
	 * �ͷ���Դ
	 * ����DBHelper����ʹ�ã��ǵ�һ��Ҫ����һ��,��Ȼ�þ�����Դ�ľ��ͻ��
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











