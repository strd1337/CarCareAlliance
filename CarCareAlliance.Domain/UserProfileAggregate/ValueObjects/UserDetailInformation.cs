using CarCareAlliance.Domain.Common.Models;

namespace CarCareAlliance.Domain.UserProfileAggregate.ValueObjects
{
    public class UserDetailInformation : ValueObject
    {
        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }
        public string? PhoneNumber { get; private set; }
        public string? Address { get; private set; }
        public string? Country { get; private set; }
        public string? City { get; private set; }
        public string? PostCode { get; private set; }

        private UserDetailInformation(
            string? firstName,
            string? lastName,
            string? phoneNumber,
            string? address,
            string? city,
            string? postCode,
            string? country)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Address = address;
            City = city;
            PostCode = postCode;
            Country = country;
        }

        public static UserDetailInformation CreateNew(
            string? firstName = null,
            string? lastName = null,
            string? phoneNumber = null,
            string? address = null,
            string? city = null,
            string? postCode = null,
            string? country = null)
        {
            return new(
                firstName,
                lastName,
                phoneNumber,
                address,
                city,
                postCode,
                country);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {

            yield return FirstName ?? string.Empty;
            yield return FirstName ?? string.Empty;
            yield return LastName ?? string.Empty;
            yield return PhoneNumber ?? string.Empty;
            yield return Address ?? string.Empty;
            yield return City ?? string.Empty;
            yield return PostCode ?? string.Empty;
            yield return Country ?? string.Empty;
        }
    }
}
