using Microsoft.Extensions.Logging;
using System;
using System.Text.RegularExpressions;
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
            
            // Sanitiza valores para prevenir log forging
            var sanitizedEmail = SanitizeForLogging(email);
            var sanitizedUserName = SanitizeForLogging(userName);
            
            // Mascara parcialmente dados sensíveis para logs
            var maskedEmail = MaskEmail(sanitizedEmail);
            
            _logger.LogInformation("Email de confirmação enviado para: {Email}", maskedEmail);
            _logger.LogInformation("Nome de usuário: {UserName}", sanitizedUserName);
            _logger.LogInformation("Link de confirmação gerado para usuário");
            
            // Console output para fins educativos (não usar em produção)
            Console.WriteLine("=== EMAIL DE CONFIRMAÇÃO ===");
            Console.WriteLine($"Para: {sanitizedEmail}");
            Console.WriteLine($"Usuário: {sanitizedUserName}");
            Console.WriteLine($"Link de confirmação: {confirmationLink}");
            Console.WriteLine("===========================");

            return Task.CompletedTask;
        }

        /// <summary>
        /// Sanitiza string removendo caracteres de controle para prevenir log forging
        /// </summary>
        private static string SanitizeForLogging(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            // Remove caracteres de controle (newlines, tabs, etc) que poderiam forjar logs
            return Regex.Replace(input, @"[\r\n\t]", "");
        }

        /// <summary>
        /// Mascara parcialmente o email para logs de produção
        /// </summary>
        private static string MaskEmail(string email)
        {
            if (string.IsNullOrEmpty(email) || !email.Contains("@"))
                return "***";

            var parts = email.Split('@');
            var localPart = parts[0];
            var domain = parts[1];

            if (localPart.Length <= 2)
                return $"***@{domain}";

            return $"{localPart[0]}***{localPart[^1]}@{domain}";
        }
    }
}
