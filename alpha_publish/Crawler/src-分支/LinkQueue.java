import java.util.HashSet;
import java.util.Hashtable;
import java.util.LinkedList;
import java.util.Set;
import java.util.Queue;
import java.util.concurrent.ConcurrentLinkedQueue;


/**
* description: 管理所有待访问和已访问的网址链接
* modificationDate: 2015-12-29
*/ 
public class LinkQueue {
	//已访问的 url 集合
	public static Set<String> visitedUrl = new HashSet<String>();
	//待访问的 url 集合
	public static ConcurrentLinkedQueue<String> unVisitedUrl = new ConcurrentLinkedQueue<String>();
	//seed url集合
	public static Queue<String> seedUrls = new LinkedList<String>();
	/**
	 * 添加未访问的Url入队
	 * @return 
	 * @param url(RankUrl),inUrl(RankUrl)
	 * @throws 
	 */

	// 保证每个 url 只被访问一次
	public static void addUnvisitedUrl(String url) {
		boolean unvisitedFlag = false;
		for (String uUrl : visitedUrl)
			if(uUrl.equals(url)) {
				unvisitedFlag = true;
				break;
			}
		synchronized(unVisitedUrl){
			if(!unvisitedFlag){
				unVisitedUrl.add(url);
				MyCrawler.dlmUnVisited.addElement(url);
			}
		}
	}
}
