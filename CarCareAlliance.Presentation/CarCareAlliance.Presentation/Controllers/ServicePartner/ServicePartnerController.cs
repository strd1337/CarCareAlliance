using CarCareAlliance.Application.ServicePartners.Commands.Add;
using CarCareAlliance.Application.ServicePartners.Commands.Delete;
using CarCareAlliance.Contracts.ServicePartners.AddServicePartner;
using CarCareAlliance.Contracts.ServicePartners.DeleteServicePartner;
using CarCareAlliance.Contracts.Users.GetProfile;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;
using CarCareAlliance.Infrastructure.Persistance.Repositories.Auth.Roles;
using CarCareAlliance.Presentation.Controllers.Common;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarCareAlliance.Presentation.Controllers.ServicePartner
{
    [Authorize]
    [HasRole(RoleType.Admin)]
    [Route("service-partners")]
    public class ServicePartnerController(
        IMediator mediator,
        IMapper mapper) : ApiController
    {
        private readonly IMediator mediator = mediator;
        private readonly IMapper mapper = mapper;

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
    }
}