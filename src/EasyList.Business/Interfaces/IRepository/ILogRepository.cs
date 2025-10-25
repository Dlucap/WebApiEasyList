using EasyList.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyList.Business.Interfaces.IRepository
{
  public interface ILogRepository : IRepository<LogEntry>
  {
    Task<IEnumerable<LogEntry>> ObterLogsPorPeriodo(DateTime dataInicio, DateTime dataFim);
    Task<IEnumerable<LogEntry>> ObterLogsPorNivel(string level);
    Task<IEnumerable<LogEntry>> ObterLogsPorUsuario(string userName);
  }
}
