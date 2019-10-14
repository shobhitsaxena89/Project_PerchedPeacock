using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogger
{
    public class LoggerFactoryImpl : ILoggerFactory
    {
        public ILogger GetLogger(LogType logType)
        {
            if(logType.Equals(LogType.FileBased))
            {
                return FileBasedLogger.logger;
            }
            else if(logType.Equals(LogType.Email))
            {
                return EmailBasedLogger.logger;
            }
            return new ConsoleLogger();
        }
    }
}
