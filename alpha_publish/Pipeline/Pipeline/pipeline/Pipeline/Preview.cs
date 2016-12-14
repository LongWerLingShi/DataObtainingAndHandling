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
    class Preview
    {
        public Preview() { }
        public ArrayList getPreview(string title = "")
        {
            string selectSQL = string.Format("SELECT * FROM XueBa.dbo.WebPage WHERE title='{0}'", title);
            ArrayList record = new ArrayList();
            SqlConnection con = Connection.instance(AppConfiguration.GetConfigValue("serverIp"), "crawler", AppConfiguration.GetConfigValue("username"), AppConfiguration.GetConfigValue("password"));
            SqlCommand Command = con.CreateCommand();
            Command.CommandText = selectSQL;
            SqlDataReader Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                PreviewItem Item = new PreviewItem();
                Item.setwid(Reader.GetInt32(0));
                Item.settid(Reader.GetInt32(1));
                Item.setpreview(Reader.GetString(2));
                record.Add(Item);
            }
            con.Close();
            return record;
        }
        public bool savePreview(PreviewItem item)
        {
            string insertSQL = string.Format("INSERT INTO XueBa.dbo.WebPage_Tag(tid,wid,preview) VALUES('{0}','{1}','{2}')", item.gettid(), item.getwid(), item.getpreview());
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
