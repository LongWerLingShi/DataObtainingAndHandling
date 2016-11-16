using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Documents;
using Lucene.Net.Search;
using Lucene.Net.QueryParsers;
using Lucene.China;

namespace Pipeline
{
    public class WordSegment
    {
        public string wordLibPath = "";
        /*
         * 分词函数
         * @srcdata:待分词的文本
         * 返回值:按照学长格式定义的分词结果的string表示
         * 即{<分词1>}{<分词2>}...{<分词n>}
         */

        //	这个函数是核心
        //	输入是待分词的内容
        //	输出是分词结果
        //	分词结果的格式是{<word>}
        //	这个格式是学长定义的，我们为了不破坏既定的接口，沿用了这个格式
        //	这个函数的工作原理主要是调用了Lucene.Net.Analysis和Lucene.China的接口
        //	调用这两个接口的配置工作很简单：1.在引用中加入dll文件 2.在可执行程序的目录下放置一个data文件夹，文件夹内有两个文件，分别是sDict和sNoise
        //	存放词库和噪声

        /*private bool isChineseWord(string word)
        {
            if (word == null)
            {
                return false;
            }
            for (int i = 0; i < word.Length; i++)
            {
                char chr = word[i];
                if (!(chr >= 0x4E00 && chr <= 0x9FFF))
                {
                    return false;
                }
            }

            return true;
        }*/

        /*private string word_seg(string srcdata)
        {
            //StringBuilder sb = new StringBuilder();
            //sb.Remove(0, sb.Length);
            string t1 = "";
            ChineseAnalyzer analyzer = new Lucene.China.ChineseAnalyzer();
            //string FilePath = @"C:\Users\梁亦清\Documents\Visual Studio 2013\Projects\中科院分词简例\1.htm";

            StringReader sr = new StringReader(srcdata);
            //Console.WriteLine(sr.ToString());
            //Environment.Exit(0);
            TokenStream stream = analyzer.TokenStream("", sr);

            //long begin = System.DateTime.Now.Ticks;
            Lucene.Net.Analysis.Token t = stream.Next();
            while (t != null)
            {
                /*
                t1 = t.ToString();   //显示格式： (关键词,0,2) ，需要处理
                t1 = t1.Replace("(", "");
                char[] separator = { ',' };
                t1 = t1.Split(separator)[0];
                if (isChineseWord(t1))
                {
                    sb.Append("{<" + t1 + ">}");
                }
                t = stream.Next();
            }
            //return sb.ToString()
        }*/



        //	这个函数是学长代码的对外接口，我们沿用了这个接口，但使用的分词方法不是朴素贝叶斯
        /*public string DoWordSegment(string strIn)
        {
            return word_seg(strIn);

        }*/

        public List<string> cutwords(string words, string analyzer = "Lucene.China.ChineseAnalyzer")
        {
            List<string> results = new List<string>();
            switch (analyzer)
            {
                case "Lucene.Net.Analysis.SimpleAnalyzer":
                    SimpleAnalyzer analyzerInstance0 = new SimpleAnalyzer();
                    TokenStream ts0 = analyzerInstance0.ReusableTokenStream("", new StringReader(words));
                    Lucene.Net.Analysis.Token token0;
                    while ((token0 = ts0.Next()) != null)
                    {
                        results.Add(token0.TermText());
                    }
                    ts0.Close();
                    analyzerInstance0.Close();
                    break;
                case "Lucene.Net.Analysis.KeywordAnalyzer":
                    KeywordAnalyzer analyzerInstance1 = new KeywordAnalyzer();
                    TokenStream ts1 = analyzerInstance1.ReusableTokenStream("", new StringReader(words));
                    Lucene.Net.Analysis.Token token1;
                    while ((token1 = ts1.Next()) != null)
                    {
                        results.Add(token1.TermText());
                    }
                    ts1.Close();
                    analyzerInstance1.Close();
                    break;
                case "Lucene.Net.Analysis.StopAnalyzer":
                    StopAnalyzer analyzerInstance2 = new StopAnalyzer();
                    TokenStream ts2 = analyzerInstance2.ReusableTokenStream("", new StringReader(words));
                    Lucene.Net.Analysis.Token token2;
                    while ((token2 = ts2.Next()) != null)
                    {
                        results.Add(token2.TermText());
                    }
                    ts2.Close();
                    analyzerInstance2.Close();
                    break;
                case "Lucene.Net.Analysis.WhitespaceAnalyzer":
                    WhitespaceAnalyzer analyzerInstance3 = new WhitespaceAnalyzer();
                    TokenStream ts3 = analyzerInstance3.ReusableTokenStream("", new StringReader(words));
                    Lucene.Net.Analysis.Token token3;
                    while ((token3 = ts3.Next()) != null)
                    {
                        results.Add(token3.TermText());
                    }
                    ts3.Close();
                    analyzerInstance3.Close();
                    break;
                case "Lucene.Net.Analysis.PanGu.PanGuAnalyzer":
                    PanGu.Segment.Init(@"G:\CProjects\Pipeline\pipeline\Pipeline\bin\Release\PanGu.xml");
                    PanGuAnalyzer analyzerInstance4 = new PanGuAnalyzer();
                    TokenStream ts4 = analyzerInstance4.TokenStream("", new StringReader(words));
                    Lucene.Net.Analysis.Token token4;
                    while ((token4 = ts4.Next()) != null)
                    {
                        results.Add(token4.TermText());
                    }
                    ts4.Close();
                    analyzerInstance4.Close();
                    break;
                case "Lucene.Net.Analysis.Standard.StandardAnalyzer":
                    StandardAnalyzer analyzerInstance5 = new StandardAnalyzer();
                    TokenStream ts5 = analyzerInstance5.ReusableTokenStream("", new StringReader(words));
                    Lucene.Net.Analysis.Token token5;
                    while ((token5 = ts5.Next()) != null)
                    {
                        results.Add(token5.TermText());
                    }
                    ts5.Close();
                    analyzerInstance5.Close();
                    break;
                case "Lucene.China.ChineseAnalyzer":
                default:
                    ChineseAnalyzer analyzerInstance6 = new ChineseAnalyzer();
                    TokenStream ts6 = analyzerInstance6.ReusableTokenStream("", new StringReader(words));
                    Lucene.Net.Analysis.Token token6;
                    while ((token6 = ts6.Next()) != null)
                    {
                        results.Add(token6.TermText());
                    }
                    ts6.Close();
                    analyzerInstance6.Close();
                    break;
            }
            return results;
        }

    }
}