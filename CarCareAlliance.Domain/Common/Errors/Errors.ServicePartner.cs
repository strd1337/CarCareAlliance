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

            public static Error DuplicateServicePartner => Error.Conflict(
                code: "ServicePartner.DuplicateServicePartner",
                description: "Service partner already exists");
        }
    }
}