using Auth_Crud.Dtos;
using Auth_Crud.Dtos.Requests;
using Auth_Crud.Entities;

namespace Auth_Crud.Services;

public interface IProductService
{
    Task<ProductResponse> CreateProduct(ProductRequest productRequest, int adminId);
    Task<ProductResponse> FindProductById(int id);
    Task<IEnumerable<ProductResponse>> FindAllProduct();
    Task<ProductResponse> UpdateProduct(ProductRequest productRequest, int id, int adminId);
    Task DeleteProduct(int id, int? adminId);
}