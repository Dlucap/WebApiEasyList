using EasyList.Business.Enum;
using EasyList.Business.Interfaces.IServices;
using EasyList.Business.Models;
using EasyList.Business.Models.Email;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;
using MimeKit;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyList.Business.Services
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _mailSettings;
        private readonly FeatureManagement _envioEmail;
        private readonly IFeatureManager _featureManager;
       // private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<MailSettings> mailSettings,
                            IOptions<FeatureManagement> envioEmail,
                           // ILogger<EmailService> logger,
                            IFeatureManager featureManager)
        {
            _mailSettings = mailSettings.Value;
            _envioEmail = envioEmail.Value;
            _featureManager = featureManager;
           // _logger = logger;
        }

        public async Task EnviarEmailConfirmacaoAsync(string enderecoEmail,string validacaoEmail)
        {
            var deveEnviarEmailConfirmacao = await _featureManager.IsEnabledAsync(nameof(_envioEmail.EnviaEmailConfirmacao));
            if (deveEnviarEmailConfirmacao)  
            {                
                var email = new MailRequest()
                { 
                    ToEmail = enderecoEmail, 
                    Body = $"Please confirm your account by visiting this URL {validacaoEmail}",
                    Subject = "Email confirmação de cadastro"
                };
                await EnviarEmailAsync(email);
            }
        }

        public async Task EnviarEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));

            if (!string.IsNullOrEmpty(mailRequest.Cc))
                email.Cc.Add(MailboxAddress.Parse(mailRequest.Cc));

            if (!string.IsNullOrEmpty(mailRequest.Bcc))
                email.Bcc.Add(MailboxAddress.Parse(mailRequest.Bcc));

            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }

            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            var smtp = new SmtpClient();
            try
            {
                //_logger.LogInformation($@"Iniciando envio de emai. Hora início: {DateTime.UtcNow}");
                //https://stackoverflow.com/questions/72543208/how-to-use-mailkit-with-google-after-may-30-2022
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.SslOnConnect);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);

            }
            catch (Exception ex)
            {
//                _logger.LogError($@"{Environment.NewLine}
//Ocorreu um erro ao realizar o envio de email.{Environment.NewLine} 
//Remetente: {email.From}
//Destinatário: {email.To.Select(e => e.Name)}{Environment.NewLine}
//Corpo do email: {email.Body}{Environment.NewLine}
//Erro: {ex.Message}");

                throw new Exception(ex.Message);
            }
            finally
            {
                //_logger.LogInformation($@"Fim envio de emai. Hora término: {DateTime.UtcNow}");
                smtp.Disconnect(true);
                smtp.Dispose();
            }
        }

        

    }
}
