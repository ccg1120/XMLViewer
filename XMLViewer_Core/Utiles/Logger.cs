using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
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

    public class Logger : IDisposable
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
            Console.WriteLine(logString);
            _instance._streamWriter.WriteLine(logString);
        }
        public static void Log(LogLevel logLevel, string msg)
        {
            string logString = $"LogLevel[{logLevel.ToString()}]:: {msg}";
            Console.WriteLine(logString);
            _instance._streamWriter.WriteLine(logString);

        }
        public void Dispose()
        {
            if (_streamWriter.Equals(StreamWriter.Null) == false)
                _streamWriter.Dispose();
        }
        private Logger()
        {
            _streamWriter = StreamWriter.Null;
            _isDebuggerAttached = Debugger.IsAttached;
            AppDomain.CurrentDomain.ProcessExit += CloseLogger!;
        }
        private void initalizeLogger()
        {
            string logFileSavePath = getLogFileFullPath();
            if (File.Exists(logFileSavePath) == false)
            {
                try
                {
                    _streamWriter = File.CreateText(logFileSavePath);
                    _streamWriter.WriteLine("initalizeLogger");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    if (_streamWriter.Equals(StreamWriter.Null) == false)
                        _streamWriter.Dispose();
                }
            }
            _isInitailizeLogger = true;
        }
        public static void CloseLogger(object sender, EventArgs e)
        {
            if (_instance._isInitailizeLogger)
            {
                if (_instance._streamWriter.Equals(StreamWriter.Null) == false)
                {
                    _instance._streamWriter.WriteLine("CloseLogger");
                    _instance._streamWriter.Close();
                }
            }
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

        private static Logger _instance;
        private bool _isInitailizeLogger;
        private StreamWriter _streamWriter;
        private readonly bool _isDebuggerAttached;
    }
}
