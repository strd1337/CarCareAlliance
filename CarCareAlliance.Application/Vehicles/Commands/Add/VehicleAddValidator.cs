using FluentValidation;

namespace CarCareAlliance.Application.Vehicles.Commands.Add
{
    public class VehicleAddValidator
        : AbstractValidator<VehicleAddCommand>
    {
        public VehicleAddValidator()
        {
            RuleFor(r => r.Brand)
                .NotEmpty()
                    .WithMessage("Brand name is required")
                .MaximumLength(30)
                    .WithMessage("Brand name cannot exceed 30 characters");

            RuleFor(r => r.Model)
                .NotEmpty()
                    .WithMessage("Model name is required")
                .MaximumLength(30)
                    .WithMessage("Model name cannot exceed 30 characters");

            RuleFor(v => v.Year)
                .NotEmpty()
                    .WithMessage("Year is required.")
                .GreaterThan(1900)
                    .WithMessage("Year must be greater than 1900.")
                .LessThanOrEqualTo(DateTime.Now.Year)
                    .WithMessage($"Year must be less than or equal to {DateTime.Now.Year}.");

            RuleFor(v => v.Vin)
                .NotEmpty()
                    .WithMessage("VIN is required.")
                .Length(17)
                    .WithMessage("VIN must be exactly 17 characters.");

            RuleFor(v => v.LicensePlate)
                .NotEmpty()
                    .WithMessage("License plate is required.")
                .Matches(@"^[A-Z0-9-]*$")
                    .WithMessage("License plate must contain only uppercase letters, numbers, and hyphens.");

            RuleFor(v => v.UserProfileId)
                .NotEmpty()
                    .WithMessage("User profile ID is required.");
        }
    }
}