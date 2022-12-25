using Npgsql;
using POSGresApi.Settings;
using System.Data;

namespace POSGresApi
{
    public class Connection
    {

        private static NpgsqlConnection? connection { get; set; }

        public async static Task<NpgsqlConnection> OpenConnection()
        {
            string? connectionString = ConfigurationProperties.ConnectionString;
            if (isConnectionOpen(connection))
            {
                return connection;
            }

            connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();
            return connection;
        }

        public static Boolean isConnectionOpen(NpgsqlConnection Connection)
        {
            return (Connection != null && (connection.State == ConnectionState.Open || Connection.State == ConnectionState.Connecting));
        }
    }
}
