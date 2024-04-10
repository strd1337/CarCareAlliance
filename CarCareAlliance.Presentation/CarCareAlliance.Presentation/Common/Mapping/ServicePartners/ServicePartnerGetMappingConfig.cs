using CarCareAlliance.Application.ServicePartners.Common;
using CarCareAlliance.Contracts.ServicePartners.Common;
using CarCareAlliance.Contracts.ServicePartners.Get;
using CarCareAlliance.Contracts.WorkSchedules.Common;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;
using Mapster;

namespace CarCareAlliance.Presentation.Common.Mapping.ServicePartners
{
    public class ServicePartnerGetMappingConfig
        : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ServicePartnerResult, ServicePartnerGetResponse>()
                .Map(dest => dest.ServicePartner, src => new ServicePartnerDto(
                    src.ServicePartner.Id.Value,
                    src.ServicePartner.Name,
                    src.ServicePartner.Description,
                    src.ServicePartner.ServiceCategories.Select(category => new ServiceCategoryDto(
                        category.Id.Value,
                        category.Name,
                        category.Description,
                        category.Services.Select(service => new ServiceDto(
                            service.Id.Value,
                            service.Name,
                            service.Description,
                            service.Price,
                            service.Duration)).ToList())).ToList(),
                    new ServicePartnerLocationDto(
                        src.ServicePartner.ServiceLocation.Id.Value,
                        src.ServicePartner.ServiceLocation.Latitude,
                        src.ServicePartner.ServiceLocation.Longitude,
                        src.ServicePartner.ServiceLocation.Address,
                        src.ServicePartner.ServiceLocation.City,
                        src.ServicePartner.ServiceLocation.Country,
                        src.ServicePartner.ServiceLocation.PostalCode,
                        src.ServicePartner.ServiceLocation.Description,
                        src.ServicePartner.ServiceLocation.State ?? ""),
                     src.Mechanics.Select(mechanic => MechanicDtoFactory.CreateMechanicDto(
                        mechanic,
                        src.MechanicProfiles.FirstOrDefault(profile => UserProfileId.Create(profile.Id.Value) == UserProfileId.Create(mechanic.UserProfileId.Value))!)).ToList(),
                     src.WorkSchedules.Select(workSchedule => new WorkScheduleDto(
                            workSchedule.Id.Value,
                            workSchedule.DayOfWeek,
                            workSchedule.StartTime,
                            workSchedule.EndTime,
                            workSchedule.OwnerId,
                            workSchedule.BreakTimes.Select(breakTime => new BreakTimeDto(
                                breakTime.StartTime,
                                breakTime.EndTime)).ToList())).ToList()));
        }
    }
}