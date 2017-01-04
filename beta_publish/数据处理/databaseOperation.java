import java.util.Map;

public interface databaseOperation {
	int getnotdealed(String id,String username,String password);
	void insert(Map<String, Object> map);
	void cleardealedtag(String ip,String username,String password);
	int queryHaveDone(String ip,String username,String password,String type);
	int queryTitle(String ip,String username,String password,String type);
	int queryHaveDone(String ip,String username,String password);
	int queryTitle(String ip,String username,String password);
	int queryHandleFail(String ip,String username,String password);
	Map<String, Object> handleWord(String URL,String WEB,int threadNo);
	Map<String, Object> handlePdfOrHTML(String URL,String WEB);
	String querybyid(int id,String ip,String username,String password);
	String queryURlbyid(int id,String ip,String username,String password);
	void update(int id,Map<String,Object> map,String ip,String username,String password);
	boolean DeleteFileFromLocal(String localpath, String localfilename);
	boolean DeleteFileFromLocal(String localfullpath);
	String getFromNetWorkConnection(String URL, String ip,String localpath, String username, String password);
}
