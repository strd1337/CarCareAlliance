using FluentValidation;

namespace CarCareAlliance.Application.Tickets.Commands.Create
{
    public class TicketCreateValidator
        : AbstractValidator<TicketCreateCommand>
    {
        public TicketCreateValidator()
        {
            
        }
    }
}
