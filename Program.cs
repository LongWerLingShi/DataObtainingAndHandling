

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using System.Net;
using System.Text.RegularExpressions;
using mshtml;
using HtmlAgilityPack;

namespace ruangong
{
    class Deal
    {
        private string CategoryListXPath ;
        private const int CODE_UTF8 = 1;
        private const int CODE_BigEndianUnicode = 2;
        private const int CODE_Unicode = 3;
        private const int CODE_Default = 4;
        private string path;
        private const int TYPE_ZHIDAO = 1;
        private const int TYPE_WENWEN = 2;
        private const int TYPE_CNBLOGS_Q = 3;
        private const int TYPE_DEWEN = 4;
        private const int TYPE_UNKNOWN = 0;
        private int type;
        private HtmlNode rootNode;
        StreamWriter sw = File.CreateText("log.txt");
        StreamWriter tag = File.CreateText("tag.txt");
        public Deal(string path)
        {
            this.path = path;
            string []list = path.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            if (list.Length > 2)
            {
                if (list[2].CompareTo(("cnblogs")) == 0)
                {
                    type = TYPE_CNBLOGS_Q;
                }
                else if (list[2].CompareTo("zhidao") == 0)
                {
                    type = TYPE_ZHIDAO;
                }
                else if (list[2].CompareTo("dewen") == 0)
                {
                    type = TYPE_DEWEN;
                }
                else if (list[2].CompareTo("wenwen") == 0)
                {
                    type = TYPE_WENWEN;
                }
                else
                {
                    type = TYPE_UNKNOWN;
                }
            }else
            {
                type = TYPE_UNKNOWN;
            }

        }
        public void run()
        {
            HttpWebRequest req;
            req = WebRequest.Create(new Uri("https://channel9.msdn.com/Browse/AllContent?page=1100")) as HttpWebRequest;
            req.Method = "GET";
            WebResponse rs = req.GetResponse();

            Stream rss = rs.GetResponseStream();

            String url = @"http://ask.csdn.net/questions/219537?sort=comments_count";

            HtmlDocument doc = new HtmlDocument();
            try
            {
                switch (GetFileEncodeType(path))
                {
                    case CODE_UTF8:
                        doc.Load(path, Encoding.UTF8);
                        break;
                    case CODE_Unicode:
                        doc.Load(path, Encoding.Unicode);
                        break;
                    case CODE_BigEndianUnicode:
                        doc.Load(path, Encoding.Unicode);
                        break;
                    case CODE_Default:
                        doc.Load(path, Encoding.GetEncoding("GBK"));
                        break;
                    default:
                        break;
                }
                /* 如果要读取在线网页，则使用以下语句*/
                doc.Load(rss, Encoding.UTF8);
                doc.Save("output.html");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                Console.WriteLine(e.StackTrace);
            }


            //HtmlNode node = doc.GetElementbyId("post_list");
            rootNode = doc.DocumentNode;

            MSDNBRO();
            Console.ReadLine();
            return;
            string denoisingdata = doc.DocumentNode.InnerText;  //  对网页中的html内容进行去噪

            /*
             * 如果编码不对应，denoising应该会是null
             */
          
            denoisingdata = denoisingdata.Replace("|", " ");
            denoisingdata = Regex.Replace(denoisingdata, "\\s+", " ");
            File.WriteAllText("denoisinged.txt", denoisingdata);
            Console.Write(denoisingdata);
            if (type == TYPE_UNKNOWN|| type == TYPE_CNBLOGS_Q)
            {
                UNKNOWN_QCNBLOGS();
            }else if (type == TYPE_ZHIDAO|| type == TYPE_DEWEN|| type == TYPE_WENWEN)
            {
                ZHIDAO_DEWEN_WENWEN();
            }
            

            // Write(sw, array3.ToString());
            sw.Close();

            Console.ReadLine();
        }
        private void UNKNOWN_QCNBLOGS()
        {
            CategoryListXPath = "//title|//t|//p|//li|//tag|//h1|//h2|//h3|//h4|//h5|//h6";
            //源代码中，qcnblog里面无任何关键字，关键字均和题目相同
            HtmlNodeCollection categoryNodeList = rootNode.SelectNodes(CategoryListXPath);
            foreach (HtmlNode child in categoryNodeList)
            {
                HtmlNode hn = HtmlNode.CreateNode(child.OuterHtml);
                //GetEncoding("GB2312")
                /*
                byte[] buffer = Encoding.GetEncoding("GB2312").GetBytes(hn.SelectSingleNode("//title").InnerText);
                str = Encoding.GetEncoding("GB2312").GetString(buffer);
                */
                if (hn.SelectSingleNode("//title|//tag|//h1|//h2|//h3|//h4|//h5|//h6") != null)
                {
                    //hn.SelectSingleNode("//title").InnerHtml;
                    Write(sw, tag, hn.SelectSingleNode("//title|//h1|//h2|//h3|//h4|//h5|//h6").InnerText);
                }
                else
                {
                    //   Write(sw, null, hn.SelectSingleNode("//title|//t|//p|//li").InnerText);
                }
                /*
                Write(sw, String.Format("标题：{0}", hn.SelectSingleNode("//*[@class=\"titlelnk\"]").InnerText));
                Write(sw, String.Format("介绍：{0}", hn.SelectSingleNode("//*[@class=\"post_item_summary\"]").InnerText));
                Write(sw, String.Format("信息：{0}", hn.SelectSingleNode("//*[@class=\"post_item_foot\"]").InnerText));
                */
                // Write(sw, "----------------------------------------");
            }
        }
        private void ASKCNBLOG()
        {
            CategoryListXPath = "//title|//t|//h1|//h2|//h3|//h4|//h5|//h6|//p|//li|//tag|//meta|//span";
            HtmlNodeCollection categoryNodeList = rootNode.SelectNodes(CategoryListXPath);
            foreach (HtmlNode child in categoryNodeList)
            {
                HtmlNode hn = HtmlNode.CreateNode(child.OuterHtml);
                if (hn.SelectSingleNode("//title|//tag|//h1|//h2|//h3|//h4|//h5|//h6") != null)
                {
                    Write(sw, tag, hn.SelectSingleNode("//title|//h1|//h2|//h3|//h4|//h5|//h6").InnerText);
                }
                else if (hn.SelectSingleNode("//meta") != null)
                {
                    string str = hn.SelectSingleNode("//meta").OuterHtml;
                    string reg = "(?<=meta content=\").*?(?=\" name=\"keywords\")";
                    string key_words = Regex.Match(str, reg).Value;
                    Write(sw, tag, key_words);
                } else if (hn.SelectSingleNode("//span") != null)
                {
                    if(hn.SelectSingleNode("//*[@class=\"user_name\"]") !=null)
                    {
                        string str = hn.SelectSingleNode("//*[@class=\"user_name\"]").InnerText;
                        Write(sw, tag, str);
                    }
                    
                }

            }
        }
        private void MSDNBRO()
        {
            CategoryListXPath = "//title|//t|//h1|//h2|//h3|//h4|//h5|//h6|//div";
            HtmlNodeCollection categoryNodeList = rootNode.SelectNodes(CategoryListXPath);
            foreach (HtmlNode child in categoryNodeList)
            {
                HtmlNode hn = HtmlNode.CreateNode(child.OuterHtml);
                if (hn.SelectSingleNode("//title|//tag|//h1|//h2|//h3|//h4|//h5|//h6") != null)
                {
                    Write(sw, tag, hn.SelectSingleNode("//title|//h1|//h2|//h3|//h4|//h5|//h6").InnerText);
                }
                else if (hn.SelectSingleNode("//*[@class=\"entry-image\"]") != null)
                {
                    if (hn.SelectSingleNode("//*[@class=\"thumb\"]") != null)
                    {
                        string str = hn.SelectSingleNode("//*[@class=\"thumb\"]").OuterHtml;
                        string reg = "(?<=alt=\")[^\"]*\"";
                        string key_words = Regex.Match(str, reg).Value;
                        Write(sw, tag, key_words);
                    }
                }

            }
        }
        private void ZHIDAO_DEWEN_WENWEN()
        {
            //这三个网页源代码很相似。。。关键词都在同一个位置故直接用一个来处理
            CategoryListXPath = "//title|//t|//p|//li|//tag|//meta|//h1|//h2|//h3|//h4|//h5|//h6";
            HtmlNodeCollection categoryNodeList = rootNode.SelectNodes(CategoryListXPath);
            foreach (HtmlNode child in categoryNodeList)
            {
                HtmlNode hn = HtmlNode.CreateNode(child.OuterHtml);
                if (hn.SelectSingleNode("//title|//tag|//h1|//h2|//h3|//h4|//h5|//h6") != null)
                {
                    Write(sw, tag, hn.SelectSingleNode("//title|//h1|//h2|//h3|//h4|//h5|//h6").InnerText);
                }
                else if (hn.SelectSingleNode("//meta") != null)
                {
                    string str = hn.SelectSingleNode("//meta").OuterHtml;
                    string reg = "(?<=meta name=\"keywords\" content=\").*?(?=\")";
                    string key_words = Regex.Match(str, reg).Value;
                    Write(sw, tag, key_words);
                }

            }
        }
        public void Write(StreamWriter writer, StreamWriter tag, string str)
        {

            Console.WriteLine(str);
            writer.WriteLine(str);
            if (tag != null)
                tag.Write(str);
        }
        private  int GetFileEncodeType(string filename)

