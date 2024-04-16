using MimeKit;

namespace CarCareAlliance.Presentation.Client.Services.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendAsync(MimeMessage email);
    }
}
