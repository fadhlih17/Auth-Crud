using System.Security.Claims;
using Auth_Crud.Dtos;
using Auth_Crud.Dtos.Requests;
using Auth_Crud.Entities;
using Auth_Crud.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth_Crud.Controllers;

[ApiController]
[Route("products")]
[Authorize]
public class ProductController : ControllerBase
{
    private IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductRequest productRequest)
    {
        var adminId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        var product = await _productService.CreateProduct(productRequest, Int32.Parse(adminId));
        var response = new CommonResponse<ProductResponse>
        {
            Message = "Product Created",
            Data = product
        };
        return Created("/products", response);
    }

    [HttpGet("list")]
    public async Task<IActionResult> FindAll()
    {
        var products = await _productService.FindAllProduct();
        var response = new CommonResponse<IEnumerable<ProductResponse>>
        {
            Message = "Successfully Get Products",
            Data = products
        };
        return Ok(response);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> FindById(int id)
    {
        var product = await _productService.FindProductById(id);
        var response = new CommonResponse<ProductResponse>
        {
            Message = "Successfully Get Product",
            Data = product
        };
        return Ok(response);
    }
    
    [HttpPut("edit/{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductRequest productRequest)
    {
        var adminId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var product = await _productService.UpdateProduct(productRequest, id, Int32.Parse(adminId));
        var response = new CommonResponse<ProductResponse>
        {
            Message = "Product Updated",
            Data = product
        };
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var adminId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _productService.DeleteProduct(id, Int32.Parse(adminId));
        var response = new CommonResponse<string?>
        {
            Message = "Product Deleted",
        };
        return Ok(response);
    }
}