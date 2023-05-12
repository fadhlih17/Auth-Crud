using System.ComponentModel.DataAnnotations;

namespace Auth_Crud.Dtos.Requests;

public class RegisterRequest
{
    [Required] public String Name { get; set; }
    [Required, EmailAddress] public String Email { get; set; }
    [Required, StringLength(maximumLength: int.MaxValue, MinimumLength = 6)]
    public String Password { get; set; }
}