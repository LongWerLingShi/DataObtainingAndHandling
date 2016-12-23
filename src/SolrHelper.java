//package dbandsolr;

import java.io.IOException;
import java.util.Map;

import org.apache.solr.client.solrj.SolrQuery;
import org.apache.solr.client.solrj.SolrServerException;
import org.apache.solr.client.solrj.beans.Field;
import org.apache.solr.client.solrj.impl.BinaryRequestWriter;
import org.apache.solr.client.solrj.impl.HttpSolrServer;
import org.apache.solr.client.solrj.response.QueryResponse;
import org.apache.solr.common.SolrDocumentList;
import org.apache.solr.common.SolrInputDocument;


/*
 * solr鎿嶄綔绫伙紝鍙互瀵箂olr杩涜鏌ユ壘锛屾彃鍏ワ紝鍒犻櫎鎿嶄綔
 */
public class SolrHelper {
	String ip;
	String port;
	private HttpSolrServer solr;
	
	/*
	 * 鏋勯�犳柟娉�
	 * @prama:solr鐨刬p鍜宲ort銆傝繖娆′綔涓氭槸ip:10.2.28.82,port:8080
	 */
	public SolrHelper(String ip,String port) 
	{
		this.ip = ip;
		this.port = port;
		String url = "http://" + ip + ":" + port + "/solr/";
		solr = new HttpSolrServer(url);
	}
	
	/*
	 * 鏌ヨ鏂规硶
	 * @prama:淇濆瓨鐫�灞炴�у悕(String)锛屽拰灞炴�у��(Object)鐨刴ap锛屾涓烘煡璇㈡潯浠�;鏌ヨ杩斿洖鐨勬渶澶ц鏁皉ow;甯屾湜杩斿洖鐨勫睘鎬ields锛屽彲杈撳叆澶氫釜
	 * @return:淇濆瓨鐫�鏌ヨ缁撴灉鐨凷olrDocumentList锛岀敤娉曠被浼糀rrayList;閲岄潰鍏冪礌绫诲瀷涓篠olrDocument锛岀敤娉曪細SolrDocument.getValue(灞炴�у悕)
	 */
	public SolrDocumentList query(Map<String,Object> map, int row, String... fields)
	{
		String attribute;
		Object value;
		String valuestr;
		SolrQuery query = new SolrQuery();
		SolrDocumentList list = null;
		int num = 0;
		for(Map.Entry<String, Object> pair : map.entrySet()) {
			valuestr = "";
			attribute = pair.getKey();
			value = pair.getValue();
			valuestr += value.toString();
			if(num == 0) {
				query.setQuery(attribute + ":" + valuestr);
			}
			else {
				query.setFilterQueries(attribute + ":" + valuestr);
			}
			num++;
		}
		if(num == 0) {
			return null;
		}
		query.setFields(fields);
		if(row > 0) {
			query.setRows(row);
		}
		else {
			return null;
		}
		try {
			QueryResponse response = solr.query(query);
			list = response.getResults();
		} catch (SolrServerException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	    return list;
	}
	
	/*
	 * 鎻掑叆鏂规硶
	 * @prama:淇濆瓨鐫�灞炴�у悕(String)锛屽拰灞炴�у��(Object)鐨刴ap
	 */
	public void insert(Map<String, Object> map) {
		SolrInputDocument doc = new SolrInputDocument();
		if(map == null) {
			return;
		}
		for(Map.Entry<String, Object> pair : map.entrySet()) {
			doc.addField(pair.getKey(), pair.getValue());
		}
		try {
			solr.add(doc);
			solr.commit();
		} catch (SolrServerException | IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}	
	}
	
	/*
	 *鎻掑叆鏂规硶
	 *@prama锛氳浼犲solr鐨勫睘鎬э紙鏆傚畾锛�
	 */
	public void insert(String id,String title,String links,String date,String content,String author) {
		SolrData sd = new SolrData();
		sd.id = id;
		sd.title = title;
		sd.links = links;
		sd.creation_date = date;
		sd.content = content;
		sd.author = author;
		solr.setRequestWriter(new BinaryRequestWriter());
		try {
			solr.addBean(sd);
			solr.optimize();
		} catch (IOException | SolrServerException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
	/*
	 * 鍒犻櫎鏂规硶
	 * @prama:瑕佸垹闄ょ殑doc鐨刬d
	 */
	public void delete(String id) {
		try {
			solr.deleteById(id);
			solr.commit();
		} catch (SolrServerException | IOException e) {
			e.printStackTrace();
		}
		
	}
	
	class SolrData
	{
		@Field
		String id;
		@Field
		String title;
		@Field
		String links;
		@Field
		String creation_date;
		@Field
		String content;
		@Field
		String author;
		
	}
}
