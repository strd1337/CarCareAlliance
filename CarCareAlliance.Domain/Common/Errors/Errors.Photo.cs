using ErrorOr;

namespace CarCareAlliance.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Photo
        {
            public static Error NotFound => Error.NotFound(
                code: "Photo.NotFound",
                description: "Photo is not found.");
        }
    }
}
