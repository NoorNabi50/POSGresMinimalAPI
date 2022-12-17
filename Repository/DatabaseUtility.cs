using Npgsql;
using System.Data;

namespace POSGresApi.Repository
{
    public class DatabaseUtility : IDatabaseUtility
    {
        private readonly IConfiguration? configuration;

        public DatabaseUtility(IConfiguration _configuration)
        {
            _configuration = configuration;
        }

        public bool ExecuteQuery(NpgsqlCommand command, NpgsqlConnection? connection = null)
        {
            throw new NotImplementedException();
        }

        public DataTable GetAll(NpgsqlCommand command, NpgsqlConnection? connection = null)
        {
            throw new NotImplementedException();
        }

        public DataTable GetAllById(NpgsqlCommand command, NpgsqlConnection? connection = null)
        {
            throw new NotImplementedException();
        }

        public NpgsqlConnection OpenConnection()
        {
            
            throw new NotImplementedException();
        }
    }
}
