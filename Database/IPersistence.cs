namespace Auth_Crud.Database;

public interface IPersistence
{
    void SaveChanges();
    Task SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}