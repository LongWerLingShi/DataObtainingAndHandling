using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace XML
{
    class Program
    {
        static void Main()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"..\..\Resources\test.xml");

            XmlElement root = null;
            root = doc.DocumentElement;

            XmlNodeList listNodes = null;
            listNodes = root.SelectNodes("//author[@name='XI AO']/sex");
            foreach (XmlNode node in listNodes)
            {
                Console.WriteLine(node.InnerText);
            }
        }
    }
}
