using Auth_Crud.Dtos;
using Auth_Crud.Entities;

namespace Auth_Crud.Services;

public interface ITransactionalService
{
    Task CreateTransactional(Transactional transactional);
    Task<IEnumerable<TransactionResponse>> FindAllTransactional();
}