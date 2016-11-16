using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using HtmlAgilityPack;


namespace Pipeline
{
    class ProcessProcedure
    {
	 public static void Processwrd(string doc,string doc2, bool needWriteToDB)
        {
            MainWindow._rawdata = doc;
            MainWindow._denoisingdata = doc2;
            if (MainWindow._denoisingdata == null)
            {
                MainWindow._denoisingdata = "";
            }
            WordSegment ws = new WordSegment();
            MainWindow._wordSegmentResult = ws.cutwords(MainWindow._denoisingdata, WordSegmentData.preWordSegmentor);
            foreach (string word in MainWindow._wordSegmentResult)
            {
                if (MainWindow.wordFreq.ContainsKey(word))
                {
                    int freq = (int)MainWindow.wordFreq[word];
                    MainWindow.wordFreq[word] = freq + 1;
                }
                else
                {
                    MainWindow.wordFreq.Add(word, 1);
                }
            }

            List<string> _tags = new List<string>();
            Hashtable tagPreview = new Hashtable();
            DataMining dm = new DataMining();
            dm.Rawdata = MainWindow._rawdata;
            dm.Denoisingdata = MainWindow._denoisingdata;
            dm.Work();
            MainWindow._title = dm.Title;
            MainWindow._author = dm.Author;
            MainWindow._postdate = dm.Postdate;
            if (needWriteToDB)
            {
                /*suiyuhao*/
                //save to table PAGE
                Page pg = new Page();
                PageItem pgitem = new PageItem();
                MainWindow.curwid = MainWindow.getWebpageNo() + 1;
                pgitem.setwid(MainWindow.curwid);
                //item1.settitle(MainWindow._title);
                //pgitem.settitle(translate(MainWindow._title));
                pgitem.settitle(MainWindow._title);
                pgitem.setauthor(MainWindow._author);
                pgitem.setpostdate(MainWindow._postdate);
                pgitem.setlink(MainWindow.curUrl);
                pgitem.setreferred("");
                pg.savePage(pgitem);

                //save to table TAGS
                foreach (string tag in _tags)
                {
                    if (!MainWindow._occurredTags.Contains(tag))
                    {
                        Tag thistag = new Tag();
                        TagItem tgitem = new TagItem();
                        tgitem.settid(MainWindow.getTagNo() + 1);
                        //MainWindow._occurredTags.Add(translate(tag));
                        MainWindow._occurredTags.Add(tag);
                        //tgitem.setname(translate(tag));
                        tgitem.setname(tag);
                        thistag.saveTag(tgitem);
                    }
                }

                foreach (string tag in _tags)
                {
                    Preview b = new Preview();
                    PreviewItem item3 = new PreviewItem();
                    //MainWindow.curtid = MainWindow.getTagNo(translate(tag));
                    MainWindow.curtid = MainWindow.getTagNo(tag);
                    item3.settid(MainWindow.curtid);
                    item3.setwid(MainWindow.curwid);
                    //item3.setpreview(translate((string)tagPreview[tag]));
                    item3.setpreview((string)tagPreview[tag]);
                    //item3.setpreview((string)tagPreview[tag]);
                    b.savePreview(item3);
                }
                /*suiyuhao*/
            }
        }

