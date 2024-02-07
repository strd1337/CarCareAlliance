using CarCareAlliance.Domain.Common.Models;
using CarCareAlliance.Domain.PhotoAggregate.ValueObjects;
using CarCareAlliance.Domain.ServicePartnerAggregate.ValueObjects;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;
using CarCareAlliance.Domain.VehicleAggregate.ValueObjects;

namespace CarCareAlliance.Domain.PhotoAggregate
{
    public sealed class Photo : AggregateRoot<PhotoId, Guid>
    {
        public byte[] ImageData { get; private set; }
        public string Format { get; private set; }
        public DateTime DateTaken { get; private set; }
        public string Description { get; private set; }
        public UserProfileId UserProfileId { get; private set; }
        public VehicleId? VehicleId { get; private set; } = null;
        public ServicePartnerId? ServicePartnerId { get; private set; } = null;

        private Photo(
            PhotoId id,
            byte[] imageData,
            string format,
            DateTime dateTaken,
            string description,
            UserProfileId userProfileId) : base(id)
        {
            ImageData = imageData;
            Format = format;
            DateTaken = dateTaken;
            Description = description;
            UserProfileId = userProfileId;
        }

        public static Photo Create(
            byte[] imageData,
            string format,
            DateTime dateTaken,
            string description,
            UserProfileId userProfileId)
        {
            return new Photo(
                PhotoId.CreateUnique(),
                imageData,
                format,
                dateTaken,
                description,
                userProfileId);
        }

        public void UpdateVehicle(VehicleId id)
        {
            VehicleId = id;
        }

        public void UpdateServicePartner(ServicePartnerId id)
        {
            ServicePartnerId = id;
        }

#pragma warning disable CS8618
        private Photo()
        {
        }
#pragma warning restore CS8618
    }
}
