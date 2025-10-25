using EasyList.Api.v1.Controllers;
using EasyList.Business.Interfaces.IRepository;
using EasyList.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyList.Api.V1.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class LogController : ApiControllerBase
    {
        private readonly ILogRepository _logRepository;

        public LogController(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        /// <summary>
        /// Retorna logs cadastrados no banco
        /// </summary>  
        /// <param name="pagina"> Página </param>
        /// <param name="tamanho">Quantidade de registros por página </param>   
        /// <response code="200"> Sucesso </response>
        /// <response code="400"> Requisição Inválida </response>
        /// <response code="404"> Não Encontrado </response>
        [HttpGet("{pagina}/{tamanho}")]
        public async Task<ActionResult<IEnumerable<LogEntry>>> GetLogs(int? pagina, int tamanho)
        {
            if (tamanho <= 0 || tamanho > 100)
                return BadRequest("Tamanho deve ser entre 1 e 100");

            var logs = await _logRepository.ObterTodosPorPaginacao(pagina, tamanho);

            if (logs == null)
                return NotFound();

            return Ok(logs);
        }

        /// <summary>
        /// Retorna log por Id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200"> Sucesso </response>
        /// <response code="404"> Não Encontrado </response>
        [HttpGet("{id}")]
        public async Task<ActionResult<LogEntry>> GetLog(Guid id)
        {
            var log = await _logRepository.ObterPorId(id);

            if (log == null)
                return NotFound();

            return Ok(log);
        }

        /// <summary>
        /// Retorna logs por período
        /// </summary>
        /// <param name="dataInicio">Data início (formato: yyyy-MM-dd)</param>
        /// <param name="dataFim">Data fim (formato: yyyy-MM-dd)</param>
        /// <response code="200"> Sucesso </response>
        /// <response code="400"> Requisição Inválida </response>
        [HttpGet("periodo/{dataInicio}/{dataFim}")]
        public async Task<ActionResult<IEnumerable<LogEntry>>> GetLogsPorPeriodo(DateTime dataInicio, DateTime dataFim)
        {
            if (dataInicio > dataFim)
                return BadRequest("Data início não pode ser maior que data fim");

            var logs = await _logRepository.ObterLogsPorPeriodo(dataInicio, dataFim);

            return Ok(logs);
        }

        /// <summary>
        /// Retorna logs por nível (Information, Warning, Error)
        /// </summary>
        /// <param name="level">Nível do log</param>
        /// <response code="200"> Sucesso </response>
        /// <response code="400"> Requisição Inválida </response>
        [HttpGet("nivel/{level}")]
        public async Task<ActionResult<IEnumerable<LogEntry>>> GetLogsPorNivel(string level)
        {
            var allowedLevels = new[] { "Information", "Warning", "Error" };
            if (string.IsNullOrWhiteSpace(level) || !Array.Exists(allowedLevels, l => l.Equals(level, StringComparison.OrdinalIgnoreCase)))
                return BadRequest("Nível deve ser: Information, Warning ou Error");

            var logs = await _logRepository.ObterLogsPorNivel(level);

            return Ok(logs);
        }

        /// <summary>
        /// Retorna logs de um usuário específico
        /// </summary>
        /// <param name="userName">Nome do usuário</param>
        /// <response code="200"> Sucesso </response>
        /// <response code="400"> Requisição Inválida </response>
        [HttpGet("usuario/{userName}")]
        public async Task<ActionResult<IEnumerable<LogEntry>>> GetLogsPorUsuario(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return BadRequest("Nome de usuário não pode ser vazio");

            // Sanitize username to prevent potential issues
            userName = userName.Trim();

            var logs = await _logRepository.ObterLogsPorUsuario(userName);

            return Ok(logs);
        }
    }
}
