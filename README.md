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
* 
* 

###杨金键
* 
* 

###金豪
* 
* 
