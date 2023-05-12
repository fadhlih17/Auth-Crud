using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth_Crud.Entities;

[Table(name: "m_admin")]
public class Admin
{
    [Key, Column(name:"id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
    
    [Column(name:"name", TypeName = "Varchar(50)")]public String Name { get; set; }
    
    [Column(name:"email"), Required, EmailAddress]
    public String Email { get; set; }
    
    [Column(name:"password"), Required, StringLength(maximumLength: Int32.MaxValue, MinimumLength = 6)]
    public String Password { get; set; }
}