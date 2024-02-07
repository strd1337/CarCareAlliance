using CarCareAlliance.Domain.Common.Models;
using CarCareAlliance.Domain.ReviewAggregate.ValueObjects;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;

namespace CarCareAlliance.Domain.ReviewAggregate
{
    public sealed class Review : AggregateRoot<ReviewId, Guid>
    {
        public Guid ObjectId { get; private set; }
        public ObjectType ObjectType { get; private set; }
        public string Text { get; private set; }
        public float Rating { get; private set; }
        public DateTime DatePublished { get; private set; }
        public UserProfileId UserProfileId { get; private set; }

        private Review(
            ReviewId id,
            Guid objectId,
            ObjectType objectType,
            string text,
            float rating,
            DateTime datePublished,
            UserProfileId userProfileId) : base(id)
        {
            ObjectId = objectId; 
            ObjectType = objectType; 
            Text = text; 
            Rating = rating;
            DatePublished = datePublished;
            UserProfileId = userProfileId;
        }

        public static Review Create(
            Guid objectId,
            ObjectType objectType,
            string text,
            float rating,
            DateTime datePublished,
            UserProfileId userProfileId)
        {
            return new Review(
                ReviewId.CreateUnique(),
                objectId,
                objectType,
                text,
                rating,
                datePublished,
                userProfileId);
        }

#pragma warning disable CS8618
        private Review()
        {
        }
#pragma warning restore CS8618
    }
}
