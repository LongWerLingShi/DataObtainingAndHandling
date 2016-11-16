using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Pipeline.AppConfig
{
    class AppConfiguration
    {
        public static String GetConfigValue(string name)
        {
            string value = ConfigurationManager.AppSettings[name];
            return value;
        }

        public static bool updateConfigValue(string key, string value)
        {
            try
            {
                Configuration conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                if (!conf.AppSettings.Settings.AllKeys.Contains(key))
                {
                    conf.AppSettings.Settings.Add(key, value);
                }
                else
                {
                    conf.AppSettings.Settings[key].Value = value;
                }
                conf.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
