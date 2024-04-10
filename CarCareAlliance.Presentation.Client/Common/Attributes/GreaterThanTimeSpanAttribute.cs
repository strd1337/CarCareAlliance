using System.ComponentModel.DataAnnotations;

namespace CarCareAlliance.Presentation.Client.Common.Attributes
{
    public class GreaterThanTimeSpanAttribute(double minimumSeconds) : ValidationAttribute
    {
        private readonly double minimumSeconds = minimumSeconds;

        protected override ValidationResult IsValid(
            object value, 
            ValidationContext validationContext)
        {
            if (value is TimeSpan timeSpanValue)
            {
                if (timeSpanValue.TotalSeconds < minimumSeconds)
                {
                    return new ValidationResult(string.Empty);
                }
            }

            return ValidationResult.Success;
        }
    }
}