        //处理pdf文件
        public static void Processpdf(string path, bool needWriteToDB)   // @path:文件路径 @needWriteToDB:表示是否需要写入数据库
        {
            //  初始化一个用来去噪的类
            Denoising de = new Denoising();
            de.Path = path;
            de.Work();  //  work后就获得了原始数据，去噪数据等
            MainWindow._rawdata = de.Rawdata;  //  获得原始数据
            MainWindow._denoisingdata = de.Denoisingdata; // 获得去噪数据
            if (MainWindow._denoisingdata == null) //  如果得到的去噪数据是null，说明可能存在编码或其他一些问题
            {
                MainWindow._denoisingdata = "";
            }
            //  初始化一个用来分词的类，对去噪数据进行分词处理
            WordSegment ws = new WordSegment();
            //bool isCHineseWebPage = WebTesting(MainWindow._denoisingdata);
            MainWindow._wordSegmentResult = ws.cutwords(MainWindow._denoisingdata, WordSegmentData.preWordSegmentor);
            foreach (string word in MainWindow._wordSegmentResult)
            {
                if (MainWindow.wordFreq.ContainsKey(word))
                {
                    int freq = (int)MainWindow.wordFreq[word];
                    MainWindow.wordFreq[word] = freq + 1;
                }
                else
                {
                    MainWindow.wordFreq.Add(word, 1);
                }
            }
            /*if (isCHineseWebPage)   //  只对中文页面采用分词算法
            {
                MainWindow._wordSegmentResult = ws.DoWordSegment(MainWindow._denoisingdata);   //  使用分词算法进行分词

                //  对分词结果中每个词的词频进行统计
                string word;
                Regex r = new Regex(@"\{<([^>]*)>\}", RegexOptions.Multiline);
                MatchCollection matches = r.Matches(MainWindow._wordSegmentResult);
                foreach (Match match in matches)
                {
                    if (match.Success)
                    {
                        word = match.Groups[1].Value;
                        if (MainWindow.wordFreq.ContainsKey(word))
                        {
                            int freq = (int)MainWindow.wordFreq[word];
                            MainWindow.wordFreq[word] = freq + 1;
                        }
                        else
                        {
                            MainWindow.wordFreq.Add(word, 1);
                        }
                    }
                }
            }
            else    //  对英文页面，直接采用分隔符分离的方法
            {
                MainWindow._wordSegmentResult = MainWindow._denoisingdata;
                string[] words = MainWindow._wordSegmentResult.Split(new char[] {' ', '`', '~', '!', '@', '$', '%', 
                            '^', '&', '*','(', ')', '_', '+', '=', '{', '[', '}', ']', ':', ';', '\"', '\'', ',', 
                            '<', '.', '>', '?', '/', '\\', '\r', '\n', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'});
                foreach (string word in words)
                {
                    if (word.Length > 1)
                    {
                        if (MainWindow.wordFreq.ContainsKey(word.ToLower()))
                        {
                            int freq = (int)MainWindow.wordFreq[word.ToLower()];
                            MainWindow.wordFreq[word.ToLower()] = freq + 1;
                        }
                        else
                        {
                            MainWindow.wordFreq.Add(word.ToLower(), 1);
                        }
                    }
                }
            }*/

            //  数据挖掘
            //  先清空两个容器
            MainWindow. _tags = new List<string>();
            MainWindow.tagPreview = new Hashtable();
            //  初始化一个数据挖掘类的实例
            DataMining dm = new DataMining();
            //  获得原始数据和去噪数据
            dm.Rawdata = MainWindow._rawdata;
            dm.Denoisingdata = MainWindow._denoisingdata;
            //  开始工作
            dm.Work();
            //  获得标题，作者，发布日期，其实很不准确
            MainWindow._title = dm.Title;
            MainWindow._author = dm.Author;
            MainWindow._postdate = dm.Postdate;

            //  已经得到了想要的信息，开始
            //DB OPERATION
            //信息已经提取完成，存入数据库，包括tag webpage tag_webpage 3张表
            //请把新的tag存入tag表中，注意编号
            if (needWriteToDB)
            {
                /*suiyuhao*/
                //save to table PAGE
                Page pg = new Page();
                PageItem pgitem = new PageItem();
                MainWindow.curwid = MainWindow.getWebpageNo() + 1;
                pgitem.setwid(MainWindow.curtid);
                //item1.settitle(MainWindow._title);
                //pgitem.settitle(translate(MainWindow._title));
                pgitem.settitle(MainWindow._title);
                pgitem.setauthor(MainWindow._author);
                pgitem.setpostdate(MainWindow._postdate);
                pgitem.setlink(MainWindow.curUrl);
                pgitem.setreferred("");
                pg.savePage(pgitem);

                //save to table TAGS
                foreach (string tag in MainWindow._tags)
                {
                    if (!MainWindow._occurredTags.Contains(tag))
                    {
                        Tag thistag = new Tag();
                        TagItem tgitem = new TagItem();
                        tgitem.settid(MainWindow.getTagNo() + 1);
                        //MainWindow._occurredTags.Add(translate(tag));
                        MainWindow._occurredTags.Add(tag);
                        //tgitem.setname(translate(tag));
                        tgitem.setname(tag);
                        thistag.saveTag(tgitem);
                    }
                }

                foreach (string tag in MainWindow._tags)
                {
                    Preview b = new Preview();
                    PreviewItem item3 = new PreviewItem();
                    //MainWindow.curtid = MainWindow.getTagNo(translate(tag));
                    MainWindow.curtid = MainWindow.getTagNo(tag);
                    item3.settid(MainWindow.curtid);
                    item3.setwid(MainWindow.curwid);
                    //item3.setpreview(translate((string)tagPreview[tag]));
                    item3.setpreview((string)MainWindow.tagPreview[tag]);
                    //item3.setpreview((string)tagPreview[tag]);
                    b.savePreview(item3);
                }
            }
        }

