using CarCareAlliance.Presentation.Client.Services;

namespace CarCareAlliance.Presentation.Client.Handlers
{
    public class ShowLoadingHandler(LoadingService loadingService) : DelegatingHandler
    {
        private readonly LoadingService _loadingService = loadingService;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _loadingService.Show();
            var response = await base.SendAsync(request, cancellationToken);
            _loadingService.Hide();
            return response;
        }
    }
}
