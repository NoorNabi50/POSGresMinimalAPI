﻿using Npgsql;
using POSGresApi.Models;
using POSGresApi.Repository;
using System.Data;
using System.Text;

namespace POSGresApi.Services
{
    public sealed class SalesService
    {

        private StringBuilder query { get; set; }
        private NpgsqlCommand npgsqlCommand { get; set; } = new NpgsqlCommand();


        public async Task<Dictionary<int,SalesDto>> GetAllSales()
        {
           try 
           {
            var sales =  await GetAllSalesMaster();

            if (sales is null)
                return null;

            var salesDetail = await GetAllSalesDetail();
            Dictionary<int, SalesDto> salesData = new();             
            foreach(var sale in sales)
            {
                int saleId = sale.saleId;
                sale.salesDetailDto = salesDetail.Where(x => x.saleId == saleId).ToList();
                salesData[saleId] =  sale;
            }
           
           }
           catch(Exception e)
           {
              return null;
           }
 }



        private async Task<List<SalesDto>> GetAllSalesMaster()
        {
            try
            {
            DataTable salesReader = await GetAllSalesMasterDatatable();
            if (salesReader == null || salesReader.Rows.Count == 0)
                return null;

            
                List<SalesDto> salesMaster = new();
                foreach (DataRow row in salesReader.Rows)
                {
                    SalesDto salesDto = new SalesDto { saleId =  (int)row["saleId"], transactionDate = (DateTime)row["transactionDate"], customerName = row["customerName"].ToString(),
                                                    status = (int)row["status"], canDelete =  (bool)row["canDelete"], canModify =  (bool)row["canModified"]};
                    salesMaster.Add(salesDto);
                }
                return salesMaster;
            }

            catch(Exception e)
            {
                return null;
            }
        }



        private async Task<List<SalesDetailDto>> GetAllSalesDetail()
        {
           try {
            DataTable salesReader = await GetAllSalesDetailDatatable();
            if (salesReader == null || salesReader.Rows.Count == 0)
                return null;

           
                List<SalesDetailDto> salesDetail = new();
                foreach (DataRow row in salesReader.Rows)
                {
                    SalesDetailDto salesDetailDto = new SalesDetailDto((int)row["detailId"], (int)row["saleId"], (int)row["ItemId"],
                                                    (int)row["qty"], (decimal)row["price"], (decimal)row["discount"], (decimal)row["totalAmount"]);
                    salesDetail.Add(salesDetailDto);
                }
                return salesDetail;
            }

            catch (Exception e)
            {
                return null;
            }
        }

        private async Task<DataTable> GetAllSalesMasterDatatable()
        {
            query = new StringBuilder();
            query.Append("SELECT saleid,transactionDate,customerName,status,canDelete,canModify from Sales");
            npgsqlCommand.CommandText = query.ToString();
            return await new DatabaseUtility().GetData(npgsqlCommand);
        }


        private async Task<DataTable> GetAllSalesDetailDatatable()
        {
            query = new StringBuilder();
            query.Append("SELECT * from SalesDetail");
            npgsqlCommand.CommandText = query.ToString();
            return await new DatabaseUtility().GetData(npgsqlCommand);
        }

    }
}
