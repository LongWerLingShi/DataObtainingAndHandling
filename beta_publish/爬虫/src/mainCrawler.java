//import org.slf4j.Logger;
//import org.slf4j.LoggerFactory;

//import edu.uci.ics.crawler4j.crawler.CrawlConfig;
//import edu.uci.ics.crawler4j.crawler.CrawlController;
//import edu.uci.ics.crawler4j.fetcher.PageFetcher;
//import edu.uci.ics.crawler4j.robotstxt.RobotstxtConfig;
//import edu.uci.ics.crawler4j.robotstxt.RobotstxtServer;

public class mainCrawler {
	//private static final Logger logger = LoggerFactory.getLogger(mainCrawler.class);
	
    public static void main(String[] args) throws Exception {
    	
        //Controller cont = new Controller();
        //cont.addSeed("http://uci.edu/");
        ///cont.addSeed("http://news.sohu.com/");
        //cont.setNumOfCrawler(3);
        //Thread.sleep(10000);
        //logger.info("-------------------------------STOP");
    	FileListener fl = new FileListener();
    	fl.start();
    }
}
