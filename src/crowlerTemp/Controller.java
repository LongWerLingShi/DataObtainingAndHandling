package crowlerTemp;

import java.util.Calendar;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import edu.uci.ics.crawler4j.crawler.CrawlConfig;
import edu.uci.ics.crawler4j.crawler.CrawlController;
import edu.uci.ics.crawler4j.fetcher.PageFetcher;
import edu.uci.ics.crawler4j.robotstxt.RobotstxtConfig;
import edu.uci.ics.crawler4j.robotstxt.RobotstxtServer;


public class Controller {
	private static final Logger logger = LoggerFactory.getLogger(main.class);
	private int curNumOfCrawler; //��ǰ��������
	private Calendar startTime; //���¿�ʼ��ʱ�� ���û�������о�Ϊnull
	private CrawlConfig config; //crawler ����
	private PageFetcher pageFetcher; //��ȡ�ļ���
	RobotstxtConfig robotstxtConfig; //robottxt����
	RobotstxtServer robotstxtServer; //���robotЭ�����
	private LinkedList<String> Seeds; //��ǰ������ַ
	
	public Controller(){
		config = new CrawlConfig();
		config.setCrawlStorageFolder("D:\\root");
        config.setIncludeBinaryContentInCrawling(true);
        pageFetcher = new PageFetcher(config);
        robotstxtConfig = new RobotstxtConfig();
        robotstxtServer = new RobotstxtServer(robotstxtConfig, pageFetcher);
        Seeds = new LinkedList<String>();
	}
	CrawlController controller;
	/*
	 * ���ص�ǰ�����õ������߳�����
	 */
	public int getCurNumOfCrawler(){
		return this.curNumOfCrawler;
	}
	/*
	 * ���õ�ǰ������߳�����, ����ɹ�����true����������� ����false
	 * ��������Ϊ0-100֮�������
	 */
	public boolean setNumOfCrawler(int aim) throws Exception{
		if(aim<=100 && aim>=0){
			
			if(aim==0){
				//stop
				startTime = null;
				if(controller == null){
					//none
				}else{
					controller.shutdown();
				}
			}else{
				//start
				if(this.curNumOfCrawler == aim){
					//������ͬ ��������
				}else{
					if(controller == null){
						//none
					}else{
						controller.shutdown();
					}
					startTime = Calendar.getInstance();
					//��ֹͣ�����¿�ʼ
					controller = new CrawlController(config, pageFetcher, robotstxtServer);
			        
			        //String[] crawlDomains = {"http://uci.edu/"};
					Iterator<String> it = Seeds.iterator();
					while(it.hasNext()){
						this.controller.addSeed(it.next());
					}
					controller.start(crawler.class, aim);
				}
			}
			this.curNumOfCrawler = aim;
			return true;
		}else{
			return false;
		}
	}
	/*
	 * �������濪ʼ��ʱ��
	 */
	public Calendar getStartTime(){
		return this.startTime;
	}
	/*
	 * ������ȡ��html����
	 */
	public int getHtmlNum(){
		return 0;
	}
	/*
	 * ������ȡ��Pdf����
	 */
	public int getPdfNum(){
		return 0;
	}/*
	 * ������ȡ��doc����
	 */
	public int getDocNum(){
		return 0;
	}
	/*
	 * ������ȡ��image����
	 */
	public int getImageNum(){
		return 0;
	}
	/*
	 * ����������ַ
	 */
	public void  addSeed(String seed){
		Seeds.add(seed);
	}
	/*
	 * ɾ��������ַ ɾ���ɹ�����true��ɾ��ʧ��false
	 */
	public boolean  delSeed(String seed){
		int i=0;
		while(i<Seeds.size()){
			if(Seeds.get(i).equals(seed)){
				Seeds.remove(i);
				return true;
			}
		}
		return false;
	}
	/*
	 * ɾ��������ַ ɾ���ɹ�����true��ɾ��ʧ��false
	 */
	public LinkedList<String> getSeeds(){
		return Seeds;
	}
	/*
	 * ����KeyWord
	 */
	public void  addKeyWord(String seed){
		return;
	}
	/*
	 * ɾ��KeyWord ɾ���ɹ�����true��ɾ��ʧ��false
	 */
	public boolean  delKeyWord(String keyWord){
		return false;
	}
	/*
	 * ɾ��KeyWord ɾ���ɹ�����true��ɾ��ʧ��false
	 */
	public List<String> getKeyWord(){
		return null;
	}
	
	
	
}
