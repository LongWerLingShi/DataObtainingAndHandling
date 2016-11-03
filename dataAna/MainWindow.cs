using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pipeline.AppConfig;

namespace Pipeline
{
    public partial class MainWindow : Form
    {
        /*
         * 从rawdata 到 postdate
         * 一个更加合理的解决方法是像之后的 wordFreq和_tags 一样设置成public static类型
         * 这样就不用在几个不同的类都声明相同的数据
         * 不管从空间成本还是实现成品来说都是第一种方式比较优秀
         * 
         */

        public static string _rawdata;    //  原始数据
        public static string _denoisingdata;//去噪数据
        public static string _author;//作者
        public static string _title;//标题
        public static string _postdate;//发布日期
        public static List<string> _wordSegmentResult;//分词结果
        public static List<WebInDB> _web;//数据信息列表(id, url, filepath)
        public static int curwid;
        public static int curtid;
        public static int _id; //  动态增长的id  
        public static string curUrl;  //  当前处理的网址
        public static int dealNo = 0;
        public static int cannotDealNo = 0;

        //two filepathes
        //const string _filepath = @"..\..\..\CrawlerFiles";
        //for test
        //const string _filepath = @"C:\Users\yuanhang1617\Desktop";

        //  标签的路径
        //const string _tagpath = @"F:\ziliao\C705\Pipeline\Tags.txt";

        public static Hashtable wordFreq = new Hashtable();
        public static List<string> _tags;
        public static List<string> _occurredTags;
        public static List<string> keywords = new List<string>();
        public static int maxKeyWordsNo = 5;
        public static bool isCHineseWebPage;
        public static Hashtable tagPreview;
        //public static List<string> _newTags;
        public static List<string> _stopWords;
        private string _stopWordsPath = Application.StartupPath + "\\" + "StopWords.txt";
        private string _tagPath = Application.StartupPath + "\\" + "Tags.txt";

        public static int getTagNo()  //  获取最大的tag_id
        {
            string selectSQL = string.Format("SELECT max(tid) FROM XueBa.dbo.Tag;");
            SqlConnection con = Connection.instance(AppConfiguration.GetConfigValue("serverIp"),"crawler",AppConfiguration.GetConfigValue("username"),AppConfiguration.GetConfigValue("password"));
            SqlCommand Command = con.CreateCommand();
            Command.CommandText = selectSQL;
            SqlDataReader reader = Command.ExecuteReader();
            reader.Read();
            if (reader.ToString().Equals(""))    //  如果本来数据库是空的，那么返回0，下一个id就会是0+1=1
            {
                con.Close();
                return 0;
            }
            else
            {
                int maxid = reader.GetInt32(0);
                con.Close();
                return maxid;
            }
        }

        public static int getTagNo(string tag)    //  获得制定tag的id
        {
            string selectSQL = string.Format("SELECT tid FROM XueBa.dbo.Tag WHERE name = '{0}'", tag);
            SqlConnection con = Connection.instance(AppConfiguration.GetConfigValue("serverIp"), "crawler", AppConfiguration.GetConfigValue("username"), AppConfiguration.GetConfigValue("password"));
            SqlCommand Command = con.CreateCommand();
            Command.CommandText = selectSQL;
            SqlDataReader reader = Command.ExecuteReader();
            int i = 0;
            if (reader.Read())
            {
                i = reader.GetInt32(0);
            }
            con.Close();
            return i;
        }

