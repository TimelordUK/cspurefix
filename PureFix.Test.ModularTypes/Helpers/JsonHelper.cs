using System.Text.Json;
using System.Text.Json.Serialization;

namespace PureFix.Test.ModularTypes.Helpers
{
    public static class JsonHelper
    {
        public static T? FromJson<T>(string s)
        {
            var instance = JsonSerializer.Deserialize<T>(s, ReadOptions);
            return instance;
        }

        private static readonly JsonSerializerOptions WriteOptions = new()
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        private static readonly JsonSerializerOptions ReadOptions = new ()
        {
            PropertyNameCaseInsensitive = true
        };

    public static string ToJson<T>(T instance)
        {
            var json = JsonSerializer.Serialize(instance, WriteOptions);
            return json;
        }

        public static string ToJson(object instance, Type t)
        {
            var json = JsonSerializer.Serialize(instance, t, WriteOptions);
            return json;
        }
    }
}
