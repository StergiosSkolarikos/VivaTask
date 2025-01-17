using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VivaTask.Infrastructure.Configurations
{
    public class SqlConfiguration
    {
        public string SqlConnectionString { get; set; }
        public SqlDatabase VivaTaskDb { get; set; }

        public string VivaTaskDbConnectionString => string.Format(SqlConnectionString, VivaTaskDb.Server, VivaTaskDb.Database);
    }
    public class SqlDatabase
    {
        public string Server { get; set; }
        public string Database { get; set; }
    }
}
