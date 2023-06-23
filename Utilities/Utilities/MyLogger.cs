using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class MyLogger : NLog.Logger
    {
        private Logger? _logger = null;
        private string? _name = null;
        private string? _level = null;
        public MyLogger(string name)
        {
            _name = name;
        }


        public Logger Create_Logger(string _name, int sourceID)
        {
            _logger = LogManager.GetLogger(_name);

            _logger.Properties.Add("appid", sourceID);

            return _logger;

        }

        private void add_handler()
        {

        }
        // Set the log level on the config on each handler in the config
        // create databse and email hander by dyanmically setting config properties 
        // create a method call add handlers 
    }

}
