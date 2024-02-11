using CarCareAlliance.Application.ServicePartners.Common;
using CarCareAlliance.Application.ServicePartners.Queries.GetAll;
using CarCareAlliance.Contracts.ServicePartners.Common;
using CarCareAlliance.Contracts.ServicePartners.GetAll;
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
                .Map(dest => dest.ServicePartners, src => src.ServicePartners
                    .Select(servicePartner =>
                        new ServicePartnerDto(
                            servicePartner.Id.Value,
                            servicePartner.Name,
                            servicePartner.Description,
                            new ServicePartnerLocationDto(
                                servicePartner.ServiceLocation.Latitude,
                                servicePartner.ServiceLocation.Longitude,
                                servicePartner.ServiceLocation.Address,
                                servicePartner.ServiceLocation.City,
                                servicePartner.ServiceLocation.Country,
                                servicePartner.ServiceLocation.PostalCode,
                                servicePartner.ServiceLocation.Description,
                                servicePartner.ServiceLocation.State ?? "")
                        )).ToList());
        }
    }
}