        {

            System.IO.FileStream fs = new System.IO.FileStream(filename, System.IO.FileMode.Open, System.IO.FileAccess.Read, FileShare.ReadWrite);
            System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
            Byte[] buffer = br.ReadBytes(2);
            if (buffer[0] >= 0xEF)
            {
                if (buffer[0] == 0xEF && buffer[1] == 0xBB)
                {
                    return CODE_UTF8;
                }
                else if (buffer[0] == 0xFE && buffer[1] == 0xFF)
                {
                    return CODE_BigEndianUnicode;
                }
                else if (buffer[0] == 0xFF && buffer[1] == 0xFE)
                {
                    return CODE_Unicode;
                }
                else
                {
                    return CODE_Default;
                }
            }
            else
            {
                /*
                HtmlDocument doc = new HtmlDocument();
                doc.Load(filename);
                //HtmlNode node = doc.GetElementbyId("post_list");
                HtmlNode rootNode = doc.DocumentNode;
                HtmlNodeCollection categoryNodeList = rootNode.SelectNodes("//meta");
                String str;
                foreach (HtmlNode child in categoryNodeList)
                {

                    HtmlNode hn = HtmlNode.CreateNode(child.OuterHtml);

                    if (hn.SelectSingleNode("//meta") != null)
                    {
                        String content = hn.SelectSingleNode("//meta").InnerText;
                        Console.WriteLine(content);
             
                    }

                }
                */
                var r_utf8 = new System.IO.StreamReader(new System.IO.FileStream(filename, FileMode.Open), Encoding.UTF8); //将html放到utf8编码的StreamReader内
                var t_utf8 = r_utf8.ReadToEnd(); //读出html内容
                                                 //  var t_gbk = r_gbk.ReadToEnd(); //读出html内容
                                                 //  r_gbk.Close();
                r_utf8.Close();
                if (!isLuan(t_utf8)) //判断utf8是否有乱码
                {
                    return CODE_UTF8;
                }
                else
                {
                    return CODE_Default;
                }
                return CODE_Default;
            }

        }
        private  bool isLuan(string txt)
        {
            var bytes = Encoding.UTF8.GetBytes(txt);
            //239 191 189
            for (var i = 0; i < bytes.Length; i++)
            {
                if (i < bytes.Length - 3)
                    if (bytes[i] == 239 && bytes[i + 1] == 191 && bytes[i + 2] == 189)
                    {
                        return true;
                    }
            }
            return false;
        }
    }
    class Program
    {
       
        static void Main(string[] args)
        {
            Deal d = new Deal("D:\\wat.html");
            d.run();
           // 如果要读取在线网页，则使用如下语句
           
        }
    }
}
