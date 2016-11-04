using System;
using System.Web;
using System.Net;
using System.Text.RegularExpressions;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Pipeline
{
    class GoogleTranslator
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
            JObject ResultParent = JObject.Parse(JsonCode);
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
            //我拥有的ID:VZ9RP3BBNbHOYNbf1GXWcd9N
            string url = string.Format("http://openapi.baidu.com/public/2.0/bmt/translate?client_id={0}&q={1}&from={2}&to={3}", "mdxqih0Fy1kGgNc1KxWtgmOt", sourceText, sourceLanguageCode, targetLanguageCode);
            WebClient wc = new WebClient();
            return wc.DownloadString(url);
        }

        public static bool isChineseWord(char chr)
        {
            if (chr >= 0x4E00 && chr <= 0x9FFF) return true;
            return false;
        }
    }
}
