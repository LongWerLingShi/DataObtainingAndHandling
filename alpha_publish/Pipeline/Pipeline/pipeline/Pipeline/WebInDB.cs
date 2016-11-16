using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline
{
    public class WebInDB
    {
        private int _id;
        private string _url;
        private string _filepath;
        private int _isDeal;

        public WebInDB(int id, string url, string filepath, int isDeal)
        {
            this._id = id;
            this._url = url;
            this._filepath = filepath;
            this._isDeal = isDeal;
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        public string Filepath
        {
            get { return _filepath; }
            set { _filepath = value; }
        }

        public int IsDeal
        {
            get { return _isDeal; }
            set { _isDeal = value; }
        }
    }
}
