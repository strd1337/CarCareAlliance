using CarCareAlliance.Domain.Common.Models;
using CarCareAlliance.Domain.ServicePartnerAggregate.ValueObjects;

namespace CarCareAlliance.Domain.ServicePartnerAggregate.Entities
{
    public sealed class ServiceLocation : Entity<ServiceLocationId>
    {
        public float Latitude { get; private set; }
        public float Longitude { get; private set; }
        public string Address { get; private set; }
        public string City { get; private set; }
        public string? State { get; private set; } = null;
        public string Country { get; private set; }
        public string PostalCode { get; private set; }
        public string Description { get; private set; }

        private ServiceLocation(
            ServiceLocationId id,
            float latitude,
            float longitude,
            string address,
            string city,
            string country,
            string postalCode,
            string description,
            string? state = null) : base(id)
        {
            Latitude = latitude;
            Longitude = longitude;
            Address = address;
            City = city;
            Country = country;
            PostalCode = postalCode;
            Description = description;
            State = state;
        }

        public static ServiceLocation Create(
            float latitude,
            float longitude,
            string address,
            string city,
            string country,
            string postalCode,
            string description,
            string? state = null)
        {
            return new(
                ServiceLocationId.CreateUnique(),
                latitude,
                longitude,
                address,
                city,
                country,
                postalCode,
                description,
                state);
        }
    }
}