        public static void Process(string path, bool needWriteToDB)   // @path:文件路径 @needWriteToDB:表示是否需要写入数据库
        {
            //  初始化一个用来去噪的类
            Denoising de = new Denoising();
            de.Path = path;
            de.Work();  //  work后就获得了原始数据，去噪数据等
            MainWindow._rawdata = de.Rawdata;  //  获得原始数据
            MainWindow._denoisingdata = de.Denoisingdata; // 获得去噪数据
            if (MainWindow._denoisingdata == null) //  如果得到的去噪数据是null，说明可能存在编码或其他一些问题
            {
                MainWindow._denoisingdata = "";
            }
            //  初始化一个用来分词的类，对去噪数据进行分词处理
            WordSegment ws = new WordSegment();
            //bool isCHineseWebPage = WebTesting(MainWindow._denoisingdata);
            MainWindow._wordSegmentResult = ws.cutwords(MainWindow._denoisingdata, WordSegmentData.preWordSegmentor);
            foreach (string word in MainWindow._wordSegmentResult)
            {
                if (MainWindow.wordFreq.ContainsKey(word))
                {
                    int freq = (int)MainWindow.wordFreq[word];
                    MainWindow.wordFreq[word] = freq + 1;
                }
                else
                {
                    MainWindow.wordFreq.Add(word, 1);
                }
            }
            /*if (isCHineseWebPage)   //  只对中文页面采用分词算法
            {
                MainWindow._wordSegmentResult = ws.DoWordSegment(MainWindow._denoisingdata);   //  使用分词算法进行分词

                //  对分词结果中每个词的词频进行统计
                string word;
                Regex r = new Regex(@"\{<([^>]*)>\}", RegexOptions.Multiline);
                MatchCollection matches = r.Matches(MainWindow._wordSegmentResult);
                foreach (Match match in matches)
                {
                    if (match.Success)
                    {
                        word = match.Groups[1].Value;
                        if (MainWindow.wordFreq.ContainsKey(word))
                        {
                            int freq = (int)MainWindow.wordFreq[word];
                            MainWindow.wordFreq[word] = freq + 1;
                        }
                        else
                        {
                            MainWindow.wordFreq.Add(word, 1);
                        }
                    }
                }
            }
            else    //  对英文页面，直接采用分隔符分离的方法
            {
                MainWindow._wordSegmentResult = MainWindow._denoisingdata;
                string[] words = MainWindow._wordSegmentResult.Split(new char[] {' ', '`', '~', '!', '@', '$', '%', 
                            '^', '&', '*','(', ')', '_', '+', '=', '{', '[', '}', ']', ':', ';', '\"', '\'', ',', 
                            '<', '.', '>', '?', '/', '\\', '\r', '\n', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'});
                foreach (string word in words)
                {
                    if (word.Length > 1)
                    {
                        if (MainWindow.wordFreq.ContainsKey(word.ToLower()))
                        {
                            int freq = (int)MainWindow.wordFreq[word.ToLower()];
                            MainWindow.wordFreq[word.ToLower()] = freq + 1;
                        }
                        else
                        {
                            MainWindow.wordFreq.Add(word.ToLower(), 1);
                        }
                    }
                }
            }*/

            //  数据挖掘
            //  先清空两个容器

            MainWindow._tags = new List<string>();
            MainWindow.tagPreview = new Hashtable();
            //  初始化一个数据挖掘类的实例
            DataMining dm = new DataMining();
            //  获得原始数据和去噪数据
            dm.Rawdata = MainWindow._rawdata;
            dm.Denoisingdata = MainWindow._denoisingdata;
            //  开始工作
            dm.Work();
            //  获得标题，作者，发布日期，其实很不准确
            MainWindow._title = dm.Title;
            MainWindow._author = dm.Author;
            MainWindow._postdate = dm.Postdate;

            //  已经得到了想要的信息，开始
            //DB OPERATION
            //信息已经提取完成，存入数据库，包括tag webpage tag_webpage 3张表
            //请把新的tag存入tag表中，注意编号
            if (needWriteToDB)
            {
                /*suiyuhao*/
                //save to table PAGE
                Page pg = new Page();
                PageItem pgitem = new PageItem();
                MainWindow.curwid = MainWindow.getWebpageNo() + 1;
                pgitem.setwid(MainWindow.curwid);
                //item1.settitle(MainWindow._title);
                //pgitem.settitle(translate(MainWindow._title));
                pgitem.settitle(MainWindow._title);
                pgitem.setauthor(MainWindow._author);
                pgitem.setpostdate(MainWindow._postdate);
                pgitem.setlink(MainWindow.curUrl);
                pgitem.setreferred("");
                pg.savePage(pgitem);
                string body = dealhtml(MainWindow._rawdata);
                string keywords = "";
                
                //save to table TAGS
                foreach (string tag in MainWindow._tags)
                {
                    if (!MainWindow._occurredTags.Contains(tag))
                    {
                        Tag thistag = new Tag();
                        TagItem tgitem = new TagItem();
                        tgitem.settid(MainWindow.getTagNo() + 1);
                        //MainWindow._occurredTags.Add(translate(tag));
                        MainWindow._occurredTags.Add(tag);
                        //tgitem.setname(translate(tag));
                        tgitem.setname(tag);
                        thistag.saveTag(tgitem);
                    }
                    keywords += tag;
                }
                MainWindow.insertintosolr(MainWindow._title, "", MainWindow.curUrl, MainWindow._postdate, keywords, 0, body);
                foreach (string tag in MainWindow._tags)
                {
                    Preview b = new Preview();
                    PreviewItem item3 = new PreviewItem();
                    //MainWindow.curtid = MainWindow.getTagNo(translate(tag));
                    MainWindow.curtid = MainWindow.getTagNo(tag);
                    item3.settid(MainWindow.curtid);
                    item3.setwid(MainWindow.curwid);
                    //item3.setpreview(translate((string)tagPreview[tag]));
                    item3.setpreview((string)MainWindow.tagPreview[tag]);
                    //item3.setpreview((string)tagPreview[tag]);
                    b.savePreview(item3);
                }
                /*suiyuhao*/
            }
        }

