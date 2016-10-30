using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    class Program
    {
        static void Main(string[] args)
        {
            var buffer = File.ReadAllBytes("log.txt");
            buffer = Encoding.Convert(Encoding.GetEncoding("GBK"), Encoding.UTF8, buffer);
            File.WriteAllBytes("a.txt", buffer);
        }
    }
}
