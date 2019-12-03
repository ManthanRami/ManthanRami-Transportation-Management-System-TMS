using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Utils
{
    public enum LogLevel
    {
        INFO,
        WARN,
        ERROR
    }

    public enum LogOrigin
    {
        UI,
        DATABASE,
        AUTH
    }

    public class Logger
    {
        public static void Info(LogOrigin origin, string message)
        {
            throw new NotImplementedException();
        }

        public static void Warn(LogOrigin origin, string message)
        {
            throw new NotImplementedException();
        }

        public static void Error(LogOrigin origin, string message)
        {
            throw new NotImplementedException();
        }

        private static void WriteLog(LogLevel level, string line)
        {
            using (StreamWriter logFile =
                new StreamWriter(Path.Combine("./logs", DateTime.Now.ToShortDateString() + ".log")))
            {
                logFile.WriteAsync(line);
            }
            
        }
    }
}
