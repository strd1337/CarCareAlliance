using ErrorOr;

namespace CarCareAlliance.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class ServicePartner
        {
            public static Error NotFound => Error.NotFound(
                code: "ServicePartner.NotFound",
                description: "Service partner is not found.");
        }
    }
}