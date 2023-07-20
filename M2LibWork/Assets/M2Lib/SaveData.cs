using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace M2Lib
{
    public static class SaveData
    {
        private static string BasePath()
        {
            if (Application.isEditor)
            {
                return $"{Application.streamingAssetsPath}/../../SaveData";
            }
            return $"{Application.persistentDataPath}/save";
        }

        public static async ValueTask SaveAsync<T>(T data, string name = null) where T : class
        {
            var dir = BasePath();
            if (!string.IsNullOrEmpty(name))
            {
                dir += $"/{name}";
            }

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var path = $"{dir}/{typeof(T).Name}.json";
            var json = JsonConvert.SerializeObject(data, GetFormatting());
            await File.WriteAllTextAsync(path, json);
        }

        public static async ValueTask<T> LoadAsync<T>(string name = null) where T : class
        {
            var dir = BasePath();
            if (!string.IsNullOrEmpty(name))
            {
                dir += $"/{name}";
            }
            var path = $"{dir}/{typeof(T).Name}.json";
            if (!File.Exists(path))
            {
                return null;
            }
            var json = await File.ReadAllTextAsync(path);
            return JsonConvert.DeserializeObject<T>(json);
        }

        private static Formatting GetFormatting()
        {
            return Application.isEditor ? Formatting.Indented : Formatting.None;
        }
    }
}
