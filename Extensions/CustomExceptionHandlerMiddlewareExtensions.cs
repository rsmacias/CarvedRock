using CarvedRock.Api.Middleware;

namespace CarvedRock.Api.Extensions;

public static class CustomExceptionHandlerMiddlewareExtensions {
    
    public static IApplicationBuilder UseCustomExceptionHandler (this IApplicationBuilder builder) {
        return builder.UseMiddleware<CustomExceptionHandlingMiddleware>();
    }

}