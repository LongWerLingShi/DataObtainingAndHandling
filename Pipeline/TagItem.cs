using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Pipeline
{
    class TagItem
    {
        private int tid;
        private string name;
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
    }
}
