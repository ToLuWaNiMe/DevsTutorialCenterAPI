using System.Net;
using DevsTutorialCenterAPI.Models.DTOs;
using Newtonsoft.Json;

namespace DevsTutorialCenterAPI.Filters;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;
    private readonly bool _isProdEnv;

    public ExceptionMiddleware(RequestDelegate next,
        ILogger<ExceptionMiddleware> logger, IHostEnvironment hostingEnvironment)
    {
        _logger = logger;
        _next = next;
        _isProdEnv = hostingEnvironment.IsProduction();
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            var errorResponse = new ResponseDto<object>
            {
                
                Data = null,
            };
            switch (error)
            {
                case UnauthorizedAccessException e:
                    _logger.LogError(
                        e,
                        e.StackTrace,
                        e.StackTrace,
                        e.Source);
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    errorResponse.Code = response.StatusCode;
                    errorResponse.Errors = new []{new Error("UnauthorizedAccessException", e.Message)};
                    errorResponse.Message = e.StackTrace;
                    break;
                case ArgumentOutOfRangeException e:
                    _logger.LogError(
                        e,
                        e.StackTrace,
                        e.Source,
                        e.ToString());
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.Code = response.StatusCode;
                    errorResponse.Errors = new []{new Error("ArgumentOutOfRangeException", e.Message)};
                    errorResponse.Message = e.Message;
                    break;
                case ArgumentNullException e:
                    _logger.LogError(
                        e,
                        e.StackTrace,
                        e.Source,
                        e.ToString());
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.Code = response.StatusCode;
                    errorResponse.Errors = new []{new Error("ArgumentNullException", e.Message)};
                    errorResponse.Message = e.Message;
                    break;
                default:
                    _logger.LogError(
                        error,
                        error.Source,
                        error.InnerException,
                        error.Message,
                        error.ToString());
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Code = response.StatusCode;
                    errorResponse.Errors = new []{new Error("UnauthorizedAccessException", error.Message)};
                    errorResponse.Message = _isProdEnv
                        ? "Internal Server Error."
                        : $"{error.Message}" +
                          "\n " +
                          $"Source: {error.Source} " +
                          $"\n StackTrace: {error.StackTrace}";
                    break;
            }

            var result = JsonConvert.SerializeObject(errorResponse);
            await response.WriteAsync(result);
        }
    }
}