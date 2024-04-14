using CarCareAlliance.Presentation.Client.Common.Constants;
using CarCareAlliance.Presentation.Client.Common.Convertors;
using CarCareAlliance.Presentation.Client.Models.WorkSchedules;
using CarCareAlliance.Presentation.Client.Services.Interfaces;
using MudBlazor;
using System.Net.Http.Json;
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

        public async Task<GetAllWorkSchedulesByOwnerIdResponse> GetAllByOwnerIdAsync(Guid ownerId)
        {
            var httpResponse = await httpClientFactory
                .CreateClient(Constants.Client)
                .GetAsync(Constants.WorkSchedule.OwnersApi + ownerId);

            await httpErrorsService.HandleExceptionResponse(httpResponse);

            var jso = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            jso.Converters.Add(new DateOnlyConverter());
            jso.Converters.Add(new TimeOnlyConverter());

            var response = await httpResponse.Content
                .ReadFromJsonAsync<GetAllWorkSchedulesByOwnerIdResponse>(jso);

            return response!;
        }

        public async Task<bool> UpdateByOwnerIdAsync(
            Guid ownerId,
            ICollection<WorkSchedule> workSchedules)
        {
            var request = new UpdateWorkSchedulesByOwnerIdRequest
            {
                WorkSchedules = workSchedules
            };

            var jso = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            jso.Converters.Add(new DateOnlyConverter());
            jso.Converters.Add(new TimeOnlyConverter());

            var json = JsonSerializer.Serialize(request, jso);
            var content = new StringContent(json, System.Text.Encoding.UTF8, Constants.MediaType);

            var response = await httpClientFactory
                .CreateClient(Constants.Client)
                .PutAsync(Constants.WorkSchedule.OwnersApi + ownerId, content);

            var isSuccess = await httpErrorsService.HandleExceptionResponse(response);

            if (isSuccess)
            {
                snackbar.Add(Constants.UpdateSuccessfulConfirmation(nameof(WorkSchedule)), Severity.Success);
            }

            return isSuccess;
        }
    }
}
