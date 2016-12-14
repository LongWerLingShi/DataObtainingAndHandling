using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data.SqlClient;
using Pipeline.AppConfig;

namespace Pipeline
{
    class Page
    {
        public Page() { }
        public ArrayList getPage(string title = "")
        {
            string selectSQL = string.Format("SELECT * FROM XueBa.dbo.Document WHERE title='{0}'", title);
            ArrayList record = new ArrayList();
            SqlConnection con = Connection.instance(AppConfiguration.GetConfigValue("serverIp"), "crawler", AppConfiguration.GetConfigValue("username"), AppConfiguration.GetConfigValue("password"));
            SqlCommand Command = con.CreateCommand();
            Command.CommandText = selectSQL;
            SqlDataReader Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                PageItem Item = new PageItem();
                Item.setwid(Reader.GetInt32(0));
                Item.settitle(Reader.GetString(1));
                Item.setauthor(Reader.GetString(2));
                Item.setpostdate(Reader.GetString(3));
                Item.setlink(Reader.GetString(4));
                Item.setreferred(Reader.GetString(5));
                record.Add(Item);
            }
            con.Close();
            return record;
        }
        public bool savePage(PageItem item)
        {
            string insertSQL = string.Format("INSERT INTO XueBa.dbo.WebPage(wid,title,author,postdate,link,referred,views,rate) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','0','0')", item.getwid(), item.gettitle(), item.getauthor(), item.getpostdate(), item.getlink(), item.getreferred());
            SqlConnection con = Connection.instance(AppConfiguration.GetConfigValue("serverIp"), "crawler", AppConfiguration.GetConfigValue("username"), AppConfiguration.GetConfigValue("password"));
            SqlCommand Command = con.CreateCommand();
            Command.CommandText = insertSQL;
            try
            {
                Command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);

                return false;
            }
            con.Close();
            return true;
        }
    }
}
