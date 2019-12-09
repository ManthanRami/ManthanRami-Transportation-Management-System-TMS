using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TMS.Utils
{
    public enum LogLevel
    {
        Info,
        Warn,
        Error
    }

    public enum LogOrigin
    {
        Ui,
        Database,
        Auth
    }

    public class Logger
    {
        private const int InfoLength = 25;
        private static string logPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        public static void Info(LogOrigin origin, string message)
        {
            WriteLog(LogLevel.Info, origin, DateTime.Now.ToLongTimeString(), message);
        }

        public static void Warn(LogOrigin origin, string message)
        {
            WriteLog(LogLevel.Warn, origin, DateTime.Now.ToLongTimeString(), message);
        }

        public static void Error(LogOrigin origin, string message)
        {
            WriteLog(LogLevel.Error, origin, DateTime.Now.ToLongTimeString(), message);
        }

        private static void WriteLog(LogLevel level, LogOrigin origin, string timestamp, string message)
        {
            if (!Directory.Exists(logPath + @"\logs"))
            {
                Directory.CreateDirectory(logPath + @"\logs");
            }

            string fileName = DateTime.Now.ToString("MM/dd/yyyy") + ".log";

            string path = logPath + @"\logs\" + fileName;
            File.AppendAllText(path, (timestamp + " [" + level.ToString().ToUpper() + "] " + origin.ToString() + ": ").PadRight(InfoLength) + message + "\n");
        }

        public static void ChangeLogPath(string path)
        {
            logPath = path;
        }
        public static string GetPath()
        {
            return logPath;
        }

 
    }
}
