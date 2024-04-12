using FluentValidation;

namespace CarCareAlliance.Application.ServicePartners.Commands.Add
{
    public class ServicePartnerAddValidator
        : AbstractValidator<ServicePartnerAddCommand>
    {
        public ServicePartnerAddValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty()
                    .WithMessage("Service partner name is required")
                .MaximumLength(30)
                    .WithMessage("Service partner name cannot exceed 30 characters");

            RuleFor(r => r.Description)
                .NotEmpty()
                    .WithMessage("Service partner description is required")
                .MaximumLength(1000)
                    .WithMessage("Service partner description cannot exceed 1000 characters");
        }
    }
}