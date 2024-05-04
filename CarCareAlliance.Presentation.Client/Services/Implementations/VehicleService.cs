using CarCareAlliance.Presentation.Client.Common.Constants;
using CarCareAlliance.Presentation.Client.Models;
using CarCareAlliance.Presentation.Client.Models.Vehicles;
using CarCareAlliance.Presentation.Client.Services.Interfaces;
using MudBlazor;
using System.Net.Http.Json;
using System.Text.Json;
using System.Web;
using Newtonsoft.Json.Linq;
using CarCareAlliance.Presentation.Client.Extensions;

namespace CarCareAlliance.Presentation.Client.Services.Implementations
{
    public class VehicleService(
        IHttpClientFactory httpClientFactory,
        HttpErrorsService httpErrorsService,
        ISnackbar snackbar) : IVehicleService
    {
        public async Task<bool> AddAsync(Vehicle vehicle)
        {
            var json = JsonSerializer.Serialize(vehicle);
            var content = new StringContent(json, System.Text.Encoding.UTF8, Constants.MediaType);

            var response = await httpClientFactory
                .CreateClient(Constants.Client)
                .PostAsync(Constants.Vehicle.Api, content);

            var isSuccess = await httpErrorsService.HandleExceptionResponse(response);

            if (isSuccess)
            {
                snackbar.Add(Constants.CreateSuccessfulConfirmation(nameof(Vehicle)), Severity.Success);
            }

            return isSuccess;
        }

        public async Task<PaginatedList<Vehicle>> GetAllByUserIdAsync(string userId, QueryParams queryParams)
        {
            string url = Constants.Vehicle.ApiByUser + userId + "?";
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            queryString.Add(nameof(queryParams.PageNumber), queryParams.PageNumber.ToString());
            queryString.Add(nameof(queryParams.PageSize), queryParams.PageSize.ToString());

            var response = await httpClientFactory.CreateClient(Constants.Client).GetAsync(url + queryString);

            var isSuccess = await httpErrorsService.HandleExceptionResponse(response);

            var list = new PaginatedList<Vehicle>();

            if (isSuccess)
            {
                list = await response.Content.ReadFromJsonAsync<PaginatedList<Vehicle>>();
            }

            return list!;
        }

        public async Task<Vehicle?> GetByVinAsync(string vin)
        {
            string url = Constants.Vehicle.ApiDetailsByVin + vin + "?";

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString.Add("apikey", Constants.Vehicle.DetailsByVinApiKey);

            var httpClient = httpClientFactory.CreateClient(Constants.Vehicle.ClientDetailsByVin);
            httpClient.BaseAddress = new Uri(Constants.Vehicle.BaseAddressDetailsByVin);

            var response = await httpClient.GetAsync(url + queryString);

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(jsonResponse);

            Vehicle? vehicle = jsonObject?.ToVehicle(vin);

            return vehicle;
        }
    }
}
