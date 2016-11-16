using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Documents;
using Lucene.Net.Search;
using Lucene.Net.QueryParsers; 
using Lucene.China;

namespace DataMining.WordsSegment
{
    static class WordSegement
    {
        public static List<string> cutwords(string words, string analyzer)
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
                    PanGu.Segment.Init(@"G:\CProjects\xueba\DataMining\DataMining\PanGu\PanGu.xml");
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
