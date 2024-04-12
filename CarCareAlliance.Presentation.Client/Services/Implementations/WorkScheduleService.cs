using CarCareAlliance.Presentation.Client.Common.Constants;
using CarCareAlliance.Presentation.Client.Models.WorkSchedules;
using CarCareAlliance.Presentation.Client.Services.Interfaces;
using MudBlazor;
using System.Text.Json;

namespace CarCareAlliance.Presentation.Client.Services.Implementations
{
    public class WorkScheduleService(
        IHttpClientFactory httpClientFactory,
        ISnackbar snackbar,
        HttpErrorsService httpErrorsService) : IWorkScheduleService
    {
        public async Task<bool> CreateAsync(WorkSchedule workSchedule)
        {
            var json = JsonSerializer.Serialize(workSchedule);
            var content = new StringContent(json, System.Text.Encoding.UTF8, Constants.MediaType);

            var response = await httpClientFactory.CreateClient(Constants.Client).PostAsync(Constants.WorkSchedule.Api, content);

            var isSuccess = await httpErrorsService.HandleExceptionResponse(response);

            if (isSuccess)
            {
                snackbar.Add(Constants.CreateSuccessfulConfirmation(nameof(WorkSchedule)), Severity.Success);
            }

            return isSuccess;
        }
    }
}
