using CarCareAlliance.Application.Users.Queries;
using CarCareAlliance.Contracts.Users.GetProfile;
using CarCareAlliance.Presentation.Controllers.Common;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarCareAlliance.Presentation.Controllers.UserProfile
{
    [Authorize]
    [Route("users")]
    public class UserController(
        IMediator mediator,
        IMapper mapper) : ApiController
    {
        private readonly IMediator mediator = mediator;
        private readonly IMapper mapper = mapper;

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetProfile(
            Guid userId,
            CancellationToken cancellationToken)
        {
            var request = new UserProfileGetRequest(userId);

            var query = mapper.Map<UserProfileGetQuery>(request);

            var profileResult = await mediator
                .Send(query, cancellationToken);

            return profileResult.Match(
                profileResult => Ok(
                    mapper.Map<UserProfileGetResponse>(profileResult)),
                errors => Problem(errors));
        }
    }
}