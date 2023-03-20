using System.ComponentModel.DataAnnotations;

namespace POSGresApi.Sales.Models
{
    public sealed record SalesDto
    {
        public int saleId { get; init; }
        public string transactionDate { get; init; }
        public string customerName { get; init; }
        public int status { get; init; }
        public bool canDelete { get; init; }
        public bool canModify { get; init; }
        public List<SalesDetailDto> salesDetailDto { get; set; }
    }
}
