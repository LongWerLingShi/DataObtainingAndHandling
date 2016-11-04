using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace Pipeline.UploadData
{
    class Program
    {
        public static List<Question> qlist;
        public static int iter = 0;
        public static void genJson()
        {
            try
            {
                if (iter < qlist.Count)
                {
                    int qid = qlist[iter].Qid;
                    string owner = qlist[iter].Owner;
                    int view_count = qlist[iter].View_count;
                    int answer_count = qlist[iter].Answer_count;
                    DateTime creation_date = qlist[iter].Creation_date;
                    string link = qlist[iter].Link;
                    string title = qlist[iter].Title;
                    string body = qlist[iter].Body;
                    List<string> tagslist = Search.SearchTagsList(qid);
                    List<Answer> answerslist = Search.SeachAnswersList(qid);
                    if (!Directory.Exists(@"json_data"))
                        Directory.CreateDirectory(@"json_data");
                    FileStream fs = new FileStream(String.Format(@"json_data\{0}.txt", iter), FileMode.Create);
                    string strdata = "";
                    strdata = "{\n\t\"id\":" + "\"" + "question" + iter + "\"" + ",\n\t\"owner_s\":" + "\"" + owner + "\"" + ",\n\t\"view_count_i\":" + view_count + ",\n\t\"answer_count_i\":" + answer_count
                        + ",\n\t\"creation_date_s\":" + "\"" + creation_date.ToString() + "\"" + ",\n\t\"links\":" + "\"" + link + "\"" + ",\n\t\"title\":" + "\"" + title.Replace("\n", "\\n").Replace("\"", "\\\"") + "\"" +
                        ",\n\t\"body_t\":" + "\"" + body.Replace("\n", "\\n").Replace("\"", "\\\"") + "\"" + ",\n\t\"tags_ss\":" + "[\n\t\t";
                    for (int i = 0; i < tagslist.Count; i++)
                    {
                        if (i == tagslist.Count - 1)
                            strdata += "\"" + tagslist[i] + "\"\n\t]";
                        else
                            strdata += "\"" + tagslist[i] + "\",\n\t\t";
                    }
                    if (answer_count != 0)
                    {
                        strdata += ",\n\t\"answers_t\":[";
                        for (int i = 0; i < answer_count; i++)
                        {
                            if (i == answer_count - 1)
                                strdata += "\n\t\t{\n\t\t\t\"owner\":" + "\"" + answerslist[i].Owner + "\"" + ",\n\t\t\t\"score\":" + answerslist[i].Score + ",\n\t\t\t\"body\":" + "\"" + answerslist[i].Body.Replace("\n", "\\n").Replace("\"", "\\\"") + "\"" + "\n\t\t}\n\t]";
                            if (i < answer_count - 2)
                                strdata += "\n\t\t{\n\t\t\t\"owner\":" + "\"" + answerslist[i].Owner + "\"" + ",\n\t\t\t\"score\":" + answerslist[i].Score + ",\n\t\t\t\"body\":" + "\"" + answerslist[i].Body.Replace("\n", "\\n").Replace("\"", "\\\"") + "\"" + "\n\t\t},";
                        }
                    }
                    strdata += "\n}";
                    byte[] writedata = Encoding.UTF8.GetBytes(strdata);
                    fs.Write(writedata, 0, writedata.Length);
                    fs.Close();
                    iter++;
                }
            }
            catch(IOException e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
        /*
        static void Main()
        {
            InitQuestionList();
            for (int iter = 1; iter < 2; iter++)
            {
                int qid = qlist[iter].Qid;
                string owner = qlist[iter].Owner;
                int view_count = qlist[iter].View_count;
                int answer_count = qlist[iter].Answer_count;
                DateTime creation_date = qlist[iter].Creation_date;
                string link = qlist[iter].Link;
                string title = qlist[iter].Title;
                string body = qlist[iter].Body;
                List<string> tagslist = Search.SearchTagsList(qid);
                List<Answer> answerslist = Search.SeachAnswersList(qid);
                if (!Directory.Exists(@"json_data"))
                    Directory.CreateDirectory(@"json_data");
                FileStream fs = new FileStream(String.Format(@"json_data\{0}.txt", iter), FileMode.OpenOrCreate);
                string strdata = "";
                strdata = "{\n\t\"id\":" +"\""+"question"+iter+"\""+ "\"owner_s\":" + "\"" + owner + "\"" + ",\n\t\"view_count+i\":" + view_count + ",\n\t\"answer_count_i\":" + answer_count
                    + ",\n\t\"creation_date_s\":" + "\""+creation_date.ToString() +"\""+ ",\n\t\"links\":" + "\"" + link + "\"" + ",\n\t\"title\":" + "\"" + title.Replace("\n", "\\n").Replace("\"", "\\\"") + "\"" +
                    ",\n\t\"body_t\":" + "\"" + body.Replace("\n", "\\n").Replace("\"", "\\\"") + "\"" + ",\n\t\"tags_ss\":" + "[\n\t\t";
                for (int i = 0; i < tagslist.Count; i++)
                {
                    if (i == tagslist.Count - 1)
                        strdata += "\"" + tagslist[i] + "\"\n\t]";
                    else
                        strdata += "\"" + tagslist[i] + "\",\n\t\t";
                }
                if (answer_count != 0)
                {
                    strdata += ",\n\t\"answers_t\":[";
                    for (int i = 0; i < answer_count; i++)
                    {
                        if (i == answer_count - 1)
                            strdata += "\n\t\t{\n\t\t\t\"owner\":" + "\"" + answerslist[i].Owner + "\"" + ",\n\t\t\t\"score\":" + answerslist[i].Score + ",\n\t\t\t\"body\":" + "\"" + answerslist[i].Body.Replace("\n", "\\n").Replace("\"", "\\\"") + "\"" + "\n\t\t}\n\t]";
                        if (i < answer_count - 2)
                            strdata += "\n\t\t{\n\t\t\t\"owner\":" + "\"" + answerslist[i].Owner + "\"" + ",\n\t\t\t\"score\":" + answerslist[i].Score + ",\n\t\t\t\"body\":" + "\"" + answerslist[i].Body.Replace("\n", "\\n").Replace("\"", "\\\"") + "\"" + "\n\t\t},";
                    }
                }
                strdata += "\n}";
                byte[] writedata = Encoding.UTF8.GetBytes(strdata);
                fs.Write(writedata, 0, writedata.Length);
            }
        }*/

        public static void InitQuestionList()
        {
            qlist = new List<Question>();
            string selectSQL = "SELECT * FROM XueBa.dbo.Question";
            SqlConnection con = Connection.instance("10.2.28.78", "XueBa", "crawler", "aimashi2015");
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = selectSQL;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (!reader.ToString().Equals(""))    //  如果本来数据库是空的，那么返回0
                {
                    Question qst = new Question( reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetDateTime(4), reader.GetString(5), reader.GetString(6), reader.GetString(7));
                    qst.Qid = reader.GetInt32(0);
                    qlist.Add(qst);
                }
            }
            con.Close();
        }
    }
}
