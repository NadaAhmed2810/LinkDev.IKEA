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
    internal class EmployeeTypeJsonConverter:JsonConverter<EmployeeType>
    {
        public override EmployeeType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return (EmployeeType)Enum.Parse(typeof(EmployeeType), reader.GetString()!);
        }
        public override void Write(Utf8JsonWriter writer, EmployeeType value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
  
}
