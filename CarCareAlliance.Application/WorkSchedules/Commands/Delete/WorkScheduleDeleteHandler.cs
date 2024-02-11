using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories;
using CarCareAlliance.Application.WorkSchedules.Common;
using CarCareAlliance.Domain.WorkScheduleAggregate;
using CarCareAlliance.Domain.WorkScheduleAggregate.ValueObjects;
using ErrorOr;
using CarCareAlliance.Domain.Common.Errors;

namespace CarCareAlliance.Application.WorkSchedules.Commands.Delete
{
    public class WorkScheduleDeleteHandler(
        IUnitOfWork unitOfWork) :
        ICommandHandler<WorkScheduleDeleteCommand, WorkScheduleDeleteResult>
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<ErrorOr<WorkScheduleDeleteResult>> Handle(
            WorkScheduleDeleteCommand command,
            CancellationToken cancellationToken)
        {
            var workSchedule = await unitOfWork
                .GetRepository<WorkSchedule, WorkScheduleId>()
                .GetByIdAsync(
                    WorkScheduleId.Create(command.WorkScheduleId),
                    cancellationToken);

            if (workSchedule is null)
            {
                return Errors.WorkSchedule.NotFound;
            }

            await unitOfWork
               .GetRepository<WorkSchedule, WorkScheduleId>()
               .RemoveAsync(workSchedule);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new WorkScheduleDeleteResult(
                workSchedule.Id.Value);
        }
    }
}