using System;
using System.Web;
using System.Net;
using System.Text.RegularExpressions;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;

namespace Pipeline
{
    public class GoogleTranslator
    {
        /// <summary>
        /// 后来我添加的2015.10.23
        /// </summary>
        private string AutoDetectLanguage = "auto";
        /// <summary>
        /// 翻译文本[自动检测源语言类型]
        /// </summary>
        /// <param name="sourceText">源文本</param>
        /// <param name="targetLanguageCode">目标语言类型代码，如：en、zh中文、jp日语，spa西班牙语，th泰语，yue粤语、ru俄罗斯语，等</param>
        /// <returns>翻译结果</returns>
        public string Translate(string sourceText, string targetLanguageCode)
        {
            return Translate(sourceText, AutoDetectLanguage, targetLanguageCode);
        }

        //将得到的{“from”:”zh”,“to”:”en”,“trans_result”:[{“src”:””,“dst”:””}]}得到其中的dst
        public String Analytical(String JsonCode)
        {
            if (JsonCode == null)
            {
                return "此文档暂时不支持翻译，对您造成的影响深表歉意！";
            }
            JObject ResultParent;
            try
            {
                ResultParent = JObject.Parse(JsonCode);
            }
            catch (Exception e)
            {
                return "";
            }
            

            if (ResultParent != null && ResultParent["trans_result"] != null)
            {
                String trans_result = ResultParent["trans_result"].ToString();
                JObject ResultChild = JObject.Parse(trans_result.Replace("[", " ").Replace("]", " "));
                //翻译后的目标结果
                if (ResultChild != null && ResultChild["dst"] != null)
                {
                    String target = ResultChild["dst"].ToString();
                    return target;
                }
            }
            return "";
        }
        /// <summary>
        /// 翻译文本
        /// </summary>
        /// <param name="sourceText">源文本</param>
        /// <param name="sourceLanguageCode">源语言类型代码，如en、zh中文、jp日语，spa西班牙语，th泰语，yue粤语、ru俄罗斯语，等</param>
        /// <param name="targetLanguageCode">目标语言类型代码，如en、zh中文、jp日语，spa西班牙语，th泰语，yue粤语、ru俄罗斯语，等</param>
        /// <returns>翻译结果</returns>
        public string Translate(string sourceText, string sourceLanguageCode, string targetLanguageCode)//changed···！！！！！！
        {
            //15界学长ID：我拥有的ID:VZ9RP3BBNbHOYNbf1GXWcd9N  "mdxqih0Fy1kGgNc1KxWtgmOt",
            //最新ID
            string appid = "20161101000031185";
            string keyword = "BfXqSK1err7LDRuHIQO5";
            string salt = MyRandom.GetRandom();
            string sign = MD5Common.GetMd5Hash(appid + sourceText + salt + keyword);
            //Console.WriteLine("appid:" + appid + "\nkeyword:" + keyword + "\nsalt:" + salt + "\nsign" + sign);
            string url = string.Format("http://api.fanyi.baidu.com/api/trans/vip/translate?q={0}&from={1}&to={2}&appid={3}&salt={4}&sign={5}", sourceText, sourceLanguageCode, targetLanguageCode,appid,salt,sign);
            WebClient wc = new WebClient();
            try
            {
                return wc.DownloadString(url);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool isChineseWord(char chr)
        {
            if (chr >= 0x4E00 && chr <= 0x9FFF) return true;
            return false;
        }
    }
    public static class MyRandom
    {
        public static int MIN = 1000000000;
        public static int MAX = 2000000000;
        public static string GetRandom()
        {
            Random rd = new Random();　　//无参即为使用系统时钟为种子
            return rd.Next(MIN, MAX).ToString();
        }
    }
    public static class MD5Common
    {
        /// <summary>
        /// 返回指定字符串的Md5
        /// </summary>
        /// <param name="strInput">指定字符串</param>
        /// <returns>返回字符串的Md5</returns>
        public static string GetMd5Hash(string strInput)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] btData = md5Hasher.ComputeHash(Encoding.Default.GetBytes(strInput));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < btData.Length; i++)
            {
                sBuilder.Append(btData[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}
