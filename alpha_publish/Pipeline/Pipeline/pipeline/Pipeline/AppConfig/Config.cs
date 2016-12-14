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
using System.Configuration;
using Pipeline.UploadData;

namespace Pipeline.AppConfig
{
    public partial class Config : Form
    {
        private string _configPath = Application.StartupPath + "\\" + "config.txt";
        public Config()
        {
            InitializeComponent();
            if (File.Exists(_configPath))
            {
                StreamReader sr = new StreamReader(_configPath);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (!line.Trim().Equals(""))
                    {
                        string[] configs = line.Split(' ');
                        usernametext.Text = configs[0];
                        passwordtext.Text = configs[1];
                        serveriptext.Text = configs[2];
                        pipeline.Checked = configs[3].Equals("1") ? true : false;
                        upload.Checked = configs[4].Equals("1") ? true : false;
                        savepassword.Checked = configs[5].Equals("1") ? true : false;
                    }
                }
                sr.Close();
            }
            else
            {
                File.Create(_configPath);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (usernametext.Text.Equals(""))
            {
                MessageBox.Show("用户名不为空!");
                return;
            }
            if (usernametext.Text.Length > 20)
            {
                MessageBox.Show("用户名长度不超过20个字符!");
                usernametext.Text = usernametext.Text.Substring(0, 20);
                return;
            }
            if (passwordtext.Text.Equals(""))
            {
                MessageBox.Show("密码不为空!");
                return;
            }
            if (passwordtext.Text.Length > 16)
            {
                MessageBox.Show("密码长度不超过16个字符!");
                passwordtext.Text = passwordtext.Text.Substring(0, 16);
                return;
            }
            if (serveriptext.Text.Equals(""))
            {
                MessageBox.Show("服务器地址不为空!");
                return;
            }
            if (serveriptext.Text.Length > 100)
            {
                MessageBox.Show("服务器地址不能超过100字符!");
                serveriptext.Text = serveriptext.Text.Substring(0, 100);
                return;
            }
            
            if (usernametext.Text.Equals("crawler") && passwordtext.Text.Equals("aimashi2015") && pipeline.Checked)
            {
                AppConfiguration.updateConfigValue("username", usernametext.Text);
                AppConfiguration.updateConfigValue("password", passwordtext.Text);
                AppConfiguration.updateConfigValue("serverIp", serveriptext.Text);
                AppConfiguration.updateConfigValue("pileline/upload", "pipeline");
                if(savepassword.Checked){
                    StreamWriter sw = new StreamWriter(_configPath);
                    string s = usernametext.Text+" "+passwordtext.Text+" "+serveriptext.Text+" 1 0 1";
                    sw.WriteLine(s);
                    sw.Close();
                }
                if(null != Connection.instance(AppConfiguration.GetConfigValue("serverIp"),"crawler",AppConfiguration.GetConfigValue("username"),AppConfiguration.GetConfigValue("password")))
                {
                    MainWindow mw = new MainWindow();
                    this.Hide();
                    mw.Show();
                }
                else
                {
                    MessageBox.Show("亲，请输入正确的登陆信息!");
                }
            }
            else if (usernametext.Text.Equals("crawler") && passwordtext.Text.Equals("aimashi2015") && upload.Checked)
            {
                AppConfiguration.updateConfigValue("username", usernametext.Text);
                AppConfiguration.updateConfigValue("password", passwordtext.Text);
                AppConfiguration.updateConfigValue("serverIp", serveriptext.Text);
                AppConfiguration.updateConfigValue("pileline/upload", "upload");
                if (savepassword.Checked)
                {
                    StreamWriter sw = new StreamWriter(_configPath);
                    string s = usernametext.Text + " " + passwordtext.Text + " " + serveriptext.Text + " 0 1 1";
                    sw.WriteLine(s);
                    sw.Close();
                }
                if (null != Connection.instance(AppConfiguration.GetConfigValue("serverIp"), "crawler", AppConfiguration.GetConfigValue("username"), AppConfiguration.GetConfigValue("password")))
                {
                    UploadProcessBar upb = new UploadProcessBar();
                    this.Hide();
                    upb.Show();
                }
                else
                {
                    MessageBox.Show("亲，请输入正确的登陆信息!");
                }
            }
        }

    }
}
