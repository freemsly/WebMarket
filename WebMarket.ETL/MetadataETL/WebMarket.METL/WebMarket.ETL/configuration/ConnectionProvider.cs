using Microsoft.Extensions.Options;
using WebMarket.Model.model;

namespace WebMarket.ETL.configuration
{
    public class ConnectionProvider : IConnectionProvider
    {
        public bool IsNoSql { get; set; }
        public  ConnectionString ConnectionStrings { get; set; }
        public ConnectionProvider(IOptions<ConnectionString> connectionString)
        {
            ConnectionStrings = connectionString.Value;
        }

        public ConnectionString Get()
        {
            return ConnectionStrings;
        }
    }
}
