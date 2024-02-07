using CarCareAlliance.Domain.Common.Models;
using CarCareAlliance.Domain.PhotoAggregate.ValueObjects;
using CarCareAlliance.Domain.ReviewAggregate.ValueObjects;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;

namespace CarCareAlliance.Domain.UserProfileAggregate
{
    public sealed class UserProfile : AggregateRoot<UserProfileId, Guid>
    {
        private readonly List<ReviewId> reviewIds = [];

        public UserDetailInformation Information { get; private set; }
        public PhotoId? PhotoId { get; private set; } = null;

        public RoleType RoleType { get; private set; }
        public IReadOnlyList<ReviewId> ReviewIds => reviewIds.AsReadOnly();


        private UserProfile(
            UserProfileId id,
            UserDetailInformation information,
            RoleType roleType) : base(id)
        {
            Information = information;
            RoleType = roleType;
        }

        public static UserProfile Create(
            UserDetailInformation information,
            RoleType roleType)
        {
            return new UserProfile(
                UserProfileId.CreateUnique(),
                information,
                roleType);
        }

        public void AddReview(ReviewId reviewId)
        {
            reviewIds.Add(reviewId);
        }

        public void UpdateInformation(
            UserDetailInformation updatedInformation)
        {
            Information = updatedInformation;
        }

        public void UpdatePhoto(PhotoId photoId)
        {
            PhotoId = photoId;
        }

#pragma warning disable CS8618
        private UserProfile()
        {
        }
#pragma warning restore CS8618
    }
}