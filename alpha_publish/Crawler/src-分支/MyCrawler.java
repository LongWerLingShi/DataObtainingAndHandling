import static org.quartz.JobBuilder.newJob;
import static org.quartz.SimpleScheduleBuilder.simpleSchedule;
import static org.quartz.TriggerBuilder.newTrigger;

import java.awt.BorderLayout;
import java.awt.EventQueue;

import javax.swing.JFrame;
import javax.swing.JPanel;
import javax.swing.border.EmptyBorder;
import javax.swing.JScrollPane;
import javax.swing.JList;
import javax.swing.JTextField;
import javax.swing.JButton;
import javax.swing.AbstractListModel;
import javax.swing.DefaultListModel;
import javax.swing.ListSelectionModel;
import javax.swing.border.MatteBorder;
import java.awt.Color;
import javax.swing.JLabel;
import javax.swing.SwingConstants;
import javax.swing.event.ListSelectionListener;
import javax.swing.event.TableModelEvent;
import javax.swing.event.TableModelListener;
import javax.swing.table.DefaultTableModel;

import org.quartz.DateBuilder;
import org.quartz.JobDataMap;
import org.quartz.JobDetail;
import org.quartz.JobExecutionContext;
import org.quartz.JobExecutionException;
import org.quartz.Scheduler;
import org.quartz.SchedulerException;
import org.quartz.SchedulerFactory;
import org.quartz.SimpleTrigger;
import org.quartz.impl.StdSchedulerFactory;

import javax.swing.event.ListDataEvent;
import javax.swing.event.ListDataListener;
import javax.swing.event.ListSelectionEvent;
import java.awt.event.ActionListener;
import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Date;
import java.util.Set;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.awt.event.ActionEvent;
import javax.swing.JProgressBar;
import javax.swing.JTable;
import javax.swing.JTextArea;

import java.awt.SystemColor;
import javax.swing.JToggleButton;
import javax.swing.ScrollPaneConstants;

public class MyCrawler extends JFrame implements org.quartz.Job{
	
	private static MyCrawler mainFrame;
	private ConnectServer connectServer = new ConnectServer();
	
	public static int count=0;//爬取线程数
	public static int ST = 0;//stackoverflow
	public static int CN = 0;//q.cnblogs
	public static int DW = 0;//dwen
	public static int BZ = 0;//zhidao.baidu
	public static int WW = 0;//wenwen
	public static int id;//当前网页id
	public static String keyword = "";//关键字
	
	
	
	
	
	public static long time;//已经进行时间
	public static long startTime;//爬取开始时间
	public static ExecutorService pool = Executors.newFixedThreadPool(50);
	
	private static String url;
	private String type;
	private String[] keywords;
	private String[] rules;
	public static int pages;
	public static int completeCount = 0;
	private int completeTarget = 0;
	public static boolean completeFlag = true;
	public static int succeed = 0;//爬取成功
	public static int failed = 0;//爬取失败
	
	public Lock lock = new Lock();
	private boolean pdfFlag = false, pptFlag = false, docFlag = false, htmlFlag = true;
	private Scheduler sched;
	
	
	private JPanel contentPane;
	private JTextField tfSeed;
	private DefaultListModel<String> dlmKeywords = new DefaultListModel<String>();
	private DefaultListModel<String> dlmRules = new DefaultListModel<String>();
	public static DefaultListModel<String> dlmUnVisited = new DefaultListModel<String>();
	public static DefaultTableModel dtmSeeds = new DefaultTableModel(null, new String[]{"Url", "爬取类型", "关键字", "过滤规则", "爬取页数"});
	public static DefaultTableModel dtmHistory = new DefaultTableModel(null, new String[]{"Seed", "成功", "失败", "目标数"});
	private JList<String> listUnVisited, listKeywords, listRules;
	private boolean firstAddFlag = true;
	private JTextField tfKeyword;
	private JTextField tfRule;
	private JTable tableHistory;
	private JTextField tfCount;
	private JTable tableSeeds;
	private JButton btnAddKeyword, btnRemoveKeyword, btnAddRule, btnRemoveRule, btnAddSeed, btnRemoveSeed, btnSetting, btnAnalyze;
	private JButton btnStart, btnStop;
	private JToggleButton tglbtnPdf, tglbtnDoc, tglbtnPpt, tglbtnQuiz;
	private JProgressBar progressBar;
	

