using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.Web;
using System.IO;
using System.IO.Compression;
using Newtonsoft.Json.Linq;

namespace GenerateJson
{
    class Tag
    {
        private int tid;
        private string name;
        private string description;
        private int count;
        public Tag(string name)
        {
            this.name = name;
            getOnlineCount();
            getOnlineDescription();
        }
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
            string insertSQL = "INSERT INTO XueBa.dbo.Tag(name,description,count) VALUES(@name,@description,@count)";
            SqlConnection con = Connection.instance("10.2.26.60", "XueBa", "crawler", "aimashi2015");
            SqlCommand cmd = new SqlCommand(insertSQL,con);

            SqlParameter name = cmd.Parameters.Add("@name", SqlDbType.VarChar, 50);
            name.Value = this.name;
            SqlParameter description = cmd.Parameters.Add("@description", SqlDbType.VarChar,-1);
            description.Value = this.description;
            SqlParameter count = cmd.Parameters.Add("@count", SqlDbType.Int);
            count.Value = this.count;

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
        public void getOnlineCount()
        {
            WebClient client2 = new WebClient();
            client2.Headers.Add("Accept-Encoding", "gzip,deflate");
            string sUrl2 = String.Format("http://api.stackexchange.com/2.2/tags/{0}/info?order=desc&sort=popular&site=stackoverflow", StrToHex(name));
            byte[] byteArray2;
            try
            {
                byteArray2 = client2.DownloadData(sUrl2);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                this.count = 0;
                return;
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
            int count = 0;
            if (jlist2.Count > 0)
            {
                JObject tempo2 = JObject.Parse(jlist2[0].ToString());
                count = Int32.Parse(tempo2["count"].ToString());
            }
            this.count = count;
        }
        public void getOnlineDescription()
        {
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
                this.description = null;
                return;
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
            this.description = description;
        }
        private string StrToHex(string s)
        {
            byte[] buffer = Encoding.GetEncoding("utf-8").GetBytes(s);
            string str = "";
            foreach (byte b in buffer)
            {
                str += "%" + b.ToString("x2");
            }
            return str;
        }
    }
}
