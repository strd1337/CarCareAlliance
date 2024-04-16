using CarCareAlliance.Presentation.Client.Models.Email;
using MudBlazor;
using CarCareAlliance.Presentation.Client.Common.Constants;
using MimeKit;
using CarCareAlliance.Presentation.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace CarCareAlliance.Presentation.Client.Components.Pages.Contacts
{
    public partial class Contacts
    {
        private EmailMessageModel EmailMessage { get; set; } = new();
        private bool isValid;

        [Inject]
        public IEmailService? EmailService { get; set; }

        private async Task SubmitAsync()
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("", EmailMessage.From));
            email.To.Add(new MailboxAddress("", Constants.Email.ContactUsSubmitted.To));
            email.Subject = Constants.Email.ContactUsSubmitted.Subject;

            email.Body = new TextPart("plain")
            {
                Text = Constants.Email.ContactUsSubmitted
                    .GetBody(EmailMessage.Name, EmailMessage.Message)
            };

            var isSuccess = await EmailService!.SendAsync(email);

            if (isSuccess)
            {
                EmailMessage.Message = string.Empty;
                EmailMessage.Name = string.Empty;
                EmailMessage.From = string.Empty;
            }
        }
    }
}
