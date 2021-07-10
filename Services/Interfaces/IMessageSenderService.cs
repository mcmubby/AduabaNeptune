using System.Threading.Tasks;
namespace AduabaNeptune.Services
{
    public interface IMessageSenderService
    {
        Task SendPasswordResetTokenSms(string phoneNumber, int customerId);
        Task SendPasswordResetTokenEmail(string emailAddress, int customerId);
        void SendOrderComfirmationEmail(string emailAddress);
        void SendOrderConfirmationSms(string phoneNumber);
    }
}