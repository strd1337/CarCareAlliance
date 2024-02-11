using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.WorkSchedules.Common;

namespace CarCareAlliance.Application.WorkSchedules.Commands.Delete
{
    public record WorkScheduleDeleteCommand(
        Guid WorkScheduleId) : ICommand<WorkScheduleDeleteResult>;
}