using System.ComponentModel.DataAnnotations;

namespace POSGresApi.Models
{
    public sealed record SalesDto (
        int saleId,
        DateTime transactionDate,
        String customerName,
        int status,
        Boolean canDelete,
        Boolean canModify      
     );
}
