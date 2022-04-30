using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

namespace M2Lib
{
    public static class MasterAdmin
    {
        private static Dictionary<Type, Dictionary<long, object>> Cache { get; } = new Dictionary<Type, Dictionary<long, object>>();

        private static HttpClient HttpClient { get; } = new HttpClient();

        // TODO
        private static string GetUrl<T>() => $"http://localhost:19088/api/json/{ProjectId}/{EnvironmentId}/{typeof(T).Name}";

        private static string ProjectId { get; set; }

        private static string EnvironmentId { get; set; }

        public static void Init(string projectId, string environmentId)
        {
            ProjectId = projectId;
            EnvironmentId = environmentId;
        }

        public static List<T> Get<T>() where T : MasterBase
        {
            var type = typeof(T);
            if (Cache.ContainsKey(type))
            {
                return Cache[type].Values.Select(m => m as T).ToList();
            }
            Debug.LogError($"not found. Type:{type.Name}");
            return new List<T>();
        }

        public static T Get<T>(long id) where T : MasterBase
        {
            var type = typeof(T);
            if (Cache.ContainsKey(type))
            {
                var dict = Cache[type];
                if (dict.ContainsKey(id))
                {
                    return dict[id] as T;
                }
            }
            Debug.LogError($"not found. Type:{type.Name} ID:{id}");
            return null;
        }
        
        public static async ValueTask PreloadAsync<T>() where T : MasterBase
        {
            var type = typeof(T);
            var url = GetUrl<T>();
            var json = await HttpClient.GetStringAsync(url);
            var response = JsonConvert.DeserializeObject<Response<T>>(json);
            var dict = response.data.ToDictionary(m => m.Id, m => (object)m);
            if (Cache.ContainsKey(type))
            {
                Cache.Remove(type);
            }
            Cache[type] = dict;
        }

        private class Response<T>
        {
            public int count;
            public List<T> data;
        }
    }

    public abstract class MasterBase
    {
        public long Id { get; set; }
    }
}
