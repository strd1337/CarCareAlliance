using CarCareAlliance.Application.Common.Services;

namespace CarCareAlliance.Infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.Now;
    }
}
