using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;
using System.Net;
using CarCareAlliance.Presentation.Client.Common.Exceptions;
using CarCareAlliance.Presentation.Client.Common.Auth;

namespace CarCareAlliance.Presentation.Client.Services
{
    public class HttpErrorsService(NavigationManager navManager, LoadingService loadingService, ISnackbar snackbar)
    {
        private readonly NavigationManager _navManager = navManager;
        private readonly ISnackbar _snackbar = snackbar;
        private readonly LoadingService _loadingService = loadingService;

        public async Task EnsureSuccessStatusCode(HttpResponseMessage response)
        {
            string message = string.Empty;
            if (!response.IsSuccessStatusCode)
            {
                var statusCode = response.StatusCode;
                switch (statusCode)
                {
                    case HttpStatusCode.NotFound:
                        _navManager.NavigateTo("/404");
                        message = "The requested resorce was not found.";
                        throw new CustomHttpRequestException(message);
                    default:
                        var errorResponse = await response.Content.ReadFromJsonAsync<HttpErrorResponse>();
                        var ex = new CustomHttpRequestException(errorResponse!.Title, statusCode);
                        if (errorResponse.Errors is not null)
                        {
                            ex.ErrorMessages.AddRange(errorResponse.Errors.Select(key => string.Join('\n', key.Value)));
                        }
                        throw ex;
                }
            }
        }

        public async Task<bool> HandleExceptionResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var statusCode = response.StatusCode;
                var errorResponse = await response.Content.ReadFromJsonAsync<HttpErrorResponse>();
                var errorsList = errorResponse!.Errors is not null ? errorResponse.Errors.Select(key => string.Join('\n', key.Value)).ToList() : [];
                
                if (errorsList.Count == 0)
                {
                    _snackbar.Add(errorResponse.Title, Severity.Error);
                }
                
                _loadingService.Hide();
                foreach (string? error in errorsList)
                {
                    _snackbar.Add(error, Severity.Error);
                }
                return false;
            } 
            return true;
        }
    }
}
