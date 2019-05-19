using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JapLang.BLL.Infrastructure
{
    public class Base64FileJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var content = reader.Value as string;
            if (string.IsNullOrEmpty(content))
            {
                return new byte[0];
            }
            else
            {
                return Convert.FromBase64String(content);
            }
        }

        //Because we are never writing out as Base64, we don't need this. 
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
