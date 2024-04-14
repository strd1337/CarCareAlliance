using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.WorkSchedules.Common;
using CarCareAlliance.Contracts.WorkSchedules.Common;

namespace CarCareAlliance.Application.WorkSchedules.Commands.Update
{
    public record UpdateWorkSchedulesByOwnerIdCommand(
        Guid OwnerId,
        ICollection<WorkScheduleDto> WorkSchedules) : ICommand<UpdateWorkSchedulesByOwnerIdResult>
    {
        public UpdateWorkSchedulesByOwnerIdCommand SetOwnerId(Guid OwnerId)
            => this with { OwnerId = OwnerId };
    }
}
