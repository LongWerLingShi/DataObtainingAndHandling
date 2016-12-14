using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq; 

namespace DataTranslation
{
    class BaiduTranslator
    {
        /// <summary>
        /// 翻译文本[自动检测源语言类型]
        /// </summary>
        /// <param name="sourceText">源文本</param>
        /// <param name="targetLanguageCode">目标语言类型代码，如：en、zh中文、jp日语，spa西班牙语，th泰语，yue粤语、ru俄罗斯语，等</param>
        /// <returns>API返回的Jason格式的翻译结果</returns>

        private string AutoDetectLanguage = "auto";///在auto状态下，自动检测源语言，并根据源语言的语种按照规则设置目标语言的语种。当源语言为非中文时，目标语言自动设置为中文。当源语言为中文时，目标语言自动设置为英文。
        public string Translate(string sourceText)
        {
            return Translate(sourceText, AutoDetectLanguage, AutoDetectLanguage);
        }

        public string Translate(string sourceText, string targetLanguageCode)
        {
            return Translate(sourceText, AutoDetectLanguage, targetLanguageCode);
        }

        /// <summary>
        /// 翻译文本
        /// </summary>
        /// <param name="sourceText">源文本</param>
        /// <param name="sourceLanguageCode">源语言类型代码，如en、zh中文、jp日语，spa西班牙语，th泰语，yue粤语、ru俄罗斯语，等</param>
        /// <param name="targetLanguageCode">目标语言类型代码，如en、zh中文、jp日语，spa西班牙语，th泰语，yue粤语、ru俄罗斯语，等</param>
        /// <returns>API返回的Jason格式的翻译结果</returns>
        public string Translate(string sourceText, string sourceLanguageCode, string targetLanguageCode)
        {

            string url = string.Format("http://openapi.baidu.com/public/2.0/bmt/translate?client_id={0}&q={1}&from={2}&to={3}", "mdxqih0Fy1kGgNc1KxWtgmOt", sourceText, sourceLanguageCode, targetLanguageCode);
            WebClient wc = new WebClient();
            return wc.DownloadString(url);
        }

        /// <summary>
        /// Jason数据解析
        /// </summary>
        /// <param name="JsonCode">API返回的Jason数据</param>
        /// <returns>翻译结果</returns>
        //请求的返回Jason格式为{"from":"en","to":"zh","trans_result":[{"src":"today","dst":"\u4eca\u5929"}]}得到其中的dst
        public String Analytical(String JsonCode)
        {
            JObject ResultParent = JObject.Parse(JsonCode);
            String trans_result = ResultParent["trans_result"].ToString();
            JObject ResultChild = JObject.Parse(trans_result.Replace("[", " ").Replace("]", " "));
            //翻译后的目标结果
            String target = ResultChild["dst"].ToString();
            return target;
        }

        static void Main()
        {
            BaiduTranslator bt = new BaiduTranslator();
            string rslt=bt.Analytical(bt.Translate("好的"));
            return;
        }
    }
}
