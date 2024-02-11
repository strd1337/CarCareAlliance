using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.WorkSchedules.Common;

namespace CarCareAlliance.Application.WorkSchedules.Queries.GetByOwnerId
{
    public record WorkScheduleGetByOwnerIdQuery(
        Guid OwnerId) : IQuery<WorkScheduleGetByOwnerIdResult>;
}