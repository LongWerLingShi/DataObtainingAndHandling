using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


using Lucene.Net.Index;
using Lucene.Net.Store;
using Lucene.Net.Analysis;
//using Lucene.Net.Analysis.Standard;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Documents;
using Lucene.Net.Search;
using Lucene.Net.QueryParsers;
using DataMining.WordsSegment;

namespace DataMining
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Segment_Text.Text = "";
            string segmentString = this.Text_For_Segment.Text.ToString();
            List<string> strList = new List<string>();
            strList = WordSegement.cutwords(segmentString, this.comboBox1.Text.ToString());
            //"Lucene.Net.Analysis.SimpleAnalyzer"
            //"Lucene.Net.Analysis.KeywordAnalyzer"
            //"Lucene.Net.Analysis.StopAnalyzer"
            //"Lucene.Net.Analysis.WhitespaceAnalyzer"
            //"Lucene.Net.Analysis.PanGu.PanGuAnalyzer"
            //"Lucene.Net.Analysis.Standard.StandardAnalyzer"
            //"Lucene.China.ChineseAnalyzer"

            foreach (string t in strList)
            {
                this.Segment_Text.Text += t + "\r\n";
            }
            ;
        }
    }
}
