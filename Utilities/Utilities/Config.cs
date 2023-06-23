using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class Config
    {
        public static string GetSingleValue(string key)
        {
            
            string? value = ConfigurationManager.AppSettings[key];

            if (string.IsNullOrEmpty(value))
            {
                return "test";
            }
            else
            {
                return value;

            }

            
        }

    }
}
