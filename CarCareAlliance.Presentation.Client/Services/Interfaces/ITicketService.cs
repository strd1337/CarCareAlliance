using CarCareAlliance.Presentation.Client.Models;
using CarCareAlliance.Presentation.Client.Models.Tickets;

namespace CarCareAlliance.Presentation.Client.Services.Interfaces
{
    public interface ITicketService
    {
        Task<PaginatedList<Ticket>> GetAllByFiltersAndUserIdAsync(
            Guid userId,
            QueryParams queryParams);

        Task<bool> CreateAsync(CreateTicketRequest ticket);
        Task<bool> UpdateAsync(UpdateTicketRequest ticket);
    }
}
