using Auth_Crud.Database;
using Auth_Crud.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth_Crud.Repositories.Implements;

public class AdminRepository : IAdminRepository
{
    private readonly AppDbContext _context;

    public AdminRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Admin> CreateAdmin(Admin admin)
    {
        var adminCreate = await _context.AddAsync(admin);
        return adminCreate.Entity;
    }

    public async Task<Admin?> FindByEmail(string email)
    {
        var admin = _context.Admins.AsQueryable();
        return await admin.FirstOrDefaultAsync(admin1 => admin1.Email.Equals(email));
    }
}