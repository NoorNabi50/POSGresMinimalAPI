using Npgsql;
using POSGresApi.Sales.Models;
using System.Data;

namespace POSGresApi.Sales.Abstraction
{
    public interface ISalesService
    {
        private static NpgsqlConnection? Connection { get; set; }
        Task<Dictionary<int, SalesDto>> GetAllSales();
        Task<List<SalesDto>> GetAllMasterSales();
        Task<SalesDto> GetSalesById(int Id = 0);
        Task<List<SalesDetailDto>> GetAllSalesDetail();
        Task<List<SalesDetailDto>> GetSalesDetailById(int saleId = 0);
    }
}