        public static int getWebpageNo()  //  获得最大的wid
        {
            string selectSQL = string.Format("SELECT max(wid) FROM XueBa.dbo.WebPage;");
            SqlConnection con = Connection.instance(AppConfiguration.GetConfigValue("serverIp"), "XueBa", AppConfiguration.GetConfigValue("username"), AppConfiguration.GetConfigValue("password"));
            SqlCommand Command = con.CreateCommand();
            Command.CommandText = selectSQL;
            SqlDataReader reader = Command.ExecuteReader();
            reader.Read();
            if (reader[0].ToString().Equals(""))
            {
                con.Close();
                return 0;
            }
            else
            {
                int maxid = reader.GetInt32(0);
                con.Close();
                return maxid;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            // test();
            _tags = new List<string>();
            _occurredTags = new List<string>();
            tagPreview = new Hashtable();
            _web = new List<WebInDB>();

            try
            {
                uint result = WNetConnectionHelper.WNetAddConnection("Crawler", "Ase12345678", @"\\10.2.28.78\XueBaResources", null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.ToString());
            }

            try
            {
                _stopWords = new List<string>();
                StreamReader sr = new StreamReader(_stopWordsPath);
                string stopWord;
                while ((stopWord = sr.ReadLine()) != null)
                {
                    _stopWords.Add(stopWord);
                }

                //For test, tags are in a local file
                //Add all occurred tags to the list

                sr = new StreamReader(_tagPath);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    _occurredTags.Add(line);
                }
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.ToString());
            }


            //For use, tags are in DB
            //DB OPERATION
            //So extracte then to _tags from DB

            string selectSQL = string.Format("SELECT name FROM XueBa.dbo.Tag;");
            SqlConnection con = Connection.instance(AppConfiguration.GetConfigValue("serverIp"), "XueBa", AppConfiguration.GetConfigValue("username"), AppConfiguration.GetConfigValue("password"));
            SqlCommand Command = con.CreateCommand();
            Command.CommandText = selectSQL;
            SqlDataReader reader = Command.ExecuteReader();
            while (reader.Read())
            {
                _occurredTags.Add((string)reader[0]);
            }
            con.Close();
        }
        public void test()
        {
            string selectSQL = string.Format("insert into XueBa.dbo.c705questions (qid,views) values (1013,2);");
            SqlConnection con = Connection.instance(AppConfiguration.GetConfigValue("serverIp"), "XueBa", AppConfiguration.GetConfigValue("username"), AppConfiguration.GetConfigValue("password"));
            SqlCommand Command = con.CreateCommand();
            Command.CommandText = selectSQL;
            Command.ExecuteNonQuery();
            //while (reader.Read())
            MessageBox.Show("ok");
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            // For test and presentation
            //添加在爬虫下载文件夹下的所有htm或html文件
            /*
            Regex re = new Regex(@"(?:\.htm|\.html)$", RegexOptions.IgnoreCase);

            DirectoryInfo TheFolder=new DirectoryInfo(_filepath);
            foreach (FileInfo NextFile in TheFolder.GetFiles())
            {
                if (re.IsMatch(NextFile.Name))
                {
                    this.filelistbox.Items.Add(NextFile.Name);
                }
            }

            this.totalfilebox.Text = this.filelistbox.Items.Count.ToString();
             * */
            //DB OPERATION
            //提取Crawler在数据库中存放的网页
            //和上面一样将网页链接存放在listbox中
            //此时修改Process方法，因为本地没有文件，应直接对Denoising实例化对象的_rawdata赋数据库中的值
            //原来的代码不要删掉，注释掉就可以
            string selectSQL = string.Format("SELECT id, myUrl, filepath, isDeal FROM crawler.dbo.fileinfo ORDER BY filepath");
            //MessageBox.Show(selectSQL);
            SqlConnection con = Connection.instance(AppConfiguration.GetConfigValue("serverIp"), "crawler", AppConfiguration.GetConfigValue("username"), AppConfiguration.GetConfigValue("password"));
            SqlCommand Command = con.CreateCommand();
            Command.CommandText = selectSQL;
            SqlDataReader reader = Command.ExecuteReader();
            while (reader.Read())
            {
                //  reader[0] : id, reader[1]：url, reader[2]：filepath, reader[3]:isDeal
                WebInDB widb = new WebInDB((int)reader[0], (string)reader[1], (string)reader[2], (int)reader[3]);
                _web.Add(widb);
                this.filelistbox.Items.Add((string)reader[2]);
            }
            this.totalfilebox.Text = this.filelistbox.Items.Count.ToString();
            con.Close();
        }

