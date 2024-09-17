using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PureFix.Types.Converters
{
    /// <summary>
    /// Serializes a MonthYear
    /// </summary>
    public sealed class MonthYearJsonConverter : JsonConverter<MonthYear>
    {
        /// <inheritdoc/>
        public override MonthYear Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var text = reader.GetString();
            if(text is null || text.Length == 0) return default;

            return MonthYear.Parse(text);
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, MonthYear value, JsonSerializerOptions options)
        {
            var text = value.AsFixString();
            writer.WriteStringValue(text);
        }
    }
}
