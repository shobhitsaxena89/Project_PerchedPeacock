using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogger
{
    public class EmailBasedLogger : AbstractLogger
    {
        private EmailBasedLogger() { }
        private static ILogger _logger;
        public static ILogger logger
        {
            get
            {
                return _logger ?? new EmailBasedLogger();
            }
        }
        public override void Log(string address, string log)
        {

        }
    }
}
