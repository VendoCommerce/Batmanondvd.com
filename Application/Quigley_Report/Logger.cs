using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Quigley_Report
{
    class Logger
    {
        private static string logFile = ConfigurationManager.AppSettings["logFile"];

        public static void LogToFile(string message)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(DateTime.Now);
            //if (Error != null)
            //{
            //    sb.Append("Exception error:" + Error.ToString() + "-");
            //}
            sb.Append("-" + message);
            try
            {
                StreamWriter log;
                if (!File.Exists(logFile))
                {
                    log = new StreamWriter(logFile);
                }
                else
                {
                    log = File.AppendText(logFile);
                }
                log.WriteLine(sb.ToString());
                log.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}