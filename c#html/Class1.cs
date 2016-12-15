
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Diagnostics;

namespace ClassLibrary1
{

    public class process
    {
        private HtmlDocument doc = new HtmlDocument();
        private static string _author = "";
        private static string _date = "";
        private static string _question = "";
        private static string _answer = "";
        private static string _title = "";
        private static string _key = "";
        private static bool flag = false;

        public process(string filepath, string curUrl, int encode)
        {
            Encoding e = Encoding.UTF8;
            // to do define encoding
            if (filepath == null || curUrl == null || encode == 0)
                // to do
                flag = false;
            else
            {
                flag = true;
                FileInfo file = new FileInfo(filepath);
                if (file.Extension.Equals(".pdf"))
                {
                    pdfprocess(filepath);
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
                        process_all(filepath);
                    }
                }
            }
            testconnect();
            getAnswer();
            getAuthor();
            getDate();
            getKey();
            getQuestion();
            getTitle();
        }
        private void process_all(string path)
        {
            HtmlDocument doc = new HtmlDocument();
            MatchCollection mc;
            string rawdata = File.ReadAllText(path, Encoding.UTF8);
            doc.Load(path, Encoding.UTF8);
            string title = null;
            string tag = null;
            string CategoryListXPath = "//title|//meta";
            HtmlNode rootNode = doc.DocumentNode;
            HtmlNodeCollection categoryNodeList = rootNode.SelectNodes(CategoryListXPath);
            if(categoryNodeList!=null)
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
                        tag+= Regex.Match(str, r).Value;
                }
            }
            Regex date = new Regex("class=\"time\">(.{1,}?)</SPAN>");
            mc = date.Matches(rawdata);
            string time = "";
            if (mc.Count != 0)
                time = mc[0].Groups[1].ToString();//class="time">
            Regex author = new Regex(@"(?<=作者).*");
            string authorResult = "";
            if (author.Matches(rawdata) != null)
                foreach (Match match in author.Matches(rawdata))
                {
                    foreach (System.Text.RegularExpressions.Capture capture in match.Groups[1].Captures)
                    {
                        authorResult += capture.Value;
                    }
                }
            _title = title;
            _author = authorResult;
            _title = time;
            _question = "根本没有问题！";
            _answer = "根本没有答案";
        }
        private void pdfprocess(string path)
        {

            PdfReader reader = new PdfReader(path);

            string text = string.Empty;

            for (int page = 1; page <= reader.NumberOfPages; page++)
            {

                text += PdfTextExtractor.GetTextFromPage(reader, page);

            }
            // 以上为读取pdf内容
            Regex author = new Regex(@"(?<=作者).*");
            string authorResult = "";
            if (author.Matches(text) != null)
                foreach (Match match in author.Matches(text))
                {
                    foreach (System.Text.RegularExpressions.Capture capture in match.Groups[1].Captures)
                    {
                        authorResult += capture.Value;
                    }
                }

            Regex date = new Regex(@"[0-9]{4}.[0-9]{1,2}.");
            string dateResult = "";
            if (date.Matches(text) != null)
                foreach (Match match in date.Matches(text))
                {
                    foreach (System.Text.RegularExpressions.Capture capture in match.Groups[1].Captures)
                    {
                        dateResult += capture.Value;
                    }
                }
            Regex title = new Regex(@"(?<=题目).*");
            string titleResult = "";
            if (title.Matches(text) != null)
                foreach (Match match in title.Matches(text))
                {
                    foreach (System.Text.RegularExpressions.Capture capture in match.Groups[1].Captures)
                    {
                        titleResult += capture.Value;
                    }
                }
            Regex key = new Regex(@"(?<=关键词).*");
            string keyResult = "";
            if (key.Matches(text) != null)
                foreach (Match match in key.Matches(text))
                {
                    foreach (System.Text.RegularExpressions.Capture capture in match.Groups[1].Captures)
                    {
                        keyResult += capture.Value;
                    }
                }
            _author = authorResult;
            _date = dateResult;
            //调用分词器，等待其完成接口
            _title = titleResult;
            _key = keyResult;
            _question = "根本没有问题！";
            _answer = "根本没有答案";


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
                categoryNodeList = rootNode.SelectNodes("//*[@class=\"answer-con\"]");
                _answer = "";
                if (categoryNodeList != null)
                    foreach (HtmlNode child in categoryNodeList)
                    {
                        _answer += child.InnerText;
                        _answer += " \n";
                    }
                categoryNodeList = rootNode.SelectNodes("//*[@id=\"ans_user_card_name0\"]");
                _author = "";
                if (categoryNodeList != null)
                    foreach (HtmlNode child in categoryNodeList)
                    {
                        _author += child.InnerText;
                        _answer += " ";
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



        private void baiduzhidaoprocess(string path)
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
            if (categoryNodeList != null)
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
            string temp = "//*[@class=\"detail_tag\"]";
            categoryNodeList = rootNode.SelectNodes(temp);
            if (categoryNodeList != null)
                foreach (HtmlNode child in categoryNodeList)
                {
                    _answer += child.InnerText;
                    _answer += " \n";
                }
            categoryNodeList = rootNode.SelectNodes("//*[@alog-action=\"qb-ask-uname\"]");
            _author = "";
            if (categoryNodeList != null)
                foreach (HtmlNode child in categoryNodeList)
                {
                    _author += child.InnerText;
                }
          
            reg = new Regex("class=\"accuse-area\"></INS>(.{1,}?)</SPAN>");
            mc = reg.Matches(rawdata);
            string time;
            if (mc.Count > 0)
                time = mc[0].Groups[1].ToString();//class="time">
            else
                time = "未匹配到时间";
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
            if (categoryNodeList != null)
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
            if (mc != null)
                foreach (Match m in mc)
                {
                    _answer += m.Value;
                    _answer += " \n";
                }
            reg = new Regex("logs\"><b>(.{1,}?)</b></a></p>");
            mc = reg.Matches(rawdata);
            string time;
            if (mc.Count > 0)
                time = mc[0].Groups[1].ToString();//class="time">
            else
                time = "未匹配到时间";//class="time">    //  2014-12-12
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
            if (categoryNodeList != null)
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
            categoryNodeList = rootNode.SelectNodes("//*[@class=\"post-text\"]");
            _question = "";
            if (categoryNodeList != null)
                foreach (HtmlNode child in categoryNodeList)
                {
                    _question += child.InnerText;
                    _question += " \n";
                }
            categoryNodeList = rootNode.SelectNodes("//*[@class=\"answercell\"]");
            _answer = "";
            if (categoryNodeList != null)
                foreach (HtmlNode child in categoryNodeList)
                {
                    _answer += child.InnerText;
                    _answer += " \n";
                }
            // author 
            categoryNodeList = rootNode.SelectNodes("//*[@class=\"user-details\"]");
            _author = "";
            if (categoryNodeList != null)
                foreach (HtmlNode child in categoryNodeList)
                {
                    _author += child.InnerText;
                    break;
                }
            //date
            reg = new Regex("asked <span title=\"(.{1,}?)Z\"");
            mc = reg.Matches(rawdata);
            string time;
            if (mc.Count > 0)
                time = mc[0].Groups[1].ToString();//class="time">
            else
                time = "未匹配到时间";
            time = time.Substring(0, 16);
            _title = title;
            _key = tag;
            _date = time;

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
            if (categoryNodeList != null)
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
            categoryNodeList = rootNode.SelectNodes("//*[@class=\"qes_content\"]");
            _question = "";
            if (categoryNodeList != null)
                foreach (HtmlNode child in categoryNodeList)
                {
                    _question += child.InnerText;
                    _question += " \n";
                }
            categoryNodeList = rootNode.SelectNodes("//*[@class=\"q_content\"]");
            _answer = "";
            if (categoryNodeList != null)
                foreach (HtmlNode child in categoryNodeList)
                {
                    _answer += child.InnerText;
                    _answer += " \n";
                }
            categoryNodeList = rootNode.SelectNodes("//*[@class=\"question_author\"]");
            _author = "";
            if (categoryNodeList != null)
                foreach (HtmlNode child in categoryNodeList)
                {
                    _author += child.InnerText;
                    break;
                }

            reg = new Regex("提问于：(.{1,}?)\n");
            mc = reg.Matches(rawdata);
            string time;
            if (mc.Count > 0)
                time = mc[0].Groups[1].ToString();//class="time">
            else
                time = "未匹配到时间"; //class="time">
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
        private void getAuthor()
        {
            int id = Process.GetCurrentProcess().Id;
            string path = "temp_html_and_pdf_" + id.ToString() + "_Author.txt";
            if (File.Exists(path) == true)
            {
                File.Delete(path);
            }
            FileStream fs = File.Create(path);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);

            if (flag)
            {
                sw.WriteLine(_author);
                sw.Close();
                return;
            }
            else
            {
                sw.WriteLine("YOU HAVE TO RUN PROCESS FIRST");
                sw.Close();
                return ;
            }
        }
        private void getDate()
        {
            int id = Process.GetCurrentProcess().Id;
            string path = "temp_html_and_pdf_" + id.ToString() + "_Date.txt";
            if (File.Exists(path) == true)
            {
                File.Delete(path);
            }
            FileStream fs = File.Create(path);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);

            if (flag)
            {
                sw.WriteLine(_date);
                sw.Close();
                return;
            }
            else
            {
                sw.WriteLine("YOU HAVE TO RUN PROCESS FIRST");
                sw.Close();
                return;
            }
        }
        private void getTitle()
        {

            int id = Process.GetCurrentProcess().Id;
            string path = "temp_html_and_pdf_" + id.ToString() + "_Title.txt";
            if (File.Exists(path) == true)
            {
                File.Delete(path);
            }
            FileStream fs = File.Create(path);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);

            if (flag)
            {
                sw.WriteLine(_title);
                sw.Close();
                return;
            }
            else
            {
                sw.WriteLine("YOU HAVE TO RUN PROCESS FIRST");
                sw.Close();
                return;
            }
        }
        private void getQuestion()
        {
            int id = Process.GetCurrentProcess().Id;
            string path = "temp_html_and_pdf_" + id.ToString() + "_Question.txt";
            if (File.Exists(path) == true)
            {
                File.Delete(path);
            }
            FileStream fs = File.Create(path);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);

            if (flag)
            {
                sw.WriteLine(_question);
                sw.Close();
                return;
            }
            else
            {
                sw.WriteLine("YOU HAVE TO RUN PROCESS FIRST");
                sw.Close();
                return;
            }
        }
        private void getAnswer()
        {
            int id = Process.GetCurrentProcess().Id;
            string path = "temp_html_and_pdf_" + id.ToString() + "_Answer.txt";
            if (File.Exists(path) == true)
            {
                File.Delete(path);
            }
            FileStream fs = File.Create(path);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);

            if (flag)
            {
                sw.WriteLine(_answer);
                sw.Close();
                return;
            }
            else
            {
                sw.WriteLine("YOU HAVE TO RUN PROCESS FIRST");
                sw.Close();
                return;
            }
        }
        private void getKey()
        {
            int id = Process.GetCurrentProcess().Id;
            string path = "temp_html_and_pdf_" + id.ToString() + "_Key.txt";
            if (File.Exists(path) == true)
            {
                File.Delete(path);
            }
            FileStream fs = File.Create(path);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);

            if (flag)
            {
                sw.WriteLine(_key);
                sw.Close();
                return;
            }
            else
            {
                sw.WriteLine("YOU HAVE TO RUN PROCESS FIRST");
                sw.Close();
                return;
            }
        }
        private void testconnect()
        {
            int id = Process.GetCurrentProcess().Id;
            string path = "temp_html_and_pdf_" + id.ToString() + ".txt";
            if (File.Exists(path) == true)
            {
                File.Delete(path);
            }
            FileStream fs = File.Create(path);
            StreamWriter sw = new StreamWriter(fs,Encoding.UTF8);
            sw.WriteLine("Connected!");
            sw.Close();
            
            return;
        }
    }
}
