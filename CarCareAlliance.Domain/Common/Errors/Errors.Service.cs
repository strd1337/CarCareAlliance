using ErrorOr;

namespace CarCareAlliance.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Service
        {
            public static Error ServicesNotFound => Error.NotFound(
                code: "Service.NotFound",
                description: "One or more services are not found.");

        }
    }
}
