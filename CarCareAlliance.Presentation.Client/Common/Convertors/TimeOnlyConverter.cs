using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace CarCareAlliance.Presentation.Client.Common.Convertors
{
    public class TimeOnlyConverter : JsonConverter<TimeOnly>
    {
        private const string TimeFormat = "HH:mm";

        public override TimeOnly Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            return TimeOnly.ParseExact(
                reader.GetString()!,
                TimeFormat,
                CultureInfo.InvariantCulture);
        }

        public override void Write(
            Utf8JsonWriter writer,
            TimeOnly value,
            JsonSerializerOptions options)
        {
            writer.WriteStringValue(
                value.ToString(
                    TimeFormat,
                    CultureInfo.InvariantCulture));
        }
    }
}
