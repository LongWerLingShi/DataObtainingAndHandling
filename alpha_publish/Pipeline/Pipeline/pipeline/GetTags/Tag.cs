using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data.SqlClient;

namespace GetTags
{
    class Tag
    {
        private int tid;
        private string name;
        private string description;
        private int count;
        public Tag(string name, string description, int count)
        {
            this.name = name;
            this.description = description;
            this.count = count;
        }
        public void settid(int num)
        {
            this.tid = num;
        }
        public int gettid()
        {
            return this.tid;
        }
        public string getname()
        {
            return this.name;
        }
        public void setname(string str)
        {
            this.name = str;
        }
        public string getdescription()
        {
            return this.description;
        }
        public void setdescription(string description)
        {
            this.description = description;
        }
        public int getcount()
        {
            return this.count;
        }
        public void setint(int count)
        {
            this.count = count;
        }
        public void saveTag()
        {
            string insertSQL = string.Format("INSERT INTO XueBa.dbo.Tag(name,description,count) VALUES('{0}','{1}','{2}')", name, description, count);
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
