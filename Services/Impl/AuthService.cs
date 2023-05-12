using Auth_Crud.Database;
using Auth_Crud.Dtos;
using Auth_Crud.Dtos.Requests;
using Auth_Crud.Entities;
using Auth_Crud.Exceptions;
using Auth_Crud.Repositories;
using Auth_Crud.Repositories.Implements;
using Auth_Crud.Security;

namespace Auth_Crud.Services.Impl;

public class AuthService : IAuthService
{
    private readonly IAdminRepository _adminRepository;
    private readonly IPersistence _persistence;
    private readonly IJwtUtil _jwtUtil;

    public AuthService(IAdminRepository adminRepository, IPersistence persistence, IJwtUtil jwtUtil)
    {
        _adminRepository = adminRepository;
        _persistence = persistence;
        _jwtUtil = jwtUtil;
    }

    private async Task<Admin> LoadEmailLogin(string email)
    {
        var admin = await _adminRepository.FindByEmail(email);
        if (admin is null) throw new UnauthorizedException("Email or Password invalid");
        return admin;
    }

    private async Task<string> LoadEmailRegister(string email)
    {
        var admin = await _adminRepository.FindByEmail(email);
        if (admin != null) throw new UnauthorizedException("Email has been registered");
        return email;
    }

    public async Task<RegisterResponse> Register(RegisterRequest registerRequest)
    {
        var email = await LoadEmailRegister(registerRequest.Email);
        var admin = new Admin
        {
            Name = registerRequest.Name,
            Email = email,
            Password = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password)
        };
        await _persistence.BeginTransactionAsync();
        RegisterResponse registerResponse = new RegisterResponse();
        try
        {
            var create = await _adminRepository.CreateAdmin(admin);
            registerResponse.Email = create.Email;
            registerResponse.Name = create.Name;
            await _persistence.SaveChangesAsync();
            await _persistence.CommitAsync();
        }
        catch (Exception e)
        {
            await _persistence.RollbackAsync();
            throw;
        }

        return registerResponse;
    }

    public async Task<LoginResponse> Login(LoginRequest loginRequest)
    {
        var admin = await LoadEmailLogin(loginRequest.Email);
        var verify = BCrypt.Net.BCrypt.Verify(loginRequest.Password, admin.Password);
        if (!verify) throw new UnauthorizedException("Email or Password invalid");

        var token = _jwtUtil.GenerateToken(admin);

        return new LoginResponse
        {
            Id = admin.Id,
            Email = admin.Email,
            Token = token
        };
    }
}