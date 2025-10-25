using EasyList.Business.Interfaces.IRepository;
using EasyList.Business.Models;
using EasyList.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyList.Data.Repository
{
  public class LogRepository : Repository<LogEntry>, ILogRepository
  {
    public LogRepository(MeuDbContext db) : base(db) { }

    public async Task<IEnumerable<LogEntry>> ObterLogsPorPeriodo(DateTime dataInicio, DateTime dataFim)
    {
      return await Db.Set<LogEntry>()
        .AsNoTracking()
        .Where(l => l.Timestamp >= dataInicio && l.Timestamp <= dataFim)
        .OrderByDescending(l => l.Timestamp)
        .ToListAsync();
    }

    public async Task<IEnumerable<LogEntry>> ObterLogsPorNivel(string level)
    {
      return await Db.Set<LogEntry>()
        .AsNoTracking()
        .Where(l => l.Level == level)
        .OrderByDescending(l => l.Timestamp)
        .ToListAsync();
    }

    public async Task<IEnumerable<LogEntry>> ObterLogsPorUsuario(string userName)
    {
      return await Db.Set<LogEntry>()
        .AsNoTracking()
        .Where(l => l.UserName == userName)
        .OrderByDescending(l => l.Timestamp)
        .ToListAsync();
    }
  }
}
