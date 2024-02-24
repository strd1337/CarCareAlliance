using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories;
using CarCareAlliance.Application.Tickets.Common;
using ErrorOr;

namespace CarCareAlliance.Application.Tickets.Commands.Create
{
    public class TicketCreateHandler(
        IUnitOfWork unitOfWork) :
        ICommandHandler<TicketCreateCommand, TicketCreateResult>
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<ErrorOr<TicketCreateResult>> Handle(
            TicketCreateCommand command,
            CancellationToken cancellationToken)
        {
            return new TicketCreateResult();
        }
    }
}
