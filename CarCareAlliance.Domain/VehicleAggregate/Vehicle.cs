using CarCareAlliance.Domain.Common.Models;
using CarCareAlliance.Domain.PhotoAggregate.ValueObjects;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;
using CarCareAlliance.Domain.VehicleAggregate.ValueObjects;

namespace CarCareAlliance.Domain.VehicleAggregate
{
    public sealed class Vehicle : AggregateRoot<VehicleId, Guid>
    {
        private readonly List<PhotoId> photoIds = [];

        public string Brand { get; private set; }
        public string Model { get; private set; }
        public int Year { get; private set; }
        public string Vin { get; private set; }
        public string LicensePlate { get; private set; }
        public UserProfileId UserProfileId { get; private set; }

        public IReadOnlyList<PhotoId> PhotoIds => photoIds.AsReadOnly();

        private Vehicle(
            VehicleId id,
            string brand,
            string model,
            int year,
            string vin,
            string licensePlate,
            UserProfileId userId) : base(id)
        {
            Brand = brand;
            Model = model;
            Year = year;
            LicensePlate = licensePlate;
            Vin = vin;
            UserProfileId = userId;
        }

        public static Vehicle Create(
            string brand,
            string model,
            int year,
            string vin,
            string licensePlate,
            UserProfileId userId)
        {
            return new Vehicle(
                VehicleId.CreateUnique(),
                brand,
                model,
                year,
                vin,
                licensePlate,
                userId);
        }

#pragma warning disable CS8618
        private Vehicle()
        {
        }
#pragma warning restore CS8618
    }
}
