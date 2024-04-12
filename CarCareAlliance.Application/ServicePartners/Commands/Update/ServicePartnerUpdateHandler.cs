using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories;
using CarCareAlliance.Application.ServicePartners.Common;
using CarCareAlliance.Contracts.ServicePartners.Common;
using CarCareAlliance.Domain.Common.Errors;
using CarCareAlliance.Domain.ServicePartnerAggregate;
using CarCareAlliance.Domain.ServicePartnerAggregate.Entities;
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
                            breakTimes);
                    }
                });

                await unitOfWork
                   .GetRepository<WorkSchedule, WorkScheduleId>()
                   .UpdateAsync(workSchedulesToUpdate);
            }

            List<ServiceCategory> serviceCategoriesToUpdate = [.. servicePartner.ServiceCategories];

            List<ServiceCategoryDto> commandServiceCategories = [.. command.ServiceCategories];

            foreach (var commandCategory in commandServiceCategories)
            {
                var categoryToUpdate = serviceCategoriesToUpdate
                    .FirstOrDefault(sc => sc.Id.Value == commandCategory.ServiceCategoryId);

                if (categoryToUpdate is null)
                {
                    var newCategory = ServiceCategory.Create(
                        commandCategory.Name,
                        commandCategory.Description,
                        commandCategory.Services.Select(serviceDto =>
                            Service.Create(
                                serviceDto.Name,
                                serviceDto.Description,
                                serviceDto.Price,
                                serviceDto.Duration)
                        ).ToList()
                    );

                    servicePartner.AddServiceCategory(newCategory);
                }
                else
                {
                    var servicesToUpdate = categoryToUpdate.Services.ToList();

                    foreach (var serviceDto in commandCategory.Services)
                    {
                        var serviceToUpdate = categoryToUpdate.Services
                            .FirstOrDefault(s => s.Id.Value == serviceDto.ServiceId);

                        if (serviceToUpdate is null)
                        {
                            categoryToUpdate.AddService(
                                Service.Create(
                                    serviceDto.Name,
                                    serviceDto.Description,
                                    serviceDto.Price,
                                    serviceDto.Duration)
                            );
                        }
                        else
                        {
                            serviceToUpdate.Update(
                                serviceDto.Name,
                                serviceDto.Description,
                                serviceDto.Price,
                                serviceDto.Duration);

                            int index = servicesToUpdate.FindIndex(s => s.Id.Value == serviceDto.ServiceId);
                            if (index != -1)
                            {
                                servicesToUpdate[index] = serviceToUpdate;
                            }
                        }
                    }

                    categoryToUpdate.Update(
                        commandCategory.Name,
                        commandCategory.Description);
                }
            }

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
