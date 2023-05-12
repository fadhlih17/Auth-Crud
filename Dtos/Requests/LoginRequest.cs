using System.ComponentModel.DataAnnotations;

namespace Auth_Crud.Dtos.Requests;

public class LoginRequest
{
    [Required, EmailAddress] public String Email { get; set; }
    [Required, StringLength(maximumLength: int.MaxValue, MinimumLength = 6)]
    public String Password { get; set; }
}