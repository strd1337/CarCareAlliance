using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories;
using CarCareAlliance.Application.WorkSchedules.Common;
using CarCareAlliance.Domain.WorkScheduleAggregate;
using CarCareAlliance.Domain.WorkScheduleAggregate.ValueObjects;
using ErrorOr;
using CarCareAlliance.Domain.Common.Errors;
using CarCareAlliance.Domain.MechanicAggregate.ValueObjects;
using CarCareAlliance.Domain.ServicePartnerAggregate.ValueObjects;
using CarCareAlliance.Domain.ServicePartnerAggregate;
using CarCareAlliance.Domain.MechanicAggregate;

namespace CarCareAlliance.Application.WorkSchedules.Commands.Update
{
    public class UpdateWorkSchedulesByOwnerIdHandler(IUnitOfWork unitOfWork) :
            ICommandHandler<UpdateWorkSchedulesByOwnerIdCommand, UpdateWorkSchedulesByOwnerIdResult>
    {
        public async Task<ErrorOr<UpdateWorkSchedulesByOwnerIdResult>> Handle(
            UpdateWorkSchedulesByOwnerIdCommand command,
            CancellationToken cancellationToken)
        {
            var servicePartner = await unitOfWork
                .GetRepository<ServicePartner, ServicePartnerId>()
                .GetByIdAsync(
                    ServicePartnerId.Create(command.OwnerId),
                    cancellationToken);

            var mechanic = await unitOfWork
                .GetRepository<MechanicProfile, MechanicProfileId>()
                .GetByIdAsync(
                    MechanicProfileId.Create(command.OwnerId),
                    cancellationToken);

            if (servicePartner is null && mechanic is null)
            {
                return Errors.WorkSchedule.OwnerNotFound;
            }

            var workSchedulesToUpdate = unitOfWork
                .GetRepository<WorkSchedule, WorkScheduleId>()
                .GetWhere(ws => ws.OwnerId == command.OwnerId)
                .ToList();

            if (workSchedulesToUpdate.Count == 0)
            {
                return Errors.WorkSchedule.OwnerWorkSchedulesNotFound;
            }

            workSchedulesToUpdate.ForEach(workScheduleToUpdate =>
            {
                var workSchedule = command.WorkSchedules
                    .FirstOrDefault(ws => ws.WorkScheduleId == workScheduleToUpdate.Id.Value);

                if (workSchedule is not null)
                {
                    var breakTimes = workSchedule.BreakTimes
                        .Select(x => BreakTime.CreateNew(x.StartTime, x.EndTime))
                        .ToList();

                    workScheduleToUpdate.Update(
                        workSchedule.DayOfWeek,
                        workSchedule.StartTime,
                        workSchedule.EndTime,
                        breakTimes);
                }
            });

            await unitOfWork
               .GetRepository<WorkSchedule, WorkScheduleId>()
               .UpdateAsync(workSchedulesToUpdate);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new UpdateWorkSchedulesByOwnerIdResult();
        }
    }
}
