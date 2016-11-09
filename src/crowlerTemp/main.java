package crowlerTemp;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import edu.uci.ics.crawler4j.crawler.CrawlConfig;
import edu.uci.ics.crawler4j.crawler.CrawlController;
import edu.uci.ics.crawler4j.fetcher.PageFetcher;
import edu.uci.ics.crawler4j.robotstxt.RobotstxtConfig;
import edu.uci.ics.crawler4j.robotstxt.RobotstxtServer;

public class main {
	private static final Logger logger = LoggerFactory.getLogger(main.class);
	public static void main(String[] args) throws Exception{


        String rootFolder = "D:/trash/root";
        int numberOfCrawlers = 3;
        String storageFolder = "D:/trash/storage";

        CrawlConfig config = new CrawlConfig();

        config.setCrawlStorageFolder(rootFolder);

    /*
     * Since images are binary content, we need to set this parameter to
     * true to make sure they are included in the crawl.
     */
        config.setIncludeBinaryContentInCrawling(true);

        String[] crawlDomains = {"http://uci.edu/"};

        PageFetcher pageFetcher = new PageFetcher(config);
        RobotstxtConfig robotstxtConfig = new RobotstxtConfig();
        RobotstxtServer robotstxtServer = new RobotstxtServer(robotstxtConfig, pageFetcher);
        CrawlController controller = new CrawlController(config, pageFetcher, robotstxtServer);
        for (String domain : crawlDomains) {
            controller.addSeed(domain);
        }

        crawler.configure(crawlDomains, storageFolder);

        controller.start(crawler.class, numberOfCrawlers);
	}
}
