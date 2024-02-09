using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.ServicePartners.Common;
using CarCareAlliance.Domain.ServicePartnerAggregate.Entities;

namespace CarCareAlliance.Application.ServicePartners.Commands.Add
{
    public record ServicePartnerAddCommand(
        string Name,
        string Description,
        Guid LogoId,
        Guid WorkScheduleId,
        float Latitude,
        float Longitude,
        string Address,
        string City,
        string? State,
        string Country,
        string PostalCode,
        string LocationDescription) : ICommand<ServicePartnerAddResult>;
}