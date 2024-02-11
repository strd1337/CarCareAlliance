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
                description: "Owner's work schedule is not found.");
        }
    }
}