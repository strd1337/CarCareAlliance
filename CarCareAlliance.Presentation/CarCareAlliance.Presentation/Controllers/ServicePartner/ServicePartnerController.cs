using CarCareAlliance.Application.ServicePartners.Commands.Add;
using CarCareAlliance.Application.ServicePartners.Commands.Delete;
using CarCareAlliance.Application.ServicePartners.Commands.Update;
using CarCareAlliance.Application.ServicePartners.Queries.Get;
using CarCareAlliance.Application.ServicePartners.Queries.GetAll;
using CarCareAlliance.Application.ServicePartners.Queries.GetAllByFilters;
using CarCareAlliance.Contracts.Common;
using CarCareAlliance.Contracts.ServicePartners.AddServicePartner;
using CarCareAlliance.Contracts.ServicePartners.Common;
using CarCareAlliance.Contracts.ServicePartners.DeleteServicePartner;
using CarCareAlliance.Contracts.ServicePartners.Get;
using CarCareAlliance.Contracts.ServicePartners.GetAll;
using CarCareAlliance.Contracts.ServicePartners.UpdateServicePartner;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;
using CarCareAlliance.Infrastructure.Persistance.Repositories.Auth.Roles;
using CarCareAlliance.Presentation.Common.Helpers;
using CarCareAlliance.Presentation.Controllers.Common;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarCareAlliance.Presentation.Controllers.ServicePartner
{
    [Route("service-partners")]
    public class ServicePartnerController(
        IMediator mediator,
        IMapper mapper) : ApiController
    {
        private readonly IMediator mediator = mediator;
        private readonly IMapper mapper = mapper;

        [Authorize]
        [HasRole(RoleType.Admin)]
        [HttpPost]
        public async Task<IActionResult> AddServicePartner(
            ServicePartnerAddRequest request,
            CancellationToken cancellationToken)
        {
            var command = mapper.Map<ServicePartnerAddCommand>(request);
            
            var servicePartnerAddResult = await mediator
                .Send(command, cancellationToken);

            return servicePartnerAddResult.Match(
                servicePartnerAddResult => Ok(
                    mapper.Map<ServicePartnerAddResponse>(servicePartnerAddResult)),
                errors => Problem(errors));
        }

        [Authorize]
        [HasRole(RoleType.Admin)]
        [HttpDelete("{servicePartnerId}")]
        public async Task<IActionResult> DeleteServicePartner(
            Guid servicePartnerId,
            CancellationToken cancellationToken)
        {
            var request = new ServicePartnerDeleteRequest(servicePartnerId);

            var command = mapper.Map<ServicePartnerDeleteCommand>(request);

            var servicePartnerDeleteResult = await mediator
                .Send(command, cancellationToken);

            return servicePartnerDeleteResult.Match(
                servicePartnerDeleteResult => Ok(
                    mapper.Map<ServicePartnerDeleteResponse>(servicePartnerDeleteResult)),
                errors => Problem(errors));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken)
        {
            var request = new ServicePartnerGetAllRequest();

            var query = mapper.Map<ServicePartnerGetAllQuery>(request);

            var servicePartnerGetAllResult = await mediator
                .Send(query, cancellationToken);

            return servicePartnerGetAllResult.Match(
                servicePartnerGetAllResult => Ok(
                    mapper.Map<ServicePartnerGetAllResponse>(servicePartnerGetAllResult)),
                errors => Problem(errors));
        }

        [HttpGet("{servicePartnerId}")]
        public async Task<IActionResult> Get(
            Guid servicePartnerId,
            CancellationToken cancellationToken)
        {
            var request = new ServicePartnerGetRequest(servicePartnerId);

            var query = mapper.Map<ServicePartnerGetQuery>(request);

            var servicePartnerGetResult = await mediator
                .Send(query, cancellationToken);

            return servicePartnerGetResult.Match(
                servicePartnerGetResult => Ok(
                    mapper.Map<ServicePartnerGetResponse>(servicePartnerGetResult)),
                errors => Problem(errors));
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetAllByFilters(
            [FromQuery(Name = "SearchKey")] string? searchKey,
            [FromQuery] PaginationFilter filter,
            CancellationToken cancellationToken)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

            var query = new GetAllServicePartnersByFiltersQuery(searchKey ?? string.Empty)
            {
                PageNumber = validFilter.PageNumber,
                PageSize = validFilter.PageSize
            };

            var result = await mediator
                .Send(query, cancellationToken);

            return result.Match(
                result => Ok(
                    mapper.Map<PagedResponse<ServicePartnerDto>>(result)),
                errors => Problem(errors));
        }

        //[Authorize]
        //[HasRole(RoleType.Admin)]
        [HttpPut]
        public async Task<IActionResult> UpdateServicePartner(
            ServicePartnerUpdateRequest request,
            CancellationToken cancellationToken)
        {
            var command = mapper.Map<ServicePartnerUpdateCommand>(request);

            var result = await mediator
                .Send(command, cancellationToken);

            return result.Match(
                result => Ok(
                    mapper.Map<ServicePartnerUpdateResponse>(result)),
                errors => Problem(errors));
        }
    }
}