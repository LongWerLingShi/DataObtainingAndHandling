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

namespace Pipeline.UploadData
{
    public partial class UploadProcessBar : Form
    {
        private Thread fThread;
        private int N = 1;
        private delegate void SetPos(int ipos, string vInfo); //代理
        public UploadProcessBar()
        {
            InitializeComponent();
            Program.InitQuestionList();
            N = Program.qlist.Count;
        }

        private void SetTextMessage(int ipos, string vinfo)
        {
            if (this.InvokeRequired)
            {
                SetPos setpos = new SetPos(SetTextMessage);
                this.Invoke(setpos, new object[] { ipos, vinfo });
            }
            else
            {
                this.label1.Text = ipos.ToString() + "/" + N;
                this.progressBar1.Value = Convert.ToInt32(ipos) * 100 / N;
                this.textBox1.AppendText(vinfo);
            }
        }

        private void SleepT()
        {
            foreach (Question qst in Program.qlist)
            {
                System.Threading.Thread.Sleep(100);
                SetTextMessage(Program.iter+1, Program.iter.ToString() + ".txt\r\n");
                Program.genJson();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Program.iter = 0;
            fThread = new Thread(new ThreadStart(SleepT));
            fThread.Start();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (fThread.ThreadState != ThreadState.Suspended)
            {
                fThread.Suspend();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (fThread.ThreadState != ThreadState.Running && fThread.ThreadState != ThreadState.WaitSleepJoin)
            {
                fThread.Resume();
            }
        }
    }
}
