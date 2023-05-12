using Auth_Crud.Entities;

namespace Auth_Crud.Dtos;

public class TransactionResponse
{
    public int Id { get; set; }
    public String Action { get; set; }
    public int AdminId { get; set; }
    public String AdminName { get; set; }
    public String ActionDate { get; set; }
    public int ProductId { get; set; }
    public ProductTransactionResponse Product { get; set; }
}