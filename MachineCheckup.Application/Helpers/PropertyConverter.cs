using MachineCheckup.Domain.Enums;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MachineCheckup.Application.Helpers
{
    public class PropertyConverter : JsonConverter<Priority>
    {
        public override Priority Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (Enum.TryParse(reader.GetString(), true, out Priority priority))
                {
                    return priority;
                }
            }

            return Priority.Low;
        }

        public override void Write(Utf8JsonWriter writer, Priority value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }

}
