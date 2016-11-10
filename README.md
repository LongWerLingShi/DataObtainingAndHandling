#注意！
##在代码协作中， 尤其是在遇到接口方面的问题需要与调用者或者被调用者协商的情况下，请将需要协商的内容写在下面
###崔正龙
```
去噪模块接口word部分：

Public string denoiseWord(string path);

Require:传入文件地址path

Effect:word文件降噪，返回降噪后的文本

Modified:无

提取关键词模块：

Public string getKey(string doc);

Require:传入需要提取关键词的文本

Effect:提取关键词，并且翻译关键词，将文本中提取到的关键词和翻译后的写成字符串返回

Modified:无

Private string segmentWord(string doc);

Require:传入降噪后的文本

Effect:对文本分词，并且将根据分词后的结果提取关键词

Modified:无

Private string translate(string key);

Require:传入提取到的关键词

Effect:翻译关键词，将关键词和翻译后的文本一起返回

Modified:无
```
###谷大鑫
* 
* 

###谢振威
```java
/*
 * 数据库管理类，可以对数据库进行查询，更新，插入，删除操作
 * 食用需求：在src中导入sqljdbc4.jar,
 * 下载地址：https://www.microsoft.com/zh-cn/download/details.aspx?id=11774
 * 下载sqljdbc_4.0.2206.100_chs.tar.gz
 */
class DBHelper
{
  private String TableName = ?; //因为之前商量说数据库只有一张表，所以构造时不用传表名，在这里定义一下即可

  /*
   * 构造方法
   * @prama：服务器ip，数据库名称basename，用户名username，密码password
   */
  public DBHelper(String ip,String basename,String username,String password);

  /*
   * 查询方法
   * 根据传入的属性值查询，返回查询结果
   * @prama:一个保存着属性名(String)，属性值(Object)的map
   * @return:保存着查询结果的ResultSet，类似一个表，具体用法可以看我的例子或者查看java api(java.sql.ResultSet)
   */
  public ResultSet query(Map<String, Object> map);

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
```
* 


###杨金键
*
* 
*

###金豪
* 
* 
