using Auth_Crud.Entities;

namespace Auth_Crud.Repositories;

public interface IProductRepository
{
    Task<Product> CreateProduct(Product product);
    Task<IEnumerable<Product>> FindAllProducts();
    Product UpdateProduct(Product product);
    void DeleteProduct(Product product);
    Task<Product?> FindProductById(int id);
}