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
        protected virtual RequestTimeZone RequestTimeZone { get; }

        /// <summary>  
        /// Constructor.
        /// </summary>  
        /// <param name="requestTimeZone">The <see cref="RequestTimeZone"/>.</param>  
        public DateTimeConverter(RequestTimeZone requestTimeZone)
        {
            this.RequestTimeZone = requestTimeZone ?? throw new ArgumentNullException(nameof(requestTimeZone));
        }

        /// <inheritdoc />
        public override bool CanConvert(Type objectType)
        {
            return
                //objectType == typeof(DateTime) ||
                //objectType == typeof(DateTime?) ||
                objectType == typeof(DateTimeOffset) ||
                objectType == typeof(DateTimeOffset?);
        }
        
        /// <inheritdoc />
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader == null) 
                throw new ArgumentNullException(nameof(reader));
            
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (writer == null) 
                throw new ArgumentNullException(nameof(writer));

            var dateTime = Convert.ToDateTime(value);
            var convertedDateTime = TimeZoneInfo.ConvertTime(dateTime, this.RequestTimeZone.TimeZone);
            var dateTimeOffset = new DateTimeOffset(convertedDateTime, this.RequestTimeZone.TimeZone.BaseUtcOffset);

            writer
                .WriteValue(dateTimeOffset.ToString(serializer.DateFormatString));

            writer
                .Flush();
        }
    }
}