using CarCareAlliance.Application.ServicePartners.Common;
using CarCareAlliance.Application.ServicePartners.Queries.GetAll;
using CarCareAlliance.Contracts.ServicePartners.Common;
using CarCareAlliance.Contracts.ServicePartners.GetAll;
using CarCareAlliance.Contracts.WorkSchedules.Common;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;
using Mapster;

namespace CarCareAlliance.Presentation.Common.Mapping.ServicePartners
{
    public class ServicePartnerGetAllMappingConfig
        : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ServicePartnerGetAllRequest, ServicePartnerGetAllQuery>();

            config.NewConfig<ServicePartnerGetAllResult, ServicePartnerGetAllResponse>()
                .Map(dest => dest.ServicePartners, src =>
                    src.ServicePartners.Select(result => new ServicePartnerDto(
                        result.ServicePartner.Id.Value,
                        result.ServicePartner.Name,
                        result.ServicePartner.Description,
                        result.ServicePartner.ServiceCategories.Select(category => new ServiceCategoryDto(
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
                            result.ServicePartner.ServiceLocation.Id.Value,
                            result.ServicePartner.ServiceLocation.Latitude,
                            result.ServicePartner.ServiceLocation.Longitude,
                            result.ServicePartner.ServiceLocation.Address,
                            result.ServicePartner.ServiceLocation.City,
                            result.ServicePartner.ServiceLocation.Country,
                            result.ServicePartner.ServiceLocation.PostalCode,
                            result.ServicePartner.ServiceLocation.Description,
                            result.ServicePartner.ServiceLocation.State ?? ""),
                        result.Mechanics.Select(mechanic => MechanicDtoFactory.CreateMechanicDto(
                            mechanic,
                            result.MechanicProfiles
                                .FirstOrDefault(
                                    profile => UserProfileId.Create(profile.Id.Value) == UserProfileId.Create(mechanic.UserProfileId.Value))!))
                                .ToList(),
                        result.WorkSchedules.Select(workSchedule => new WorkScheduleDto(
                            workSchedule.Id.Value,
                            workSchedule.DayOfWeek,
                            workSchedule.StartTime,
                            workSchedule.EndTime,
                            workSchedule.OwnerId,
                            workSchedule.Weekends.ToList(),
                            workSchedule.BreakTimes.Select(breakTime => new BreakTimeDto(
                                breakTime.StartTime,
                                breakTime.EndTime)).ToList())).ToList())));
        }
    }
}