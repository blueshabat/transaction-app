using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Core.Infrastructure.Database
{
    public class DapperContext(IConfiguration configuration)
    {
        private readonly string connectionString = configuration.GetConnectionString("SqlConnection") ?? throw new ArgumentNullException("Connection string is not defined");

        public IDbConnection CreateConnection() => new SqlConnection(connectionString);
    }
}
