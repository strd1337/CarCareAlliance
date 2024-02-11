using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories;
using CarCareAlliance.Application.WorkSchedules.Common;
using CarCareAlliance.Domain.MechanicAggregate;
using CarCareAlliance.Domain.MechanicAggregate.ValueObjects;
using CarCareAlliance.Domain.ServicePartnerAggregate;
using CarCareAlliance.Domain.ServicePartnerAggregate.ValueObjects;
using CarCareAlliance.Domain.WorkScheduleAggregate;
using CarCareAlliance.Domain.WorkScheduleAggregate.ValueObjects;
using ErrorOr;
using CarCareAlliance.Domain.Common.Errors;

namespace CarCareAlliance.Application.WorkSchedules.Commands.Add
{
    public class WorkScheduleAddHandler(
        IUnitOfWork unitOfWork) :
        ICommandHandler<WorkScheduleAddCommand, WorkScheduleAddResult>
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<ErrorOr<WorkScheduleAddResult>> Handle(
            WorkScheduleAddCommand command,
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
 
            var workSchedule = WorkSchedule.Create(
                command.DayOfWeek,
                command.StartTime,
                command.EndTime,
                command.OwnerId);

            servicePartner?.UpdateWorkSchedule(
                    WorkScheduleId.Create(workSchedule.Id.Value));

            mechanic?.UpdateWorkSchedule(
                    WorkScheduleId.Create(workSchedule.Id.Value));

            workSchedule.AddWeekends([..command.Weekends]);
            workSchedule.AddBreakTimes([..command.BreakTimes]);

            await unitOfWork
                .GetRepository<WorkSchedule, WorkScheduleId>()
                .AddAsync(workSchedule, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new WorkScheduleAddResult(workSchedule);
        }
    }
}