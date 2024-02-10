using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.ServicePartners.Common;

namespace CarCareAlliance.Application.ServicePartners.Queries.GetAll
{
    public record ServicePartnerGetAllQuery()
        : IQuery<ServicePartnerGetAllResult>;
}
