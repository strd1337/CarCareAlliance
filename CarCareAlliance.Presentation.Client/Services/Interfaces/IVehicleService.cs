using CarCareAlliance.Presentation.Client.Models;
using CarCareAlliance.Presentation.Client.Models.Vehicles;

namespace CarCareAlliance.Presentation.Client.Services.Interfaces
{
    public interface IVehicleService
    {
        Task<bool> AddAsync(Vehicle vehicle);
        Task<PaginatedList<Vehicle>> GetAllByUserIdAsync(string userId, QueryParams queryParams);
        Task<Vehicle?> GetByVinAsync(string vin);
    }
}
