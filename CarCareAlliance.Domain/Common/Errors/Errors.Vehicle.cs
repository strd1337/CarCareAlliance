using ErrorOr;

namespace CarCareAlliance.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Vehicle
        {
            public static Error DuplicateVehicle => Error.Conflict(
                code: "Vehicle.DuplicateVehicle",
                description: "Vehicle already exists.");

            public static Error NotFound => Error.NotFound(
                code: "Vehicle.NotFound",
                description: "Vehicle is not found.");
        }
    }
}
