﻿using CarCareAlliance.Presentation.Client.Models;
using CarCareAlliance.Presentation.Client.Models.ServicePartners;

namespace CarCareAlliance.Presentation.Client.Services.Interfaces
{
    public interface IServicePartnerService
    {
        Task<PaginatedList<ServicePartner>> GetAllByFiltersAsync(
            QueryParams queryParams);

        Task<bool> UpdateAsync(ServicePartner servicePartner);
        Task DeleteAsync(ServicePartner servicePartner);
        Task<bool> CreateAsync(ServicePartner servicePartner);

        Task<GetAllServiceCategoriesResponse> GetAllServiecCategoriesAsync();
        Task<GetAllServicePartnersResponse> GetAllAsync();
    }
}
