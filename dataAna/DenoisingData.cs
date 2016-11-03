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
    public partial class DenoisingData : Form
    {
        private string _denoisingdata;
        //private GoogleTranslator googletranslator;

        public string Denoisingdata
        {
            set { _denoisingdata = value; }
        }

        public DenoisingData()
        {
            InitializeComponent();
            //googletranslator = new GoogleTranslator();
        }

        private void DenoisingData_Load(object sender, EventArgs e)
        {
            //this.richTextBox1.Text = translate(_denoisingdata);
            this.richTextBox1.Text = _denoisingdata;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        /*
        
        private string translate(string str)
        {
            if (str == null || str == "")
                return str;
            if (!isChineseWord(str[0]))
            {
                str = str + "(" + googletranslator.Analytical(googletranslator.Translate(str, "zh")) + ")";
            }
            return str;
        }*/
    }
}
