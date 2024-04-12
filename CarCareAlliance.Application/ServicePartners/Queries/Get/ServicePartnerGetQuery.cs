using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.ServicePartners.Common;

namespace CarCareAlliance.Application.ServicePartners.Queries.Get
{
    public record ServicePartnerGetQuery(
        Guid ServicePartnerId) : IQuery<ServicePartnerResult>;
}