	/**
	 * Launch the application.
	 */
	public static void main(String[] args) {
		EventQueue.invokeLater(new Runnable() {
			public void run() {
				try {
					mainFrame = new MyCrawler();
					mainFrame.setVisible(true);
				} catch (Exception e) {
					e.printStackTrace();
				}
			}
		});
	}

	/**
	 * Create the frame.
	 */
	public MyCrawler() {
		setResizable(false);
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		setBounds(100, 100, 970, 472);
		contentPane = new JPanel();
		contentPane.setBackground(Color.WHITE);
		contentPane.setBorder(new EmptyBorder(5, 5, 5, 5));
		setContentPane(contentPane);
		contentPane.setLayout(null);
		
		JScrollPane scrollPane = new JScrollPane();
		scrollPane.setBounds(317, 57, 332, 359);
		contentPane.add(scrollPane);
		
		
		
		dlmUnVisited.addListDataListener(new ListDataListener(){

			@Override
			public void contentsChanged(ListDataEvent arg0) {
			}

			@Override
			public void intervalAdded(ListDataEvent arg0) {
				if((completeCount < completeTarget) && completeFlag){
					completeFlag = false;
					if(!dlmUnVisited.isEmpty()){
						dlmUnVisited.removeAllElements();
					}
					for(int i = 0; i < LinkQueue.unVisitedUrl.size(); i++){
						LinkQueue.unVisitedUrl.poll();
					}
					LinkQueue.addUnvisitedUrl(String.valueOf(dtmSeeds.getValueAt(0, 0)));
					listUnVisited.setModel(dlmUnVisited);
					type = String.valueOf(dtmSeeds.getValueAt(0, 1));
					keywords = String.valueOf(dtmSeeds.getValueAt(0, 2)).split("#");
					rules = String.valueOf(dtmSeeds.getValueAt(0, 3)).split("#");
					pages = Integer.parseInt(String.valueOf(dtmSeeds.getValueAt(0, 4)));
					progressBar.setMaximum(pages);
					crawling();
				}
			}

			@Override
			public void intervalRemoved(ListDataEvent arg0) {
			}
			
		});
		
		tableSeeds = new JTable(dtmSeeds);
		tableSeeds.setFillsViewportHeight(true);
		scrollPane.setViewportView(tableSeeds);
		
		
		tfSeed = new JTextField();
		tfSeed.setBounds(10, 10, 284, 21);
		contentPane.add(tfSeed);
		tfSeed.setColumns(10);
		
		btnAddSeed = new JButton("\u6DFB\u52A0");
		btnAddSeed.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent arg0) {
				if(!tfSeed.getText().trim().equals("") && !tfCount.getText().trim().equals("")){
					completeTarget++;
					String crawlUrl = tfSeed.getText();
					String crawlType = "";
					String crawlKeywords = "";
					String crawlRules = "";
					String crawlPages = tfCount.getText();
					if(tglbtnPdf.isSelected()){
						crawlType = "pdf";
					}
					else if(tglbtnPpt.isSelected()){
						crawlType = "ppt";
					}
					else if(tglbtnDoc.isSelected()){
						crawlType = "doc";
					}
					for(int i = 0; i < dlmKeywords.size(); i++){
						crawlKeywords += dlmKeywords.getElementAt(i) + "#";
					}
					for(int i = 0; i < dlmRules.size(); i++){
						crawlRules += dlmRules.getElementAt(i) + "#";
					}
					dtmSeeds.addRow(new Object[]{crawlUrl, crawlType, crawlKeywords, crawlRules, crawlPages});
					tableSeeds.setModel(dtmSeeds);
					tfSeed.setText("");
				}
			}
		});
		btnAddSeed.setBackground(Color.WHITE);
		btnAddSeed.setBounds(317, 9, 67, 23);
		contentPane.add(btnAddSeed);
		
		btnRemoveSeed = new JButton("\u5220\u9664");
		btnRemoveSeed.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				int selectedRow = tableSeeds.getSelectedRow();
				if(selectedRow != -1){
					completeTarget--;
					dtmSeeds.removeRow(selectedRow);
					tableSeeds.setModel(dtmSeeds);
				}
			}
		});
		btnRemoveSeed.setBackground(Color.WHITE);
		btnRemoveSeed.setBounds(394, 9, 67, 23);
		contentPane.add(btnRemoveSeed);
		
		JScrollPane scrollPane_1 = new JScrollPane();
		scrollPane_1.setHorizontalScrollBarPolicy(ScrollPaneConstants.HORIZONTAL_SCROLLBAR_NEVER);
		scrollPane_1.setBounds(670, 42, 284, 178);
		contentPane.add(scrollPane_1);
		
		listUnVisited = new JList<String>(dlmUnVisited);
		scrollPane_1.setViewportView(listUnVisited);
		
		JLabel lblurl_1 = new JLabel("\u5F85\u8BBF\u95EEURL");
		lblurl_1.setHorizontalAlignment(SwingConstants.CENTER);
		scrollPane_1.setColumnHeaderView(lblurl_1);
		
		JScrollPane scrollPane_2 = new JScrollPane();
		scrollPane_2.setBounds(10, 105, 137, 218);
		contentPane.add(scrollPane_2);
		
		listKeywords = new JList<String>(dlmKeywords);
		scrollPane_2.setViewportView(listKeywords);
		
		JLabel label = new JLabel("\u5173\u952E\u8BCD");
		label.setHorizontalAlignment(SwingConstants.CENTER);
		scrollPane_2.setColumnHeaderView(label);
		
		JScrollPane scrollPane_3 = new JScrollPane();
		scrollPane_3.setBounds(157, 105, 137, 218);
		contentPane.add(scrollPane_3);
		
		dlmRules.addElement("http://");
		dlmRules.addElement("https://");
		listRules = new JList<String>(dlmRules);
		scrollPane_3.setViewportView(listRules);
		
		JLabel label_1 = new JLabel("\u8FC7\u6EE4\u89C4\u5219");
		label_1.setHorizontalAlignment(SwingConstants.CENTER);
		scrollPane_3.setColumnHeaderView(label_1);
		
		progressBar = new JProgressBar();
		progressBar.setEnabled(false);
		progressBar.setStringPainted(true);
		progressBar.setForeground(SystemColor.activeCaption);
		progressBar.setBackground(Color.WHITE);
		progressBar.setBounds(670, 11, 284, 22);
		contentPane.add(progressBar);
		
		tfKeyword = new JTextField();
		tfKeyword.setBounds(10, 333, 137, 21);
		contentPane.add(tfKeyword);
		tfKeyword.setColumns(10);
		
		tfRule = new JTextField();
		tfRule.setColumns(10);
		tfRule.setBounds(157, 333, 137, 21);
		contentPane.add(tfRule);
		
		btnAddKeyword = new JButton("\u6DFB\u52A0");
		btnAddKeyword.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent arg0) {
				if(!tfKeyword.getText().trim().equals("")){
					dlmKeywords.addElement(tfKeyword.getText());
					listKeywords.setModel(dlmKeywords);
					tfKeyword.setText("");
				}
			}
		});
		btnAddKeyword.setBackground(Color.WHITE);
		btnAddKeyword.setBounds(10, 364, 67, 23);
		contentPane.add(btnAddKeyword);
		
		btnAddRule = new JButton("\u6DFB\u52A0");
		btnAddRule.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				if(!tfRule.getText().trim().equals("")){
					dlmRules.addElement(tfRule.getText());
					listRules.setModel(dlmRules);
					tfRule.setText("");
				}
			}
		});
		btnAddRule.setBackground(Color.WHITE);
		btnAddRule.setBounds(157, 364, 61, 23);
		contentPane.add(btnAddRule);
		
		JScrollPane scrollPane_4 = new JScrollPane();
		scrollPane_4.setBounds(670, 252, 284, 167);
		contentPane.add(scrollPane_4);
		
		tableHistory = new JTable(dtmHistory);
		tableHistory.setFillsViewportHeight(true);
		scrollPane_4.setViewportView(tableHistory);
		
		JLabel lblNewLabel = new JLabel("\u722C\u53D6\u8BB0\u5F55");
		lblNewLabel.setOpaque(true);
		lblNewLabel.setBackground(SystemColor.controlHighlight);
		lblNewLabel.setHorizontalAlignment(SwingConstants.CENTER);
		lblNewLabel.setBounds(670, 237, 284, 15);
		contentPane.add(lblNewLabel);
		
		tglbtnQuiz = new JToggleButton("QUIZ");
		tglbtnQuiz.setBackground(Color.WHITE);
		tglbtnQuiz.setBounds(10, 39, 135, 23);
		contentPane.add(tglbtnQuiz);
		
		tglbtnPpt = new JToggleButton("PPT");
		tglbtnPpt.setBackground(Color.WHITE);
		tglbtnPpt.setBounds(10, 72, 135, 23);
		contentPane.add(tglbtnPpt);
		
		tglbtnPdf = new JToggleButton("PDF");
		tglbtnPdf.setBackground(Color.WHITE);
		tglbtnPdf.setBounds(159, 39, 135, 23);
		contentPane.add(tglbtnPdf);
		
		tglbtnDoc = new JToggleButton("DOC");
		tglbtnDoc.setBackground(Color.WHITE);
		tglbtnDoc.setBounds(159, 72, 135, 23);
		contentPane.add(tglbtnDoc);
		
		btnSetting = new JButton("");
		btnSetting.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent arg0) {
				ConnectSetting settingFrame = new ConnectSetting(connectServer, mainFrame);
				mainFrame.setVisible(false);
			}
		});
		btnSetting.setBackground(Color.WHITE);
		btnSetting.setBounds(10, 396, 23, 23);
		contentPane.add(btnSetting);
		
		JLabel label_2 = new JLabel("\u8BBE\u7F6E");
		label_2.setBounds(43, 396, 39, 23);
		contentPane.add(label_2);
		
		btnAnalyze = new JButton("");
		btnAnalyze.setBackground(Color.WHITE);
		btnAnalyze.setBounds(92, 397, 23, 23);
		contentPane.add(btnAnalyze);
		
		JLabel label_3 = new JLabel("\u5206\u6790");
		label_3.setBackground(new Color(240, 240, 240));
		label_3.setBounds(125, 396, 39, 23);
		contentPane.add(label_3);
		
		tfCount = new JTextField();
		tfCount.setBounds(228, 397, 66, 23);
		contentPane.add(tfCount);
		tfCount.setColumns(10);
		
		JLabel label_4 = new JLabel("\u722C\u53D6\u6570");
		label_4.setBounds(179, 396, 39, 23);
		contentPane.add(label_4);
		
		JLabel lblurl = new JLabel("\u79CD\u5B50URL\u961F\u5217");
		lblurl.setOpaque(true);
		lblurl.setHorizontalAlignment(SwingConstants.CENTER);
		lblurl.setBackground(SystemColor.controlHighlight);
		lblurl.setBounds(317, 42, 332, 15);
		contentPane.add(lblurl);
		
		btnRemoveKeyword = new JButton("\u79FB\u9664");
		btnRemoveKeyword.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				int selectedIndex = listKeywords.getSelectedIndex();
				if(selectedIndex != -1){
					dlmKeywords.removeElementAt(selectedIndex);
					listKeywords.setModel(dlmKeywords);
				}
			}
		});
		btnRemoveKeyword.setBackground(Color.WHITE);
		btnRemoveKeyword.setBounds(80, 364, 67, 23);
		contentPane.add(btnRemoveKeyword);
		
		btnRemoveRule = new JButton("\u79FB\u9664");
		btnRemoveRule.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				int selectedIndex = listRules.getSelectedIndex();
				if(selectedIndex != -1){
					dlmRules.removeElementAt(selectedIndex);
					listRules.setModel(dlmRules);
				}
			}
		});
		btnRemoveRule.setBackground(Color.WHITE);
		btnRemoveRule.setBounds(233, 364, 61, 23);
		contentPane.add(btnRemoveRule);
		
		btnStart = new JButton("\u5F00\u59CB");
		btnStart.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent arg0) {
				System.out.println("初始化调度工厂**********" + dtmSeeds.getRowCount());
				SchedulerFactory sf = new StdSchedulerFactory();
				try {
					sched = sf.getScheduler();
					System.out.println("初始化完成");
					Date startTime = DateBuilder.nextGivenSecondDate(null,5);
					JobDetail job = newJob(MyCrawler.class).withIdentity("job1", "group1").build();
					if(dtmSeeds.getRowCount() > 0){
						job.getJobDataMap().put("seeds", dtmSeeds);
						job.getJobDataMap().put("unvisited", dlmUnVisited);
						job.getJobDataMap().put("flag", completeFlag);
					}
					SimpleTrigger trigger = (SimpleTrigger) newTrigger().withIdentity("trigger1", "group1")  
					        .startAt(startTime)
					        .withSchedule(  
					                simpleSchedule()  
					                .withIntervalInSeconds(5)  
					                .repeatForever()  
					        )                     
					        .build(); 
					sched.scheduleJob(job, trigger);
					System.out.println(job.getKey() + " will run at: " + startTime);
					sched.start();
				} catch (SchedulerException e1) {
					e1.printStackTrace();
				}
			}
		});
		btnStart.setBackground(Color.WHITE);
		btnStart.setBounds(471, 9, 89, 23);
		contentPane.add(btnStart);
		
		btnStop = new JButton("\u505C\u6B62");
		btnStop.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent arg0) {
				try {
					sched.shutdown();
				} catch (SchedulerException e1) {
					e1.printStackTrace();
				}
			}
		});
		btnStop.setBackground(Color.WHITE);
		btnStop.setBounds(570, 9, 79, 23);
		contentPane.add(btnStop);
	}
	
	public void crawling(){
		succeed = 0;
		failed = 0;
		while(succeed < pages)
		{
			if(LinkQueue.unVisitedUrl.isEmpty()){
				break;
			}
			url = LinkQueue.unVisitedUrl.poll();
			LinkFilter linkFilter = new LinkFilter(){
				public boolean accept(String url) {
					if(rules[0].equals("")){
						return true;
					}
					else{
						for(int i = 0; i < rules.length; i++){
							if(rules[i].equals("")){
								continue;
							}
							if(url.contains(rules[i])){
								return true;
							}
						}
						return false;			
					}
				}
			};
			KeywordFilter keywordFilter = new KeywordFilter(keywords);
			time = System.currentTimeMillis();
			DownLoadFile_N downloader = new DownLoadFile_N(progressBar, connectServer, url, linkFilter, keywordFilter, htmlFlag, pdfFlag, pptFlag, docFlag);
			downloader.start();
			System.out.println(url);
		}
		
	}
	
	@SuppressWarnings("unchecked")
	public void execute(JobExecutionContext arg0) throws JobExecutionException {
		JobDataMap data = arg0.getJobDetail().getJobDataMap();
		dtmSeeds = (DefaultTableModel)data.get("seeds");
		dlmUnVisited = (DefaultListModel<String>)data.get("unvisited");
		completeFlag = (boolean)data.get("flag");
		System.out.println("Now Start!" + dtmSeeds.getRowCount());
		if(dtmSeeds.getRowCount() != 0 && completeFlag){
			dlmUnVisited.addElement((String) dtmSeeds.getValueAt(0, 0));
		}
	}
}
