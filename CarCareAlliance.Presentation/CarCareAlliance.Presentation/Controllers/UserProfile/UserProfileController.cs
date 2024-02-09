using CarCareAlliance.Application.Users.Commands;
using CarCareAlliance.Application.Users.Queries;
using CarCareAlliance.Contracts.Users.GetProfile;
using CarCareAlliance.Contracts.Users.UpdateProfile;
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

            var profileGetResult = await mediator
                .Send(query, cancellationToken);

            return profileGetResult.Match(
                profileGetResult => Ok(
                    mapper.Map<UserProfileGetResponse>(profileGetResult)),
                errors => Problem(errors));
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateProfile(
            UserProfileUpdateRequest request,
            CancellationToken cancellationToken)
        {
            var command = mapper.Map<UserProfileUpdateCommand>(request);

            var profileUpdateResult = await mediator
                .Send(command, cancellationToken);

            return profileUpdateResult.Match(
                profileUpdateResult => Ok(
                    mapper.Map<UserProfileUpdateResponse>(profileUpdateResult)),
                errors => Problem(errors));
        }
    }
}