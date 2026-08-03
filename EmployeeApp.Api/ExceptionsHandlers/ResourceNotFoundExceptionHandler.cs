using Microsoft.AspNetCore.Diagnostics;
using EmployeeApp.Core.Exceptions;

namespace EmployeeApi.ExceptionsHandlers;

public class ResourceNotFoundExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        if (exception is not ResourceNotFoundException)
            return false;

        httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
        httpContext.Response.ContentType = "application/json";

        var response = new { error = "Ресурс не найден", details = exception.Message };
        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
        
        return true;
    }
}