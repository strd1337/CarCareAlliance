using CarCareAlliance.Application.WorkSchedules.Common;
using FluentValidation;

namespace CarCareAlliance.Application.WorkSchedules.Commands.Add
{
    public class WorkScheduleAddValidator
        : AbstractValidator<WorkScheduleAddCommand>
    {
        public WorkScheduleAddValidator()
        {
            RuleFor(r => r.DayOfWeek)
                .Must(BeValidDayOfWeek)
                    .WithMessage("Invalid day of week.");

            RuleFor(r => r.StartTime)
                .NotEmpty()
                    .WithMessage("Start time is required.");

            RuleFor(r => r.EndTime)
                .NotEmpty()
                    .WithMessage("End time is required.");

            RuleFor(r => r.EndTime)
                .GreaterThan(r => r.StartTime)
                    .WithMessage("End time must be greater than start time.");

            RuleFor(r => r.OwnerId)
                .NotEmpty()
                .WithMessage("Owner Id is required.");

            RuleForEach(r => r.BreakTimes)
                .SetValidator(new BreakTimeValidator());
        }

        private bool BeValidDayOfWeek(DayOfWeek dayOfWeek)
        {
            return dayOfWeek >= DayOfWeek.Sunday && 
                dayOfWeek <= DayOfWeek.Saturday;
        }

        private bool BeValidWeekends(ICollection<DayOfWeek> weekends)
        {
            foreach (var dayOfWeek in weekends)
            {
                if (dayOfWeek < DayOfWeek.Sunday || dayOfWeek > DayOfWeek.Saturday)
                {
                    return false;
                }
            }
            return true;
        }
    }
}