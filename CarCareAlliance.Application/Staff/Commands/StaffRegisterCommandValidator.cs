using FluentValidation;

namespace CarCareAlliance.Application.Staff.Commands
{
    public class StaffRegisterCommandValidator
        : AbstractValidator<StaffRegisterCommand>
    {
        public StaffRegisterCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                    .WithMessage("Username is required")
                .MinimumLength(5)
                    .WithMessage("Username must have at least 5 characters")
                .MaximumLength(15)
                    .WithMessage("Username cannot have more than 15 characters");

            RuleFor(r => r.Email)
                .NotEmpty()
                    .WithMessage("Email is required")
                .EmailAddress()
                    .WithMessage("Email is not valid");

            RuleFor(r => r.Password)
                .NotEmpty()
                    .WithMessage("Password is required")
                .MinimumLength(6)
                    .WithMessage("Password must have at least 6 characters")
                .MaximumLength(20)
                    .WithMessage("Password cannot have more than 20 characters")
                .Matches("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{6,}$")
                    .WithMessage("Password must contain at least one " +
                        "number and one uppercase letter");

            RuleFor(r => r.FirstName)
                .NotEmpty()
                    .WithMessage("First name is required")
                .MaximumLength(50)
                    .WithMessage("First name cannot exceed 50 characters");

            RuleFor(r => r.LastName)
                .NotEmpty()
                    .WithMessage("Last name is required")
                .MaximumLength(50)
                    .WithMessage("Last name cannot exceed 50 characters");

            RuleFor(r => r.PhoneNumber)
                .NotEmpty()
                    .WithMessage("Phone number is required")
                .Matches(@"^\d{10,}$")
                    .WithMessage("Phone number must be at least 10 digits")
                .MaximumLength(30)
                    .WithMessage("Phone number cannot exceed 30 digits");

            RuleFor(r => r.Address)
                .NotEmpty()
                    .WithMessage("Address is required")
                .MaximumLength(200)
                    .WithMessage("Address cannot exceed 200 characters");

            RuleFor(r => r.City)
                .NotEmpty()
                    .WithMessage("City is required")
                .MaximumLength(50)
                    .WithMessage("City cannot exceed 50 characters");

            RuleFor(r => r.PostCode)
                .NotEmpty()
                    .WithMessage("Post code is required")
                .MaximumLength(10)
                    .WithMessage("Post code cannot exceed 10 characters");

            RuleFor(r => r.Country)
                .NotEmpty()
                    .WithMessage("Country is required")
                .MaximumLength(50)
                    .WithMessage("Country cannot exceed 50 characters");

            RuleFor(r => r.Experience)
               .NotEmpty()
                   .WithMessage("Experience is required");
        }
    }
}