        public static void baiduzhidaoprocess(string path, string link)
        {
            //  1.read all content from file
            //  2.get questions, abstract, tags, answernum
            //  3.insert into db
            HtmlDocument doc = new HtmlDocument();
            try
            {
                doc.Load(path, Encoding.GetEncoding("GBK"));
            }
            catch (Exception e)
            {
                return;
            }
            string rawdata;
            try
            {
                rawdata = File.ReadAllText(path, Encoding.UTF8);
            }
            catch (Exception e)
            {
                return;
            }
            Regex reg;
            MatchCollection mc;
            string title=null;
            string tag=null;
            string CategoryListXPath = "//title|//meta";
            HtmlNode rootNode = doc.DocumentNode;
            HtmlNodeCollection categoryNodeList = rootNode.SelectNodes(CategoryListXPath);
            foreach (HtmlNode child in categoryNodeList)
            {
                HtmlNode hn = HtmlNode.CreateNode(child.OuterHtml);
                if (hn.SelectSingleNode("//title") != null)
                {
                    if(title == null || title.Length < 2)
                        title=hn.SelectSingleNode("//title").InnerText;
                }
                else if (hn.SelectSingleNode("//meta") != null)
                {
                    string str = hn.SelectSingleNode("//meta").OuterHtml;
                    string r = "(?<=meta name=\"keywords\" content=\").*?(?=\")";
                    if(tag == null || tag.Length < 2)
                        tag = Regex.Match(str, r).Value;
                }

            }

            reg = new Regex("accuse=\"q(Supply|Content)\">(.{1,}?)</PRE>");
            mc = reg.Matches(rawdata);
            
            string _abstract = "";
            if (mc.Count != 0 && mc[0].Length >= 2)
            {
                _abstract = mc.Count == 0 ? "" : mc[0].Groups[2].ToString();//id="questionContent">
            }

            reg = new Regex(" ,answerNum: '(.{1,}?)'");
            mc = reg.Matches(rawdata);
            string snum = mc.Count != 0 ? mc[0].Groups[1].ToString() : "";//class="answer-wrap"><h3> 其他回答 (
            int num;
            int.TryParse(snum, out num);
            num++;
            reg = new Regex("class=\"accuse-area\"></INS>(.{1,}?)</SPAN>");
            mc = reg.Matches(rawdata);
            string time;
            time = mc.Count != 0 ? mc[0].Groups[1].ToString() : "";//class="time">
            reg = new Regex("(1|2)[0-9]{3}-.*");
            if (!reg.IsMatch(time))
            {
                time = DateTime.Now.ToString();

                time = time.Replace('/', '-');
                time = time.Substring(0, 16);
            }
            //  insert into db
            string body = dealhtml(rawdata);
            MainWindow.insertintodb(title, _abstract, link, time, tag, num);
            MainWindow.insertintosolr(title, _abstract, link, time, tag, num, body);
            MainWindow._id += 15;
        }

