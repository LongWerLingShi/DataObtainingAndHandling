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
    public partial class RawData : Form
    {
        private string _rawdata;

        public string Rawdata
        {
            set { _rawdata = value; }
        }

        public RawData()
        {
            InitializeComponent();
        }

        private void RawData_Load(object sender, EventArgs e)
        {
            this.richTextBox1.Text = this._rawdata;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
