using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Vivet.AspNetCore.RequestTimeZone
{
    /// <inheritdoc />
    public class DateTimeConverter : DateTimeConverterBase
    {
        /// <summary>  
        /// Request Time Zone.
        /// </summary>  
        protected virtual Func<RequestTimeZone> RequestTimeZone { get; }

        /// <summary>  
        /// Constructor.
        /// </summary>  
        /// <param name="requestTimeZone">The <see cref="RequestTimeZone"/>.</param>  
        public DateTimeConverter(Func<RequestTimeZone> requestTimeZone)
        {
            this.RequestTimeZone = requestTimeZone ?? throw new ArgumentNullException(nameof(requestTimeZone));
        }

        /// <inheritdoc />
        public override bool CanConvert(Type objectType)
        {
            return
                objectType == typeof(DateTimeOffset) ||
                objectType == typeof(DateTimeOffset?);
        }
        
        /// <inheritdoc />
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader == null) 
                throw new ArgumentNullException(nameof(reader));

            var value = reader.Value;

            if (value == null)
                return null;

            DateTimeOffset.TryParse(value.ToString(), out var parsedDateTime);

            return parsedDateTime;
        }

        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (writer == null) 
                throw new ArgumentNullException(nameof(writer));

            DateTimeOffset.TryParse(value.ToString(), out var parsedDateTime);

            var timeZone = this.RequestTimeZone().TimeZone;
            var convertTime = TimeZoneInfo.ConvertTime(parsedDateTime, timeZone);

            writer
                .WriteValue(convertTime); 
            
            writer
                .Flush();
        }
    }
}