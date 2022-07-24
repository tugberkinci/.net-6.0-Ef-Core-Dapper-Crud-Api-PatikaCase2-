using Npgsql;
using System.Data;

namespace PatikaHomework2.Data.Context
{
    public class DapperContext
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;
        
        public DapperContext(IConfiguration configuration)
        {
            this.configuration = configuration;
            
            connectionString = GetConnectionString();
        }
        private string GetConnectionString()
        {
            return this.configuration.GetConnectionString("PostgreSqlConnection");
        }
        public IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(connectionString);
        }
    }
}
