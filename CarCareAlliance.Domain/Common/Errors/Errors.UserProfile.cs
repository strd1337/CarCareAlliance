using ErrorOr;

namespace CarCareAlliance.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class UserProfile
        {
            public static Error DuplicateEmail => Error.Conflict(
                code: "UserProfile.DuplicateEmail",
                description: "Email is already in use.");

            public static Error DuplicateUsername => Error.Conflict(
                code: "UserProfile.DuplicateUsername",
                description: "Username is already in use.");

            public static Error NotFound => Error.NotFound(
                code: "UserProfile.NotFound",
                description: "User profile is not found.");

            public static Error DataConflict => Error.Conflict(
                code: "UserProfile.DataConflict",
                description: "The provided data is the same as the existing data.");
        }
    }
}
