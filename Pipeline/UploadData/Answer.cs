using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Pipeline.UploadData
{
    class Answer
    {
        private int aid;
        private string owner;
        private int score;
        private string body;

        public int Aid
        {
            get { return aid; }
            set { aid = value; }
        }

        public string Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public string Body
        {
            get { return body; }
            set { body = value; }
        }
       // public Answer();
        public Answer(string owner, int score, string body)
        {
            this.owner = owner;
            this.score = score;
            this.body = body;
        }
        public void saveAnswer()
        {
            string insertSQL = "INSERT INTO XueBa.dbo.Answer(owner,score,body) VALUES(@owner,@score,@body)";
            SqlConnection con = Connection.instance("10.2.26.60", "XueBa", "crawler", "aimashi2015");
            SqlCommand cmd = new SqlCommand(insertSQL,con);

            SqlParameter owner = cmd.Parameters.Add("@owner", SqlDbType.VarChar, 50);
            owner.Value = this.owner;
            SqlParameter score = cmd.Parameters.Add("@score", SqlDbType.Int);
            score.Value = this.score;
            SqlParameter body = cmd.Parameters.Add("@body", SqlDbType.VarChar,-1);
            body.Value = this.body;

            cmd.CommandType = CommandType.Text;
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
