using Npgsql;
using POSGresApi.Settings;
using System.Data;

namespace POSGresApi.Repository
{
    public class DatabaseUtility : IDatabaseUtility
    {

        public async Task<bool> ExecuteQuery(NpgsqlCommand command, NpgsqlConnection? connection = null)
        {
            try
            {
                command.Connection = await IDatabaseUtility.OpenConnection(ConfigurationProperties.ConnectionString);
                await command.ExecuteNonQueryAsync();
                return true;
            }

            catch(Exception e)
            {
                return false;
            }
        }

        public async Task<DataTable> GetData(NpgsqlCommand command, NpgsqlConnection? connection = null)
        {
            DataTable? dataTable = null;
            try
            {
                command.Connection = await IDatabaseUtility.OpenConnection(ConfigurationProperties.ConnectionString);
                NpgsqlDataReader  reader =   await command.ExecuteReaderAsync();
                dataTable = new DataTable();
                dataTable.Load(reader);
                return dataTable;

            }

            catch(Exception e)
            {
                return null;
            }
        }

        public async Task<DataTable> GetAllById(NpgsqlCommand command, NpgsqlConnection? connection = null)
        {
            throw new NotImplementedException();
        }


    }
}
