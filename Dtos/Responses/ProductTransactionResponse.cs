namespace Auth_Crud.Dtos;

public class ProductTransactionResponse
{
    public int Id { get; set; }
    public String Name { get; set; }
    public String Description { get; set; }
    public int LastAdminId { get; set; }
    public String LastUpdate { get; set; }
    public int EntryAdminId { get; set; }
    public String EntryDate { get; set; }
    public int? DeleteAdminId { get; set; }
    public String? DeleteDate { get; set; }
}