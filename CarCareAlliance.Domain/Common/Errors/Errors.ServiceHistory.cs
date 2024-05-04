using ErrorOr;

namespace CarCareAlliance.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class ServiceHistory
        {
            public static Error NotFound => Error.NotFound(
                code: "ServiceHistory.NotFound",
                description: "Service history is not found.");

            public static Error TicketNotFound => Error.NotFound(
                code: "ServiceHistory.TicketNotFound",
                description: "Ticket is not found.");
        }
    }
}
