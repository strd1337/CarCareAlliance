using CarCareAlliance.Presentation.Client.Common.Auth;
using CarCareAlliance.Presentation.Client.Common.Exceptions;
using CarCareAlliance.Presentation.Client.Services;
using MudBlazor;
using System.Text.Json;

namespace CarCareAlliance.Presentation.Client.Handlers
{
    public class HttpRequestErrorHandler(ISnackbar snackbar, LoadingService loadingService) : DelegatingHandler
    {
        private readonly ISnackbar _snackbar = snackbar;
        private readonly LoadingService _loadingService = loadingService;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await base.SendAsync(request, cancellationToken);
                if (!response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    var error = await JsonSerializer.DeserializeAsync<HttpErrorResponse>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    throw new CustomHttpRequestException(error.Title, response.StatusCode);
                }
                return response;
            }
            catch (CustomHttpRequestException ex)
            {
                HandleException(ex);
                return default!;
            }
        }

        private void HandleException(CustomHttpRequestException ex)
        {
            if (ex is not null)
            {
                var message = string.Join(Environment.NewLine, ex.ErrorMessages);
                _loadingService.Hide();
                _snackbar.Add(message, Severity.Error);
            }
        }
    }
}
