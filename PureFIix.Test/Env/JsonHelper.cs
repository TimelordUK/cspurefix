using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PureFIix.Test.Env
{
    internal static class JsonHelper
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
