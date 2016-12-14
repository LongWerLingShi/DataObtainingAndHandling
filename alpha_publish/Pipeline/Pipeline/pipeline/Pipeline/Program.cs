using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pipeline.AppConfig;

namespace Pipeline
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Config());
            //Application.Run(new InputNewContent());
            //Application.Run(new WordSegmentData());
            //Application.Run(new ProgressBar());
        }
    }
}
