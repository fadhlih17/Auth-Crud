using System.ComponentModel.DataAnnotations.Schema;

namespace Auth_Crud.Entities;

[Table(name:"m_product")]
public class Product
{

    [Column(name:"id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Column(name:"name")]
    public String Name { get; set; }
    
    [Column(name:"description")] public String Description { get; set; }
    
    [Column(name:"last_admin")]
    public int? LastAdminId { get; set; }
    [ForeignKey("LastAdminId")]
    public virtual Admin? LastAdmin { get; set; }
    
    [Column(name:"last_update")] 
    public DateTime LastUpdate { get; set; }
    
    [Column(name:"entry_admin")]
    public int? EntryAdminId { get; set; }
    [ForeignKey("EntryAdminId")]
    public virtual Admin? EntryAdmin { get; set; }

    [Column(name:"entry_date")]
    public DateTime EntryDate { get; set; }
    
    [Column(name:"delete_admin")]
    public int? DeleteAdminId { get; set; }
    [ForeignKey("DeleteAdminId")]
    public virtual Admin? DeleteAdmin { get; set; }

    [Column(name:"delete_date")]
    public DateTime? DeleteDate { get; set; }
    
    public bool isDelete { get; set; }
    
    public virtual IEnumerable<Transactional>? Transactionals { get; set; }
}