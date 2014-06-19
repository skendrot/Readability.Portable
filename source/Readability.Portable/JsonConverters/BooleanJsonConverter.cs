using System;

using Newtonsoft.Json;

namespace Readability.JsonConverters
{
    class BooleanJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var actualValue = (bool?) value;
            int? val = actualValue.HasValue == false
                ? (int?) null
                : actualValue.Value ? 1 : 0;
            writer.WriteValue(val);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null) return null;
            var value = (int) reader.Value;

            return value == 1;
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}
