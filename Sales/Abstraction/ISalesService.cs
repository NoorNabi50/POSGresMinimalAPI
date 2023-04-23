using Npgsql;
using POSGresApi.Sales.Models;
using System.Data;

namespace POSGresApi.Sales.Abstraction
{
    public interface ISalesService
    {
        private static NpgsqlConnection? Connection { get; set; }
        Task<Dictionary<int, SalesDto>> GetAllSalesBySingleDBRequest();
        Task<Dictionary<int, SalesDto>> GetAllSalesByTwoDBRequests();
        Task<List<SalesDto>> GetAllMasterSales();
        Task<SalesDto> GetSalesById(int Id = 0);
        Task<List<SalesDetailDto>> GetSalesDetailById(int saleId = 0);
    }
}