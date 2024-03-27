namespace CarCareAlliance.Presentation.Client.Models.Mechanics
{
    public class MechanicProfile
    {
        public Guid MechanicId { get; set; }
        public Guid ProfileId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public float Experience { get; set; }
    }
}