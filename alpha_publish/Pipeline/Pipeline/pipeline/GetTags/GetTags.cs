using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using Newtonsoft.Json.Linq;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace GetTags
{
    class GetTags
    {
        static void stackoverflowTags(int beginpage=282, int endpage=800)
        {
            FileStream fs = null;
            string filepath = @"H:\CProjects\Pipeline\pipeline2\Pipeline\bin\Release\Tags.txt";
            Encoding encoder = Encoding.UTF8;
            fs = File.OpenWrite(filepath);
            WebClient client = new WebClient();
            for (int iter = beginpage; iter <= endpage; iter++)
            {
                client.Headers.Add("Accept-Encoding", "gzip,deflate");
                string sUrl = String.Format("http://api.stackexchange.com/2.2/tags?page={0}&order=desc&sort=popular&site=stackoverflow",iter);
                byte[] byteArray = client.DownloadData(sUrl);
                // 处理　gzip 
                string sContentEncoding = client.ResponseHeaders["Content-Encoding"];
                if (sContentEncoding == "gzip")
                {
                    //ToolClass.LogMessage("gzip ok", page);
                    MemoryStream ms = new MemoryStream(byteArray);
                    MemoryStream msTemp = new MemoryStream();
                    int count = 0;
                    GZipStream gzip = new GZipStream(ms, CompressionMode.Decompress);
                    byte[] buf = new byte[1000];
                    while ((count = gzip.Read(buf, 0, buf.Length)) > 0)
                    {
                        msTemp.Write(buf, 0, count);
                    }
                    byteArray = msTemp.ToArray();
                } // end-gzip
                string sHtml = Encoding.GetEncoding(936).GetString(byteArray);
                JObject r = JObject.Parse(sHtml);
                JArray jlist = JArray.Parse(r["items"].ToString());
                //List<string> taglist = new List<string>();
                for (int i = 0; i < jlist.Count; i++)
                {
                    JObject tempo = JObject.Parse(jlist[i].ToString());
                    byte[] bytes = encoder.GetBytes(tempo["name"].ToString()+"\n");
                    //taglist.Add(tempo["name"].ToString());
                    fs.Position = fs.Length;
                    fs.Write(bytes, 0, bytes.Length);
                }
            }  
        }

        static void Main()
        {
            Encoding encoder = Encoding.UTF8;
            WebClient client = new WebClient();
            //List<Tag> taglist = new List<Tag>();
            for (int iter = 1; iter <= 20; iter++)
            {
                client.Headers.Add("Accept-Encoding", "gzip,deflate");
                string sUrl = String.Format("http://api.stackexchange.com/2.2/tags?page={0}&order=desc&sort=popular&site=stackoverflow", iter);
                byte[] byteArray = client.DownloadData(sUrl);
                // 处理　gzip 
                string sContentEncoding = client.ResponseHeaders["Content-Encoding"];
                if (sContentEncoding == "gzip")
                {
                    MemoryStream ms = new MemoryStream(byteArray);
                    MemoryStream msTemp = new MemoryStream();
                    int count = 0;
                    GZipStream gzip = new GZipStream(ms, CompressionMode.Decompress);
                    byte[] buf = new byte[1000];
                    while ((count = gzip.Read(buf, 0, buf.Length)) > 0)
                    {
                        msTemp.Write(buf, 0, count);
                    }
                    byteArray = msTemp.ToArray();
                } // end-gzip
                string sHtml = Encoding.GetEncoding(936).GetString(byteArray);
                JObject r = JObject.Parse(sHtml);
                JArray jlist = JArray.Parse(r["items"].ToString());
                for (int i = 0; i < jlist.Count; i++)
                {
                    JObject tempo = JObject.Parse(jlist[i].ToString());
                    string name = tempo["name"].ToString();
                    int count = (int)tempo["count"];
                    Tag tag;
                    tag = new Tag(name, getDescription(name), count);
                    tag.saveTag();
                }
            }
        }
        
        private static string StrToHex(string s)
        {
            byte[] buffer = Encoding.GetEncoding("utf-8").GetBytes(s);
            string str = "";
            foreach (byte b in buffer)
            {
                str += "%"+b.ToString("x2");
            }
            return str;
        }

        private static string getDescription(string name)
        {
            Encoding encoder2 = Encoding.UTF8;
            WebClient client2 = new WebClient();
            client2.Headers.Add("Accept-Encoding", "gzip,deflate");
            string sUrl2 = String.Format("http://api.stackexchange.com/2.2/tags/{0}/wikis?site=stackoverflow", StrToHex(name));
            byte[] byteArray2;
            try
            {
                byteArray2 = client2.DownloadData(sUrl2);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
            // 处理　gzip 
            string sContentEncoding2 = client2.ResponseHeaders["Content-Encoding"];
            if (sContentEncoding2 == "gzip")
            {
                MemoryStream ms2 = new MemoryStream(byteArray2);
                MemoryStream msTemp2 = new MemoryStream();
                int count2 = 0;
                GZipStream gzip2 = new GZipStream(ms2, CompressionMode.Decompress);
                byte[] buf2 = new byte[1000];
                while ((count2 = gzip2.Read(buf2, 0, buf2.Length)) > 0)
                {
                    msTemp2.Write(buf2, 0, count2);
                }
                byteArray2 = msTemp2.ToArray();
            } // end-gzip
            string sHtml2 = Encoding.GetEncoding(936).GetString(byteArray2);
            JObject r2 = JObject.Parse(sHtml2);
            JArray jlist2 = JArray.Parse(r2["items"].ToString());
            string description = null;
            if (jlist2.Count > 0)
            {
                JObject tempo2 = JObject.Parse(jlist2[0].ToString());
                description = tempo2["excerpt"].ToString();
            }
            return description;
        }
    }
}
