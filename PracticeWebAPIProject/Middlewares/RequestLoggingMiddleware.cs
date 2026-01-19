namespace PracticeWebAPIProject.Middlewares;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    
    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        Console.WriteLine("-------------------------------------------------------------------------");
        Console.WriteLine($"Request: {httpContext.Request.Path}");
        Console.WriteLine("-------------------------------------------------------------------------");
        await _next(httpContext);
        // Log HTTP Response
        Console.WriteLine("-------------------------------------Response------------------------------------");
        Console.WriteLine($"Response: {httpContext.Response.StatusCode} {httpContext.Response.ContentType} {httpContext.Response.Body}");
        Console.WriteLine("-------------------------------------------------------------------------");
        
    }
}