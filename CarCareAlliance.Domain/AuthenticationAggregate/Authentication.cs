using CarCareAlliance.Domain.AuthenticationAggregate.ValueObjects;
using CarCareAlliance.Domain.Common.Models;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;

namespace CarCareAlliance.Domain.AuthenticationAggregate
{
    public sealed class Authentication : AggregateRoot<AuthenticationId, Guid>
    {
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string Salt { get; private set; }
        public UserProfileId UserProfileId { get; private set; }
        
        private Authentication(
            AuthenticationId id,
            string userName,
            string email,
            string passwordHash,
            string salt,
            UserProfileId userProfileId) : base(id)
        {
            UserName = userName;
            Email = email;
            PasswordHash = passwordHash;
            Salt = salt;
            UserProfileId = userProfileId;
        }

        public static Authentication Create(
            string userName,
            string email,
            string passwordHash,
            string salt,
            UserProfileId userProfileId)
        {
            return new Authentication(
                AuthenticationId.CreateUnique(),
                userName,
                email,
                passwordHash,
                salt,
                userProfileId);
        }

        public void SetPassword(
            string passwordHash,
            string salt)
        {
            PasswordHash = passwordHash;
            Salt = salt;
        }

#pragma warning disable CS8618
        private Authentication()
        {
        }
#pragma warning restore CS8618
    }
}
