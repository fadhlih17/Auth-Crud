using Auth_Crud.Database;
using Auth_Crud.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth_Crud.Repositories.Implements;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> FindProductById(int id)
    {
        var product = await _context.Products
            .Include(product => product.LastAdmin)
            .Include(product => product.EntryAdmin)
            .Where(product => product.isDelete == false)
            .FirstOrDefaultAsync(p => p.Id.Equals(id));
        return product;
    }
    
    public async Task<Product> CreateProduct(Product product)
    {
        var createProduct = await _context.AddAsync(product);
        return createProduct.Entity;
    }

    public async Task<IEnumerable<Product>> FindAllProducts()
    {
        var products = _context.Products
            .Include(product => product.LastAdmin)
            .Include(product => product.EntryAdmin)
            .Where(product => product.isDelete == false);
        return await products.ToListAsync();
    }
    
    public Product UpdateProduct(Product product)
    {
        var attach = _context.Products.Attach(product);
        _context.Update(attach.Entity);
        return attach.Entity;
    }

    public void DeleteProduct(Product product)
    {
        _context.Products.Remove(product);
    }
}