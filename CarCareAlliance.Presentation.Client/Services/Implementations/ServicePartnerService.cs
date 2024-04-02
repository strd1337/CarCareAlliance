﻿using CarCareAlliance.Presentation.Client.Common.Constants;
using CarCareAlliance.Presentation.Client.Common.Convertors;
using CarCareAlliance.Presentation.Client.Models;
using CarCareAlliance.Presentation.Client.Models.ServicePartners;
using CarCareAlliance.Presentation.Client.Services.Interfaces;
using MudBlazor;
using System.Net.Http.Json;
using System.Text.Json;
using System.Web;

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

        public async Task<bool> UpdateAsync(ServicePartner servicePartner)
        {
            var model = new ServicePartnerRequest
            {
                Name = servicePartner.Name,
                Description = servicePartner.Description,
                ServiceCategories = servicePartner.ServiceCategories,
                WorkSchedules = servicePartner.WorkSchedules,
                Location = servicePartner.Location,
            };

            var jso = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            jso.Converters.Add(new DateOnlyConverter());
            jso.Converters.Add(new TimeOnlyConverter());

            var json = JsonSerializer.Serialize(model, jso);
            var content = new StringContent(json, System.Text.Encoding.UTF8, Constants.MediaType);

            var response = await httpClientFactory
                .CreateClient(Constants.Client)
                .PutAsync(Constants.ServicePartner.Api + servicePartner.ServicePartnerId, content);

            var isSuccess = await httpErrorsService.HandleExceptionResponse(response);

            if (isSuccess)
            {
                snackbar.Add(Constants.UpdateSuccessfulConfirmation(nameof(ServicePartner)), Severity.Success);
            }

            return isSuccess;
        }

        public async Task DeleteAsync(ServicePartner servicePartner)
        {
            var response = await httpClientFactory.CreateClient(Constants.Client).DeleteAsync(Constants.ServicePartner.Api + servicePartner.ServicePartnerId);
            await httpErrorsService.EnsureSuccessStatusCode(response);
            snackbar.Add(Constants.DeleteSuccessfulConfirmation(nameof(servicePartner)), Severity.Success);
        }
    }
}
