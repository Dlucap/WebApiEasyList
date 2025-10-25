using System.Threading.Tasks;

namespace EasyList.Api.Services
{
    public interface IEmailService
    {
        Task SendEmailConfirmationAsync(string email, string userName, string confirmationLink);
    }
}
