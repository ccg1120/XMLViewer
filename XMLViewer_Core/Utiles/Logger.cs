using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Channels;
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
        static Logger()
        {
            Console.WriteLine("static Logger()");
            _instance = new Logger();
            _instance.initalizeLogger();
        }
        public static void Log_Dev(string msg)
        {
            if (_instance._isDebuggerAttached == false)
                return;

            string logString = $"Log [DEV]:: {msg}";
            _instance._logQueue.Enqueue(logString);
            Console.WriteLine(logString);
        }
        public static void Log(LogLevel logLevel, string msg)
        {
            string logString = $"LogLevel[{logLevel.ToString()}]:: {msg}";
            _instance._logQueue.Enqueue(logString);
            Console.WriteLine(logString);
        }
        public string LogFilePath { get; private set; }
       
        private Logger()
        {
            _isDebuggerAttached = Debugger.IsAttached;
            LogFilePath = string.Empty;
            _isInitailizeLogger = false;
            _logQueue = new Queue<string>();
            _lock = new object();
        }
        private void initalizeLogger()
        {
            string logFileSavePath = getLogFileFullPath();
            if (File.Exists(logFileSavePath) == false)
            {
                using(TextWriter writer = File.CreateText(logFileSavePath))
                {
                    writer.WriteLine("Logger Open");
                }
            }
            LogFilePath = logFileSavePath;
            _isInitailizeLogger = true;
        }
        private string getLogFolderPath()
        {
            string logFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "log");

            if (Directory.Exists(logFolderPath) == false)
                Directory.CreateDirectory(logFolderPath);

            return logFolderPath;
        }
        private string getLogFileFullPath()
        {
            DateTime currentDateTime = DateTime.Now;
            string timeFormat = "yyyyMMdd_HHmmss";
            string time = currentDateTime.ToString(timeFormat);
            string fileName = $"XMLViewer_{time}.log";

            string logFolderPath = getLogFolderPath();

            return Path.Combine(logFolderPath, fileName);
        }

        private static Logger        _instance;

        private bool                _isInitailizeLogger;
        private readonly bool       _isDebuggerAttached;
        private Queue<string>       _logQueue;
        private object              _lock;
    }
}
