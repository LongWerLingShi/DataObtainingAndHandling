import java.sql.*;

import javax.swing.JOptionPane;
/**
* description: �������ݿ�������������Զ����ݿ��ڴ����
* note: ִ��sql��ѯ�Ͳ���ʱ������������
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
	 * ��ʼ�����ݿ�����
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
			System.out.println("���ݿ����� successful_!!!");
		} catch (SQLException e) {
			JOptionPane.showMessageDialog(null, "���ݿ�����ʧ��!", "Error", JOptionPane.INFORMATION_MESSAGE);
			e.printStackTrace();
		}
	}
	
	/**
	 * �Ͽ����ݿ�����
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
	 * ִ��sql��ѯ
	 * @return ResultSet(��ѯ���)
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
	 * ִ��sql����ɾ���Ĳ���
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
	 * ��ȡ���޸����ݿ��ַ���û���������
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

