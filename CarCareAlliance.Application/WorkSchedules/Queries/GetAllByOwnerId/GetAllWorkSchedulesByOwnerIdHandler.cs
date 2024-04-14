using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories;
using CarCareAlliance.Application.WorkSchedules.Common;
using CarCareAlliance.Domain.WorkScheduleAggregate;
using CarCareAlliance.Domain.WorkScheduleAggregate.ValueObjects;
using ErrorOr;

namespace CarCareAlliance.Application.WorkSchedules.Queries.GetAllByOwnerId
{
    public class GetAllWorkSchedulesByOwnerIdHandler(
        IUnitOfWork unitOfWork) :
        IQueryHandler<GetAllWorkSchedulesByOwnerIdQuery, GetAllWorkSchedulesByOwnerIdResult>
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<ErrorOr<GetAllWorkSchedulesByOwnerIdResult>> Handle(
            GetAllWorkSchedulesByOwnerIdQuery query,
            CancellationToken cancellationToken)
        {
            var workSchedules = await unitOfWork
                .GetRepository<WorkSchedule, WorkScheduleId>()
                .GetWhereAsync(
                    x => x.OwnerId == query.OwnerId,
                    cancellationToken);

            return new GetAllWorkSchedulesByOwnerIdResult(workSchedules.ToList());
        }
    }
}