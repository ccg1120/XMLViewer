using System;
using System.Collections.Generic;
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

        }

        public static void Log(LogLevel logLevel, string msg)
        {


        }
    }
}
