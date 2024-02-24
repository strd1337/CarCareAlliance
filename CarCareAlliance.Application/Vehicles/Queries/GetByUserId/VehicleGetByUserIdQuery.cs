using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Vehicles.Common;

namespace CarCareAlliance.Application.Vehicles.Queries.GetByUserId
{
    public record VehicleGetByUserIdQuery(
        Guid UserId) : IQuery<VehicleGetByUserIdResult>;
}
