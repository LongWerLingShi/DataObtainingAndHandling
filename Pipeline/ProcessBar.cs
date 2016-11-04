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
using System.Data.SqlClient;
using System.Windows.Forms.DataVisualization.Charting;
using Pipeline.AppConfig;


namespace Pipeline
{
    //产生相应的显示功能
    public partial class ProgressBar : Form
    {
        private Thread fThread;
        private int N = 1;
        private delegate void SetPos(int ipos, string vInfo); //代理

        Series series1 = new Series("未处理");
        Series series2 = new Series("处理过");
        Series series3 = new Series("未完成");

        public ProgressBar()
        {
            InitializeComponent();
            string countsql = string.Format("select count(distinct id) from Crawler.dbo.fileinfo;");
            SqlConnection con = Connection.instance(AppConfiguration.GetConfigValue("serverIp"), "crawler", AppConfiguration.GetConfigValue("username"), AppConfiguration.GetConfigValue("password"));
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = countsql;
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                N = reader.GetInt32(0);
            }
            con.Close();

            chart1.Series.Clear();
            //设置图表类型
            series1.ChartType = SeriesChartType.Column;
            series1.BorderWidth = 9;
            series1.ShadowOffset = 3;
            //x轴名称
            series1.Points.AddY(5 * N / 10);
            series1.Points[0].AxisLabel = "未处理 处理过 未完成";


            //设置图表类型
            series2.ChartType = SeriesChartType.Column;
            series2.BorderWidth = 9;
            series2.ShadowOffset = 3;
            //x轴名称
            series2.Points.AddY(4*N / 10);


            //设置图表类型
            series3.ChartType = SeriesChartType.Column;
            series3.BorderWidth = 9;
            series3.ShadowOffset = 3;
            //x轴名称
            series3.Points.AddY(N/10);

            chart1.Series.Add(series1);
            chart1.Series.Add(series2);
            chart1.Series.Add(series3);

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
                this.progressBar1.Value = Convert.ToInt32(ipos)*100/N;
                this.textBox1.AppendText(vinfo);
            }
        }
        private void ProgressBar_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(fThread!=null)
                fThread.Abort();
            this.Close();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (fThread != null && fThread.ThreadState != ThreadState.Running && fThread.ThreadState != ThreadState.WaitSleepJoin)
            {
                fThread.Resume();
            }
        }

        private void SleepT()
        {
            foreach (WebInDB webpage in MainWindow._web)
            {
                System.Threading.Thread.Sleep(100);
                SetTextMessage(MainWindow.dealNo+MainWindow.cannotDealNo+6*N/10, webpage.Id.ToString() + "\r\n");
                this.chart1.Series[0].Points[0].YValues[0] = (9 * N / 10 - (MainWindow.dealNo + MainWindow.cannotDealNo));
                this.chart1.Series[1].Points[0].YValues[0] = (N / 10 + MainWindow.dealNo);
                this.chart1.Series[2].Points[0].YValues[0] = (MainWindow.cannotDealNo);
                MainWindow.ProcessSingle(webpage);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fThread = new Thread(new ThreadStart(SleepT));
            fThread.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (fThread != null && fThread.ThreadState != ThreadState.Suspended)
            {
                fThread.Suspend();
            }
        }

    }
}
