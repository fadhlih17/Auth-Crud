using Auth_Crud.Dtos;
using Auth_Crud.Dtos.Requests;

namespace Auth_Crud.Services;

public interface IAuthService
{
    Task<RegisterResponse> Register(RegisterRequest registerRequest);
    Task<LoginResponse> Login(LoginRequest loginRequest);
}