using EasyList.Business.Models.Email;
using System.Threading.Tasks;

namespace EasyList.Business.Interfaces.IServices
{
    public interface IEmailService
    {
        Task EnviarEmailConfirmacaoAsync(string enderecoEmail, string validacaoEmail);
        Task EnviarEmailAsync(MailRequest mailRequest);
    }
}
