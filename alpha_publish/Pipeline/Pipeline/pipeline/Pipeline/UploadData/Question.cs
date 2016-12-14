using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Pipeline.UploadData
{
    class Question
    {
        private int qid;
        private string owner;
        private int view_count = 0;
        private int answer_count = 0;
        private DateTime creation_date;
        private string link;
        private string title;
        private string body;

        public int Qid
        {
            get { return qid; }
            set { qid = value; }
        }
        public string Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        public int View_count
        {
            get { return view_count; }
            set { view_count = value; }
        }

        public int Answer_count
        {
            get { return answer_count; }
            set { answer_count = value; }
        }

        public DateTime Creation_date
        {
            get { return creation_date; }
            set { creation_date = value; }
        }

        public string Link
        {
            get { return link; }
            set { link = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Body
        {
            get { return body; }
            set { body = value; }
        }
        //public Question();
        public Question(string owner, int view_count, int answer_count, DateTime creation_date, string link, string title, string body)
        {
            this.owner = owner;
            this.view_count = view_count;
            this.answer_count = answer_count;
            this.creation_date = creation_date;
            this.link = link;
            this.title = title;
            this.body = body;
        }
        public void saveQuestion()
        {
            string insertSQL = "INSERT INTO XueBa.dbo.Question(owner,view_count,answer_count,creation_date,link,title,body) VALUES(@owner,@view_count,@answer_count,@creation_date,@link,@title,@body)";
            SqlConnection con = Connection.instance("10.2.26.60", "XueBa", "crawler", "aimashi2015");
            SqlCommand cmd = new SqlCommand(insertSQL, con);

            SqlParameter owner = cmd.Parameters.Add("@owner", SqlDbType.VarChar,50);
            owner.Value = this.owner;
            SqlParameter view_count = cmd.Parameters.Add("@view_count", SqlDbType.Int);
            view_count.Value = this.view_count;
            SqlParameter answer_count = cmd.Parameters.Add("@answer_count", SqlDbType.Int);
            answer_count.Value = this.answer_count;
            SqlParameter creation_date = cmd.Parameters.Add("@creation_date", SqlDbType.DateTime);
            creation_date.Value = this.creation_date.ToString();
            SqlParameter link = cmd.Parameters.Add("@link", SqlDbType.VarChar, 200);
            link.Value = this.link;
            SqlParameter title = cmd.Parameters.Add("@title", SqlDbType.VarChar, -1);
            title.Value = this.title;
            SqlParameter body = cmd.Parameters.Add("@body", SqlDbType.VarChar, -1);
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