        public static void stackoverflowprocess(string path, string link)
        {
            string rawdata;
            try
            {
                rawdata = File.ReadAllText(path, Encoding.UTF8);
            }
            catch (Exception e)
            {
                return;
            }
            Regex reg = new Regex("<title>(.{1,}?)</title>");
            MatchCollection mc = reg.Matches(rawdata);
            string title = mc[0].Groups[1].ToString();
            //tag
            reg = new Regex("        <a href=\"/questions/tagged/(.{1,}?)\"");
            mc = reg.Matches(rawdata);
            string tag = mc.Count == 0 ? "" : mc[0].Groups[1].ToString();
            //  answer num
            int num;
            reg = new Regex("<span style=\"display:none;\" itemprop=\"answerCount\">(.{1,}?)</span>");
            if (reg.IsMatch(rawdata) == false)
            {
                num = 0;
            }
            else
            {
                mc = reg.Matches(rawdata);
                int.TryParse(mc[0].Groups[1].ToString(), out num);
            }

            //  abstract
            string _abstract;
            string cp = rawdata;
            while (cp != cp.Replace("\r\n", ""))
            {
                cp = cp.Replace("\r\n", "");
            }
            while (cp != cp.Replace("\n", ""))
            {
                cp = cp.Replace("\n", "");
            }
            reg = new Regex("itemprop=\"description\" content=\"(.{1,}?)\" />");
            if (reg.IsMatch(cp) == false)
            {
                _abstract = "-";
            }
            else
            {
                mc = reg.Matches(cp);
                _abstract = mc.Count == 0 ? "" : mc[0].Groups[1].ToString();
            }

            //date
            reg = new Regex("asked <span title=\"(.{1,}?)Z\"");
            mc = reg.Matches(rawdata);
            string date = mc.Count == 0 ? "" : mc[0].Groups[1].ToString();
            date = date.Substring(0, 16);
            string body = dealhtml(rawdata);
            MainWindow.insertintodb(title, _abstract, link, date, tag, num);
            MainWindow.insertintosolr(title, _abstract, link, date, tag, num, body);
            MainWindow._id += 15;

        }

