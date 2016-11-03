using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Pipeline
{
    class PreviewItem
    {
        private int wid;
        private int tid;
        private string preview;
        public void setwid(int num)
        {
            this.wid = num;
        }
        public int getwid()
        {
            return this.wid;
        }
        public void settid(int num)
        {
            this.tid = num;
        }
        public int gettid()
        {
            return this.tid;
        }
        public string getpreview()
        {
            return this.preview;
        }
        public void setpreview(string str)
        {
            this.preview = str;
        }
    }
}
