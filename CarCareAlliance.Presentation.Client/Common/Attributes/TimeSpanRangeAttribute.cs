using System.ComponentModel.DataAnnotations;

namespace CarCareAlliance.Presentation.Client.Common.Attributes
{
    public class TimeSpanRangeAttribute(
        double minimumSeconds, 
        double maximumSeconds) : ValidationAttribute
    {
        private readonly double minimumSeconds = minimumSeconds;
        private readonly double maximumSeconds = maximumSeconds;

        protected override ValidationResult IsValid(
            object value, 
            ValidationContext validationContext)
        {
            if (value is TimeSpan timeSpanValue)
            {
                double totalSeconds = timeSpanValue.TotalSeconds;

                if (totalSeconds < minimumSeconds || totalSeconds > maximumSeconds)
                {
                    return new ValidationResult(string.Empty);
                }
            }

            return ValidationResult.Success;
        }
    }
}
