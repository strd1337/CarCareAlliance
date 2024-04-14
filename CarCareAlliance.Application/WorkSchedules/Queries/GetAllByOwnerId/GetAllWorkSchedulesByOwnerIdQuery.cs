using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.WorkSchedules.Common;

namespace CarCareAlliance.Application.WorkSchedules.Queries.GetAllByOwnerId
{
    public record GetAllWorkSchedulesByOwnerIdQuery(
        Guid OwnerId) : IQuery<GetAllWorkSchedulesByOwnerIdResult>;
}