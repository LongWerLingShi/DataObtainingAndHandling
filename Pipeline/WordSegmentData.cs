using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pipeline
{
    public partial class WordSegmentData : Form
    {
        private List<string> _result;
        public static string preWordSegmentor = "Lucene.China.ChineseAnalyzer";

        public List<string> Result
        {
            set { _result = value; }
            get { return this._result; }
        }


        public WordSegmentData()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void WordSegmentData_Load(object sender, EventArgs e)
        {
            //this.richTextBox1.Text = _result;
            string onResult = "";
            if (_result != null)
            {
                foreach (string s in _result)
                {
                    onResult += s + "/";
                }
            }
            this.richTextBox1.Text = onResult;
            listView1.Columns.Add("Word", listView1.Size.Width / 2, HorizontalAlignment.Left);
            listView1.Columns.Add("Frequency", listView1.Size.Width / 2, HorizontalAlignment.Center);

            //sort the words by freq in descending order
            ArrayList freqList = new ArrayList(MainWindow.wordFreq.Values);
            ArrayList occurredFreqList = new ArrayList();
            freqList.Sort();        //升序排列 
            freqList.Reverse();     //降序排列
            foreach (int freq in freqList)
            {
                if (!occurredFreqList.Contains(freq))
                {
                    IDictionaryEnumerator de = MainWindow.wordFreq.GetEnumerator();
                    while (de.MoveNext())
                    {
                        if ((int)de.Value == freq)
                        {
                            ListViewItem lvi = new ListViewItem(de.Key.ToString());
                            lvi.SubItems.Add(de.Value.ToString());
                            listView1.Items.Add(lvi);
                        }
                    }
                    occurredFreqList.Add(freq);
                }
            }
        }

        private void changeWordSegement()
        {
            if (comboBox1.Text != preWordSegmentor)
            {
                //  初始化一个用来分词的类，对去噪数据进行分词处理
                WordSegment ws = new WordSegment();
                preWordSegmentor = comboBox1.Text;
                MainWindow._wordSegmentResult = ws.cutwords(MainWindow._denoisingdata, preWordSegmentor);
                MainWindow.wordFreq.Clear();
                foreach (string word in MainWindow._wordSegmentResult)
                {
                    if (MainWindow.wordFreq.ContainsKey(word))
                    {
                        int freq = (int)MainWindow.wordFreq[word];
                        MainWindow.wordFreq[word] = freq + 1;
                    }
                    else
                    {
                        MainWindow.wordFreq.Add(word, 1);
                    }
                }

                listView1.Items.Clear();
                listView1.Columns.Add("Word", listView1.Size.Width / 2, HorizontalAlignment.Left);
                listView1.Columns.Add("Frequency", listView1.Size.Width / 2, HorizontalAlignment.Center);
                ArrayList freqList = new ArrayList(MainWindow.wordFreq.Values);
                ArrayList occurredFreqList = new ArrayList();
                freqList.Sort();        //升序排列 
                freqList.Reverse();     //降序排列
                foreach (int freq in freqList)
                {
                    if (!occurredFreqList.Contains(freq))
                    {
                        IDictionaryEnumerator de = MainWindow.wordFreq.GetEnumerator();
                        while (de.MoveNext())
                        {
                            if ((int)de.Value == freq)
                            {
                                ListViewItem lvi = new ListViewItem(de.Key.ToString());
                                lvi.SubItems.Add(de.Value.ToString());
                                listView1.Items.Add(lvi);
                            }
                        }
                        occurredFreqList.Add(freq);
                    }
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            changeWordSegement();
        }
    }
}
