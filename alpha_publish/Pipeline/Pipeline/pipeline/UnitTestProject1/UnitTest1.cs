using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Pipeline
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ConnectionTest()
        {
            Connection c = new Connection("10.2.28.78", "XueBa", "crawler", "aimashi2015");
            SqlConnection con = Connection.instance("10.2.28.78", "XueBa", "crawler", "aimashi2015");
            Assert.IsNotNull(con);
        }
        [TestMethod]
        public void tegsText()
        {
            List<String> test1 = new List<string>();
            test1.Add("hello");
            test1.Add("this");
            MainWindow._tags = test1;
            String test2 = "is";
            DataMining d = new DataMining();
            bool r = d.RepeattedTag(test2);
            Assert.IsTrue(r);
        }
        [TestMethod]
        public void translateTest()
        {
            GoogleTranslator g = new GoogleTranslator();
            string s = g.Analytical(g.Translate("apple", "auto"));
            Assert.IsTrue(s=="苹果");
        }
        [TestMethod]
        public void cutWordTest()
        {
            WordSegment w = new WordSegment();
            Assert.IsTrue(w.cutwords("apple,123").Count>0);
        }
    }
}
