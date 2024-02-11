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

            RuleFor(r => r.Latitude)
                .NotEmpty()
                    .WithMessage("Latitude is required");

            RuleFor(r => r.Longitude)
                .NotEmpty()
                    .WithMessage("Longitude is required");

            RuleFor(r => r.Address)
                .NotEmpty()
                    .WithMessage("Address is required");

            RuleFor(r => r.City)
                .NotEmpty()
                    .WithMessage("City is required");

            RuleFor(r => r.Country)
                .NotEmpty()
                    .WithMessage("Country is required");

            RuleFor(r => r.PostalCode)
                .NotEmpty()
                    .WithMessage("PostalCode is required");

            RuleFor(r => r.LocationDescription)
                .NotEmpty()
                    .WithMessage("Location description is required");
        }
    }
}