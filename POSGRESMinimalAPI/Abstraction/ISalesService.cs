using Npgsql;
using POSGresApi.Models;
using System.Data;

namespace POSGresApi.Repository
{
    public interface ISalesService
    {
       private static NpgsqlConnection? Connection { get; set; }
        /// <summary>
        /// Method to return all Sales with its detail
        /// </summary>
        /// <returns>Dictionary where key = SaleId, Value = SalesDto</returns>
        Task<Dictionary<int, SalesDto>> GetAllSales();
       Task<SalesDto> GetSalesById(int Id = 0);
       Task<List<SalesDetailDto>> GetSalesDetailById(int saleId = 0);
    }
}