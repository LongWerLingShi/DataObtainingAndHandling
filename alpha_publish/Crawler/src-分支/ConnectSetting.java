

import javax.swing.JFrame;
import javax.swing.JPanel;
import javax.swing.border.EmptyBorder;
import java.awt.Color;
import javax.swing.JTextField;
import javax.swing.JButton;
import javax.swing.JLabel;
import javax.swing.JOptionPane;
import javax.swing.SwingConstants;
import java.awt.event.ActionListener;
import java.awt.event.WindowEvent;
import java.awt.event.WindowListener;
import java.awt.event.ActionEvent;

@SuppressWarnings("serial")
public class ConnectSetting extends JFrame {

	private ConnectSetting thisFrame;
	private MyCrawler mainFrame;
	private ConnectServer c;
	private JPanel contentPane;
	private JTextField tfIP;
	private JTextField tfDB;
	private JTextField tfUsr;
	private JTextField tfPwd;
	private JLabel label_2;
	private JButton btnSave, btnCancel;

	

	/**
	 * Create the frame.
	 */
	public ConnectSetting(ConnectServer c, MyCrawler m) {
		thisFrame = this;
		mainFrame = m;
		this.c = c;
		setResizable(false);
		setBackground(Color.WHITE);
		setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE);
		setBounds(100, 100, 295, 309);
		contentPane = new JPanel();
		contentPane.setBackground(Color.WHITE);
		contentPane.setBorder(new EmptyBorder(5, 5, 5, 5));
		setContentPane(contentPane);
		contentPane.setLayout(null);
		
		tfIP = new JTextField();
		tfIP.setBounds(71, 28, 187, 21);
		contentPane.add(tfIP);
		tfIP.setColumns(10);
		tfIP.setText(c.getip());
		
		tfDB = new JTextField();
		tfDB.setColumns(10);
		tfDB.setBounds(71, 79, 187, 21);
		contentPane.add(tfDB);
		tfDB.setText(c.getdbname());
		
		tfUsr = new JTextField();
		tfUsr.setColumns(10);
		tfUsr.setBounds(71, 128, 187, 21);
		contentPane.add(tfUsr);
		tfUsr.setText(c.getusername());
		
		tfPwd = new JTextField();
		tfPwd.setColumns(10);
		tfPwd.setBounds(71, 182, 187, 21);
		contentPane.add(tfPwd);
		tfPwd.setText(c.getpassword());
		
		btnSave = new JButton("\u4FDD\u5B58");
		btnSave.setBackground(Color.WHITE);
		btnSave.setBounds(81, 223, 66, 23);
		contentPane.add(btnSave);
		
		JLabel lbltcpip = new JLabel("IP\u5730\u5740");
		lbltcpip.setHorizontalAlignment(SwingConstants.RIGHT);
		lbltcpip.setBounds(10, 31, 51, 15);
		contentPane.add(lbltcpip);
		
		JLabel label = new JLabel("\u6570\u636E\u5E93");
		label.setHorizontalAlignment(SwingConstants.RIGHT);
		label.setBounds(10, 82, 51, 15);
		contentPane.add(label);
		
		JLabel label_1 = new JLabel("\u7528\u6237\u540D");
		label_1.setHorizontalAlignment(SwingConstants.RIGHT);
		label_1.setBounds(10, 131, 51, 15);
		contentPane.add(label_1);
		
		label_2 = new JLabel("\u5BC6\u7801");
		label_2.setHorizontalAlignment(SwingConstants.RIGHT);
		label_2.setBounds(10, 185, 51, 15);
		contentPane.add(label_2);
		
		btnCancel = new JButton("\u53D6\u6D88");
		btnCancel.setBackground(Color.WHITE);
		btnCancel.setBounds(157, 223, 66, 23);
		contentPane.add(btnCancel);
		setVisible(true);
		
		initListeners();
	}
	
	//监听器事件集合
	void initListeners(){
		thisFrame.addWindowListener(new WindowListener(){

			@Override
			public void windowActivated(WindowEvent arg0) {
				// TODO Auto-generated method stub
				
			}

			@Override
			public void windowClosed(WindowEvent arg0) {
				// TODO Auto-generated method stub
				mainFrame.setVisible(true);
			}

			@Override
			public void windowClosing(WindowEvent arg0) {
				// TODO Auto-generated method stub
				
			}

			@Override
			public void windowDeactivated(WindowEvent arg0) {
				// TODO Auto-generated method stub
				
			}

			@Override
			public void windowDeiconified(WindowEvent arg0) {
				// TODO Auto-generated method stub
				
			}

			@Override
			public void windowIconified(WindowEvent arg0) {
				// TODO Auto-generated method stub
				
			}

			@Override
			public void windowOpened(WindowEvent arg0) {
				// TODO Auto-generated method stub
				
			}
			
		});
	
		btnSave.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				if(tfDB.getText().trim().equals("") || tfIP.getText().trim().equals("") || 
						tfUsr.getText().trim().equals("") || tfPwd.getText().trim().equals("")){
					JOptionPane.showMessageDialog(null, "请输入完整信息", "Error", JOptionPane.INFORMATION_MESSAGE);
				}
				c.setdbname(tfDB.getText());
				c.setip(tfIP.getText());
				c.setusername(tfUsr.getText());
				c.setpassword(tfPwd.getText());
				c.disConn();
				c.conn();
				thisFrame.dispose();
			}
		});

		btnCancel.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent arg0) {
				thisFrame.dispose();
			}
		});
	}
}
