using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace EasyList.Api.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public Task SendEmailConfirmationAsync(string email, string userName, string confirmationLink)
        {
            // Para fins educativos, apenas loga a informação
            // Em produção, implemente um serviço real de envio de email (SendGrid, SMTP, etc.)
            _logger.LogInformation($"Email de confirmação enviado para: {email}");
            _logger.LogInformation($"Nome de usuário: {userName}");
            _logger.LogInformation($"Link de confirmação: {confirmationLink}");
            
            Console.WriteLine("=== EMAIL DE CONFIRMAÇÃO ===");
            Console.WriteLine($"Para: {email}");
            Console.WriteLine($"Usuário: {userName}");
            Console.WriteLine($"Link de confirmação: {confirmationLink}");
            Console.WriteLine("===========================");

            return Task.CompletedTask;
        }
    }
}
