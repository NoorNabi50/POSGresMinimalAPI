using Npgsql;
using System.Data;

namespace POSGresApi.Repository
{
    public interface IDatabaseUtility
    {
        public Task<bool> ExecuteQuery(NpgsqlCommand command, NpgsqlConnection? connection = null);
        public Task<NpgsqlDataReader> GetData(NpgsqlCommand command, NpgsqlConnection? connection = null);
        public Task<DataTable> GetAllById(NpgsqlCommand command, NpgsqlConnection? connection = null);
        public static Boolean isConnectionOpen(NpgsqlConnection Connection)
        {
            return (Connection != null && (Connection.State == ConnectionState.Open || Connection.State == ConnectionState.Connecting));                
        }

        
    }
}
