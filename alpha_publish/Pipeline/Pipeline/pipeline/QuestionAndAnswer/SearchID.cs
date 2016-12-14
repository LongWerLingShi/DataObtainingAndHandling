using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace QuestionAndAnswer
{
    class SearchID
    {
        public static int getLastTagID()
        {
            string selectSQL = string.Format("select max(tid) from XueBa.dbo.Tag");
            SqlConnection con = Connection.instance("10.2.26.60", "XueBa", "crawler", "aimashi2015");
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = selectSQL;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (reader.ToString().Equals(""))    //  如果本来数据库是空的，那么返回0
                {
                    con.Close();
                    return 0;
                }
                else
                {
                    int id = reader.GetInt32(0);
                    con.Close();
                    return id;
                }
            }
            return 0;
        }
        public static int SearchTagID(string tagname)
        {
            string selectSQL = "select tid from XueBa.dbo.Tag where name = @name";
            SqlConnection con = Connection.instance("10.2.26.60", "XueBa", "crawler", "aimashi2015");
            SqlCommand cmd = new SqlCommand(selectSQL,con);

            SqlParameter sp = cmd.Parameters.Add("@name", SqlDbType.VarChar, 50);
            sp.Value = tagname;

            cmd.CommandType = CommandType.Text;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (reader.ToString().Equals(""))    //  如果本来数据库是空的，那么返回0
                {
                    con.Close();
                    return 0;
                }
                else
                {
                    int id = reader.GetInt32(0);
                    con.Close();
                    return id;
                }
            }
            return 0;
        }
        public static int getLastQuestionID()
        {
            string selectSQL = string.Format("select max(qid) from XueBa.dbo.Question");
            SqlConnection con = Connection.instance("10.2.26.60", "XueBa", "crawler", "aimashi2015");
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = selectSQL;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (reader.ToString().Equals(""))    //  如果本来数据库是空的，那么返回0
                {
                    con.Close();
                    return 0;
                }
                else
                {
                    int id = reader.GetInt32(0);
                    con.Close();
                    return id;
                }
            }
            return 0;
        }

        public static int SearchQuestionID(string title)
        {
            string selectSQL = "select qid from XueBa.dbo.Question where name = @title";
            SqlConnection con = Connection.instance("10.2.26.60", "XueBa", "crawler", "aimashi2015");
            SqlCommand cmd = new SqlCommand(selectSQL,con);

            SqlParameter sp = cmd.Parameters.Add("@title", SqlDbType.VarChar, -1);
            sp.Value = title;

            cmd.CommandType = CommandType.Text;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (reader.ToString().Equals(""))    //  如果本来数据库是空的，那么返回0
                {
                    con.Close();
                    return 0;
                }
                else
                {
                    int id = reader.GetInt32(0);
                    con.Close();
                    return id;
                }
            }
            return 0;
        }

        public static int getLastAnswerID()
        {
            string selectSQL = string.Format("select max(aid) from XueBa.dbo.Answer");
            SqlConnection con = Connection.instance("10.2.26.60", "XueBa", "crawler", "aimashi2015");
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = selectSQL;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (reader.ToString().Equals(""))    //  如果本来数据库是空的，那么返回0
                {
                    con.Close();
                    return 0;
                }
                else
                {
                    int id = reader.GetInt32(0);
                    con.Close();
                    return id;
                }
            }
            return 0;
        }
    }
}
