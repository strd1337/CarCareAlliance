using CarCareAlliance.Presentation.Client.Common.Constants;
using CarCareAlliance.Presentation.Client.Models.Users;
using CarCareAlliance.Presentation.Client.Services.Interfaces;
using MudBlazor;
using System.Net.Http.Json;
using System.Text.Json;

namespace CarCareAlliance.Presentation.Client.Services.Implementations
{
    public class UserService(
        IHttpClientFactory httpClientFactory,
        HttpErrorsService httpErrorsService,
        ISnackbar snackbar) : IUserService
    {
        public async Task<User> GetAsync(string userId)
        {
            var response = await httpClientFactory
                .CreateClient(Constants.Client)
                .GetAsync(Constants.User.Api + userId);

            await httpErrorsService.HandleExceptionResponse(response);

            var user = await response.Content.ReadFromJsonAsync<User>();

            return user!;
        }

        public async Task<bool> UpdateAsync(User user)
        {
            var json = JsonSerializer.Serialize(user);
            var content = new StringContent(json, System.Text.Encoding.UTF8, Constants.MediaType);

            var response = await httpClientFactory
                .CreateClient(Constants.Client)
                .PutAsync(Constants.User.Api + user.UserId, content);

            var isSuccess = await httpErrorsService.HandleExceptionResponse(response);

            if (isSuccess)
            {
                snackbar.Add(Constants.UpdateSuccessfulConfirmation(nameof(User)), Severity.Success);
            }

            return isSuccess;
        }
    }
}
