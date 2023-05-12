namespace Auth_Crud.Database;

public class DbPersistence : IPersistence
{
    private AppDbContext _appDbContext;

    public DbPersistence(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    public void SaveChanges()
    {
        _appDbContext.SaveChanges();
    }
    
    public async Task SaveChangesAsync()
    {
        await _appDbContext.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        await _appDbContext.Database.BeginTransactionAsync();
    }
    
    public async Task CommitAsync()
    {
        await _appDbContext.Database.CommitTransactionAsync();
    }

    public async Task RollbackAsync()
    {
        await _appDbContext.Database.RollbackTransactionAsync();
    }
}