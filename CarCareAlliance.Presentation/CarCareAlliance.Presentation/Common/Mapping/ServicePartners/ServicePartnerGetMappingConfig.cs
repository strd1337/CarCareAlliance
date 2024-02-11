using CarCareAlliance.Application.ServicePartners.Common;
using CarCareAlliance.Contracts.ServicePartners.Common;
using CarCareAlliance.Contracts.ServicePartners.Get;
using Mapster;

namespace CarCareAlliance.Presentation.Common.Mapping.ServicePartners
{
    public class ServicePartnerGetMappingConfig
        : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ServicePartnerGetResult, ServicePartnerGetResponse>()
                .Map(dest => dest.ServicePartner, src => new ServicePartnerDto(
                    src.ServicePartner.Id.Value,
                    src.ServicePartner.Name,
                    src.ServicePartner.Description,
                    new ServicePartnerLocationDto(
                        src.ServicePartner.ServiceLocation.Latitude,
                        src.ServicePartner.ServiceLocation.Longitude,
                        src.ServicePartner.ServiceLocation.Address,
                        src.ServicePartner.ServiceLocation.City,
                        src.ServicePartner.ServiceLocation.Country,
                        src.ServicePartner.ServiceLocation.PostalCode,
                        src.ServicePartner.ServiceLocation.Description,
                        src.ServicePartner.ServiceLocation.State ?? "")));
        }
    }
}