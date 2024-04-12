using CarCareAlliance.Application.ServicePartners.Commands.Add;
using CarCareAlliance.Application.ServicePartners.Common;
using CarCareAlliance.Contracts.ServicePartners.AddServicePartner;
using Mapster;

namespace CarCareAlliance.Presentation.Common.Mapping.ServicePartners
{
    public class ServicePartnerAddMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ServicePartnerAddRequest, ServicePartnerAddCommand>();

            config.NewConfig<ServicePartnerAddResult, ServicePartnerAddResponse>();
        }
    }
}