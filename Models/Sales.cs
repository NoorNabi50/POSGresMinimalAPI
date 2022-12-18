using System.ComponentModel.DataAnnotations;

namespace POSGresApi.Models
{
    public sealed record SalesDto {
      public int saleId { get; init; }
      public  DateTime transactionDate { get; init; }
      public  String customerName { get; init; }
      public  int status { get; init; }
      public  Boolean canDelete { get; init; }
      public Boolean canModify { get; init; }
      public  List<SalesDetailDto> salesDetailDto { get; set; }
    }
}
