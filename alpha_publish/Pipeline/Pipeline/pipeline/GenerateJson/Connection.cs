﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace GenerateJson
{
    class Connection
    {
        protected SqlConnection connection;
        protected string conString;
        public Connection(string serverIp, string dbName, string user, string password)
        {
            this.conString = string.Format("data Source='{0}';initial Catalog='{1}';User ID='{2}';password='{3}'", serverIp, dbName, user, password);
            connection = new SqlConnection(conString);
            connection.Open();
        }
        //public static SqlConnection instance(string serverIp = "localhost", string dbName = "crawler", string user = "yuanhang1617", string password = "yuanhang@1617")
        public static SqlConnection instance(string serverIp = "10.2.26.60", string dbName = "crawler", string user = "crawler", string password = "aimashi2015")
        {
            Connection con = null;
            try
            {
                if (con == null)
                {
                    con = new Connection(serverIp, dbName, user, password);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.StackTrace);
                System.Console.WriteLine("Error connecting the SQL server.");
                return null;
            }
            return con.connection;
        }
    }
}