        public static void cnblogsprocess(string path, string link)
        {
            HtmlDocument doc = new HtmlDocument();
            try
            {
                doc.Load(path, Encoding.UTF8);
            }
            catch (Exception e)
            {
                return;
            }
            string rawdata;
            try
            {
                rawdata = File.ReadAllText(path, Encoding.UTF8);
            }
            catch (Exception e)
            {
                return;
            }
            Regex reg;
            MatchCollection mc;
            string title = null;
            string tag = null;
            string CategoryListXPath = "//title|//meta";
            HtmlNode rootNode = doc.DocumentNode;
            CategoryListXPath = "//title|//a";
            HtmlNodeCollection categoryNodeList = rootNode.SelectNodes(CategoryListXPath);
            foreach (HtmlNode child in categoryNodeList)
            {
                HtmlNode hn = HtmlNode.CreateNode(child.OuterHtml);
                if (hn.SelectSingleNode("//title") != null)
                {
                    //hn.SelectSingleNode("//title").InnerHtml;
                    if(title == null || title.Length < 2)
                        title =hn.SelectSingleNode("//title|//h1|//h2|//h3|//h4|//h5|//h6").InnerText;
                }
                else if (hn.SelectSingleNode("//*[@class=\"detail_tag\"]") != null)
                {
                    if (tag == null)
                        tag = hn.SelectSingleNode("//*[@class=\"detail_tag\"]").InnerText;
                }
            }
            reg = new Regex("<meta name=\"description\" content=\"(.{1,}?)/>");
            mc = reg.Matches(rawdata);
            string _abstract = mc.Count == 0 ? "" : mc[0].Groups[1].ToString();//id="questionContent">

            reg = new Regex("var acount=(.{1,}?);");
            int num;
            mc = reg.Matches(rawdata);
            string snum = mc.Count == 0 ? "" : mc[0].Groups[1].ToString();//class="answer-wrap"><h3> 其他回答 (
            int.TryParse(snum, out num);

            reg = new Regex("提问于：(.{1,}?)\n");
            mc = reg.Matches(rawdata);
            string time = mc.Count == 0 ? "" : mc[0].Groups[1].ToString();//class="time">
            reg = new Regex("(1|2)[0-9]{3}-.*");
            if (!reg.IsMatch(time))
            {
                time = DateTime.Now.ToString();

                time = time.Replace('/', '-');
                time = time.Substring(0, 16);
            }
            time = time.Substring(0, 16);
            string body = dealhtml(rawdata);
            MainWindow.insertintodb(title, _abstract, link, time, tag, num);
            MainWindow.insertintosolr(title, _abstract, link, time, tag, num, body);
            MainWindow._id += 15;
        }
        public static void msdn_channel9process(string path, string link)
        {
            Denoising de = new Denoising();
            de.Path = path;
            de.Work();  //  work后就获得了原始数据，去噪数据等
            MainWindow._rawdata = de.Rawdata;  //  获得原始数据
            MainWindow._denoisingdata = de.Denoisingdata; // 获得去噪数据
            if (MainWindow._denoisingdata == null) //  如果得到的去噪数据是null，说明可能存在编码或其他一些问题
            {
                MainWindow._denoisingdata = "";
            }
            //  初始化一个用来分词的类，对去噪数据进行分词处理
            WordSegment ws = new WordSegment();
            //bool isCHineseWebPage = WebTesting(MainWindow._denoisingdata);
            MainWindow._wordSegmentResult = ws.cutwords(MainWindow._denoisingdata, WordSegmentData.preWordSegmentor);
            foreach (string word in MainWindow._wordSegmentResult)
            {
                if (MainWindow.wordFreq.ContainsKey(word))
                {
                    int freq = (int)MainWindow.wordFreq[word];
                    MainWindow.wordFreq[word] = freq + 1;
                }
                else
                {
                    MainWindow.wordFreq.Add(word, 1);
                }
            }
            MainWindow._tags = new List<string>();
            MainWindow.tagPreview = new Hashtable();
            //  初始化一个数据挖掘类的实例
            DataMining dm = new DataMining();
            //  获得原始数据和去噪数据
            dm.Rawdata = MainWindow._rawdata;
            dm.Denoisingdata = MainWindow._denoisingdata;
            //  开始工作
            dm.Work();
            //  获得标题，作者，发布日期，其实很不准确
            MainWindow._title = dm.Title;
            MainWindow._author = dm.Author;
            MainWindow._postdate = dm.Postdate;
            Page pg = new Page();
            PageItem pgitem = new PageItem();
            MainWindow.curwid = MainWindow.getWebpageNo() + 1;
            pgitem.setwid(MainWindow.curwid);
            //item1.settitle(MainWindow._title);
            //pgitem.settitle(translate(MainWindow._title));
            pgitem.settitle(MainWindow._title);
            pgitem.setauthor(MainWindow._author);
            pgitem.setpostdate(MainWindow._postdate);
            pgitem.setlink(MainWindow.curUrl);
            pgitem.setreferred("");
            pg.savePage(pgitem);

            HtmlDocument doc = new HtmlDocument();
            try
            {
                doc.Load(path, Encoding.UTF8);
            }
            catch(Exception e)
            {
                return;
            }
            HtmlNode rootNode = doc.DocumentNode;
            string CategoryListXPath = "//title|//img";
            HtmlNodeCollection categoryNodeList = rootNode.SelectNodes(CategoryListXPath);
            foreach (HtmlNode child in categoryNodeList)
            {
                HtmlNode hn = HtmlNode.CreateNode(child.OuterHtml);
                if (hn.SelectSingleNode("//title") != null)
                {
                   MainWindow._title=hn.SelectSingleNode("//title").InnerText;
                }
                else if (hn.SelectSingleNode("//*[@class=\"thumb\"]") != null)
                {
                    string str = hn.SelectSingleNode("//*[@class=\"thumb\"]").OuterHtml;
                    string reg = "(?<=alt=\")[^\"]*\"";
                    string key_words = Regex.Match(str, reg).Value;
                    MainWindow._tags.Add(key_words);

                }

            }
            //save to table TAGS
            foreach (string tag in MainWindow._tags)
            {
                if (!MainWindow._occurredTags.Contains(tag))
                {
                    Tag thistag = new Tag();
                    TagItem tgitem = new TagItem();
                    tgitem.settid(MainWindow.getTagNo() + 1);
                    //MainWindow._occurredTags.Add(translate(tag));
                    MainWindow._occurredTags.Add(tag);
                    //tgitem.setname(translate(tag));
                    tgitem.setname(tag);
                    thistag.saveTag(tgitem);
                }
            }

            foreach (string tag in MainWindow._tags)
            {
                Preview b = new Preview();
                PreviewItem item3 = new PreviewItem();
                //MainWindow.curtid = MainWindow.getTagNo(translate(tag));
                MainWindow.curtid = MainWindow.getTagNo(tag);
                item3.settid(MainWindow.curtid);
                item3.setwid(MainWindow.curwid);
                //item3.setpreview(translate((string)tagPreview[tag]));
                item3.setpreview((string)MainWindow.tagPreview[tag]);
                //item3.setpreview((string)tagPreview[tag]);
                b.savePreview(item3);
             }
        }
        public static void sosowenwenprocess(string path, string link)
        {
            if (MainWindow._rawdata != null)
            {

                string rawdata;
                try
                {
                    rawdata = File.ReadAllText(path, Encoding.UTF8);
                }
                catch (Exception e)
                {
                    return;
                }
                Regex reg = new Regex("id=\"questionTitle\">(.{1,}?)</h3>");
                MatchCollection mc = reg.Matches(rawdata);
                if (mc.Count != 0)
                {
                    HtmlDocument doc = new HtmlDocument();
                    try
                    {
                        doc.Load(path, Encoding.UTF8);
                    }
                    catch (Exception e)
                    {
                        return;
                    }
                  
                    string title = null;
                    string tag = null;
                    string CategoryListXPath = "//title|//meta";
                    HtmlNode rootNode = doc.DocumentNode;
                    HtmlNodeCollection categoryNodeList = rootNode.SelectNodes(CategoryListXPath);
                    foreach (HtmlNode child in categoryNodeList)
                    {
                        HtmlNode hn = HtmlNode.CreateNode(child.OuterHtml);
                        if (hn.SelectSingleNode("//title") != null)
                        {
                            if (title == null)
                                title = hn.SelectSingleNode("//title").InnerText;
                        }
                        else if (hn.SelectSingleNode("//meta") != null)
                        {
                            string str = hn.SelectSingleNode("//meta").OuterHtml;
                            string r = "(?<=meta name=\"keywords\" content=\").*?(?=\")";
                            if(tag == null || tag.Length < 2)
                                tag = Regex.Match(str, r).Value;
                        }

                    }
                    reg = new Regex("id=\"questionContent\">(.{1,}?)</DIV>");
                    mc = reg.Matches(rawdata);
                    string _abstract = "";
                    if(mc.Count != 0)
                        _abstract = mc.Count == 0 ? "" : mc[0].Groups[2].ToString();//id="questionContent">

                    reg = new Regex("class=\"answer-wrap\"><h3> 其他回答 ((.{1,}?))");
                    int num;
                    if (reg.IsMatch(rawdata))
                    {
                        mc = reg.Matches(rawdata);
                        string snum = mc.Count == 0 ? "" : mc[0].Groups[1].ToString();//class="answer-wrap"><h3> 其他回答 (
                        int.TryParse(snum, out num);
                        num++;
                    }
                    else
                        num = 1;
                    reg = new Regex("class=\"time\">(.{1,}?)</SPAN>");
                    mc = reg.Matches(rawdata);
                    string time = "";
                    if(mc.Count != 0)
                        time = mc.Count == 0 ? "" : mc[0].Groups[1].ToString();//class="time">
                    reg = new Regex("(1|2)[0-9]{3}-.*");
                    if (!reg.IsMatch(time))
                    {
                        time = DateTime.Now.ToString();

                        time = time.Replace('/', '-');
                        time = time.Substring(0, 16);
                    }
                    string body = dealhtml(rawdata);
                    MainWindow.insertintodb(title, _abstract, link, time, tag, num);
                    MainWindow.insertintosolr(title, _abstract, link, time, tag, num, body);
                    MainWindow._id += 15;
                }
            }
        }

