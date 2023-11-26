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
        Dev,
        Info,
        Warn,
        Error
    }

    public class Logger : IDisposable
    {
        public static void Log(LogLevel logLevel, string msg)
        {
            if( _instance._isInitailizeLogger == false)
                _instance.initalizeLogger();

            if ( (logLevel == LogLevel.Dev) && (_instance._isDebuggerAttached == false) )
                return;

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
        }
        private void initalizeLogger()
        {
            string logFileSavePath = getLogFilePath();
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
        private string getLogFilePath()
        {
            DateTime currentDateTime = DateTime.Now;
            string timeFormat = "yyyyMMdd_HHmmss";
            string time = currentDateTime.ToString(timeFormat);
            return Path.Combine(Directory.GetCurrentDirectory(), "XMLVi`ewer_", time, ".log");
        }

        private static Logger       _instance = new Logger();
        private bool                _isInitailizeLogger;
        private StreamWriter        _streamWriter;
        private readonly bool       _isDebuggerAttached;
    }
}
