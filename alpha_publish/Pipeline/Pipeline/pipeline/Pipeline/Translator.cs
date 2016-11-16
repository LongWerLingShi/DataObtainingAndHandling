using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Pipeline
{
    public partial class Translator : Form
    {
        public Translator()
        {
            string strTran = "";
            string[] strs;
            if (MainWindow._denoisingdata != null)
            {
                strs = MainWindow._denoisingdata.Split(new Char[] { '.', '。', ',', '，' });
                for (int i = 0; i < strs.Length; i++)
                {
                    strTran += translate(strs[i]);
                }
            }
            InitializeComponent();
            this.original_Text.Text = MainWindow._denoisingdata;
            this.text_Compare_Original_Text.Text = MainWindow._denoisingdata;
            this.translated_Text.Text = strTran;
            this.text_Compare_Translated_Text.Text = strTran;
        }

        private void Translator_Load(object sender, EventArgs e)
        {


        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void Ori_Txt_Tab_Ctrl_DrawIt(object sender, DrawItemEventArgs e)
        {
            SolidBrush color1 = new SolidBrush(Color.Tomato);
            SolidBrush color2 = new SolidBrush(Color.Orange);
            SolidBrush color3 = new SolidBrush(Color.LightGreen);
            SolidBrush black = new SolidBrush(Color.Black);
            StringFormat stringFormat = new StringFormat();
            Rectangle rec1 = tabControl1.GetTabRect(0);
            e.Graphics.FillRectangle(color1, rec1);
            Rectangle rec2 = tabControl1.GetTabRect(1);
            e.Graphics.FillRectangle(color2, rec2);
            Rectangle rec3 = tabControl1.GetTabRect(2);
            e.Graphics.FillRectangle(color3, rec3);
            stringFormat.Alignment = StringAlignment.Center;
            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                Rectangle rec = tabControl1.GetTabRect(i);
                e.Graphics.DrawString(tabControl1.TabPages[i].Text, new Font("黑体", 10), black, rec, stringFormat);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private static string translate(string str)
        {
            if (str == null || str == "")
                return str;
            GoogleTranslator googletranslator = new GoogleTranslator();
            str = googletranslator.Analytical(googletranslator.Translate(str, "auto"));
            return str;
        }
    }
}
