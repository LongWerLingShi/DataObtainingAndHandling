import java.sql.*;

import javax.swing.JOptionPane;
/**
* description: 连接数据库服务器，并可以对数据库内储存的
* note: 执行sql查询和插入时避免死锁发生
* modificationDate: 2014-11-27
*/ 
public class ConnectServer {
	private Connection con=null;
	private static Statement st=null;
	private ResultSet rs=null;
	private String ip =  "192.168.26.113";
	private String dbName = "Crawler";
	private String conURL = "jdbc:mysql://192.168.26.113:3306/Crawler?characterEncoding=UTF-8";

	private String usr = "root";
	private String pwd = "o0o0o0o";
	
	/**
	 * 初始化数据库连接
	 * @return
	 * @param 
	 * @throws ClassNotFoundException,SQLException
	 * @wbp.parser.entryPoint
	 */
	public ConnectServer()
	{
		try {
			Class.forName("com.mysql.jdbc.Driver").newInstance();
		} catch (InstantiationException | IllegalAccessException | ClassNotFoundException e) {
			e.printStackTrace();
		}
		conn();
	}
	
	public void conn(){
		try {
			con = DriverManager.getConnection(conURL, usr, pwd);
			st = con.createStatement();
			System.out.println("数据库连接 successful_!!!");
		} catch (SQLException e) {
			JOptionPane.showMessageDialog(null, "数据库连接失败!", "Error", JOptionPane.INFORMATION_MESSAGE);
			e.printStackTrace();
		}
	}
	
	/**
	 * 断开数据库连接
	 * @return
	 * @param 
	 * @throws SQLException
	 */
	public void disConn()
	{
		try{
			st.close();
			con.close();
		}catch(SQLException e)
		{
			e.printStackTrace();
		}
	}
	/**
	 * 执行sql查询
	 * @return ResultSet(查询结果)
	 * @param sql(String)
	 * @throws SQLException
	 */
	public ResultSet getResultSet(String sql){
		try {
			rs = st.executeQuery(sql);
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return rs;
	}
	
	/**
	 * 执行sql增、删、改操作
	 * @return void
	 * @param sql(String)
	 * @throws SQLException
	 * */
	public void execute(String sql){
		try {
			st.executeUpdate(sql);
		} catch (SQLException e) {
			e.printStackTrace();
		}
	}
	
	/**
	 * 获取与修改数据库地址，用户名，密码
	 */
	public String getip(){
		return this.ip;
	}
	public void setip(String ip){
		this.ip = ip;
	}
	public String getdbname(){
		return this.dbName;
	}
	public void setdbname(String dbName){
		this.dbName = dbName;
	}
	public String getusername(){
		return this.usr;
	}
	public void setusername(String usr){
		this.usr = usr;
	}
	public String getpassword(){
		return this.pwd;
	}
	public void setpassword(String pwd){
		this.pwd = pwd;
	}

}

