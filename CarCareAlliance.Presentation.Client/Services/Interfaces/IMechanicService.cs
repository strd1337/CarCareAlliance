using CarCareAlliance.Presentation.Client.Models;
using CarCareAlliance.Presentation.Client.Models.Mechanics;

namespace CarCareAlliance.Presentation.Client.Services.Interfaces
{
    public interface IMechanicService
    {
        Task<PaginatedList<MechanicProfile>> GetAllByFiltersAsync(
           QueryParams queryParams);
    }
}
