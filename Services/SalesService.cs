using Npgsql;
using POSGresApi.Models;
using POSGresApi.Repository;
using System.Data;
using System.Text;

namespace POSGresApi.Services
{
    public sealed class SalesService : ISalesService
    {

        private StringBuilder query { get; set; } = new StringBuilder();
        public async Task<Dictionary<int, SalesDto>> GetAllSales()
        {
            try
            {

                var sales = await GetAllMasterSales();

                if (sales is null)
                    return null;

                var salesDetail = await GetAllSalesDetail();
                Dictionary<int, SalesDto> salesData = new();
                foreach (var sale in sales)
                {
                    int saleId = sale.saleId;
                    sale.salesDetailDto = salesDetail.Where(x => x.saleId == saleId).ToList();
                    salesData[saleId] = sale;
                }

                return salesData;
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public async Task<SalesDto> GetSalesById(int Id = 0)
        {
            try
            {
                SalesDto? salesDto = null;
                query.Clear().Append("SELECT saleid,transactionDate,customerName,status FROM sales where saleid = @id");
                await using (NpgsqlCommand command = new NpgsqlCommand(query.ToString(), await Connection.OpenConnection()))
                {
                    command.Parameters.AddWithValue("@id", Id);
                    await using (NpgsqlDataReader salesReader = await command.ExecuteReaderAsync())
                    {
                        
                        while (await salesReader.ReadAsync())
                        {
                            salesDto =  MapToSalesDtoObject(salesReader);
                        }

                        await salesReader.CloseAsync();

                        if (salesDto is not null)
                        {
                            salesDto.salesDetailDto = await GetSalesDetailById(salesDto.saleId);
                        }
                    }
                    return salesDto;
                }
            }

            catch(Exception e)
            {

                return null;

            }
        }


        public async Task<List<SalesDetailDto>> GetSalesDetailById(int saleId = 0)
        {
            try
            {
                query.Clear().Append("SELECT * FROM SalesDetail where saleid = @id");
                List<SalesDetailDto> salesDetail = null;
                await using (NpgsqlCommand command = new NpgsqlCommand(query.ToString(), await Connection.OpenConnection()))
                {
                    salesDetail = new List<SalesDetailDto>();
                    command.Parameters.AddWithValue("@id", saleId);
                    await using (NpgsqlDataReader salesReader = await command.ExecuteReaderAsync())
                        while (await salesReader.ReadAsync())
                        {
                            salesDetail.Add(MapToSalesDetailDtoObject(salesReader));

                        }

                    return salesDetail;
                }


            }

            catch (Exception e)
            {
                return null;
            }
        }






        public async Task<List<SalesDto>> GetAllMasterSales()
        {
            try
            {
                query.Clear().Append("SELECT saleid,transactionDate,customerName,status FROM sales");
                List<SalesDto> salesMaster = new();
                await using (NpgsqlCommand command = new NpgsqlCommand(query.ToString(), await Connection.OpenConnection()))
                {
                    await using (NpgsqlDataReader salesReader = await command.ExecuteReaderAsync())
                    {
                        while (await salesReader.ReadAsync())
                        {
                            salesMaster.Add(MapToSalesDtoObject(salesReader));
                        }

                       await salesReader.CloseAsync();
                    }
                    return salesMaster;
                }
            }

            catch (Exception e)
            {
                return null;
            }
        }




        public async Task<List<SalesDetailDto>> GetAllSalesDetail()
        {
            try
            {
                query.Clear().Append("SELECT * FROM SalesDetail");
                List<SalesDetailDto> salesDetail =null;
                await using (NpgsqlCommand command = new NpgsqlCommand(query.ToString(), await Connection.OpenConnection()))
                {
                    salesDetail = new List<SalesDetailDto>();

                    await using (NpgsqlDataReader salesReader = await command.ExecuteReaderAsync())
                        while (await salesReader.ReadAsync())
                        {
                            salesDetail.Add(MapToSalesDetailDtoObject(salesReader));

                        }

                    return salesDetail;
                }


            }

            catch (Exception e)
            {
                return null;
            }
        }



        private SalesDto MapToSalesDtoObject(NpgsqlDataReader salesReader)
        {

            return new SalesDto
            {
                saleId = (int)salesReader["saleId"],
                transactionDate = salesReader["transactionDate"].ToString(),
                customerName = salesReader["customerName"].ToString(),
                status = (int)salesReader["status"]
            };
           
        }

        private SalesDetailDto MapToSalesDetailDtoObject(NpgsqlDataReader salesReader)
        {
           return new SalesDetailDto((int)salesReader["detailId"], (int)salesReader["saleId"], (int)salesReader["ItemId"],
                                                          (int)salesReader["qty"], (decimal)salesReader["price"], (decimal)salesReader["discount"],
                                                          (decimal)salesReader["totalAmount"]);
        }

    }
}
