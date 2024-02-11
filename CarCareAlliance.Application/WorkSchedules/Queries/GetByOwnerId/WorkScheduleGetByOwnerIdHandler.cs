using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories;
using CarCareAlliance.Application.WorkSchedules.Common;
using CarCareAlliance.Domain.WorkScheduleAggregate;
using CarCareAlliance.Domain.WorkScheduleAggregate.ValueObjects;
using ErrorOr;
using CarCareAlliance.Domain.Common.Errors;

namespace CarCareAlliance.Application.WorkSchedules.Queries.GetByOwnerId
{
    public class WorkScheduleGetByOwnerIdHandler(
        IUnitOfWork unitOfWork) :
        IQueryHandler<WorkScheduleGetByOwnerIdQuery, WorkScheduleGetByOwnerIdResult>
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<ErrorOr<WorkScheduleGetByOwnerIdResult>> Handle(
            WorkScheduleGetByOwnerIdQuery query,
            CancellationToken cancellationToken)
        {
            var workSchedule = await unitOfWork
                .GetRepository<WorkSchedule, WorkScheduleId>()
                .FirstOrDefaultAsync(
                    x => x.OwnerId == query.OwnerId,
                    cancellationToken);

            if (workSchedule is null)
            {
                return Errors.WorkSchedule.NotFound;
            }

            return new WorkScheduleGetByOwnerIdResult(workSchedule);
        }
    }
}