using CarCareAlliance.Presentation.Client.Common.Constants;
using CarCareAlliance.Presentation.Client.Models;
using CarCareAlliance.Presentation.Client.Models.ServicePartners;
using CarCareAlliance.Presentation.Client.Models.Tickets;
using CarCareAlliance.Presentation.Client.Services.Interfaces;
using MudBlazor;
using System.Net.Http.Json;
using System.Text.Json;
using System.Web;

namespace CarCareAlliance.Presentation.Client.Services.Implementations
{
    public class TicketService(
        IHttpClientFactory httpClientFactory,
        HttpErrorsService httpErrorsService,
        ISnackbar snackbar) : ITicketService
    {
        public async Task<bool> CreateAsync(CreateTicketRequest ticket)
        {
            var json = JsonSerializer.Serialize(ticket);
            var content = new StringContent(json, System.Text.Encoding.UTF8, Constants.MediaType);

            var response = await httpClientFactory.CreateClient(Constants.Client).PostAsync(Constants.Ticket.Api, content);

            var isSuccess = await httpErrorsService.HandleExceptionResponse(response);

            if (isSuccess)
            {
                snackbar.Add(Constants.CreateSuccessfulConfirmation(nameof(Ticket)), Severity.Success);
            }

            return isSuccess;
        }

        public async Task<PaginatedList<Ticket>> GetAllByFiltersAndUserIdAsync(
            Guid userId, 
            QueryParams queryParams)
        {
            string url = Constants.Ticket.Api + userId + Constants.Ticket.SearchApi;
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            queryString.Add(nameof(queryParams.PageNumber), queryParams.PageNumber.ToString());
            queryString.Add(nameof(queryParams.PageSize), queryParams.PageSize.ToString());

            var response = await httpClientFactory.CreateClient(Constants.Client).GetAsync(url + queryString);

            var isSuccess = await httpErrorsService.HandleExceptionResponse(response);

            var list = new PaginatedList<Ticket>();

            if (isSuccess)
            {
                list = await response.Content.ReadFromJsonAsync<PaginatedList<Ticket>>();
            }

            return list!;
        }

        public async Task<bool> UpdateAsync(UpdateTicketRequest ticket)
        {
            var json = JsonSerializer.Serialize(ticket);
            var content = new StringContent(json, System.Text.Encoding.UTF8, Constants.MediaType);

            var response = await httpClientFactory
                .CreateClient(Constants.Client)
                .PutAsync(Constants.Ticket.Api + ticket.TicketId, content);

            var isSuccess = await httpErrorsService.HandleExceptionResponse(response);

            if (isSuccess)
            {
                snackbar.Add(Constants.UpdateSuccessfulConfirmation(nameof(Ticket)), Severity.Success);
            }

            return isSuccess;
        }
    }
}
