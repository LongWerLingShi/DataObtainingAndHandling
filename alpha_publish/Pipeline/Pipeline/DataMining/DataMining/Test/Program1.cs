using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DataMining.Test
{
    using Lucene.Net.Index;
    using Lucene.Net.Store;
    using Lucene.Net.Analysis;
    //using Lucene.Net.Analysis.Standard;
    using Lucene.Net.Analysis.PanGu;
    using Lucene.Net.Documents;
    using Lucene.Net.Search;
    using Lucene.Net.QueryParsers;
    using DataMining.WordsSegment;
    class Program1
    {
        /*static void Main(string[] args)
        {
            string s = "文章1984年出生于陕西省西安市。上高三的时候，文章被保送到四川师范大学艺术学院学习影视表演，但是他并未进入这个学校，而是决心去北京学习。在填写大学志愿之前，文章专门去北京考察了中国两大艺术院校—北京电影学院和中央戏剧学院。回到西安之后，文章不顾父母阻拦，将大学志愿从一本到专科总共八个志愿全部填成中央戏剧学院。2002年文章被中央戏剧学院表演系录取。";
            List<string> strList = new List<string>();
            //strList = WordSegement.cutwords(s, "Lucene.Net.Analysis.SimpleAnalyzer");
            //strList = WordSegement.cutwords(s, "Lucene.Net.Analysis.KeywordAnalyzer");
            //strList = WordSegement.cutwords(s, "Lucene.Net.Analysis.StopAnalyzer");
            //strList = WordSegement.cutwords(s, "Lucene.Net.Analysis.WhitespaceAnalyzer");
            strList = WordSegement.cutwords(s, "Lucene.Net.Analysis.PanGu.PanGuAnalyzer");
            //strList = WordSegement.cutwords(s, "Lucene.Net.Analysis.Standard.StandardAnalyzer");
            //strList = WordSegement.cutwords(s, "Lucene.China.ChineseAnalyzer");
            foreach (string t in strList)
            {
                Console.WriteLine(t);
            }
        }
            /*
            Analyzer analyzer = new Lucene.Net.Analysis.PanGu.PanGuAnalyzer();
            IndexWriter writer = new IndexWriter("IndexDirectory", analyzer, true);
            AddDocument(writer, "SQL Server 2008 的发布", "SQL Server 2008 的新特性");
            AddDocument(writer, "SQL Server 2013 的发布", "SQL Server 2013 的新特性");
            AddDocument(writer, "三国", "曹操、鲁肃、演好");
            AddDocument(writer, "ASP.Net MVC框架配置与分析", "而今，微软推出了新的MVC开发框架，也就是Microsoft ASP.NET 3.5 Extensions");
            
            
            writer.Optimize();
            writer.Close();

            IndexSearcher searcher = new IndexSearcher("IndexDirectory");
            MultiFieldQueryParser parser = new MultiFieldQueryParser(new string[] { "title", "content" }, analyzer);
            Query query = parser.Parse("sql");
            Hits hits = searcher.Search(query);

            for (int i = 0; i < hits.Length(); i++)
            {
                Document doc = hits.Doc(i);
                Console.WriteLine(string.Format("title:{0} content:{1}", doc.Get("title"), doc.Get("content")));
            }

            query = parser.Parse("asp");
            hits = searcher.Search(query);
            for (int i = 0; i < hits.Length(); i++)
            {
                Document doc = hits.Doc(i);
                foreach(string s in cutwords(doc.Get("content"),analyzer))
                {
                    Console.WriteLine(string.Format("{0}", s));
                }
            }

            searcher.Close();
           // Console.ReadKey();
        }

        static void AddDocument(IndexWriter writer, string title, string content)
        {
            Document document = new Document();
            document.Add(new Field("title", title, Field.Store.YES, Field.Index.TOKENIZED));
            document.Add(new Field("content", content, Field.Store.YES, Field.Index.TOKENIZED));
            writer.AddDocument(document);
        }

        private static List<string> cutwords(string words, Analyzer analyzer)
        {
            List<string> results = new List<string>();
            TokenStream ts = analyzer.ReusableTokenStream("", new StringReader(words));
            Lucene.Net.Analysis.Token token;
            while ((token = ts.Next()) != null)
            {
                results.Add(token.TermText());
            }
            ts.Close();
            return results;
        }*/
    }
}
