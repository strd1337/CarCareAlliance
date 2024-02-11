using CarCareAlliance.Application.WorkSchedules.Commands.Add;
using CarCareAlliance.Contracts.WorkSchedules.AddWorkSchedule;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;
using CarCareAlliance.Infrastructure.Persistance.Repositories.Auth.Roles;
using CarCareAlliance.Presentation.Controllers.Common;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarCareAlliance.Presentation.Controllers.WorkSchedule
{
    [Authorize]
    [Route("work-schedules")]
    public class WorkScheduleController(
        IMediator mediator,
        IMapper mapper) : ApiController
    {
        private readonly IMediator mediator = mediator;
        private readonly IMapper mapper = mapper;

        [HasRole(RoleType.Admin)]
        [HttpPost]
        public async Task<IActionResult> AddWorkSchedule(
            WorkScheduleAddRequest request,
            CancellationToken cancellationToken)
        {
            var command = mapper.Map<WorkScheduleAddCommand>(request);

            var workScheduleAddResult = await mediator
                .Send(command, cancellationToken);

            return workScheduleAddResult.Match(
                workScheduleAddResult => Ok(
                    mapper.Map<WorkScheduleAddResponse>(workScheduleAddResult)),
                errors => Problem(errors));
        }
    }
}