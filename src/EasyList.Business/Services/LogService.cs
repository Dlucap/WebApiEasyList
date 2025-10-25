using EasyList.Business.Interfaces.IRepository;
using EasyList.Business.Interfaces.IServices;
using EasyList.Business.Models;
using Microsoft.Extensions.Logging;
using System;
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

    public async Task LogInformacao(string message, string category = null, string additionalInfo = null)
    {
      try
      {
        var logEntry = new LogEntry
        {
          Level = "Information",
          Category = category,
          Message = message,
          AdditionalInfo = additionalInfo
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
          Category = category,
          Message = message,
          AdditionalInfo = additionalInfo
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
          Category = category,
          Message = message,
          Exception = exception?.ToString(),
          AdditionalInfo = additionalInfo
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
          Message = $"{httpMethod} {path} - Status: {statusCode} - Duration: {duration}ms",
          HttpMethod = httpMethod,
          Path = path,
          StatusCode = statusCode,
          Duration = duration,
          UserId = userId,
          UserName = userName,
          IpAddress = ipAddress
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
        var logEntry = new LogEntry
        {
          Level = sucesso ? "Information" : "Warning",
          Category = "Authentication",
          Message = sucesso 
            ? $"Autenticação bem-sucedida para o usuário: {userName}"
            : $"Tentativa de autenticação falhou para o usuário: {userName}",
          UserName = userName,
          IpAddress = ipAddress,
          AdditionalInfo = additionalInfo
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
          Message = $"Operação: {operacao} - Entidade: {entidade}",
          AdditionalInfo = detalhes
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
