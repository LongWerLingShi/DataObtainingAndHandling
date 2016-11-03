using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Pipeline
{

    class DataMining
    {
        /*
         * 原网页和去噪后的数据
         */
        private string _rawdata;
        private string _denoisingdata;
        /*
         * 需要抽取的项
         */
        private string _title;
        private string _postdate;
        private string _author;

        const int TAGNUM = 4;

        public string Rawdata
        {
            set { _rawdata = value; }
        }

        public string Denoisingdata
        {
            set { _denoisingdata = value; }
        }

        public string Title
        {
            get { return _title; }
        }

        public string Author
        {
            get { return _author; }
        }

        public string Postdate
        {
            get { return _postdate; }
        }

        public DataMining()
        {
            this._author = "";
            this._postdate = "";
            this._denoisingdata = "";
            this._title = "";
        }

        private string getCombination(string str1, string str2)
        {
            if (MainWindow.isCHineseWebPage)
            {
                return str1 + str2;
            }
            else
            {
                return str1 + " " + str2;
            }
        }

        private string RemoveBlank(string curTag)
        {
            return Regex.Replace(curTag, @"\s", "");
        }

        //部分包含也是重复的一种
        //譬如已有tag "AB"，则tag "A"或"B"都是重复的 
        private bool RepeattedTag(string curTag)
        {
            foreach (string tag in MainWindow._tags)
            {
                if (tag.Contains(curTag) || curTag.Contains(tag))
                {
                    return true;
                }
            }
            return false;
        }

        private void FreshDenoisingData()
        {
            /*if (!MainWindow.isCHineseWebPage)
            {
                string[] words = _denoisingdata.Split(new char[] {' ', '`', '~', '!', '@', '$', '%', 
                            '^', '&', '*','(', ')', '_', '+', '=', '{', '[', '}', ']', ':', ';', '\"', '\'', ',', 
                            '<', '.', '>', '?', '/', '\\'});
                _denoisingdata = "";
                foreach (string word in words)
                {
                    _denoisingdata += word.ToLower();
                }
            }*/
            _denoisingdata = _denoisingdata.ToLower();
        }

        private string getPostdate()
        {
            Regex date = new Regex(@"[0-9]{4}.[0-9]{1,2}.");
            string dateResult = "";
            foreach (Match match in date.Matches(_denoisingdata))
            {
                foreach (System.Text.RegularExpressions.Capture capture in match.Groups[1].Captures)
                {
                    dateResult += capture.Value;
                }
            }
            return dateResult;
        }

        private string getAuthor()
        {
            Regex author = new Regex(@"作者\W{0-5}\w+");
            string authorResult = "";
            foreach (Match match in author.Matches(_denoisingdata))
            {
                foreach (System.Text.RegularExpressions.Capture capture in match.Groups[1].Captures)
                {
                    authorResult += capture.Value;
                }
            }
            return authorResult;
        }

        private string getTitle()
        {
            Regex title = new Regex(@"(<title>)(.*)(</title>)");
            string titleResult = "";
            foreach (Match match in title.Matches(_rawdata))
            {
                foreach (System.Text.RegularExpressions.Capture capture in match.Groups[2].Captures)
                {
                    titleResult += capture.Value;
                }
            }
            return titleResult;
        }

        /**
         * 一开始本来打算用TF-IDF算法，这样可以明显提高算法的准确度
         * 但是考虑到时间上的原因，我们简单的采用频率计算的方式
         * 
         */
        private void getKeyWords()
        {
            /*int N=0,n=0;
            SqlConnection con = Connection.instance("10.2.26.60", "XueBa", "crawler", "aimashi2015");
            SqlCommand cmd = con.CreateCommand();
            string countsql = "select count(distinct wid) from XueBa.dbo.WordInverseTable;";
            cmd.CommandText = countsql;
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                N = reader.GetInt32(0);
            }
            con.Close();
            string[] list = new string[MainWindow.wordFreq.Count]; 
            float[] valuelist = new float[MainWindow.wordFreq.Count];
            MainWindow.wordFreq.Keys.CopyTo(list,0);
            MainWindow.wordFreq.Values.CopyTo(valuelist, 0);
            
            float sum = 0;
            foreach (float num in valuelist)
            {
                sum += num;
            }
            int i = 0;
            foreach (string word in list)
            {
                int num = (int)MainWindow.wordFreq[word];
                float freq = num / sum;
                string sqlstr = string.Format("INSERT INTO XueBa.dbo.WordInverseTable(word,wid,freq) values('{0}', '{1}', '{2}')",word,MainWindow.curwid,freq);
                con = Connection.instance("10.2.26.60", "XueBa", "crawler", "aimashi2015");
                cmd = con.CreateCommand();
                cmd.CommandText = sqlstr;
                cmd.ExecuteNonQuery();
                con.Close();

                countsql = string.Format("select count(distinct wid) from XueBa.dbo.WordInverseTable where word = '{0}';",word);
                con = Connection.instance("10.2.26.60", "XueBa", "crawler", "aimashi2015");
                cmd = con.CreateCommand();
                cmd.CommandText = countsql;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    n = reader.GetInt32(0);
                }
                con.Close();
                valuelist[i++] = freq*(float)Math.Log(1.0 * (N + 2) / n);
            }
            Array.Sort(valuelist,list);*/
            string[] list = new string[MainWindow.wordFreq.Count];
            int[] valuelist = new int[MainWindow.wordFreq.Count];
            MainWindow.wordFreq.Keys.CopyTo(list, 0);
            MainWindow.wordFreq.Values.CopyTo(valuelist, 0);
            Array.Sort(valuelist, list);
            if ((list.Length + 9) > MainWindow.maxKeyWordsNo * 10)
            {
                for (int j = 1, k = 1; k <= MainWindow.maxKeyWordsNo && j <= list.Length; j++)
                {
                    if (MainWindow._stopWords.Contains(list[list.Length - j]) || Regex.IsMatch(list[list.Length - j], "^[0-9]+(\\.[0-9]+)?$"))
                    {
                        continue;
                    }
                    else
                    {
                        MainWindow.keywords.Add(list[list.Length - j]);
                        k++;
                    }
                }
            }
            else
            {
                for (int j = 1, k = 1; k <= (list.Length + 9) / 10 && j <= list.Length; j++)
                {
                    if (MainWindow._stopWords.Contains(list[list.Length - j]) || Regex.IsMatch(list[list.Length - j], "^[0-9]+(\\.[0-9]+)?$"))
                    {
                        continue;
                    }
                    else
                    {
                        MainWindow.keywords.Add(list[list.Length - j]);
                        k++;
                    }
                }
            }
        }

        private void getTags()
        {
            for (int i = 0; i < TAGNUM && i < MainWindow.keywords.Count; i++)
            {
                MainWindow._tags.Add(MainWindow.keywords[i]);
            }
        }

        public void Work()
        {
            this._author = getAuthor();
            this._postdate = getPostdate();
            this._title = getTitle();
            this.getKeyWords();
            this.getTags();
        }
    }
}
