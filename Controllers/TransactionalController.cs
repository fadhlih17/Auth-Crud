using Auth_Crud.Dtos;
using Auth_Crud.Entities;
using Auth_Crud.Services;
using Microsoft.AspNetCore.Mvc;

namespace Auth_Crud.Controllers;

[ApiController]
[Route("transactional")]
public class TransactionalController : ControllerBase
{
    private readonly ITransactionalService _transactionalService;

    public TransactionalController(ITransactionalService transactionalService)
    {
        _transactionalService = transactionalService;
    }

    [HttpGet]
    public async Task<IActionResult> FindAll()
    {
        var findAllTransactional = await _transactionalService.FindAllTransactional();
        var result = new CommonResponse<IEnumerable<TransactionResponse>>
        {
            Message = "Successfully get all transactions",
            Data = findAllTransactional
        };
        
        return Ok(result);
    }
}