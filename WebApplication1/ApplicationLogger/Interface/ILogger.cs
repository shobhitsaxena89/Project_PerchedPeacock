using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogger
{
    public interface ILogger
    {
        void WriteLogs(string path, string log);
    }
}
