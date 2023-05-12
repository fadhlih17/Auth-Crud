using Auth_Crud.Database;
using Auth_Crud.Dtos;
using Auth_Crud.Dtos.Requests;
using Auth_Crud.Entities;
using Auth_Crud.Exceptions;
using Auth_Crud.Repositories;

namespace Auth_Crud.Services.Impl;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IPersistence _persistence;
    private readonly ITransactionalService _transactionalService;

    public ProductService(IProductRepository repository, IPersistence persistence, ITransactionalService transactionalService)
    {
        _repository = repository;
        _persistence = persistence;
        _transactionalService = transactionalService;
    }

    public async Task<ProductResponse> CreateProduct(ProductRequest productRequest, int adminId)
    {
        var product = new Product
        {
            Name = productRequest.Name,
            Description = productRequest.Description,
            LastAdminId = adminId,
            LastUpdate = DateTime.Now,
            EntryAdminId = adminId,
            EntryDate = DateTime.Now,
            isDelete = false
        };

        await _persistence.BeginTransactionAsync();
        Product saveProduct = new Product();
        try
        {
            saveProduct = await _repository.CreateProduct(product);
            await _persistence.SaveChangesAsync();
            await _persistence.CommitAsync();
        }
        catch (Exception e)
        {
            await _persistence.RollbackAsync();
            throw;
        }

        Transactional transactional = new Transactional
        {
            Action = "Create",
            AdminId = saveProduct.LastAdminId ?? 0,
            ActionDate = DateTime.Now,
            ProductId = saveProduct.Id,
        };

        await _transactionalService.CreateTransactional(transactional);

        return await FindProductById(saveProduct.Id);
    }

    public async Task<IEnumerable<ProductResponse>> FindAllProduct()
    {
        var findAllProducts = await _repository.FindAllProducts();

        var responses = findAllProducts.Select(product =>
        {
            var productResponse = new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                LastAdminId = product.LastAdminId,
                LastAdminName = product.LastAdmin.Name,
                LastUpdate = product.LastUpdate.ToString("g"),
                EntryAdminId = product.EntryAdminId,
                EntryAdminName = product.EntryAdmin.Name,
                EntryDate = product.EntryDate.ToString("g")
            };
            return productResponse;
        });
        
        return responses;
    }

    public async Task<ProductResponse> FindProductById(int id)
    {
        var product = await _repository.FindProductById(id);
        if (product is null ) throw new NotFoundException("Product Not Found");
        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            LastAdminId = product.LastAdminId,
            LastAdminName = product.LastAdmin.Name,
            LastUpdate = product.LastUpdate.ToString("g"),
            EntryAdminId = product.EntryAdminId,
            EntryAdminName = product.EntryAdmin.Name,
            EntryDate = product.EntryDate.ToString("g")
        };
    }
    
    public async Task<ProductResponse> UpdateProduct(ProductRequest productRequest, int id ,int adminId)
    {
        var product = await _repository.FindProductById(id);
        if (product == null || product.isDelete) throw new NotFoundException("Product Not Found");
        
        product.Name = productRequest.Name;
        product.Description = productRequest.Description;
        product.LastAdminId = adminId;
        product.LastUpdate = DateTime.Now;

        _repository.UpdateProduct(product);
        await _persistence.SaveChangesAsync();
        
        Transactional transactional = new Transactional
        {
            Action = "Update",
            AdminId = product.LastAdminId ?? 0,
            ActionDate = DateTime.Now,
            ProductId = product.Id,
        };

        await _transactionalService.CreateTransactional(transactional);

        return await FindProductById(product.Id);
    }

    public async Task DeleteProduct(int id, int? adminId)
    {
        var product = await _repository.FindProductById(id);
        if (product == null) throw new NotFoundException("Product Not Found");
        product.isDelete = true;
        product.DeleteAdminId = adminId;
        product.DeleteDate = DateTime.Now;
        
        _repository.UpdateProduct(product);
        await _persistence.SaveChangesAsync();
        Transactional transactional = new Transactional
        {
            Action = "Delete",
            AdminId = adminId ?? 0,
            ActionDate = DateTime.Now,
            ProductId = product.Id,
        };
        
        await _transactionalService.CreateTransactional(transactional);
    }
}