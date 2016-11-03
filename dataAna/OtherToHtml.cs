using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using org.pdfbox.pdmodel;
using org.pdfbox.util;

using System.Windows.Forms;
namespace PipeLine
{
    class OtherToHtml
    {


        public interface IcDocument
        {
            void TransformDocument();
        }
        public abstract class BaseDocument
        {
            /// <summary>
            /// 目標文件夾
            /// </summary>
            protected string TargetFolder;
            /// <summary>
            /// 原文件
            /// </summary>
            protected string source;

            /// <summary>
            /// 目標文件
            /// </summary>
            protected string Target;

            protected virtual void GetCurrentTarget()
            {

                if (!Directory.Exists(TargetFolder))
                {

                    Directory.CreateDirectory(TargetFolder);
                }

                FileInfo temp = new FileInfo(source);
                string fileName = temp.Name + ".html";
                Target = TargetFolder + @"\" + fileName;
            }

            public BaseDocument(string TargetFolder, string source)
            {
                this.source = source;
                this.TargetFolder = TargetFolder;
                GetCurrentTarget();
            }
        }
        public class FactoryDocument
        {
            /// <summary>
            /// 得到操作的文檔
            /// </summary>
            /// <param name="TargetFolder">生成的文件夾</param>
            /// <param name="source">要讀取的文件</param>
            /// <returns></returns>
            public static IcDocument GetDocoment(string TargetFolder, string source)
            {

                FileInfo file = new FileInfo(source);
                IcDocument document = null;
                if (file.Exists)
                {
                    switch (Path.GetExtension(source).ToUpper())
                    {


                        case ".PDF":

                            document = new PdfDocument(TargetFolder, source);
                            break;

                    }
                }
                else
                {
                    MessageBox.Show("文件沒有找到");
                }
                return document;
            }

            internal static IcDocument GetDocoment(DirectoryInfo directoryInfo, string curItem)
            {
                throw new NotImplementedException();
            }
        }
        public class PdfDocument : BaseDocument, IcDocument
        {

            public PdfDocument(string TargetFolder, string source)
                : base(TargetFolder, source)
            {

            }



            public void pdf2txt(FileInfo file)
            {
                PDDocument doc = PDDocument.load(file.FullName);

                PDFTextStripper pdfStripper = new PDFTextStripper();

                string text = pdfStripper.getText(doc);

                StreamWriter swPdfChange = new StreamWriter(Target, false, Encoding.GetEncoding(65001));

                swPdfChange.Write(text);
                swPdfChange.Close();

            }
            public void handletxt()
            {
                String path = Target;
                String[] lines = File.ReadAllLines(path);
                List<String> list = new List<String>();
                foreach (String line in lines)
                {
                    if (line.Length > 4)
                        list.Add(line);
                }
                lines = list.ToArray();
                File.WriteAllLines(path, lines);
            }

            #region IcDocument 成員

            public void TransformDocument()
            {
                FileInfo pdffile = new FileInfo(source);

                if (pdffile.Exists)
                {
                    pdf2txt(pdffile);
                    handletxt();
                }
                else
                {
                    Console.WriteLine("The File is NOT Exist.");
                }
            }

            #endregion


        }
    }


}