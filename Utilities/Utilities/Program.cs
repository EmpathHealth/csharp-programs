using NLog;
using System;
using System.Data.SqlClient;

namespace Utilities
{
    class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();


        static void Main(string[] args)
        {
            try
            {
                Logger.Info("Hello world");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Goodbye cruel world");
            }

        }
    }

}


