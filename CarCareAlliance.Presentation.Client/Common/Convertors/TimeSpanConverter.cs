using System.Text.Json.Serialization;
using System.Text.Json;
using System.Globalization;

namespace CarCareAlliance.Presentation.Client.Common.Convertors
{
    public class TimeSpanConverter : JsonConverter<TimeSpan>
    {
        private const string TimeSpanFormat = @"hh\:mm\:ss";

        public override TimeSpan Read(
            ref Utf8JsonReader reader, 
            Type typeToConvert, 
            JsonSerializerOptions options)
        {
            if (TimeSpan.TryParseExact(
                reader.GetString(), 
                TimeSpanFormat, 
                CultureInfo.InvariantCulture, 
                out TimeSpan result))
            {
                return result;
            }
            return TimeSpan.Zero;
        }

        public override void Write(
            Utf8JsonWriter writer, 
            TimeSpan value, 
            JsonSerializerOptions options)
        {
            writer.WriteStringValue(
                value.ToString(
                    TimeSpanFormat, 
                    CultureInfo.InvariantCulture));
        }
    }
}
