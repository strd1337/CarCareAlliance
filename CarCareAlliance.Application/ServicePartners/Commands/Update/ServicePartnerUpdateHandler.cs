using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories;
using CarCareAlliance.Application.ServicePartners.Common;
using CarCareAlliance.Domain.Common.Errors;
using CarCareAlliance.Domain.ServicePartnerAggregate;
using CarCareAlliance.Domain.ServicePartnerAggregate.ValueObjects;
using CarCareAlliance.Domain.WorkScheduleAggregate;
using CarCareAlliance.Domain.WorkScheduleAggregate.ValueObjects;
using ErrorOr;

namespace CarCareAlliance.Application.ServicePartners.Commands.Update
{
    public class ServicePartnerUpdateHandler(IUnitOfWork unitOfWork) :
        ICommandHandler<ServicePartnerUpdateCommand, ServicePartnerUpdateResult>
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<ErrorOr<ServicePartnerUpdateResult>> Handle(
           ServicePartnerUpdateCommand command,
           CancellationToken cancellationToken)
        {
            var servicePartner = await unitOfWork
                .GetRepository<ServicePartner, ServicePartnerId>()
                .GetByIdAsync(ServicePartnerId.Create(command.ServicePartnerId), cancellationToken);

            if (servicePartner is null)
            {
                return Errors.ServicePartner.NotFound;
            }

            if (command.WorkSchedules.Count != 0)
            {
                var workScheduleIds = command.WorkSchedules
                    .Select(ws => WorkScheduleId.Create(ws.WorkScheduleId)).ToList();

                var workSchedulesToUpdate = unitOfWork
                    .GetRepository<WorkSchedule, WorkScheduleId>()
                    .GetWhere(ws => workScheduleIds.Contains(ws.Id))
                    .ToList();

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
                            workSchedule.OwnerId,
                            [.. workSchedule.Weekends],
                            breakTimes);
                    }
                });

                await unitOfWork
                   .GetRepository<WorkSchedule, WorkScheduleId>()
                   .UpdateAsync(workSchedulesToUpdate);
            }

            var serviceCategoriesToUpdate = servicePartner.ServiceCategories.ToList();

            serviceCategoriesToUpdate.ForEach(serviceCategoryToUpdate =>
            {
                var serviceCategory = command.ServiceCategories
                    .FirstOrDefault(sc => sc.ServiceCategoryId == serviceCategoryToUpdate.Id.Value);

                if (serviceCategory is not null)
                {
                    var servicesToUpdate = serviceCategoryToUpdate.Services.ToList();

                    servicesToUpdate.ForEach(serviceToUpdate =>
                    {
                        var service = serviceCategory.Services
                            .FirstOrDefault(s => s.ServiceId == serviceToUpdate.Id.Value);

                        if (service is not null)
                        {
                            serviceToUpdate.Update(
                                service.Name,
                                service.Description,
                                service.Price,
                                service.Duration);
                        }
                    });

                    serviceCategoryToUpdate.Update(
                        serviceCategory.Name,
                        serviceCategory.Description,
                        servicesToUpdate);
                }
            });

            servicePartner.ServiceLocation.Update(
                command.Location.Latitude,
                command.Location.Longitude,
                command.Location.Address,
                command.Location.City,
                command.Location.Country,
                command.Location.PostalCode,
                command.Location.Description,
                command.Location.State);

            servicePartner.Update(command.Name, command.Description);

            servicePartner.UpdateServiceCategories(serviceCategoriesToUpdate);

            await unitOfWork
                .GetRepository<ServicePartner, ServicePartnerId>()
                .UpdateAsync(servicePartner);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new ServicePartnerUpdateResult(
                servicePartner.Id.Value,
                servicePartner.Name);
        }
    }
}
