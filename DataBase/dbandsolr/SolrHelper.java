package dbandsolr;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Collection;
import java.util.Map;

import org.apache.solr.client.solrj.SolrQuery;
import org.apache.solr.client.solrj.SolrServer;
import org.apache.solr.client.solrj.SolrServerException;
import org.apache.solr.client.solrj.beans.Field;
import org.apache.solr.client.solrj.impl.BinaryRequestWriter;
import org.apache.solr.client.solrj.impl.HttpSolrServer;
import org.apache.solr.client.solrj.response.QueryResponse;
import org.apache.solr.common.SolrDocument;
import org.apache.solr.common.SolrDocumentList;
import org.apache.solr.common.SolrInputDocument;

/*
 * solr操作类，可以对solr进行查找，插入，删除操作
 */
public class SolrHelper {
	String ip;
	String port;
	private HttpSolrServer solr;
	
	/*
	 * 构造方法
	 * @prama:solr的ip和port。这次作业是ip:10.2.28.82,port:8080
	 */
	public SolrHelper(String ip,String port) 
	{
		this.ip = ip;
		this.port = port;
		String url = "http://" + ip + ":" + port + "/solr/";
		solr = new HttpSolrServer(url);
	}
	
	/*
	 * 查询方法
	 * @prama:保存着属性名(String)，和属性值(Object)的map，此为查询条件;查询返回的最大行数row;希望返回的属性fields，可输入多个
	 * @return:保存着查询结果的SolrDocumentList，用法类似ArrayList;里面元素类型为SolrDocument，用法：SolrDocument.getValue(属性名)
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
	 * 插入方法
	 * @prama:保存着属性名(String)，和属性值(Object)的map
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
		} catch (SolrServerException | IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}	
	}
	
	/*
	 *插入方法
	 *@prama：要传导solr的属性（暂定）
	 */
	public void insert(String id,String title,String links,String date,String content,String[] keywords, String author,String doc_type,String question_content,String[] answer_content) {
		SolrInputDocument doc = new SolrInputDocument();
		if(id != null) {
			doc.addField("id", id);			
		}
		if(title != null) {
			doc.addField("title",title);			
		}
		if(links != null) {
			doc.addField("links",links);			
		}
		if(date != null) {
			doc.addField("creation_date",date);			
		}
		if(content != null) {
			doc.addField("content", content);			
		}
		if(keywords != null) {
			for(int i = 0;i < keywords.length;i++) {
				doc.addField("keywords", keywords[i]);		
			}
		}
		if(author != null) {
			doc.addField("author",author);			
		}
		if(doc_type != null) {
			doc.addField("doc_type",doc_type);			
		}
		if(question_content != null) {
			doc.addField("question_content",question_content);			
		}
		if(answer_content != null) {
			for(int i = 0;i < answer_content.length;i++) {
				doc.addField("answer_content",answer_content[i]);
			}			
		}
		try {
			solr.add(doc);			
		} catch (SolrServerException | IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}	
	}
	
	/*
	 * 删除方法
	 * @prama:要删除的doc的id
	 */
	public void delete(String id) {
		try {
			solr.deleteById(id);			
		} catch (SolrServerException | IOException e) {
			e.printStackTrace();
		}
		
	}
	
	/*
	 * 提交改动
	 * insert,delete操作结束后要commit才会生效
	 */
	public void commitchange() {
		try {
			solr.commit();
		} catch (SolrServerException | IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

	
}
