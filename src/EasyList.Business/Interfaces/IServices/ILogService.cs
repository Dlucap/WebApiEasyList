using EasyList.Business.Models;
using System;
using System.Threading.Tasks;

namespace EasyList.Business.Interfaces.IServices
{
  public interface ILogService
  {
    Task LogInformacao(string message, string category = null, string additionalInfo = null);
    Task LogAviso(string message, string category = null, string additionalInfo = null);
    Task LogErro(string message, Exception exception = null, string category = null, string additionalInfo = null);
    Task LogRequisicaoHttp(string httpMethod, string path, int statusCode, int duration, string userId = null, string userName = null, string ipAddress = null);
    Task LogAutenticacao(string userName, bool sucesso, string ipAddress = null, string additionalInfo = null);
    Task LogOperacaoBancoDados(string operacao, string entidade, string detalhes = null);
  }
}
