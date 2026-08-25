using System.Text.Json;
using KYC.TrueFace.Core.Application.Messaging.Response;
using KYC.TrueFace.Core.Application.Messaging.Response.Base;
using KYC.TrueFace.Core.Domain.Exceptions;

namespace KYC.TrueFace.Core.API.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (KycException ex)
        {
            await WriteResponse(context, StatusCodes.Status400BadRequest, BaseResponse.Create(ex.Message));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception on {Method} {Path}", context.Request.Method, context.Request.Path);
            await WriteResponse(context, StatusCodes.Status500InternalServerError, ResponseError.Create());
        }
    }

    private static Task WriteResponse(HttpContext context, int statusCode, object body)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        return context.Response.WriteAsync(JsonSerializer.Serialize(body, JsonOptions));
    }
}
