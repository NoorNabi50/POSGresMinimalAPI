using Npgsql;
using System.Data;

namespace POSGresApi.Repository
{
    public interface IDatabaseUtility
    {

        private static NpgsqlConnection? Connection { get; set; }

        public async static Task<NpgsqlConnection> OpenConnection(string connectionString)
        {
            if (Connection != null && (Connection.State == ConnectionState.Open || Connection.State == ConnectionState.Connecting))
            {
                return Connection;
            }

            Connection = new NpgsqlConnection(connectionString);
            await Connection.OpenAsync();
            return Connection;
        }

        public  Boolean ExecuteQuery(NpgsqlCommand command, NpgsqlConnection? connection = null);

        public DataTable GetAll(NpgsqlCommand command, NpgsqlConnection? connection = null);

        public DataTable GetAllById(NpgsqlCommand command, NpgsqlConnection? connection = null);


    }
}
