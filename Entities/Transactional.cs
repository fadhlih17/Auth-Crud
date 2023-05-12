using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth_Crud.Entities;

[Table(name:"t_transactional")]
public class Transactional
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column(name:"id")]
    public int Id { get; set; }
    
    [Column(name:"action", TypeName = "Varchar(10)")]
    public String Action { get; set; }

    [Column(name:"admin")]
    public int AdminId { get; set; }
    public virtual Admin? Admin { get; set; }
    
    [Column(name:"action_date")] public DateTime ActionDate { get; set; }
    
    [Column(name:"product_id")] public int? ProductId { get; set; }
    public virtual Product? Product { get; set; }
}