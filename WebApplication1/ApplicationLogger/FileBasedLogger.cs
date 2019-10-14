using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogger
{
    public class FileBasedLogger : AbstractLogger
    {
        private FileBasedLogger() {}
        private static ILogger _logger;
        public static ILogger logger
        {
            get
            {
                return _logger ?? new FileBasedLogger();
            }
        }     

        public override void Log(string path, string log)
        {
           
        }
    }
}
