using CarCareAlliance.Presentation.Client.Common.Constants;
using CarCareAlliance.Presentation.Client.Common.Convertors;
using CarCareAlliance.Presentation.Client.Models;
using CarCareAlliance.Presentation.Client.Models.ServicePartners;
using CarCareAlliance.Presentation.Client.Services.Interfaces;
using MudBlazor;
using System.Net.Http.Json;
using System.Text.Json;
using System.Web;
using static System.Net.WebRequestMethods;

namespace CarCareAlliance.Presentation.Client.Services.Implementations
{
    public class ServicePartnerService(
        IHttpClientFactory httpClientFactory, 
        HttpErrorsService httpErrorsService, 
        ISnackbar snackbar) : IServicePartnerService
    {
        public async Task<PaginatedList<ServicePartner>> GetAllByFiltersAsync(
            QueryParams queryParams)
        {
            string url = Constants.ServicePartner.SearchApi;
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            if (queryParams.SearchKey is not null)
            {
                queryString.Add(nameof(queryParams.SearchKey), queryParams.SearchKey);
            }

            queryString.Add(nameof(queryParams.PageNumber), queryParams.PageNumber.ToString());
            queryString.Add(nameof(queryParams.PageSize), queryParams.PageSize.ToString());

            var response = await httpClientFactory.CreateClient(Constants.Client).GetAsync(url + queryString);
           
            await httpErrorsService.EnsureSuccessStatusCode(response);

            var jso = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            jso.Converters.Add(new DateOnlyConverter());
            jso.Converters.Add(new TimeOnlyConverter());

            var servicePartnersResponse = await response.Content.ReadFromJsonAsync<PaginatedList<ServicePartner>>(jso);

            return servicePartnersResponse!;
        }
    }
}
