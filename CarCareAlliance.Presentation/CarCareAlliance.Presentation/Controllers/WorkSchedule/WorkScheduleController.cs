using CarCareAlliance.Application.WorkSchedules.Commands.Add;
using CarCareAlliance.Application.WorkSchedules.Commands.Delete;
using CarCareAlliance.Application.WorkSchedules.Queries.GetByOwnerId;
using CarCareAlliance.Contracts.WorkSchedules.AddWorkSchedule;
using CarCareAlliance.Contracts.WorkSchedules.Delete;
using CarCareAlliance.Contracts.WorkSchedules.GetByOwnerId;
using CarCareAlliance.Presentation.Controllers.Common;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarCareAlliance.Presentation.Controllers.WorkSchedule
{
    [Route("work-schedules")]
    public class WorkScheduleController(
        IMediator mediator,
        IMapper mapper) : ApiController
    {
        private readonly IMediator mediator = mediator;
        private readonly IMapper mapper = mapper;

        //[Authorize]
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

        [HttpGet("owner/{ownerId}")]
        public async Task<IActionResult> Get(
            Guid ownerId,
            CancellationToken cancellationToken)
        {
            var request = new WorkScheduleGetByOwnerIdRequest(ownerId);

            var query = mapper.Map<WorkScheduleGetByOwnerIdQuery>(request);

            var workScheduleGetByOwnerIdResult = await mediator
                .Send(query, cancellationToken);

            return workScheduleGetByOwnerIdResult.Match(
                workScheduleGetByOwnerIdResult => Ok(
                    mapper.Map<WorkScheduleGetByOwnerIdResponse>(workScheduleGetByOwnerIdResult)),
                errors => Problem(errors));
        }

        [Authorize]
        [HttpDelete("{workScheduleId}")]
        public async Task<IActionResult> Delete(
            Guid workScheduleId,
            CancellationToken cancellationToken)
        {
            var request = new WorkScheduleDeleteRequest(workScheduleId);

            var command = mapper.Map<WorkScheduleDeleteCommand>(request);

            var workScheduleDeleteResult = await mediator
                .Send(command, cancellationToken);

            return workScheduleDeleteResult.Match(
                workScheduleDeleteResult => Ok(
                    mapper.Map<WorkScheduleDeleteResponse>(workScheduleDeleteResult)),
                errors => Problem(errors));
        }
    }
}