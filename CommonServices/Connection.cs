using Npgsql;
using POSGresApi.Settings;
using System.Data;

namespace POSGresApi.CommonServices
{
    public class Connection
    {

        private static NpgsqlConnection? connection { get; set; }

        public static async Task<IDbConnection> GetConnection()
        {
            string? connectionString = AppSettings.ConnectionString;

            if (isConnectionOpen(connection))
            {
                return connection;
            }

            connection = new NpgsqlConnection(connectionString);
            return connection;
        }

        public static bool isConnectionOpen(NpgsqlConnection Connection)
        {
            return Connection != null && (connection.State == ConnectionState.Open || Connection.State == ConnectionState.Connecting);
        }
    }
}
