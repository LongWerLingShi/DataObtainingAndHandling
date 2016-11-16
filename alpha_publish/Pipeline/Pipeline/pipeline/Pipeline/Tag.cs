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
    class Tag
    {
        public Tag() { }
        public bool saveTag(TagItem item)
        {
            string insertSQL = string.Format("INSERT INTO XueBa.dbo.Tag(tid,name,prevtid) VALUES('{0}','{1}',NULL)", item.gettid(), item.getname());
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
