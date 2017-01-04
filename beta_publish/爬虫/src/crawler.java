import java.io.File;
import java.io.IOException;
import java.util.HashMap;
import java.util.LinkedList;
import java.util.Map;
import java.util.Set;
import java.util.UUID;
import java.util.regex.Pattern;

import com.google.common.io.Files;

import edu.uci.ics.crawler4j.crawler.Page;
import edu.uci.ics.crawler4j.crawler.WebCrawler;
//import edu.uci.ics.crawler4j.parser.BinaryParseData;
import edu.uci.ics.crawler4j.parser.HtmlParseData;
import edu.uci.ics.crawler4j.url.WebURL;

/**
 * @author Yasser Ganjisaffar
 */

/*
 * This class shows how you can crawl images on the web and store them in a
 * folder. This is just for demonstration purposes and doesn't scale for large
 * number of images. For crawling millions of images you would need to store
 * downloaded images in a hierarchy of folders
 */
public class crawler extends WebCrawler {

    private static final Pattern filters = Pattern.compile(
        ".*(\\.(css|js|mid|mp2|mp3|mp4|wav|avi|mov|mpeg|ram|m4v|pdf" +
        "|rm|smil|wmv|swf|wma|zip|rar|gz))$");

    private static final Pattern imgPatterns = Pattern.compile(".*(\\.(bmp|gif|jpe?g|png|tiff?))$");
    private static final Pattern docPatterns = Pattern.compile(".*(\\.(doc|docx))$");
    private static final Pattern pdfPatterns = Pattern.compile(".*(\\.(pdf))$");
    public static int countHtml=0;
    public static int countPdf=0;
    public static int countDoc=0;
    public static int countImage=0;
    
    //private static final Pattern htmlPatterns = Pattern.compile(".*(\\.(doc|docx))$");
    public static DBHelper db = new DBHelper("10.2.28.78","XueBa","fileinfo","crawler","aimashi2015");
    
    private static File storageFolder;
    private static LinkedList<String> crawlDomains;
    
    public static void configure(LinkedList<String> domain, String storageFolderName) {
        crawlDomains =  domain;
        
        storageFolder = new File(storageFolderName);
        if (!storageFolder.exists()) {
            storageFolder.mkdirs();
        }
    }

    @Override
    public boolean shouldVisit(Page referringPage, WebURL url) {
    	logger.info("Judging if visit: "+url);
        String href = url.getURL().toLowerCase();
        if (filters.matcher(href).matches()) {
            return false;
        }
        if (imgPatterns.matcher(href).matches()) {
            return true;
        }
        if(docPatterns.matcher(href).matches()){
        	return true;
        }
        if(pdfPatterns.matcher(href).matches()){
        	return true;
        }
        
        for (String domain : crawlDomains) {
            if (href.startsWith(domain)) {
                return true;
            }
        }
        return false;
    }

    @Override
    public void visit(Page page) {
        String url = page.getWebURL().getURL();
        logger.info("Visting! : "+url);
        
        if (page.getParseData() instanceof HtmlParseData) {
        	crawler.countHtml++;
            HtmlParseData htmlParseData = (HtmlParseData) page.getParseData();
            String text = htmlParseData.getText();
            String html = htmlParseData.getHtml();
            Set<WebURL> links = htmlParseData.getOutgoingUrls();
            
            String hashedName = UUID.randomUUID() + ".html";
            String filename = storageFolder.getAbsolutePath() + "/" + hashedName;
            try {
                Files.write(page.getContentData(), new File(filename));
                logger.info("Stored: {}", url);
                //logger.info("HIHI");
            } catch (IOException iox) {
                logger.error("Failed to write file: " + filename, iox);
            }
            
            //try to write to DB
            
    		Map<String,Object> map = new HashMap<String,Object>();
    		map.put("filetype", "html");
    		map.put("filepath", filename);
    		map.put("url", url);
    		map.put("encode", "utf-8");
    		map.put("isDeal", "0");
    		db.insertline(map);
    		//db.release();
            
            System.out.println("Text length: " + text.length());
            System.out.println("Html length: " + html.length());
            System.out.println("Number of outgoing links: " + links.size());
        }
        
        
        // We are only interested in processing images which are bigger than 10k
        Map<String,Object> map = new HashMap<String,Object>();
		
        if(imgPatterns.matcher(url).matches() && page.getContentData().length >= (10 * 1024)){
        	crawler.countImage++;
        	map.put("filetype", "image");
        }
        else if(docPatterns.matcher(url).matches()){
        	crawler.countDoc++;
        	map.put("filetype", "doc");
        }
        else if(pdfPatterns.matcher(url).matches()){
        	crawler.countPdf++;
        	map.put("filetype", "pdf");
        }else{
        	return;
        }

        // get a unique name for storing this image
        String extension = url.substring(url.lastIndexOf('.'));
        String hashedName = UUID.randomUUID() + extension;

        // store image
        String filename = storageFolder.getAbsolutePath() + "/" + hashedName;
        try {
            Files.write(page.getContentData(), new File(filename));
            logger.info("Stored: {}", url);
            //logger.info("HIHI");
        } catch (IOException iox) {
            logger.error("Failed to write file: " + filename, iox);
        }
        map.put("filepath", filename);
		map.put("url", url);
		map.put("encode", "utf-8");
		map.put("isDeal", "0");
		db.insertline(map);
    }
}