using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Vivet.AspNetCore.RequestTimeZone.Serialization
{
    /// <inheritdoc />
    public class MicrosoftDateTimeConverter : JsonConverter<DateTimeOffset?>
    {
        /// <summary>  
        /// Request Time Zone.
        /// </summary>  
        protected virtual Func<RequestTimeZone> RequestTimeZone { get; }

        /// <summary>  
        /// Constructor.
        /// </summary>  
        /// <param name="requestTimeZone">The <see cref="RequestTimeZone"/>.</param>  
        public MicrosoftDateTimeConverter(Func<RequestTimeZone> requestTimeZone)
        {
            this.RequestTimeZone = requestTimeZone ?? throw new ArgumentNullException(nameof(requestTimeZone));
        }

        /// <inheritdoc />
        public override DateTimeOffset? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();

            if (value == null)
                return null;

            DateTimeOffset.TryParse(value, out var parsedDateTime);

            return parsedDateTime;
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, DateTimeOffset? value, JsonSerializerOptions options)
        {
            if (writer == null) 
                throw new ArgumentNullException(nameof(writer));

            DateTimeOffset.TryParse(value.ToString(), out var parsedDateTime);

            var timeZone = this.RequestTimeZone().TimeZone;
            var convertTime = TimeZoneInfo.ConvertTime(parsedDateTime, timeZone);

            writer
                .WriteStringValue(convertTime);
            
            writer
                .Flush();
        }
    }
}