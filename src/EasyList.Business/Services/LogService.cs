using EasyList.Business.Interfaces.IRepository;
using EasyList.Business.Interfaces.IServices;
using EasyList.Business.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EasyList.Business.Services
{
  public class LogService : ILogService
  {
    private readonly ILogRepository _logRepository;
    private readonly ILogger<LogService> _logger;

    public LogService(ILogRepository logRepository, ILogger<LogService> logger)
    {
      _logRepository = logRepository;
      _logger = logger;
    }

    private string SanitizeLogInput(string input)
    {
      if (string.IsNullOrEmpty(input))
        return input;

      // Remove newline characters to prevent log forging
      return Regex.Replace(input, @"[\r\n]+", " ");
    }

    public async Task LogInformacao(string message, string category = null, string additionalInfo = null)
    {
      try
      {
        var logEntry = new LogEntry
        {
          Level = "Information",
          Category = SanitizeLogInput(category),
          Message = SanitizeLogInput(message),
          AdditionalInfo = SanitizeLogInput(additionalInfo)
        };

        await _logRepository.Adicionar(logEntry);
        _logger.LogInformation($"[{category}] {message}");
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Erro ao salvar log de informação");
      }
    }

    public async Task LogAviso(string message, string category = null, string additionalInfo = null)
    {
      try
      {
        var logEntry = new LogEntry
        {
          Level = "Warning",
          Category = SanitizeLogInput(category),
          Message = SanitizeLogInput(message),
          AdditionalInfo = SanitizeLogInput(additionalInfo)
        };

        await _logRepository.Adicionar(logEntry);
        _logger.LogWarning($"[{category}] {message}");
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Erro ao salvar log de aviso");
      }
    }

    public async Task LogErro(string message, Exception exception = null, string category = null, string additionalInfo = null)
    {
      try
      {
        var logEntry = new LogEntry
        {
          Level = "Error",
          Category = SanitizeLogInput(category),
          Message = SanitizeLogInput(message),
          Exception = exception?.ToString(),
          AdditionalInfo = SanitizeLogInput(additionalInfo)
        };

        await _logRepository.Adicionar(logEntry);
        _logger.LogError(exception, $"[{category}] {message}");
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Erro ao salvar log de erro");
      }
    }

    public async Task LogRequisicaoHttp(string httpMethod, string path, int statusCode, int duration, string userId = null, string userName = null, string ipAddress = null)
    {
      try
      {
        var logEntry = new LogEntry
        {
          Level = statusCode >= 400 ? "Warning" : "Information",
          Category = "HTTP",
          Message = SanitizeLogInput($"{httpMethod} {path} - Status: {statusCode} - Duration: {duration}ms"),
          HttpMethod = SanitizeLogInput(httpMethod),
          Path = SanitizeLogInput(path),
          StatusCode = statusCode,
          Duration = duration,
          UserId = SanitizeLogInput(userId),
          UserName = SanitizeLogInput(userName),
          IpAddress = SanitizeLogInput(ipAddress)
        };

        await _logRepository.Adicionar(logEntry);
        _logger.LogInformation($"HTTP Request: {httpMethod} {path} - {statusCode} ({duration}ms)");
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Erro ao salvar log de requisição HTTP");
      }
    }

    public async Task LogAutenticacao(string userName, bool sucesso, string ipAddress = null, string additionalInfo = null)
    {
      try
      {
        var sanitizedUserName = SanitizeLogInput(userName);
        var logEntry = new LogEntry
        {
          Level = sucesso ? "Information" : "Warning",
          Category = "Authentication",
          Message = sucesso 
            ? $"Autenticação bem-sucedida para o usuário: {sanitizedUserName}"
            : $"Tentativa de autenticação falhou para o usuário: {sanitizedUserName}",
          UserName = sanitizedUserName,
          IpAddress = SanitizeLogInput(ipAddress),
          AdditionalInfo = SanitizeLogInput(additionalInfo)
        };

        await _logRepository.Adicionar(logEntry);
        _logger.LogInformation($"Authentication: {userName} - Success: {sucesso}");
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Erro ao salvar log de autenticação");
      }
    }

    public async Task LogOperacaoBancoDados(string operacao, string entidade, string detalhes = null)
    {
      try
      {
        var logEntry = new LogEntry
        {
          Level = "Information",
          Category = "Database",
          Message = SanitizeLogInput($"Operação: {operacao} - Entidade: {entidade}"),
          AdditionalInfo = SanitizeLogInput(detalhes)
        };

        await _logRepository.Adicionar(logEntry);
        _logger.LogInformation($"Database Operation: {operacao} on {entidade}");
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Erro ao salvar log de operação de banco de dados");
      }
    }
  }
}
