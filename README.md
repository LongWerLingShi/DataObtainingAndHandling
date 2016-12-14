<<<<<<< HEAD
#注意！
##在代码协作中， 尤其是在遇到接口方面的问题需要与调用者或者被调用者协商的情况下，请将需要协商的内容写在下面
###崔正龙
```
=======
# DataObtainingAndHandling

>>>>>>> refs/remotes/origin/word
去噪模块接口word部分：

Public string denoiseWord(string path);

Require:传入文件地址path

Effect:word文件降噪，返回降噪后的文本

Modified:无

<<<<<<< HEAD
=======

>>>>>>> refs/remotes/origin/word
提取关键词模块：

Public string getKey(string doc);

Require:传入需要提取关键词的文本

Effect:提取关键词，并且翻译关键词，将文本中提取到的关键词和翻译后的写成字符串返回

Modified:无

<<<<<<< HEAD
=======

>>>>>>> refs/remotes/origin/word
Private string segmentWord(string doc);

Require:传入降噪后的文本

Effect:对文本分词，并且将根据分词后的结果提取关键词

Modified:无

<<<<<<< HEAD
=======

>>>>>>> refs/remotes/origin/word
Private string translate(string key);

Require:传入提取到的关键词

Effect:翻译关键词，将关键词和翻译后的文本一起返回

Modified:无
<<<<<<< HEAD
```
###谷大鑫
* 
* 

###谢振威
```java
/*
 * 数据库管理类，可以对数据库进行查询，更新，插入，删除操作
 * 使用需求：在src中导入sqljdbc4.jar,
 * 下载地址：https://www.microsoft.com/zh-cn/download/details.aspx?id=11774
 * 下载sqljdbc_4.0.2206.100_chs.tar.gz
 */
class DBHelper
{

  /*
   * 构造方法
   * @prama：服务器ip，数据库名称basename，表名tablename, 用户名username，密码password
   */
  public DBHelper(String ip,String basename, String tableName, String username,String password);

  /*
   * 查询方法
   * 根据传入的属性值查询，返回查询结果
   * @prama:一个保存着属性名(String)，属性值(Object)的map,count为返回的最大条数，小于等于0时视为返回全部。
   * @return:保存着查询结果的ResultSet，类似一个表，具体用法可以看我的例子或者查看java api(java.sql.ResultSet)
   */
  public ResultSet query(int count, Map<String, Object> map);

  /*
   * 更新数据
   * 根据id，更新该表项数据
   * @prama:要更新的表项的id，一个保存着属性名(String)，属性值(Object)的map
   * @return：修改是否成功，成功为1，失败为0
   */
  public int update(int id,Map<String,Object> map);

  /*
   * 插入新的一行
   * 新建一行，并且根据map提供的属性名和属性值在该行的相应属性栏填入数据
   * @prama:一个保存着属性名(String)，属性值(Object)的map
   * @return:
   */
  public void insertline(Map<String,Object> map);

  /*
   * 删除行
   * 根据map提供的属性名和属性值删除符合条件的行
   * @prama:一个保存着属性名(String)，属性值(Object)的map
   * @return:删除的行数
   */
  public int delete(Map<String,Object> map);

  /*
   * 释放资源
   * 若此DBHelper不再使用，记得一定要调用一次,不然用久了资源耗尽就会挂
   */
  public void release();
}


/*
 * solr操作类，可以对solr进行查找，插入，删除操作
 *
 */
class SolrHelper
{
  /*
   * 构造方法
   * @prama:solr的ip和port。这次作业是ip:10.2.28.82,port:8080
   */
  public SolrHelper(String ip,String port);

  /*
   * 查询方法
   * @prama:保存着属性名(String)，和属性值(Object)的map，此为查询条件;查询返回的最大行数row;希望返回的属性fields，可输入多个
   * @return:保存着查询结果的SolrDocumentList，用法类似ArrayList;里面元素类型为SolrDocument，用法：SolrDocument.getValue(属性名)
   */
  public SolrDocumentList query(Map<String,Object> map, int row, String... fields);

  /*
   * 插入方法
   * @prama:保存着属性名(String)，和属性值(Object)的map
   */
  public void insert(Map<String, Object> map);

  /*
   *插入方法
   *@prama：要传导solr的属性（暂定）
   */
  public void insert(String id,String title,String links,String date,String content,String author);

  /*
   * 删除方法
   * @prama:要删除的doc的id
   */
  public void delete(String id);
}
```
于2016/11/20


###杨金键
*
* 
*

###金豪
```
/*
 * 爬虫管理类，初始化设置线程数即可开始工作，自动保存到数据库里面
 */
public class Controller {
	/*
	 * 返回当前所设置的爬虫线程数量
	 */
	public int getCurNumOfCrawler()
	/*
	 * 设置当前爬虫的线程数量, 如果成功返回true，如果有问题 返回false
	 * 限制数量为0-100之间的整数
	 * 如果要关闭爬虫 就设置为0即可
	 * 重新启动目前还有一些问题 建议重新建立一个对象 进度可以保存
	 */
	public boolean setNumOfCrawler(int aim) throws Exception
	/*
	 * 返回爬虫开始的时间
	 */
	public Calendar getStartTime()
	/*
	 * 返回爬取的html数量
	 */
	public int getHtmlNum()
	/*
	 * 返回爬取的Pdf数量
	 */
	public int getPdfNum()/*
	 * 返回爬取的doc数量
	 */
	public int getDocNum()
	/*
	 * 返回爬取的image数量
	 */
	public int getImageNum()
	/*
	 * 增加种子网址
	 */
	public void  addSeed(String seed)
	/*
	 * 删除种子网址 删除成功返回true，删除失败false
	 */
	public boolean  delSeed(String seed)
	/*
	 * 删除种子网址 删除成功返回true，删除失败false
	 */
	public LinkedList<String> getSeeds()
	/*
	 * 增加KeyWord
	 */
	public void  addKeyWord(String seed)
	/*
	 * 删除KeyWord 删除成功返回true，删除失败false
	 */
	public boolean  delKeyWord(String keyWord)
	/*
	 * 删除KeyWord 删除成功返回true，删除失败false
	 */
	public List<String> getKeyWord()
```
=======



