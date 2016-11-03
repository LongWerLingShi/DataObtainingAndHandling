using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Pipeline.AppConfig;

namespace Pipeline
{
    class ConnectCrawler
    {
        protected SqlConnection connect;
        protected string connectString;
        public ConnectCrawler(string serverIp, string dbName, string user, string password)
        {
            this.connectString = string.Format("data Source='{0}';initial Catalog='{1}';User ID='{2}';password='{3}'", serverIp, dbName, user, password);
            connect = new SqlConnection(connectString);
            connect.Open();
        }
        //public static SqlConnection instance(string serverIp = "localhost", string dbName = "crawler", string user = "sa", string password = "Test@123")
        public static SqlConnection instance(string serverIp, string dbName, string user, string password)
        {
            ConnectCrawler con = null;
            try
            {
                if (con == null)
                {
                    con = new ConnectCrawler( serverIp,  dbName,  user,  password);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.StackTrace);
                System.Console.WriteLine("Error connecting the SQL server.");
                return null;
            }
            return con.connect;
        }
    }
}