using Dapper;
using POSGresApi.Authentication.Abstraction;
using System.Data;

namespace POSGresApi.Authentication.Services
{
    public class UserManagementService : IUserManagementService
    {
        public async Task<string> GetAllUsersBySP()
        {
            using var connection = await Connection.GetConnection();
            #region otherways
            //  ResponseDTO result =  await connection.QuerySingleAsync<ResponseDTO>("call public.getallusersp()", commandType:CommandType.StoredProcedure);
            /*   ResponseDTO result =  await connection.QuerySingleAsync<ResponseDTO>($"call getallusersp(:id);",new { id = 10},commandType:CommandType.Text);
               ResponseDTO response =  await connection.QuerySingleAsync<ResponseDTO>(@"SELECT json_agg(row_to_json(q)) AS result FROM 
                                                                                     (SELECT * FROM users) q;",
                                                                                     commandType:CommandType.Text);*/
            #endregion
            string response = await connection.ExecuteScalarAsync<string>("public.getallusers", null,
                                     commandType: CommandType.StoredProcedure);
            return response;
        }

        public async Task<string> SaveUserBySP(string payload)
        {
            using var connection = await Connection.GetConnection();
            string response = await connection.ExecuteScalarAsync<string>("public.saveuser", new { payload = payload },
                commandType: CommandType.StoredProcedure);
            return response;
        }

    }
}
