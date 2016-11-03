using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using mshtml;
using System.Windows.Forms;

namespace Pipeline
{
    class Denoising
    {
        private string path;    //  文件路径
        private string rawdata; //  原始数据
        private string denoisingdata; //    去噪数据

        public string Path
        {
            set { path = value; }
        }

        public string Rawdata
        {
            get { return (rawdata); }
        }

        public string Denoisingdata
        {
            get { return (denoisingdata); }
        }

        public Denoising()  //  构造函数，全为空串
        {
            this.path = "";
            this.rawdata = "";
            this.denoisingdata = "";
        }

        public void Work()  //  获得去噪数据
        {
            try
            {

                rawdata = File.ReadAllText(path, Encoding.UTF8);    //  先读取原始数据
                //MessageBox.Show(rawdata);

                HTMLDocument doc = new HTMLDocument();
                IHTMLDocument2 doc2 = (IHTMLDocument2)doc;
                doc2.write(rawdata);

                denoisingdata = doc.documentElement.innerText;  //  对网页中的html内容进行去噪

                /*
                 * 如果编码不对应，denoising应该会是null
                 */
                if (denoisingdata != null)  //  如果获得了去噪数据
                {
                    denoisingdata = denoisingdata.Replace("|", " ");
                    denoisingdata = Regex.Replace(denoisingdata, "\\s+", " ");
                    File.WriteAllText("denoisinged.txt", denoisingdata);
                    return;
                }
                denoisingdata = null;
            }

            catch (FileNotFoundException e)
            {
                denoisingdata = null;
            }
            catch (PathTooLongException f)
            {
                denoisingdata = null;
            }
            catch (ArgumentException a)
            {
                denoisingdata = null;
            }
        }
    }
}
