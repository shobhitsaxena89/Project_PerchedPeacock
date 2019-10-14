using ApplicationLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class DbConnectorFactoryImpl : IDbConnectorFactory
    {
        private ApplicationLogger.ILogger _logger;

        public DbConnectorFactoryImpl(ILogger logger)
        {
            // TODO: Complete member initialization
            this._logger = logger;
        }
        public ICrudOperator GetDb(DbType dbType)
        {
            switch(dbType)
            {
                case DbType.Sqlite:
                    return new SqliteCrudOperator(_logger);
                    
                default :
                    return null;
            }
        }
    }
}
