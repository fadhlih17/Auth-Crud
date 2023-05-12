using Auth_Crud.Database;
using Auth_Crud.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth_Crud.Repositories.Implements;

public class TransactionalRepository : ITransactionalRepository
{
    private readonly AppDbContext _context;

    public TransactionalRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateTransactional(Transactional transactional)
    {
        await _context.Transactionals.AddAsync(transactional);
    }

    public async Task<IEnumerable<Transactional>> FindAllTransactional()
    {
        return await _context.Transactionals
            .Include(transactional => transactional.Product)
            .Include(transactional => transactional.Admin)
            .ToListAsync();
    }
}