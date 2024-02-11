using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.ServicePartners.Common;

namespace CarCareAlliance.Application.ServicePartners.Commands.Add
{
    public record ServicePartnerAddCommand(
        string Name,
        string Description,
        float Latitude,
        float Longitude,
        string Address,
        string City,
        string Country,
        string PostalCode,
        string LocationDescription,
        string? State = null) : ICommand<ServicePartnerAddResult>;
}