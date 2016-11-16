using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

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
        public void cutWordTest()
        {
            WordSegment w = new WordSegment();
            int a = w.cutwords("He is student").Count;
            Assert.AreEqual(a,1);
        }
        [TestMethod]
        public void cutWordTest2()
        {
            WordSegment w = new WordSegment();
            int a = w.cutwords("去噪模块接口word部分：").Count;
            Assert.AreEqual(a, 5);
        }
        [TestMethod]
        public void cutWordTest3()
        {
            WordSegment w = new WordSegment();
            int a = w.cutwords("去噪模块接口word部分He is student").Count;
            Assert.AreEqual(a, 6);
        }
        [TestMethod]
        public void translateTest()
        {
            Translator t = new Translator();
            Console.Write(t.translate("sleepy"));
            Assert.AreEqual(t.translate("sleepy"), "困乏的");
        }
        [TestMethod]
        public void processTest()
        {
            ProcessProcedure p = new ProcessProcedure();
            p.Processwrd("我要困死啊","困死了",false);
            Assert.AreEqual(1, 1);
        }
    }
}
