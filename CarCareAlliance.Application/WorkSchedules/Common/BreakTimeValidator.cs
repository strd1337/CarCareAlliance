using CarCareAlliance.Domain.WorkScheduleAggregate.ValueObjects;
using FluentValidation;

namespace CarCareAlliance.Application.WorkSchedules.Common
{
    public class BreakTimeValidator
        : AbstractValidator<BreakTime>
    {
        public BreakTimeValidator()
        {
            RuleFor(bt => bt.StartTime)
                .NotEmpty()
                    .WithMessage("Break start time is required.");

            RuleFor(bt => bt.EndTime)
                .NotEmpty()
                    .WithMessage("Break end time is required.");

            RuleFor(bt => bt.EndTime)
                .GreaterThan(bt => bt.StartTime)
                    .WithMessage("Break end time must be greater than start time.");
        }
    }
}