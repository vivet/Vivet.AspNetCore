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
            return objectType == typeof(DateTime) || objectType == typeof(DateTime?);
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
            
            var userTimeZone = this.RequestTimeZone();

            writer
                .WriteValue(TimeZoneInfo.ConvertTime(Convert.ToDateTime(value), userTimeZone.TimeZone)
                    .ToString(serializer.DateFormatString));
            
            writer
                .Flush();
        }
    }
}