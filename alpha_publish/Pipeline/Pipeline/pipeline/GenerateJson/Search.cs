using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace GenerateJson
{
    class Search
    {
        public static List<string> SearchTagsList(int qid)
        {
            List<string> tagslist= new List<string>();
            string selectSQL = String.Format("SELECT name FROM XueBa.dbo.Tag,XueBa.dbo.Question_Tag WHERE qid = '{0}' AND XueBa.dbo.Tag.tid = XueBa.dbo.Question_Tag.tid",qid);
            SqlConnection con = Connection.instance("10.2.26.60", "XueBa", "crawler", "aimashi2015");
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = selectSQL;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (!reader.ToString().Equals(""))    //  如果本来数据库是空的，那么返回0
                {
                    string tagname = reader.GetString(0);
                    tagslist.Add(tagname);
                    
                }
            }
            con.Close();
            return tagslist;
        }

        public static List<Answer> SeachAnswersList(int qid)
        {
            List<Answer> answerslist = new List<Answer>();
            string selectSQL = String.Format("SELECT XueBa.dbo.Answer.aid,owner,score,body FROM XueBa.dbo.Answer,XueBa.dbo.Question_Answer WHERE qid = '{0}' AND XueBa.dbo.Answer.aid = XueBa.dbo.Question_Answer.aid", qid);
            SqlConnection con = Connection.instance("10.2.26.60", "XueBa", "crawler", "aimashi2015");
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = selectSQL;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (!reader.ToString().Equals(""))    //  如果本来数据库是空的，那么返回0
                {
                    Answer ans = new Answer(reader.GetString(1), reader.GetInt32(2), reader.GetString(3));
                    ans.Aid = reader.GetInt32(0);
                    answerslist.Add(ans);
                }
            }
            con.Close();
            return answerslist;
        }
    }
}
