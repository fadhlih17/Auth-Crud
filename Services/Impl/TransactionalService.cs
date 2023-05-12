using Auth_Crud.Database;
using Auth_Crud.Dtos;
using Auth_Crud.Entities;
using Auth_Crud.Repositories;

namespace Auth_Crud.Services.Impl;

public class TransactionalService : ITransactionalService
{
    private readonly IPersistence _persistence;
    private readonly ITransactionalRepository _repository;

    public TransactionalService(IPersistence persistence, ITransactionalRepository repository)
    {
        _persistence = persistence;
        _repository = repository;
    }

    public async Task CreateTransactional(Transactional transactional)
    {
        await _repository.CreateTransactional(transactional);
        await _persistence.SaveChangesAsync();
    }

    public async Task<IEnumerable<TransactionResponse>> FindAllTransactional()
    {
        var transactions = await _repository.FindAllTransactional();

        var transactionResponse = transactions.Select(transactional =>
        {
            ProductTransactionResponse productResponse = new ProductTransactionResponse
            {
                Id = transactional.Product.Id,
                Name = transactional.Product.Name,
                Description = transactional.Product.Description,
                LastAdminId = transactional.Product.LastAdminId ?? 0,
                LastUpdate = transactional.Product.LastUpdate.ToString("g"),
                EntryAdminId = transactional.Product.EntryAdminId ?? 0,
                EntryDate = transactional.Product.EntryDate.ToString("g"),
                DeleteAdminId = transactional.Product.DeleteAdminId,
                DeleteDate = transactional.Product.DeleteDate.ToString()
            };
            
            return new TransactionResponse
            {
                Id = transactional.Id,
                Action = transactional.Action,
                AdminId = transactional.AdminId,
                AdminName = transactional.Admin.Name,
                ActionDate = transactional.ActionDate.ToString("g"),
                ProductId = transactional.ProductId ?? 0,
                Product = productResponse
            };
        });

        return transactionResponse;
    }
}