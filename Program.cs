

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HtmlAgilityPack;
using System.Net;

namespace ruangong
{
    class Program
    {
        private const string CategoryListXPath = "//title|//t|//p|//li|//tag";
        private const int CODE_UTF8 = 1;
        private const int CODE_BigEndianUnicode = 2;
        private const int CODE_Unicode = 3;
        private const int CODE_Default = 4;
        static void Main(string[] args)
        {
            
           // 如果要读取在线网页，则使用如下语句
            HttpWebRequest req;
            req = WebRequest.Create(new Uri("http://blog.csdn.net/eric_guodongliang/article/details/7187880")) as HttpWebRequest;       
            req.Method = "GET";
            WebResponse rs = req.GetResponse();
            
            Stream rss = rs.GetResponseStream();
            
            String url = @"https://zhidao.baidu.com/question/264213135726373365.html";
            
            HtmlDocument doc = new HtmlDocument();
            try
            {
                switch (GetFileEncodeType("D:\\wat.html"))
                {
                    case CODE_UTF8:
                        doc.Load(@"D:\wat.html", Encoding.UTF8);
                        break;
                    case CODE_Unicode:
                        doc.Load(@"D:\wat.html", Encoding.Unicode);
                        break;
                    case CODE_BigEndianUnicode:
                        doc.Load(@"D:\wat.html", Encoding.Unicode);
                        break;
                    case CODE_Default:
                        doc.Load(@"D:\wat.html", Encoding.GetEncoding("GBK"));
                        break;
                    default:
                        break;
                }
                /* 如果要读取在线网页，则使用以下语句*/
               // doc.Load(rss, Encoding.UTF8);
                
                //doc.Save("output.html");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                Console.WriteLine(e.StackTrace);
            }


            //HtmlNode node = doc.GetElementbyId("post_list");
            HtmlNode rootNode = doc.DocumentNode;
            HtmlNodeCollection categoryNodeList = rootNode.SelectNodes(CategoryListXPath);
            StreamWriter sw = File.CreateText("log.txt");
            StreamWriter tag = File.CreateText("tag.txt");
            String str;
            foreach (HtmlNode child in categoryNodeList)
            {
               
                HtmlNode hn = HtmlNode.CreateNode(child.OuterHtml);
               
                //GetEncoding("GB2312")

                /*
                byte[] buffer = Encoding.GetEncoding("GB2312").GetBytes(hn.SelectSingleNode("//title").InnerText);
                str = Encoding.GetEncoding("GB2312").GetString(buffer);
                */
                if (hn.SelectSingleNode("//title|//tag") != null)
                {
                    Write(sw, tag, hn.SelectSingleNode("//title").InnerText);
                }
                else
                {
                    Write(sw, null,hn.SelectSingleNode("//title|//t|//p|//li").InnerText);
                }
                /*
                Write(sw, String.Format("标题：{0}", hn.SelectSingleNode("//*[@class=\"titlelnk\"]").InnerText));
                Write(sw, String.Format("介绍：{0}", hn.SelectSingleNode("//*[@class=\"post_item_summary\"]").InnerText));
                Write(sw, String.Format("信息：{0}", hn.SelectSingleNode("//*[@class=\"post_item_foot\"]").InnerText));
                */
                // Write(sw, "----------------------------------------");

            }
    
           // Write(sw, array3.ToString());
            sw.Close();

            Console.ReadLine();
        }

        static void Write(StreamWriter writer, StreamWriter tag,string str)
        {
            Console.WriteLine(str);
            writer.WriteLine(str);
            if (tag != null)
                tag.Write(str);
        }

        private static int GetFileEncodeType(string filename)

        {

            System.IO.FileStream fs = new System.IO.FileStream(filename, System.IO.FileMode.Open, System.IO.FileAccess.Read, FileShare.ReadWrite);

            System.IO.BinaryReader br = new System.IO.BinaryReader(fs);

            Byte[] buffer = br.ReadBytes(2);

            if (buffer[0] >= 0xEF) 

       {

              if (buffer[0] == 0xEF&& buffer[1] == 0xBB) 

              {

                    return CODE_UTF8;

              } 

              else if (buffer[0] == 0xFE&& buffer[1] == 0xFF) 
            
              {

                    return CODE_BigEndianUnicode;

              } 

              else if (buffer[0] == 0xFF&& buffer[1] == 0xFE) 

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
              //  var r_gbk = new System.IO.StreamReader(new FileStream(filename, FileMode.Open), Encoding.Default); //将html放到gbk编码的StreamReader内
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
        private static bool isLuan(string txt)
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
}
