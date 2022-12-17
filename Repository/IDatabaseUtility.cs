using Npgsql;
using System.Data;

namespace POSGresApi.Repository
{
    public interface IDatabaseUtility
    {

        private static NpgsqlConnection? Connection { get; set; }

        public async static Task<NpgsqlConnection> OpenConnection(string connectionString)
        {
            if (isConnectionOpen(Connection))
            {
                return Connection;
            }

            Connection = new NpgsqlConnection(connectionString);
            await Connection.OpenAsync();
            return Connection;
        }

        public Task<bool> ExecuteQuery(NpgsqlCommand command, NpgsqlConnection? connection = null);

        public Task<DataTable> GetAll(NpgsqlCommand command, NpgsqlConnection? connection = null);

        public Task<DataTable> GetAllById(NpgsqlCommand command, NpgsqlConnection? connection = null);


        public static Boolean isConnectionOpen(NpgsqlConnection Connection)
        {
            return (Connection != null && (Connection.State == ConnectionState.Open || Connection.State == ConnectionState.Connecting));                
        }

    }
}
