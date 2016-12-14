using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace QuestionAndAnswer
{
    class Handler
    {
        public static void InsertQuestionAndAnswer(int qid,int aid)
        {
            string insertSQL = string.Format("INSERT INTO XueBa.dbo.Question_Answer(qid,aid) VALUES('{0}','{1}')", qid, aid);
            SqlConnection con = Connection.instance("10.2.26.60", "XueBa", "crawler", "aimashi2015");
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = insertSQL;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            con.Close();
            return;
        }

        public static void InsertQuestionAndTag(int qid, int tid)
        {
            string insertSQL = string.Format("INSERT INTO XueBa.dbo.Question_Tag(qid,tid) VALUES('{0}','{1}')", qid, tid);
            SqlConnection con = Connection.instance("10.2.26.60", "XueBa", "crawler", "aimashi2015");
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = insertSQL;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            con.Close();
            return;
        }
    }
}
