using CarCareAlliance.Domain.Common.Models;
using CarCareAlliance.Domain.PhotoAggregate.ValueObjects;
using CarCareAlliance.Domain.ReviewAggregate.ValueObjects;
using CarCareAlliance.Domain.UserProfileAggregate.Entities;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;

namespace CarCareAlliance.Domain.UserProfileAggregate
{
    public sealed class UserProfile : AggregateRoot<UserProfileId, Guid>
    {
        private readonly List<Role> roles = [];
        private readonly List<ReviewId> reviewIds = [];

        public UserDetailInformation Information { get; private set; }
        public PhotoId? PhotoId { get; private set; } = null;

        public IReadOnlyList<Role> Roles => roles.AsReadOnly();
        public IReadOnlyList<ReviewId> ReviewIds => reviewIds.AsReadOnly();


        private UserProfile(
            UserProfileId id,
            UserDetailInformation information) : base(id)
        {
            Information = information;
        }

        public static UserProfile Create(
            UserDetailInformation information)
        {
            return new UserProfile(
                UserProfileId.CreateUnique(),
                information);
        }

        public void AddRole(Role role)
        {
            roles.Add(role);
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