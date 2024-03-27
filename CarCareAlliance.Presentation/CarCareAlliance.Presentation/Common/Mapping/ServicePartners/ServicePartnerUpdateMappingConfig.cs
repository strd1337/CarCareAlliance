using CarCareAlliance.Application.ServicePartners.Commands.Update;
using CarCareAlliance.Application.ServicePartners.Common;
using CarCareAlliance.Contracts.ServicePartners.UpdateServicePartner;
using Mapster;

namespace CarCareAlliance.Presentation.Common.Mapping.ServicePartners
{
    public class ServicePartnerUpdateMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ServicePartnerUpdateRequest, ServicePartnerUpdateCommand>();

            config.NewConfig<ServicePartnerUpdateResult, ServicePartnerUpdateResponse>();
        }
    }
}
