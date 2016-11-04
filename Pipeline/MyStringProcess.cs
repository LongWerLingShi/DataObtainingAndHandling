using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pipeline
{
    class MyStringProcess
    {
        const int PREVIEWLENGTH = 50;

        public static int GetWordFrequency(String sentence, String word)
        {
            int freq = 0;
            try
            {
                string[] sArray = Regex.Split(sentence, word, RegexOptions.IgnoreCase);
                freq = sArray.Length - 1;
            }
            catch (ArgumentException e)
            {
                return 0;
            }
            return freq;
        }

        /*
        public static string GetPreview(string source, string tag)  //  to bo changed
        {
            #region xuezhang code
            string result = "";
            int tagMatchLength = 0;
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i].ToString().ToLower() == tag[tagMatchLength].ToString().ToLower())
                {
                    tagMatchLength++;
                    if (tagMatchLength == tag.Length)
                    {
                        for (int k = 0; k < PREVIEWLENGTH; k++)
                        {
                            if (i - k >= 0)
                            {
                                if (source[i - k] != '\n' && source[i - k] != '\r')
                                {
                                    result = source[i - k] + result;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        for (int k = 0; k < PREVIEWLENGTH; k++)
                        {
                            if (i + k + 1 == source.Length)
                            {
                                break;
                            }
                            if (source[i + k + 1] != '\n' && source[i + k + 1] != '\r')
                            {
                                result += source[i + k + 1];
                            }
                        }
                        result += "...";
                        tagMatchLength = 0;
                        i = i + PREVIEWLENGTH;
                    }
                }
                else
                {
                    tagMatchLength = 0;
                }
            }

            return result;
            #endregion
            /*string pattern = ".{0,10}" + tag + ".{0,10}";

            MatchCollection matches = Regex.Matches(source, pattern, RegexOptions.IgnoreCase);


            string result = "";
            foreach (Match match in matches)
            {
                result += "..." + match.Value + "...\n";

            }

            return result;
        }*/
    }
}
