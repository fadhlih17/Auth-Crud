using System.Net;
using Auth_Crud.Dtos;
using Auth_Crud.Exceptions;

namespace Auth_Crud.Middleware;

public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (NotFoundException e)
        {
            await HandleExceptionAsync(context, e);
            _logger.LogError(e.Message);
        }
        catch (UnauthorizedException e)
        {
            await HandleExceptionAsync(context, e);
            _logger.LogError(e.Message);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
            _logger.LogError(e.Message);
        }
    }
    
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var errorResponse = new ErrorResponse();

        switch (exception)
        {
            case NotFoundException :
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                errorResponse.Message = exception.Message;
                break;
            case UnauthorizedException :
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                errorResponse.Message = exception.Message;
                break;
            case not null :
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorResponse.Message = "Internal Server Error";
                break;
        }
        await context.Response.WriteAsJsonAsync(errorResponse);
    }
}