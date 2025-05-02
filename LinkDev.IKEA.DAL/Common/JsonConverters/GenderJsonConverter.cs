using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using LinkDev.IKEA.DAL.Common.Enums;

namespace LinkDev.IKEA.DAL.Common.JsonConverters
{
    public class GenderJsonConverter:JsonConverter<Gender>
    {
        public override Gender Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var GenderAsString = reader.GetString();
            return GenderAsString?.ToLower() switch
            {
                "male" => Gender.Male,
                "female" => Gender.Female,
                _ => Gender.Male
            };

        }
        public override void Write(Utf8JsonWriter writer, Gender value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }

    }
}
