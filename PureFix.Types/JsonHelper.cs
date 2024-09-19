using System.Text.Json;

namespace PureFix.Types
{
    public static class JsonHelper
    {
        public static T FromJson<T>(string s)
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
                WriteIndented = true
            };
            var json = JsonSerializer.Serialize(instance, options);
            return json;
        }
    }
}
