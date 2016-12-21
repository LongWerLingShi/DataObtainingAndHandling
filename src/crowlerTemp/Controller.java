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
	private int curNumOfCrawler; //当前爬虫数量
	private Calendar startTime; //重新开始的时间 如果没有在运行就为null
	private CrawlConfig config; //crawler 配置
	private PageFetcher pageFetcher; //爬取文件的
	RobotstxtConfig robotstxtConfig; //robottxt配置
	//RobotstxtServer robotstxtServer; //检测robot协议相关
	RobotstxtServer_son robotstxtServer;
	private LinkedList<String> Seeds; //当前种子网址
	
	public Controller(){
		config = new CrawlConfig();
		config.setCrawlStorageFolder("D:\\root");
        config.setIncludeBinaryContentInCrawling(true);
        pageFetcher = new PageFetcher(config);
        robotstxtConfig = new RobotstxtConfig();
        //robotstxtServer = new RobotstxtServer(robotstxtConfig, pageFetcher);
        robotstxtServer = new RobotstxtServer_son(robotstxtConfig, pageFetcher);

        Seeds = new LinkedList<String>();
	}
	CrawlController controller;
	/*
	 * 返回当前所设置的爬虫线程数量
	 */
	public int getCurNumOfCrawler(){
		return this.curNumOfCrawler;
	}
	/*
	 * 设置当前爬虫的线程数量, 如果成功返回true，如果有问题 返回false
	 * 限制数量为0-100之间的整数
	 */
	public boolean setNumOfCrawler(int aim) throws Exception{
		if(aim<=100 && aim>=0){
			
			if(aim==0){
				//stop
				startTime = null;
				if(controller == null){
					//none
				}else{
					//crawler.db.release();
					controller.shutdown();
					controller.waitUntilFinish();
				}
			}else{
				//start
				if(this.curNumOfCrawler == aim){
					//数量相同 不做处理
				}else{
					if(controller == null){
						//none
					}else{
						//crawler.db.release();
						controller.shutdown();
						controller.waitUntilFinish();
					}
					startTime = Calendar.getInstance();
					//先停止后重新开始
					controller = new CrawlController(config, pageFetcher, robotstxtServer);
			        
			        //String[] crawlDomains = {"http://uci.edu/"};
					Iterator<String> it = Seeds.iterator();
					while(it.hasNext()){
						this.controller.addSeed(it.next());
					}
					//controller.start(crawler.class, aim);
					crawler.configure(Seeds, "D:\\storage");
					controller.startNonBlocking(crawler.class, aim);

				}
			}
			this.curNumOfCrawler = aim;
			return true;
		}else{
			return false;
		}
	}
	/*
	 * 返回爬虫开始的时间
	 */
	public Calendar getStartTime(){
		return this.startTime;
	}
	/*
	 * 返回爬取的html数量
	 */
	public int getTotNum(){
		return crawler.countHtml+crawler.countDoc+crawler.countImage+crawler.countPdf;
	}
	public int getHtmlNum(){
		return crawler.countHtml;
	}
	/*
	 * 返回爬取的Pdf数量
	 */
	public int getPdfNum(){
		return crawler.countPdf;
	}/*
	 * 返回爬取的doc数量
	 */
	public int getDocNum(){
		return crawler.countDoc;
	}
	/*
	 * 返回爬取的image数量
	 */
	public int getImageNum(){
		return crawler.countImage;
	}
	/*
	 * 增加种子网址
	 */
	public void  addSeed(String seed){
		Seeds.add(seed);
	}
	/*
	 * 删除种子网址 删除成功返回true，删除失败false
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
	 * 删除种子网址 删除成功返回true，删除失败false
	 */
	public LinkedList<String> getSeeds(){
		return Seeds;
	}
	/*
	 * 增加KeyWord
	 */
	public void  addKeyWord(String seed){
		return;
	}
	/*
	 * 删除KeyWord 删除成功返回true，删除失败false
	 */
	public boolean  delKeyWord(String keyWord){
		return false;
	}
	/*
	 * 删除KeyWord 删除成功返回true，删除失败false
	 */
	public List<String> getKeyWord(){
		return null;
	}
	
	
	
}
