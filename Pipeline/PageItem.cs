using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Text.RegularExpressions;

namespace Pipeline
{
    class PageItem
    {
        private int wid;        //ID
        private string title;
        private string author;   //作者
        private string postdate;
        private string link;
        private string referred;
        public PageItem() { }
        public void setwid(int num)
        {
            this.wid = num;
        }
        public int getwid()
        {
            return this.wid;
        }
        public void settitle(string str)
        {
            this.title = str;
        }
        public string gettitle()
        {
            return this.title;
        }
        public void setauthor(string str)
        {
            this.author = str;
        }
        public string getauthor()
        {
            return this.author;
        }
        public void setpostdate(string str)
        {
            try
            {
                this.postdate = str;
            }
            catch
            {
                System.Console.WriteLine("日期转换时出错！");
            }
        }
        public string getpostdate()
        {
            try
            {
                return this.postdate;
            }
            catch
            {
                System.Console.WriteLine("Date转string时出错！");
                return null;
            }
        }
        public void setlink(string str)
        {
            this.link = str;
        }
        public string getlink()
        {
            return this.link;
        }
        public void setreferred(string str)
        {
            this.referred = str;
        }
        public string getreferred()
        {
            return this.referred;
        }
    }
}
