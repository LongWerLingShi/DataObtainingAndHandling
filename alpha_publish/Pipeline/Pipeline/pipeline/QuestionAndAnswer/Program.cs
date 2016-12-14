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

namespace QuestionAndAnswer
{
    class Program
    {
        static void Main()
        {
            Encoding encoder = Encoding.UTF8;
            WebClient client = new WebClient();
            //List<Tag> taglist = new List<Tag>();
            for (int iter = 19999; iter >= 19980; iter--)
            {
                client.Headers.Add("Accept-Encoding", "gzip,deflate");
                string sUrl = String.Format("http://api.stackexchange.com/2.2/questions?page={0}&order=desc&sort=activity&site=stackoverflow&filter=!-*f(6rkuau1P", iter);
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
                JObject r;
                try
                {
                    r = JObject.Parse(sHtml);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    continue;
                }
                JArray jlist = JArray.Parse(r["items"].ToString());
                for (int i = 0; i < jlist.Count; i++)
                {
                    JObject tempo = JObject.Parse(jlist[i].ToString());
                    string owner = JObject.Parse(tempo["owner"].ToString())["display_name"].ToString();
                    int view_count = Int32.Parse(tempo["view_count"].ToString());
                    int answer_count = Int32.Parse(tempo["answer_count"].ToString());
                    DateTime creation_date = new DateTime(1970,1,1).AddSeconds(Int32.Parse(tempo["creation_date"].ToString()));
                    string link = tempo["link"].ToString();
                    string title = tempo["title"].ToString();
                    string body = tempo["body"].ToString();
                    Question qst = new Question(owner, view_count, answer_count, creation_date, link, title, body);
                    qst.saveQuestion();

                    int qid = SearchID.getLastQuestionID();
                    if (answer_count != 0)
                    {
                        JArray answerslist = JArray.Parse(tempo["answers"].ToString());
                        for (int j = 0; j < answerslist.Count; j++)
                        {
                            JObject answertmp = JObject.Parse(answerslist[j].ToString());
                            string answerowner = JObject.Parse(answertmp["owner"].ToString())["display_name"].ToString();
                            int answerscore = Int32.Parse(answertmp["score"].ToString());
                            string answerbody = answertmp["body"].ToString();
                            Answer answer = new Answer(answerowner, answerscore, answerbody);
                            answer.saveAnswer();
                            int aid = SearchID.getLastAnswerID();
                            Handler.InsertQuestionAndAnswer(qid, aid);
                        }
                    }

                    JArray tagslist = JArray.Parse(tempo["tags"].ToString());
                    for (int j = 0; j < tagslist.Count; j++)
                    {
                        string tagname = tagslist[j].ToString();
                        int tid = SearchID.SearchTagID(tagname);
                        if (tid == 0)
                        {
                            Tag tag = new Tag(tagname);
                            tag.saveTag();
                            tid = SearchID.getLastTagID();
                        }
                        Handler.InsertQuestionAndTag(qid, tid);
                    }
                }
            }
        }

        // 时间戳转为C#格式时间
        private static DateTime StampToDateTime(string timeStamp)
        {
            DateTime dateTimeStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);

            return dateTimeStart.Add(toNow);
        }

        // DateTime时间格式转换为Unix时间戳格式
        private static int DateTimeToStamp(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
    }
    
}
