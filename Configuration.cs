using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace MobLib
{
    /// <summary>
    /// Class for access on configuration file.
    /// all maped configurantions are static Properties
    /// </summary>
    public static class Configuration
    {
        public static string GetConfigurationValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
