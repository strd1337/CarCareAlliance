using CarCareAlliance.Application.ServicePartners.Common;
using CarCareAlliance.Application.ServicePartners.Queries.GetAllCategories;
using CarCareAlliance.Contracts.ServicePartners.Common;
using CarCareAlliance.Contracts.ServicePartners.GetAllServicePartnersCategories;
using Mapster;

namespace CarCareAlliance.Presentation.Common.Mapping.ServicePartners
{
    public class ServicePartnersGetAllCategoriesMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<GetAllServicePartnersCategoriesRequest, GetAllServicePartnersCategoriesQuery>();

            config.NewConfig<GetAllServicePartnersCategoriesResult, GetAllServicePartnersCategoriesResponse>()
               .Map(dest => dest.ServiceCategories, src => src.ServiceCategories.Select(category => 
                    new ServiceCategoryDto(
                       category.Id.Value,
                       category.Name,
                       category.Description,
                       category.Services.Select(service => new ServiceDto(
                               service.Id.Value,
                               service.Name,
                               service.Description,
                               service.Price,
                               service.Duration)).ToList())).ToList());
        }
    }
}
