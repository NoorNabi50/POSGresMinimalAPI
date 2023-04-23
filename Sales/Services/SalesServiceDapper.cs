using POSGresApi.Sales.Abstraction;
using POSGresApi.Sales.Models;
using Dapper;
using System.Data;

namespace POSGresApi.Sales.Services
{
    public class SalesServiceDapper : ISalesService
    {
        public async Task<List<SalesDto>> GetAllMasterSales()
        {
            using (var connection = await Connection.GetConnection())
            {
                string query = "SELECT * FROM public.sales;";
                var sales = await connection.QueryAsync<SalesDto>(query).ConfigureAwait(false);
                return sales.ToList();
            }
        }

        public Task<Dictionary<int, SalesDto>> GetAllSalesBySingleDBRequest()
        {
            throw new NotImplementedException();
        }

        public async Task<Dictionary<int, SalesDto>> GetAllSalesByTwoDBRequests()
        {
            var sales = await GetAllMasterSales().ConfigureAwait(false);
             if(sales is null) 
                return null;
             Dictionary<int,SalesDto> result = new();
             var salesDetail = await GetAllSalesDetail().ConfigureAwait(false);
            sales.ForEach(sale =>
            {
                int saleId = sale.saleId;
                sale.salesDetailDto = salesDetail.Where(x=>x.saleId== saleId).ToList();
                result[saleId] = sale;
            });
            return result;
        }

        public async Task<SalesDto> GetSalesById(int Id = 0)
        {
            using (var connection = await Connection.GetConnection())
            {
                string query = "SELECT * FROM public.sales where saleid = @Id";
                var sale  = await connection.QueryFirstOrDefaultAsync<SalesDto>(query,new { Id = Id}).ConfigureAwait(false);
                return sale;
            }
        }

        public async Task<List<SalesDetailDto>> GetSalesDetailById(int saleId = 0)
        {
            using var connection = await Connection.GetConnection();
            string query = "SELECT * FROM public.saledetails where saleid = @SaleId;";
            var sales = await connection.QueryAsync<SalesDetailDto>(query,new {SaleId = saleId}).ConfigureAwait(false);
            return sales.ToList();
        }

        private async Task<List<SalesDetailDto>> GetAllSalesDetail()
        {
            using var connection = await Connection.GetConnection();
            string query = "SELECT * FROM public.saledetails;";
            var sales = await connection.QueryAsync<SalesDetailDto>(query).ConfigureAwait(false);
            return sales.ToList();
        }

        
    }
}
