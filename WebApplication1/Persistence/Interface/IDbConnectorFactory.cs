using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public interface IDbConnectorFactory
    {
        ICrudOperator GetDb(DbType dbType);
    }
}
