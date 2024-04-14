using ErrorOr;

namespace CarCareAlliance.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class WorkSchedule
        {
            public static Error NotFound => Error.NotFound(
                code: "WorkSchedule.NotFound",
                description: "Work schedule is not found.");
         
            public static Error OwnerNotFound => Error.NotFound(
                code: "WorkSchedule.OwnerNotFound",
                description: "Owner is not found.");

            public static Error OwnerWorkSchedulesNotFound => Error.NotFound(
                code: "WorkSchedule.OwnerWorkSchedulesNotFound",
                description: "Owner's work schedules are not found.");
        }
    }
}