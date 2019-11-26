using System;
using System.Collections.Generic;
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

    public enum LogType
    {
        UI,
        DATABASE,
        AUTH
    }

    public class Logger
    {
        public static void Info(string message)
        {
            throw new NotImplementedException();
        }

        public static void Warn(string message)
        {
            throw new NotImplementedException();
        }

        public void Error(string message)
        {
            throw new NotImplementedException();
        }
    }
}
