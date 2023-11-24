using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLViewer_Core.Utiles
{
    public enum LogLevel
    {
        Info,
        Warn,
        Error
    }

    public class Logger
    {
        private static Logger instance = new Logger();
        private Logger()
        {
            string logFileSavePath = getLogFilePath();

            if(File.Exists(logFileSavePath) == false)
            {
                try
                {
                    using(File.CreateText( logFileSavePath))
                    {

                    }

                }
                catch (Exception e)
                {

                }
            }
        }

        public static void Log(LogLevel logLevel, string msg)
        {
            string logString = $"LogLevel[{logLevel.ToString()}]:: {msg}";
            Console.WriteLine(logString);
            Console.WriteLine(logLevel);
        }

        private static string getLogFilePath()
        {
            DateTime currentDateTime = DateTime.Now;
            string timeFormat = "yyyyMMdd_HHmmss";
            string time = currentDateTime.ToString(timeFormat);
            return Path.Combine(Directory.GetCurrentDirectory(), "XMLViewer_", time,".log");
        }
    }
}
