﻿using System;
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
            
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (writer == null) 
                throw new ArgumentNullException(nameof(writer));

            var timeZone = this.RequestTimeZone().TimeZone;
            var parsedDateTime = DateTimeOffset.Parse(value.ToString());
            var convertTime = TimeZoneInfo.ConvertTime(parsedDateTime.DateTime, timeZone);
            var dateTimeOffset = new DateTimeOffset(convertTime, timeZone.BaseUtcOffset);

            writer.WriteValue(dateTimeOffset
                .ToString(serializer.DateFormatString));
            
            writer
                .Flush();
        }
    }
}