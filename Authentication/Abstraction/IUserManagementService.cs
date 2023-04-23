namespace POSGresApi.Authentication.Abstraction
{
    public interface IUserManagementService
    {
        Task<string> GetAllUsersBySP();
        Task<string> SaveUserBySP(string payload);
    }
}