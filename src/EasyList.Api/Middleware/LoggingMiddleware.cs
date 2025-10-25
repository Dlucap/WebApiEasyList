using EasyList.Business.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace EasyList.Api.Middleware
{
  public class LoggingMiddleware
  {
    private readonly RequestDelegate _next;

    public LoggingMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILogService logService)
    {
      var stopwatch = Stopwatch.StartNew();
      var originalBodyStream = context.Response.Body;

      try
      {
        // Log request
        await LogRequest(context, logService);

        // Continue processing
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        await _next(context);

        stopwatch.Stop();

        // Log response
        await LogResponse(context, logService, stopwatch.ElapsedMilliseconds);

        // Copy response back
        context.Response.Body = originalBodyStream;
        await responseBody.CopyToAsync(originalBodyStream);
      }
      catch (Exception ex)
      {
        stopwatch.Stop();
        
        // Log exception
        await logService.LogErro(
          $"Erro não tratado na requisição {context.Request.Method} {context.Request.Path}",
          ex,
          "HTTP",
          $"StatusCode: 500, Duration: {stopwatch.ElapsedMilliseconds}ms"
        );

        context.Response.Body = originalBodyStream;
        throw;
      }
    }

    private async Task LogRequest(HttpContext context, ILogService logService)
    {
      var request = context.Request;
      
      if (ShouldLog(request.Path))
      {
        var userId = context.User?.FindFirst("sub")?.Value ?? context.User?.FindFirst("id")?.Value;
        var userName = context.User?.Identity?.Name;
        var ipAddress = context.Connection.RemoteIpAddress?.ToString();

        await logService.LogInformacao(
          $"Iniciando requisição: {request.Method} {request.Path}{request.QueryString}",
          "HTTP",
          $"User: {userName ?? "Anonymous"}, IP: {ipAddress}"
        );
      }
    }

    private async Task LogResponse(HttpContext context, ILogService logService, long duration)
    {
      var request = context.Request;
      var response = context.Response;

      if (ShouldLog(request.Path))
      {
        var userId = context.User?.FindFirst("sub")?.Value ?? context.User?.FindFirst("id")?.Value;
        var userName = context.User?.Identity?.Name;
        var ipAddress = context.Connection.RemoteIpAddress?.ToString();

        await logService.LogRequisicaoHttp(
          request.Method,
          $"{request.Path}{request.QueryString}",
          response.StatusCode,
          (int)duration,
          userId,
          userName,
          ipAddress
        );
      }
    }

    private bool ShouldLog(PathString path)
    {
      // Não logar requisições para recursos estáticos
      return !path.StartsWithSegments("/swagger") &&
             !path.StartsWithSegments("/health") &&
             !path.StartsWithSegments("/_framework");
    }
  }
}