        public static void dewenprocess(string path, string link)
        {
            HtmlDocument doc = new HtmlDocument();
            try
            {
                doc.Load(path, Encoding.UTF8);
            }
            catch (Exception e)
            {
                return;
            }
            string rawdata;
            try
            {
                rawdata = File.ReadAllText(path, Encoding.UTF8);
            }
            catch (Exception e)
            {
                return;
            }
            Regex reg;
            MatchCollection mc;
            string title = null;
            string tag = null;
            string CategoryListXPath = "//title|//meta";
            HtmlNode rootNode = doc.DocumentNode;
            HtmlNodeCollection categoryNodeList = rootNode.SelectNodes(CategoryListXPath);
            foreach (HtmlNode child in categoryNodeList)
            {
                HtmlNode hn = HtmlNode.CreateNode(child.OuterHtml);
                if (hn.SelectSingleNode("//title") != null)
                {
                    if(title==null||title.Length<2)
                        title = hn.SelectSingleNode("//title").InnerText;
                }
                else if (hn.SelectSingleNode("//meta") != null)
                {
                    string str = hn.SelectSingleNode("//meta").OuterHtml;
                    string r = "(?<=meta name=\"keywords\" content=\").*?(?=\")";
                    if(tag==null||tag.Length<2)
                        tag = Regex.Match(str, r).Value;
                }

            }


            string _abstract;
            string cp = rawdata;
            while (cp != cp.Replace("\r\n", ""))
            {
                cp = cp.Replace("\r\n", "");
            }
            while (cp != cp.Replace("\n", ""))
            {
                cp = cp.Replace("\n", "");
            }
            reg = new Regex("<div class=\"que_con\">(.*?)<p>(.{1,}?)</p>");
            if (reg.IsMatch(cp) == false)
            {
                _abstract = "-";
            }
            else
            {
                mc = reg.Matches(cp);
                _abstract = mc.Count == 0 ? "" : mc[0].Groups[2].ToString();
            }

            reg = new Regex("<span style=\"float: left;\">(.{1,}?)个答案</span>");
            int num;
            if (reg.IsMatch(rawdata))
            {
                mc = reg.Matches(rawdata);
                string snum = mc.Count == 0 ? "" : mc[0].Groups[1].ToString();//class="answer-wrap"><h3> 其他回答 (
                int.TryParse(snum, out num);
            }
            else
            {
                num = 0;
            }

            reg = new Regex("logs\"><b>(.{1,}?)</b></a></p>");
            mc = reg.Matches(rawdata);
            string time = mc.Count == 0 ? "" : mc[0].Groups[1].ToString();//class="time">    //  2014-12-12
            reg = new Regex("(1|2)[0-9]{3}-.*");
            if (!reg.IsMatch(time))
            {
                time = new FileInfo(path).LastAccessTime.ToString();
                time = time.Replace('/', '-');
                time = time.Substring(0, 16);
            }
            string body = dealhtml(rawdata);
            MainWindow.insertintodb(title, _abstract, link, time, tag, num);
            MainWindow.insertintosolr(title, _abstract, link, time, tag, num, body);
            MainWindow._id += 15;
        }

