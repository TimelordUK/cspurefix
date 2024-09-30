using System.Text.Json;
using System.Text.Json.Serialization;

namespace PureFix.Types
{
    public static class JsonHelper
    {
        public static T? FromJson<T>(string s)
        {
            JsonSerializerOptions options2 = new()
            {
                PropertyNameCaseInsensitive = true
            };
            var instance = JsonSerializer.Deserialize<T>(s, options2);
            return instance;
        }

        public static string ToJson<T>(T instance)
        {
            JsonSerializerOptions options = new()
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            var json = JsonSerializer.Serialize(instance, options);
            return json;
        }

        public static string ToJson(object instance, Type t)
        {
            JsonSerializerOptions options = new()
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            var json = JsonSerializer.Serialize(instance, t, options);
            return json;
        }
    }
}
