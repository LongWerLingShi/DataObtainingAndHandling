import java.util.HashSet;
import java.util.Hashtable;
import java.util.LinkedList;
import java.util.Set;
import java.util.Queue;
import java.util.concurrent.ConcurrentLinkedQueue;


/**
* description: �������д����ʺ��ѷ��ʵ���ַ����
* modificationDate: 2015-12-29
*/ 
public class LinkQueue {
	//�ѷ��ʵ� url ����
	public static Set<String> visitedUrl = new HashSet<String>();
	//�����ʵ� url ����
	public static ConcurrentLinkedQueue<String> unVisitedUrl = new ConcurrentLinkedQueue<String>();
	//seed url����
	public static Queue<String> seedUrls = new LinkedList<String>();
	/**
	 * ���δ���ʵ�Url���
	 * @return 
	 * @param url(RankUrl),inUrl(RankUrl)
	 * @throws 
	 */

	// ��֤ÿ�� url ֻ������һ��
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
