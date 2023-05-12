namespace Auth_Crud.Dtos;

public class ProductResponse
{
    public int Id { get; set; }
    public String Name { get; set; }
    public String Description { get; set; }
    public int? LastAdminId { get; set; }
    public String? LastAdminName { get; set; }
    public String LastUpdate { get; set; }
    public int? EntryAdminId { get; set; }
    public String? EntryAdminName { get; set; }
    public String EntryDate { get; set; }
}