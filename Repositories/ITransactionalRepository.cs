using Auth_Crud.Entities;

namespace Auth_Crud.Repositories;

public interface ITransactionalRepository
{
    Task CreateTransactional(Transactional transactional);
    Task<IEnumerable<Transactional>> FindAllTransactional();
}