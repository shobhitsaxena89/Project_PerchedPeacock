using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogger
{
    public abstract class AbstractLogger : ILogger
    {
        public void WriteLogs(string path, string log)
        {
            Log(path, log);
        }

        public abstract void Log(string loggingAdress, string log);

    }
}