        public static void insertintodb(string title, string _abstract, string link, string date, string tag, int answer)
        {
            tag = tag.Substring(0, (int)Math.Min(60, tag.Length)).Replace("\"", "").Replace("'", "");
            string selectSQL = string.Format("select tid from XueBa.dbo.Tag where (name = '{0}')", tag);
            SqlConnection con1 = Connection.instance(AppConfiguration.GetConfigValue("serverIp"), "XueBa", AppConfiguration.GetConfigValue("username"), AppConfiguration.GetConfigValue("password"));
            SqlCommand Command1 = con1.CreateCommand();
            Command1.CommandText = selectSQL;
            int tid;
            // try
            //{
            SqlDataReader reader = Command1.ExecuteReader();
            //tid = (int)reader[0];
            if (reader.Read())
                tid = (int)reader["tid"];
            else
            {
                string insertSQL2 = string.Format("INSERT INTO XueBa.dbo.Tag(name) VALUES('{0}')", tag);
                SqlConnection con2 = Connection.instance(AppConfiguration.GetConfigValue("serverIp"), "XueBa", AppConfiguration.GetConfigValue("username"), AppConfiguration.GetConfigValue("password"));
                SqlCommand Command2 = con2.CreateCommand();
                Command2.CommandText = insertSQL2;
                try
                {
                    Command2.ExecuteNonQuery();
                }
                catch (Exception f)
                {
                    Console.WriteLine(f.StackTrace);
                }
                con2.Close();
                string selectSQL3 = string.Format("select tid from XueBa.dbo.Tag where (name = '{0}')", tag);
                SqlConnection con3 = Connection.instance(AppConfiguration.GetConfigValue("serverIp"), "XueBa", AppConfiguration.GetConfigValue("username"), AppConfiguration.GetConfigValue("password"));
                SqlCommand Command3 = con3.CreateCommand();
                Command3.CommandText = selectSQL3;
                SqlDataReader reader1 = Command3.ExecuteReader();
                if (reader1.Read())
                    tid = (int)reader1["tid"];
                else
                    tid = 10000000;
                con3.Close();
            }

            // }
            // catch (InvalidOperationException e){
            //     tid = 10000000;
            // }

            con1.Close();
            title = title.Substring(0, (int)Math.Min(60, title.Length)).Replace("\"", "").Replace("'", "");
            _abstract = _abstract.Substring(0, (int)Math.Min(60, _abstract.Length)).Replace("\"", "").Replace("'", "");
            string insertSQL = string.Format("INSERT INTO XueBa.dbo.c705questions(title,abstract,link,created,answer,tid, views) VALUES('{0}','{1}','{2}','{3}',{4},{5},{6})", title, _abstract, link, date, answer, tid, 0);
            SqlConnection con = Connection.instance(AppConfiguration.GetConfigValue("serverIp"), "XueBa", AppConfiguration.GetConfigValue("username"), AppConfiguration.GetConfigValue("password"));
            SqlCommand Command = con.CreateCommand();
            Command.CommandText = insertSQL;
            try
            {
                Command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                //MessageBox.Show("wrong");
            }
            con.Close();
        }

        private void Input_New_Click(object sender, EventArgs e)
        {
            //WordSegmentData wsd = new WordSegmentData();
            InputNewContent INC = new InputNewContent();
            //wsd.Result = this._wordSegmentResult;
            this.Hide();
            INC.ShowDialog();
            this.Show();
        }

        private void Raw_Data_Click(object sender, EventArgs e)
        {
            RawData rd = new RawData();
            rd.Rawdata = _rawdata;
            this.Hide();
            rd.ShowDialog();
            this.Show();
        }

        private void Denoising_Data_Click(object sender, EventArgs e)
        {
            DenoisingData dd = new DenoisingData();
            dd.Denoisingdata = _denoisingdata;
            this.Hide();
            dd.ShowDialog();
            this.Show();
        }

