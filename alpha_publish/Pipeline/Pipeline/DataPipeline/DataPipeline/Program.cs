using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Text;
using MySQLDriverCS;

namespace DataPipeline
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            */

           
            String text= @"00:41:23 GET /admin_save.asp 202.108.212.39 404 1468 176
01:04:36 GET /userbuding.asp 202.108.212.39 404 1468 176
10:00:59 GET /upfile_flash.asp 202.108.212.39 404 1468 178
12:59:00 GET /cp.php 202.108.212.39 404 1468 168
19:23:04 GET /sqldata.php 202.108.212.39 404 1468 173
23:00:00 GET /Evil-Skwiz.htm 202.108.212.39 404 1468 176
23:59:59 GET /bil.html 202.108.212.39 404 1468 170";
            Regex regex = new Regex(@"((0[0-9]|1[0-9]|2[0-3])(:[0-5][0-9]){2})\s(GET)\s([^\s]+)\s(\d{1,3}(\.\d{1,3}){3})\s(\d{3})", RegexOptions.None);
            MatchCollection matchCollection = regex.Matches(text);
            for (int i = 0; i < matchCollection.Count; i++)
            {
                Match match = matchCollection[i];
                Console.WriteLine("Match[{0}]========================", i);
                for (int j = 0; j < match.Groups.Count; j++)
                {
                    Console.WriteLine("Groups[{0}]={1}", j, match.Groups[j].Value);
                }
            }
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            


            //你好吗
            //我很好

            /*
             * 抽取网页信息
             */

        
            /*
            //string uri = "http://coderzh.cnblogs.com";
            string uri = "http://www.cnblogs.com/fightingsnail/archive/2012/11/12/2767003.html";

            WebClient wc = new WebClient();

            Console.WriteLine("Sending an HTTP GET request to " + uri);

            byte[] bResponse = wc.DownloadData(uri);

            string strResponse = Encoding.UTF8.GetString(bResponse);

            Console.WriteLine("HTTP response is: ");

            Console.WriteLine(strResponse);
             */


        }

    }
}
