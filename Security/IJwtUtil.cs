using Auth_Crud.Entities;

namespace Auth_Crud.Security;

public interface IJwtUtil
{
    String GenerateToken(Admin admin);
}