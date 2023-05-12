using Auth_Crud.Entities;

namespace Auth_Crud.Repositories;

public interface IAdminRepository
{
    Task<Admin> CreateAdmin(Admin admin);
    Task<Admin?> FindByEmail(String email);
}