        private void Word_Segment_Click(object sender, EventArgs e)
        {
            WordSegmentData wsd = new WordSegmentData();
            wsd.Result = _wordSegmentResult;
            this.Hide();
            wsd.ShowDialog();
            this.Show();
        }
        private static void updateData(int i, int webid)
        {
            cannotDealNo++;
            string updateSQL = string.Format("update crawler.dbo.fileinfo set isDeal='{0}' where id = '{1}'", i, webid);
            SqlConnection con = Connection.instance(AppConfiguration.GetConfigValue("serverIp"), "crawler", AppConfiguration.GetConfigValue("username"), AppConfiguration.GetConfigValue("password"));
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = updateSQL;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        private static void updateData(int i, string filepath)
        {
            cannotDealNo++;
            string updateSQL = string.Format("update crawler.dbo.fileinfo set isDeal='{0}' where FilePath = '{1}'", i, filepath);
            SqlConnection con = Connection.instance(AppConfiguration.GetConfigValue("serverIp"), "crawler", AppConfiguration.GetConfigValue("username"), AppConfiguration.GetConfigValue("password"));
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = updateSQL;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        private void Process_Sig_Click(object sender, EventArgs e)
        {
            /*
             * 处理一个选中的html或htm文件
             * 实现是否选择的判定机制
             */
            bool needtoInsert = true;
            int id;
            if (filelistbox.SelectedItem == null)
            {
                MessageBox.Show("Please choose a file first!", "ERROR");
                return;
            }
            String curPath = this.filelistbox.SelectedItem.ToString();
            string selectSQL = string.Format("SELECT id, myUrl FROM crawler.dbo.fileinfo where filepath = '{0}'", curPath);
            SqlConnection con = Connection.instance(AppConfiguration.GetConfigValue("serverIp"), "crawler", AppConfiguration.GetConfigValue("username"), AppConfiguration.GetConfigValue("password"));
            SqlCommand Command = con.CreateCommand();
            Command.CommandText = selectSQL;
            SqlDataReader reader = Command.ExecuteReader();
            if (reader.Read())
            {
                id = reader.GetInt32(0);
                curUrl = reader[1].ToString();
                con.Close();
            }
            else
            {
                updateData(2, curPath);
                MessageBox.Show("related url can not be found. ");
                con.Close();
                return;
            }

            selectSQL = string.Format("SELECT * FROM XueBa.dbo.WebPage WHERE link = '{0}'", curUrl);
            con = Connection.instance(AppConfiguration.GetConfigValue("serverIp"), "XueBa", AppConfiguration.GetConfigValue("username"), AppConfiguration.GetConfigValue("password"));
            Command = con.CreateCommand();
            Command.CommandText = selectSQL;
            reader = Command.ExecuteReader();

            string selectSQL1 = string.Format("SELECT * FROM XueBa.dbo.c705questions WHERE link like '{0}'", curUrl);
            SqlConnection con1 = Connection.instance(AppConfiguration.GetConfigValue("serverIp"), "XueBa", AppConfiguration.GetConfigValue("username"), AppConfiguration.GetConfigValue("password"));
            SqlCommand Command1 = con1.CreateCommand();
            Command1.CommandText = selectSQL1;
            SqlDataReader reader1 = Command1.ExecuteReader();
            if (reader.Read() || reader1.Read())
            {
                MessageBox.Show("This page has been processed!");
                needtoInsert = false;
            }

            curPath = Regex.Split(curPath, "XueBaResources")[1];
            curPath = @"\\10.2.28.78\XueBaResources" + curPath;
            FileInfo temp = new FileInfo(curPath);
            if (temp.Extension.Equals(".pdf"))
            {
                //PipeLine.OtherToHtml.IcDocument document = PipeLine.OtherToHtml.FactoryDocument.GetDocoment(@"F:\ziliao\C705\Pipeline\text", curPath);
                PipeLine.OtherToHtml.IcDocument document = PipeLine.OtherToHtml.FactoryDocument.GetDocoment(@"\\10.2.28.78\XueBaResources", curPath);
                document.TransformDocument();
                //ProcessProcedure.Processpdf(@"F:\ziliao\C705\Pipeline\text\" + temp.Name + ".html", true);
                ProcessProcedure.Processpdf(@"\\10.2.28.78\XueBaResources\" + temp.Name + ".html", needtoInsert);
            }
            else if (temp.Extension.Equals(".doc"))
            {
            }
            else
            {
                if (ProcessProcedure.baiduzhidao(curUrl))
                {
                    ProcessProcedure.baiduzhidaoprocess(curPath, curUrl);
                }
                else if (ProcessProcedure.cnblogs(curUrl))
                {
                    ProcessProcedure.cnblogsprocess(curPath, curUrl);
                }
                else if (ProcessProcedure.dewen(curUrl))
                {
                    ProcessProcedure.dewenprocess(curPath, curUrl);
                }
                else if (ProcessProcedure.stackoverflow(curUrl))
                {
                    ProcessProcedure.stackoverflowprocess(curPath, curUrl);//  process stackoverflow question answer pair
                }
                else if (ProcessProcedure.sosowenwen(curUrl))
                {
                    ProcessProcedure.sosowenwenprocess(curPath, curUrl);//  process sosowenwen question answer pair
                }
                else
                {
                    ProcessProcedure.Process(curPath, needtoInsert);//@"C:\C705\Pipeline\TestFiles\" + 
                }
            }
            con.Close();
            con1.Close();
            if (needtoInsert)
            {
                dealNo++;
                updateData(1, this.filelistbox.SelectedItem.ToString());
            }
            MessageBox.Show("Processing OK!");
        }

        public static void ProcessSingle(WebInDB web)
        {
            /*
             * 处理一个选中的html或htm文件
             * 实现是否选择的判定机制
             */
            if (web.Filepath != null && web.IsDeal == 0)
            {
                String curPath = web.Filepath;
                string selectSQL = string.Format("SELECT id, myUrl FROM crawler.dbo.fileinfo where filepath = '{0}'", curPath);
                SqlConnection con = Connection.instance(AppConfiguration.GetConfigValue("serverIp"), "crawler", AppConfiguration.GetConfigValue("username"), AppConfiguration.GetConfigValue("password"));
                SqlCommand Command = con.CreateCommand();
                Command.CommandText = selectSQL;
                SqlDataReader reader = Command.ExecuteReader();
                if (reader.Read())
                {
                    curUrl = reader[1].ToString();
                    con.Close();
                }
                else
                {
                    updateData(2, web.Id);
                    cannotDealNo++;
                    con.Close();
                    return;
                }

                selectSQL = string.Format("SELECT * FROM XueBa.dbo.WebPage WHERE link = '{0}'", curUrl);
                con = Connection.instance(AppConfiguration.GetConfigValue("serverIp"), "XueBa", AppConfiguration.GetConfigValue("username"), AppConfiguration.GetConfigValue("password"));
                Command = con.CreateCommand();
                Command.CommandText = selectSQL;
                reader = Command.ExecuteReader();

                string selectSQL1 = string.Format("SELECT * FROM XueBa.dbo.c705questions WHERE link like '{0}'", curUrl);
                //MessageBox.Show(curItem);
                SqlConnection con1 = Connection.instance(AppConfiguration.GetConfigValue("serverIp"), "XueBa", AppConfiguration.GetConfigValue("username"), AppConfiguration.GetConfigValue("password"));
                SqlCommand Command1 = con1.CreateCommand();
                Command1.CommandText = selectSQL1;
                SqlDataReader reader1 = Command1.ExecuteReader();
                if (reader.Read() || reader1.Read())
                {
                    con.Close();
                    con1.Close();
                    return;
                }
                else
                {
                    dealNo++;
                    curPath = Regex.Split(curPath, "XueBaResources")[1];
                    curPath = @"\\10.2.28.78\XueBaResources" + curPath;
                    FileInfo temp = new FileInfo(curPath);
                    if (temp.Extension.Equals(".pdf"))
                    {
                        //PipeLine.OtherToHtml.IcDocument document = PipeLine.OtherToHtml.FactoryDocument.GetDocoment(@"F:\ziliao\C705\Pipeline\text", curPath);
                        PipeLine.OtherToHtml.IcDocument document = PipeLine.OtherToHtml.FactoryDocument.GetDocoment(@"\\10.2.28.78\XueBaResources", curPath);
                        document.TransformDocument();
                        //ProcessProcedure.Processpdf(@"F:\ziliao\C705\Pipeline\text\" + temp.Name + ".html", true);
                        ProcessProcedure.Processpdf(@"\\10.2.28.78\XueBaResources\" + temp.Name + ".html", true);
                    }
                    else
                    {
                        if (ProcessProcedure.baiduzhidao(curUrl))
                        {
                            ProcessProcedure.baiduzhidaoprocess(curPath, curUrl);
                        }
                        else if (ProcessProcedure.cnblogs(curUrl))
                        {
                            ProcessProcedure.cnblogsprocess(curPath, curUrl);
                        }
                        else if (ProcessProcedure.dewen(curUrl))
                        {
                            ProcessProcedure.dewenprocess(curPath, curUrl);
                        }
                        else if (ProcessProcedure.stackoverflow(curUrl))
                        {
                            ProcessProcedure.stackoverflowprocess(curPath, curUrl);//  process stackoverflow question answer pair
                        }
                        else if (ProcessProcedure.sosowenwen(curUrl))
                        {
                            ProcessProcedure.sosowenwenprocess(curPath, curUrl);//  process sosowenwen question answer pair
                        }
                        else
                        {
                            ProcessProcedure.Process(curPath, true);//@"C:\C705\Pipeline\TestFiles\" + 
                        }
                    }
                    updateData(1, web.Id);
                }
            }
            else if (web.IsDeal == 0 && web.Filepath == null)
            {
                cannotDealNo++;
                string updateSQL = string.Format("update crawler.dbo.fileinfo set isDeal='{0}' where id = '{1}'", 2, web.Id);
                SqlConnection con = Connection.instance(AppConfiguration.GetConfigValue("serverIp"), "crawler", AppConfiguration.GetConfigValue("username"), AppConfiguration.GetConfigValue("password"));
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = updateSQL;
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        private void Final_Data_Click(object sender, EventArgs e)
        {
            FinalData fd = new FinalData();
            fd.Author = _author;
            fd.Title = _title;
            fd.Postdate = _postdate;
            this.Hide();
            fd.ShowDialog();
            this.Show();
        }

        private void Process_All_Click(object sender, EventArgs e)
        {
            string selectSQL = string.Format("SELECT count(*) FROM crawler.dbo.fileinfo where isDeal='{0}';", 1);
            SqlConnection con = Connection.instance(AppConfiguration.GetConfigValue("serverIp"), "crawler", AppConfiguration.GetConfigValue("username"), AppConfiguration.GetConfigValue("password"));
            SqlCommand Command = con.CreateCommand();
            Command.CommandText = selectSQL;
            SqlDataReader reader = Command.ExecuteReader();
            while (reader.Read())
            {
                dealNo = reader.GetInt32(0);
            }
            con.Close();

            selectSQL = string.Format("SELECT count(*) FROM crawler.dbo.fileinfo where isDeal='{0}';", 2);
            con = Connection.instance(AppConfiguration.GetConfigValue("serverIp"), "crawler", AppConfiguration.GetConfigValue("username"), AppConfiguration.GetConfigValue("password"));
            Command = con.CreateCommand();
            Command.CommandText = selectSQL;
            reader = Command.ExecuteReader();
            while (reader.Read())
            {
                cannotDealNo = reader.GetInt32(0);
            }
            con.Close();

            this.Hide();
            ProgressBar pb = new ProgressBar();
            pb.ShowDialog();
            this.Show();
        }

        /*public static void ProcessAll()  //  process all
        {
        REPEAT:
            {
                string selectSQL = string.Format("SELECT id, myUrl, filepath FROM crawler.dbo.fileinfo ORDER BY filepath");
                //MessageBox.Show(selectSQL);
                SqlConnection con = Connection.instance();
                SqlCommand Command = con.CreateCommand();
                Command.CommandText = selectSQL;
                SqlDataReader reader = Command.ExecuteReader();
                while (reader.Read())
                {
                    //  reader[0] : id, reader[1]：url, reader[2]：filepath
                    WebInDB widb = new WebInDB((int)reader[0], (string)reader[1], (string)reader[2]);
                    _web.Add(widb);
                }
                con.Close();
            }
            /*
            int i = 0;
            foreach (WebInDB widb in _web)
            {
                i++;
                dealNo++;
                if (i == 30000)
                    i = i + 1;
                curUrl = widb.Url;
                _tags.Clear();
                tagPreview.Clear();
                string selectSQL = string.Format("SELECT * FROM XueBa.dbo.WebPage WHERE link = '{0}'", curUrl);
                SqlConnection con = Connection.instance("10.2.28.78", "XueBa", "crawler", "aimashi2015");
                SqlCommand Command = con.CreateCommand();
                Command.CommandText = selectSQL;
                SqlDataReader reader = Command.ExecuteReader();
                string selectSQL1 = string.Format("SELECT * FROM XueBa.dbo.c705questions WHERE link like '{0}'", curUrl);
                //MessageBox.Show(curItem);
                SqlConnection con1 = Connection.instance();
                SqlCommand Command1 = con1.CreateCommand();
                Command1.CommandText = selectSQL1;
                SqlDataReader reader1 = Command1.ExecuteReader();
                if (reader.Read() || reader1.Read())
                {
                    //MessageBox.Show("This page has been processed!");
                }
                else
                {
                    string curPath = curPath = Regex.Split(widb.Filepath, "XueBaResources")[1]; 
                    curPath = @"\\10.2.28.78\XueBaResources" + curPath;
                    FileInfo temp = new FileInfo(curPath);
                    if (temp.Extension.Equals(".pdf"))
                    {
                        //PipeLine.OtherToHtml.IcDocument document = PipeLine.OtherToHtml.FactoryDocument.GetDocoment(@"F:\ziliao\C705\Pipeline\text", curPath);
                        PipeLine.OtherToHtml.IcDocument document = PipeLine.OtherToHtml.FactoryDocument.GetDocoment(@"\\10.2.28.78\XueBaResources",curPath);
                        document.TransformDocument();
                        //ProcessProcedure.Processpdf(@"F:\ziliao\C705\Pipeline\text\" + temp.Name + ".html", true);
                        ProcessProcedure.Processpdf(@"\\10.2.28.78\XueBaResources\"+temp.Name+".html",true);
                        //MessageBox.Show("Processing OK!");
                    }
                    /*
                    FileInfo temp = new FileInfo(@widb.Filepath);
                    // MessageBox.Show(temp.Extension.ToString());
                    if (temp.Extension.Equals(".pdf"))
                    {
                        PipeLine.OtherToHtml.IcDocument document = PipeLine.OtherToHtml.FactoryDocument.GetDocoment(@"F:\ziliao\C705\Pipeline\text", @widb.Filepath);
                        document.TransformDocument();
                        ProcessProcedure.Process(@"F:\ziliao\C705\Pipeline\text\" + temp.Name + ".html", true);
                        // MessageBox.Show(@"C:\C705\Pipeline\text\" + temp.Name + ".txt");
                        MessageBox.Show("Processing OK!");
                    }*/
        /*
     else
     {
         if (ProcessProcedure.baiduzhidao(curUrl))
         {

             ProcessProcedure.baiduzhidaoprocess(curPath, curUrl);
         }
         else if (ProcessProcedure.cnblogs(curUrl))
         {

             ProcessProcedure.cnblogsprocess(curPath, curUrl);
         }
         else if (ProcessProcedure.dewen(curUrl))
         {

             ProcessProcedure.dewenprocess(curPath, curUrl);
         }
         else if (ProcessProcedure.stackoverflow(curUrl))
         {

             ProcessProcedure.stackoverflowprocess(curPath, curUrl);//  process stackoverflow question answer pair
         }
         else if (ProcessProcedure.sosowenwen(curUrl))
         {

             ProcessProcedure.sosowenwenprocess(curPath, curUrl);//  process sosowenwen question answer pair
         }
         else
         {
             ProcessProcedure.Process(curPath, true);//@"C:\C705\Pipeline\TestFiles\" + 
         }
     }
     MessageBox.Show("OK!");
 }
 con.Close();
 con1.Close();
 //Process(@widb.Filepath, true);
 //Process(@"C:\C705\Pipeline\TestFiles\" + @widb.Filepath, true);
}
//goto REPEAT;*/

        //}

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Translate_Btn_Click(object sender, EventArgs e)
        {
            Translator tsl = new Translator();
            //tsl.original_Text = _author;
            this.Hide();
            tsl.ShowDialog();
            this.Show();
        }

        private void filelistbox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
