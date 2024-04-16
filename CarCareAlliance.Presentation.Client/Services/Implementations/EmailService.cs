using CarCareAlliance.Presentation.Client.Common.Constants;
using CarCareAlliance.Presentation.Client.Services.Interfaces;
using MimeKit;
using MudBlazor;

namespace CarCareAlliance.Presentation.Client.Services.Implementations
{
    public class EmailService(
        IHttpClientFactory httpClientFactory,
        HttpErrorsService httpErrorsService,
        ISnackbar snackbar) : IEmailService
    {
        public async Task<bool> SendAsync(MimeMessage email)
        {
            // TO DO: send an email
            try
            {
                snackbar.Add(Constants.Email.SendEmailSuccess, Severity.Success);

                return true;
            }
            catch (Exception ex)
            {
                snackbar.Add(Constants.Email.SendEmailFailure, Severity.Error);

                return false;
            }
        }
    }
}