        static string dealhtml(string html)
        {
            HtmlDocument doc = new HtmlDocument();
            try
            {
                doc.LoadHtml(html);
            }
            catch (Exception e)
            {
                return "";
            }
            
            string body = "";
            ArrayList tags = new ArrayList();
            HtmlNodeCollection allbody = doc.DocumentNode.SelectNodes("//body");
            int count = 0;
            if (allbody == null)
            {
                return null;
            }
            foreach (HtmlNode child in allbody)
            {
                count++;
                if (count >= 1000)
                {
                    break;
                }
                foreach (var n in child.Descendants())
                {
                    count++;
                    if (count >= 1000)
                    {
                        break;
                    }
                    if (n.Name == "p" || n.Name == "pre")
                    {
                        string c = n.InnerText;
                        c = DealSth(c);
                        body += "\n" + c + "\n";
                    }
                    else if (n.Name == "h1")
                    {
                        string c = n.InnerText;
                        c = DealSth(c);
                        body += "\n" + c + "\n";
                        tags.Add(c);
                    }
                    else if (n.Name == "a")
                    {
                        string c = n.InnerText;
                        string href = "";
                        //if (n.Attributes["href"] != null)
                        //{
                        //    href += n.Attributes["href"].Value;
                        //}
                        c = DealSth(c);
                        body += "\n" + c + " " + href;
                    }
                    else if (n.Name == "table")
                    {
                        body += "\n";
                        foreach (var tr in n.Descendants())
                        {
                            if (tr.Name == "tr")
                            {
                                string line = "";
                                foreach (var td in tr.Descendants())
                                {
                                    if (td.Name == "td")
                                    {
                                        line += "\t" + td.InnerText;
                                    }
                                }
                                line = DealSth(line);
                                body += line + "\n";
                            }
                        }
                        body += "\n";

                    }
                    else if (n.Name == "br")
                    {
                        body += "\n";
                    }
                    //else if (n.Name == "img")
                    //{
                    //    body += "img : ";
                    //    if (n.Attributes["src"] != null)
                    //    {
                    //        body += n.Attributes["src"].Value;
                    //    }
                    //    body += "\n";
                    //}
                }
            }
            return body;
        }

        static string DealSth(string s)
        {
            string result;
            result = Regex.Replace(s, @"<!--[\s\S]*?-->", "", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, @"&nbsp;", " ", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, @"&amp;", "&", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, @"&gt;", ">", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, @"&lt;", "<", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, @"&quot;", "\"", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, @"&qpos;", "\'", RegexOptions.IgnoreCase);
            return result;
        }


        public static bool msdn_channel9(string url)
        {
            Regex r = new Regex("^https://channel9.msdn.com/Browse/.*");
            if (r.IsMatch(url))
            {
                return true;
            }
            return false;
        }
        public static bool baiduzhidao(string url)
        {
            Regex r = new Regex("^http://zhidao.baidu.com/question/.*");
            if (r.IsMatch(url))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool stackoverflow(string url)
        {
            Regex r = new Regex("^http://stackoverflow.com/questions/.*");
            if (r.IsMatch(url))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool sosowenwen(string url)
        {
            Regex r = new Regex("^http://wenwen.sogou.com/z/.*");
            if (r.IsMatch(url))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool cnblogs(string url)
        {
            Regex r = new Regex("^http://q.cnblogs.com/q/.*");
            if (r.IsMatch(url))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool dewen(string url)
        {
            Regex r = new Regex("^http://www.dewen.io/q.*");
            if (r.IsMatch(url))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Test the webpage is Chinese or English
        private static bool WebTesting(string webContent)
        {
            int chineseCH = 0;
            for (int i = 0; i < webContent.Length; i++)
            {
                if (isChineseWord(webContent[i]))
                {
                    chineseCH += 1;
                    if (chineseCH >= 10)
                    {
                        break;
                    }
                }
            }
            if (chineseCH >= 10)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool isChineseWord(char chr)
        {
            if (chr >= 0x4E00 && chr <= 0x9FFF) return true;
            return false;
        }

        private static string translate(string str)
        {
            if (str == null || str == "")
                return str;
            /*if (!isChineseWord(str[0]))
            {
                GoogleTranslator googletranslator = new GoogleTranslator();
                //str = str + "(" + googletranslator.Analytical(googletranslator.Translate(str, "zh")) + ")";
            }*/
            GoogleTranslator googletranslator = new GoogleTranslator();
            str = googletranslator.Analytical(googletranslator.Translate(str, "auto"));
            return str;
        }
    }
}
