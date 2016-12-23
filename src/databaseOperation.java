import java.util.Map;

public interface databaseOperation {
	String getnotdealed(String id,String username,String password);
	void insert(String id,String title,String links,String date,String content,String author);
	void insert(Map<String, Object> map);
	void cleardealedtag(String ip,String username,String password);
	int queryHaveDone(String ip,String username,String password,String type);
	int queryTitle(String ip,String username,String password,String type);
	int queryHaveDone(String ip,String username,String password);
	int queryTitle(String ip,String username,String password);
	int queryHandleFail(String ip,String username,String password);
	Map<String, Object> handleWord(String URL,int threadNo);
}
