using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pipeline
{
    public partial class InputNewContent : Form
    {
        private string pagetitle;
        private string author;
        private string postdate;
        private List<string> tags;
        private Hashtable tagpreviews;



        public InputNewContent()
        {
            InitializeComponent();
            tags = new List<string>();
            tagpreviews = new Hashtable();
        }

        private void InputNewContent_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)  //  Denoising
        {
            pagetitle = textBox1.Text;
            author = textBox2.Text;
            postdate = textBox3.Text;
            Page a = new Page();
            PageItem item1 = new PageItem();
            int no1 = MainWindow.getWebpageNo() + 1;
            item1.setwid(no1);
            item1.settitle(pagetitle);
            item1.setauthor(author);
            item1.setpostdate(postdate);
            item1.setlink(textBox5.Text);
            item1.setreferred("");
            a.savePage(item1);
            //save to table TAGS
            foreach (string tag in tags)
            {
                if (!MainWindow._occurredTags.Contains(tag))
                {
                    Tag c = new Tag();
                    TagItem item2 = new TagItem();
                    item2.settid(MainWindow.getTagNo() + 1);
                    item2.setname(tag);
                    c.saveTag(item2);
                }
            }

            foreach (string tag in tags)
            {
                Preview b = new Preview();
                PreviewItem item3 = new PreviewItem();
                int no2 = MainWindow.getTagNo(tag);
                item3.settid(no2);
                item3.setwid(no1);
                item3.setpreview((string)tagpreviews[tag]);
                b.savePreview(item3);
            }
        }

        private void button2_Click(object sender, EventArgs e)  //  Raw data
        {

            this.tags.Add(textBox4.Text.Trim());
            listView1.Items.Add(textBox4.Text.Trim());
            tagpreviews.Add(textBox4.Text, richTextBox1.Text);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
