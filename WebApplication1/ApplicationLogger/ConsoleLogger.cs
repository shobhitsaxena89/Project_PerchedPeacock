using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogger
{
    public class ConsoleLogger : AbstractLogger
    {
        public override void Log(string loggingAdress, string log)
        {
            Console.WriteLine(log);
        }
    }
}
