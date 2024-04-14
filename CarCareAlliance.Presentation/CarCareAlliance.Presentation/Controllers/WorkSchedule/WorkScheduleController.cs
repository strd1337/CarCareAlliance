using CarCareAlliance.Application.WorkSchedules.Commands.Add;
using CarCareAlliance.Application.WorkSchedules.Commands.Delete;
using CarCareAlliance.Application.WorkSchedules.Commands.Update;
using CarCareAlliance.Application.WorkSchedules.Queries.GetAllByOwnerId;
using CarCareAlliance.Contracts.WorkSchedules.AddWorkSchedule;
using CarCareAlliance.Contracts.WorkSchedules.Delete;
using CarCareAlliance.Contracts.WorkSchedules.GetAllByOwnerId;
using CarCareAlliance.Contracts.WorkSchedules.UpdateByOwnerId;
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

        [HttpGet("owners/{ownerId}")]
        public async Task<IActionResult> GetAllByOwner(
            Guid ownerId,
            CancellationToken cancellationToken)
        {
            var request = new GetAllWorkSchedulesByOwnerIdRequest(ownerId);

            var query = mapper.Map<GetAllWorkSchedulesByOwnerIdQuery>(request);

            var result = await mediator
                .Send(query, cancellationToken);

            return result.Match(
                result => Ok(
                    mapper.Map<GetAllWorkSchedulesByOwnerIdResponse>(result)),
                errors => Problem(errors));
        }

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

        [HttpPut("{ownerId}")]
        public async Task<IActionResult> UpdateByOwnerId(
            Guid ownerId,
            UpdateWorkSchedulesByOwnerIdRequest request,
            CancellationToken cancellationToken)
        {
            var command = mapper.Map<UpdateWorkSchedulesByOwnerIdCommand>(request)
                .SetOwnerId(ownerId);

            var result = await mediator
                .Send(command, cancellationToken);

            return result.Match(
                result => Ok(
                    mapper.Map<UpdateWorkSchedulesByOwnerIdResponse>(result)),
                errors => Problem(errors));
        }
    }
}