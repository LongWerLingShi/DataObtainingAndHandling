using System;
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
    public partial class FinalData : Form
    {
        private string _title;
        private string _author;
        private string _postdate;
        private GoogleTranslator googletranslator;

        public string Title
        {
            set { _title = value; }
        }

        public string Author
        {
            set { _author = value; }
        }

        public string Postdate
        {
            set { _postdate = value; }
        }

        public FinalData()
        {
            InitializeComponent();
            googletranslator = new GoogleTranslator();
        }

        private void FinalData_Load(object sender, EventArgs e)
        {
            //this.textBox1.Text = translate(_title);
            string str = "", str2 = "";
            foreach (string s in MainWindow._tags)
            {
                str += s + "；";
            }
            this.TagsText.Text = str;
            this.TitleText.Text = MainWindow._title;
            this.AuthorText.Text = MainWindow._author;
            this.PostDateText.Text = MainWindow._postdate;
            this.ContentText.Text = MainWindow._denoisingdata;
            str = "";
            foreach (string s in MainWindow.keywords)
            {
                str += s + "；";
            }
            GoogleTranslator gt = new GoogleTranslator();
            str2 = gt.Analytical(gt.Translate(str, "auto", "auto"));
            if (str!=null && str.Length>0 && GoogleTranslator.isChineseWord(str[0]))
            {
                this.KeywordsText.Text = str;
                this.KeywordsTransText.Text = str2;
            }
            else
            {
                this.KeywordsText.Text = str2;
                this.KeywordsTransText.Text = str;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
