namespace POSGresApi.Sales.Models
{
    public sealed record SalesDetailDto
   (
        int detailId,
        int saleId,
        int itemId,
        int qty,
        decimal price,
        decimal discount,
        decimal totalAmount

    );
}
