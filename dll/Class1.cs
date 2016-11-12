using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClassLibrary1
{
   
    public class process
    {
        private  HtmlDocument doc;
        private  static string _author="";
        private  static string _date = "";
        private  static string _question = "";
        private  static string _answer = "";
        private  static string _title = "";
        private  static string _key = "";
        private static bool flag = false;
        // 以下代码仅为了测试是否成功使用java包装这个dll而写出，请不要过多考录他们的作用
        /*
        private  static int cal;
       
        public   process()
        {
            _author = "testAuthor";
            _date = "testdate";
            _question = "testquestion";
            _answer = "testAnswer";
            _title = "testTitle";
            _key = "testKey";
            flag = true;
        }
        */
        // 以上代码仅为了测试是否成功使用java包装这个dll而写出，请不要过多考录他们的作用
        public process()
        {
            // do nothing! 这个方法仅仅是为了访问变量而存在，至于为什么不直接process::这样访问变量
            //这是因为，我懒得改c++那头的dll了。。要改得该好多处，而这里志勇家一个空包方法，easy!
        }
        public  process(string filepath, string curUrl, int encode)
        {
            Encoding e = Encoding.UTF8;
            // to do define encoding
            if (filepath == null || curUrl == null || encode == 0)
                // to do
                flag = false;
            else {
                flag = true;
                FileInfo file = new FileInfo(filepath);
                if (file.Extension.Equals(".pdf"))
                {
                    //pdf
                }
                else if (file.Extension.Equals(".html"))
                {
                    doc.Load(filepath, e);
                    if (baiduzhidao(curUrl))
                    {
                        baiduzhidaoprocess(filepath);
                    }
                    else if (cnblogs(curUrl))
                    {
                        cnblogsprocess(filepath);
                    }
                    else if (dewen(curUrl))
                    {
                        dewenprocess(filepath);
                    }
                    else if (stackoverflow(curUrl))
                    {
                        stackoverflowprocess(filepath);//  process stackoverflow question answer pair
                    }
                    else if (sosowenwen(curUrl))
                    {
                        sosowenwenprocess(filepath);//  process sosowenwen question answer pair
                    }
                    else
                    {
                        //Process(filepath);//@"C:\C705\Pipeline\TestFiles\" + 
                    }
                }
            }
            // 为什么这样做，因为作者并不太懂如何使用java调用c#的dll，故出此下策
            // 至少这样是可以做的。。。到时候以关键词进行分割出来各部件。。。
            //遇到的困难，具体来讲是不知为何静态方法调用会在java中出错
            /*
            return _title + "LongWeiLingShi" + _author + "LongWeiLingShi"
                + _date + "LongWeiLingShi" + _key + "LongWeiLingShi" +
                _question + "LongWeiLingShi" + _answer;
                */
        }

        private void sosowenwenprocess(string path)
        {


            string rawdata = File.ReadAllText(path, Encoding.UTF8);
            Regex reg = new Regex("id=\"questionTitle\">(.{1,}?)</h3>");
            MatchCollection mc = reg.Matches(rawdata);
            if (mc.Count != 0)
            {
                HtmlDocument doc = new HtmlDocument();
                doc.Load(path, Encoding.UTF8);

                string title = null;
                string tag = null;
                _question = null;
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
                        if (tag == null || tag.Length < 2)
                            tag = Regex.Match(str, r).Value;
                        r = "(?<=meta name=\"description\" content=\").*?(?=\")";
                   
                        if (tag == null || tag.Length < 2)
                            _question = Regex.Match(str, r).Value;
                    }

                }
                categoryNodeList = rootNode.SelectNodes("//*[@class=\"answer-con\"");
                _answer = "";
                foreach (HtmlNode child in categoryNodeList)
                {
                    _answer += child.InnerText;
                    _answer += " \n";
                }
                reg = new Regex("id=\"questionContent\">(.{1,}?)</DIV>");
                mc = reg.Matches(rawdata);
                string _abstract = "";
                if (mc.Count != 0)
                    _abstract = mc[0].Groups[2].ToString();//id="questionContent">
                
                reg = new Regex("class=\"time\">(.{1,}?)</SPAN>");
                mc = reg.Matches(rawdata);
                string time = "";
                if (mc.Count != 0)
                    time = mc[0].Groups[1].ToString();//class="time">
                reg = new Regex("(1|2)[0-9]{3}-.*");
                if (!reg.IsMatch(time))
                {
                    time = DateTime.Now.ToString();

                    time = time.Replace('/', '-');
                    time = time.Substring(0, 16);
                }
                _title = title;
                _key = tag;
                _date = time;
            }
        }



        private  void baiduzhidaoprocess(string path)
        {
            //  1.read all content from file
            //  2.get questions, abstract, tags, answernum
            //  3.insert into db
            HtmlDocument doc = new HtmlDocument();
            doc.Load(path, Encoding.GetEncoding("GBK"));
            string rawdata = File.ReadAllText(path, Encoding.UTF8);
            Regex reg;
            MatchCollection mc;
            string title = null;
            string tag = null;
            string CategoryListXPath = "//title|//meta|//pre";
            HtmlNode rootNode = doc.DocumentNode;
            HtmlNodeCollection categoryNodeList = rootNode.SelectNodes(CategoryListXPath);
            foreach (HtmlNode child in categoryNodeList)
            {
                HtmlNode hn = HtmlNode.CreateNode(child.OuterHtml);
                if (hn.SelectSingleNode("//title") != null)
                {
                    if (title == null || title.Length < 2)
                        title = hn.SelectSingleNode("//title").InnerText;
                }
                else if (hn.SelectSingleNode("//meta") != null)
                {
                    string str = hn.SelectSingleNode("//meta").OuterHtml;
                    string r = "(?<=meta name=\"keywords\" content=\").*?(?=\")";
                    if (tag == null || tag.Length < 2)
                        tag = Regex.Match(str, r).Value;
                    r = "(?<=meta name=\"description\" content=\").*?(?=\")";
                    _question = Regex.Match(str, r).Value;
                }
                else if (hn.SelectSingleNode("//pre") != null)
                {
                    _answer = "";
                    _answer += hn.SelectSingleNode("//pre").InnerText;
                    _answer += " \n";
                }

            }
            categoryNodeList = rootNode.SelectNodes("//*[@class=\"detail_tag");
            foreach (HtmlNode child in categoryNodeList){
                _answer += child.InnerText;
                _answer += " \n";
            }
            /*
            reg = new Regex("(?<=<div class=\"que_con\">([^<]*)<p>)(.{1,}?)(?=</p>)");
            question = reg.Match(cp).Value;

            reg = new Regex("(?<=<div class=\"ans_con\">([^<]*)<p>)(.{1,}?)(?=</p>)");
            answer = "";
            mc = reg.Matches(cp);
            foreach (Match m in mc)
            {
                answer += m.Value;
            }
            */
            reg = new Regex("class=\"accuse-area\"></INS>(.{1,}?)</SPAN>");
            mc = reg.Matches(rawdata);
            string time = mc[0].Groups[1].ToString();//class="time">
            reg = new Regex("(1|2)[0-9]{3}-.*");
            if (!reg.IsMatch(time))
            {
                time = DateTime.Now.ToString();

                time = time.Replace('/', '-');
                time = time.Substring(0, 16);
            }
            _title = title;
            _key = tag;
            _date = time;

        }
        private void dewenprocess(string filepath)
        {
            string rawdata = File.ReadAllText(filepath, Encoding.UTF8);
            MatchCollection mc;
            string title = null;
            string tag = null;
            Regex reg;
            string CategoryListXPath = "//title|//meta";
            HtmlNode rootNode = doc.DocumentNode;
            HtmlNodeCollection categoryNodeList = rootNode.SelectNodes(CategoryListXPath);
            foreach (HtmlNode child in categoryNodeList)
            {
                HtmlNode hn = HtmlNode.CreateNode(child.OuterHtml);
                if (hn.SelectSingleNode("//title") != null)
                {
                    if (title == null || title.Length < 2)
                        title = hn.SelectSingleNode("//title").InnerText;
                }
                else if (hn.SelectSingleNode("//meta") != null)
                {
                    string str = hn.SelectSingleNode("//meta").OuterHtml;
                    string r = "(?<=meta name=\"keywords\" content=\").*?(?=\")";
                    if (tag == null || tag.Length < 2)
                        tag = Regex.Match(str, r).Value;
                }

            }


            string cp = rawdata;
            while (cp != cp.Replace("\r\n", ""))
            {
                cp = cp.Replace("\r\n", "");
            }
            while (cp != cp.Replace("\n", ""))
            {
                cp = cp.Replace("\n", "");
            }
            reg = new Regex("(?<=<div class=\"que_con\">([^<]*)<p>)(.{1,}?)(?=</p>)");
            _question = reg.Match(cp).Value;
            
            reg = new Regex("(?<=<div class=\"ans_con\">([^<]*)<p>)(.{1,}?)(?=</p>)");
            _answer = "";
            mc = reg.Matches(cp);
            foreach (Match m in mc)
            {
                _answer += m.Value;
                _answer += " \n";
            }
            reg = new Regex("logs\"><b>(.{1,}?)</b></a></p>");
            mc = reg.Matches(rawdata);
            string time = mc[0].Groups[1].ToString();//class="time">    //  2014-12-12
            reg = new Regex("(1|2)[0-9]{3}-.*");
            if (!reg.IsMatch(time))
            {
                time = new FileInfo(filepath).LastAccessTime.ToString();
                time = time.Replace('/', '-');
                time = time.Substring(0, 16);
            }
            _title = title;
            _key = tag;
            _date = time;
        }
        private void stackoverflowprocess(string path)
        {
            string rawdata = File.ReadAllText(path, Encoding.UTF8);
            HtmlDocument doc = new HtmlDocument();
            doc.Load(path, Encoding.UTF8);
            Regex reg;
            MatchCollection mc;
            string title = null;
            string tag = null;
            string CategoryListXPath = "//title|//meta";
            HtmlNode rootNode = doc.DocumentNode;
            CategoryListXPath = "//title";
            HtmlNodeCollection categoryNodeList = rootNode.SelectNodes(CategoryListXPath);
            foreach (HtmlNode child in categoryNodeList)
            {
                HtmlNode hn = HtmlNode.CreateNode(child.OuterHtml);
                if (hn.SelectSingleNode("//title") != null)
                {
                    //hn.SelectSingleNode("//title").InnerHtml;
                    if (title == null || title.Length < 2)
                        title = hn.SelectSingleNode("//title|//h1|//h2|//h3|//h4|//h5|//h6").InnerText;
                }
            }
            tag = "";
            categoryNodeList = rootNode.SelectNodes("//*[@class=\"post-text\"");
            _question = "";
            foreach (HtmlNode child in categoryNodeList)
            {
                _question += child.InnerText;
                _question += " \n";
            }
            categoryNodeList = rootNode.SelectNodes("//*[@class=\"answercell\"");
            _answer = "";
            foreach (HtmlNode child in categoryNodeList)
            {
                _answer += child.InnerText;
                _answer += " \n";
            }
            //date
            reg = new Regex("asked <span title=\"(.{1,}?)Z\"");
            mc = reg.Matches(rawdata);
            string date = mc[0].Groups[1].ToString();
            date = date.Substring(0, 16);
            _title = title;
            _key = tag;
            _date = date;

        }

        private void cnblogsprocess(string path)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.Load(path, Encoding.UTF8);
            string rawdata = File.ReadAllText(path, Encoding.UTF8);
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
                    if (title == null || title.Length < 2)
                        title = hn.SelectSingleNode("//title|//h1|//h2|//h3|//h4|//h5|//h6").InnerText;
                }
                else if (hn.SelectSingleNode("//*[@class=\"detail_tag\"]") != null)
                {
                    if (tag == null)
                        tag = hn.SelectSingleNode("//*[@class=\"detail_tag\"]").InnerText;
                }
            }
            categoryNodeList = rootNode.SelectNodes("//*[@class=\"qes_content\"");
            _question = "";
            foreach (HtmlNode child in categoryNodeList)
            {
                _question += child.InnerText;
                _question += " \n";
            }
            categoryNodeList = rootNode.SelectNodes("//*[@class=\"q_content\"");
            _answer = "";
            foreach (HtmlNode child in categoryNodeList)
            {
                _answer += child.InnerText;
                _answer += " \n";
            }
            reg = new Regex("提问于：(.{1,}?)\n");
            mc = reg.Matches(rawdata);
            string time = mc[0].Groups[1].ToString();//class="time">
            reg = new Regex("(1|2)[0-9]{3}-.*");
            if (!reg.IsMatch(time))
            {
                time = DateTime.Now.ToString();

                time = time.Replace('/', '-');
                time = time.Substring(0, 16);
            }
            time = time.Substring(0, 16);
            _title = title;
            _key = tag;
            _date = time;

        }
        private bool msdn_channel9(string url)
        {
            Regex r = new Regex("^https://channel9.msdn.com/Browse/.*");
            if (r.IsMatch(url))
            {
                return true;
            }
            return false;
        }
        private bool baiduzhidao(string url)
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
        private bool stackoverflow(string url)
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
        private bool sosowenwen(string url)
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

        private bool cnblogs(string url)
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

        private bool dewen(string url)
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
        public string getAuthor()
        {
            if (flag)
                return _author;
            else
                return "YOU HAVE TO RUN PROCESS FIRST";
        }
        public string getDate()
        {
            if (flag)
                return _date;
            else
                return "YOU HAVE TO RUN PROCESS FIRST";
        }
        public string getTitle()
        {
           
            if (flag)
                return _title;
            else
                return "YOU HAVE TO RUN PROCESS FIRST";
        }
        public string getQuestion()
        {
            if (flag)
            {
                if (_question.Length<2)
                    return _question;
                return _title;
            }else
            {
                return "YOU HAVE TO RUN PROCESS FIRST";
            }
        }
        public string getAnswer()
        {
            if(flag)
                return _answer;
            else
                return "YOU HAVE TO RUN PROCESS FIRST";
        }
        public string testconnect()
        {
            return "Connected!";
        }
    }
}
