using CarCareAlliance.Domain.Common.Models;
using CarCareAlliance.Domain.MechanicAggregate.ValueObjects;
using CarCareAlliance.Domain.ReviewAggregate.ValueObjects;
using CarCareAlliance.Domain.ServicePartnerAggregate.ValueObjects;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;
using CarCareAlliance.Domain.WorkScheduleAggregate.ValueObjects;

namespace CarCareAlliance.Domain.MechanicAggregate
{
    public sealed class MechanicProfile : AggregateRoot<MechanicProfileId, Guid>
    {
        private readonly List<ReviewId> reviewIds = [];

        public UserProfileId UserProfileId { get; private set; }
        public ServicePartnerId ServicePartnerId { get; private set; }
        public WorkScheduleId WorkScheduleId { get; private set; }
        public float Experience { get; private set; }

        public IReadOnlyList<ReviewId> ReviewIds => reviewIds.AsReadOnly();

        private MechanicProfile(
            MechanicProfileId id,
            float experience,
            UserProfileId userProfileId,
            ServicePartnerId servicePartnerId,
            WorkScheduleId workScheduleId) : base(id)
        {
            Experience = experience;
            UserProfileId = userProfileId;
            ServicePartnerId = servicePartnerId;
            WorkScheduleId = workScheduleId;
        }

        public static MechanicProfile Create(
            float experience,
            UserProfileId userProfileId,
            ServicePartnerId servicePartnerId,
            WorkScheduleId workScheduleId)
        {
            return new MechanicProfile(
                MechanicProfileId.CreateUnique(),
                experience,
                userProfileId,
                servicePartnerId,
                workScheduleId);
        }

        public void AddReview(ReviewId reviewId)
        {
            reviewIds.Add(reviewId);
        }

        public void UpdateWorkSchedule(WorkScheduleId workScheduleId)
        {
            WorkScheduleId = workScheduleId;
        }

#pragma warning disable CS8618
        private MechanicProfile()
        {
        }
#pragma warning restore CS8618
    }
}