using FluentValidation;

namespace CarCareAlliance.Application.Tickets.Commands.Create
{
    public class TicketCreateValidator
        : AbstractValidator<TicketCreateCommand>
    {
        public TicketCreateValidator()
        {
            RuleFor(x => x.UserProfileId)
                .NotEmpty().WithMessage("User profile ID cannot be empty.");

            RuleFor(x => x.VehicleId)
                .NotEmpty().WithMessage("Vehicle ID cannot be empty.");

            RuleFor(x => x.Mileage)
                .GreaterThanOrEqualTo(0)
                    .WithMessage("Mileage must be greater than or equal to 0.");

            RuleFor(x => x.ServiceIds)
                .Must(NotBeEmptyGuids)
                    .WithMessage("At least one service ID must be provided.");

            RuleFor(x => x.OrderDetailsComments)
                .MaximumLength(250)
                    .WithMessage("Order details comments cannot exceed 250 characters.");

            RuleFor(x => x.TicketDescription)
                .MaximumLength(100)
                    .WithMessage("Ticket description cannot exceed 100 characters.");
        }

        private bool NotBeEmptyGuids(ICollection<Guid> serviceIds)
        {
            return serviceIds != null && serviceIds.Count > 0 && serviceIds.All(id => id != Guid.Empty);
        }
    }
}