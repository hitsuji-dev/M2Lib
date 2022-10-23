using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace M2Lib.Utils
{
    public static class UserDataManager
    {
        private static Dictionary<Type, IUserData> _instanceCache = new Dictionary<Type, IUserData>();

        private static object _lockObject = new object();

        public static T Get<T>() where T : class, IUserData, new()
        {
            var type = typeof(T);
            if (_instanceCache.TryGetValue(type, out var userData))
            {
                return userData as T;
            }

            lock (_lockObject)
            {
                if (_instanceCache.TryGetValue(type, out userData))
                {
                    return userData as T;
                }

                var path = GetSaveFilePath<T>();
                if (File.Exists(path))
                {
                    var json = File.ReadAllText(path);
                    if (!string.IsNullOrEmpty(json))
                    {
                        userData = JsonConvert.DeserializeObject<T>(json);
                        if (userData == null)
                        {
                            throw new Exception("Savedata is broken!!");
                        }
                        return userData as T;
                    }
                }

                userData = new T();
                userData.OnCreate();
                SaveToFile<T>();
                _instanceCache[type] = userData;
                return userData as T;
            }
        }

        public static void SaveToFile<T>() where T : IUserData
        {
            if (_instanceCache.TryGetValue(typeof(T), out var userData))
            {
                SaveToFileInternal(userData);
            }
        }

        public static void SaveToFile<T>(T userData) where T : IUserData
        {
            SaveToFileInternal(userData);
        }

        private static void SaveToFileInternal<T>(T userData) where T : IUserData
        {
            var path = GetSaveFilePath<T>();
            var format = Application.isEditor ? Formatting.Indented : Formatting.None;
            var json = JsonConvert.SerializeObject(userData, format);
            File.WriteAllText(path, json);
        }

        private static string GetSaveFilePath<T>()
        {
            return $"{GetUserDataDir()}/{typeof(T).Name}.json";
        }

        public static string GetUserDataDir()
        {
            string dir;
            if (Application.isEditor)
            {
                dir = Application.streamingAssetsPath;
            }
            else if (Application.isMobilePlatform)
            {
                dir = Application.persistentDataPath;
            }
            else
            {
                dir = Directory.GetCurrentDirectory();
            }

            dir += "/userdata";
            Directory.CreateDirectory(dir);
            return dir;
        }
    }

    public interface IUserData
    {
        void OnCreate();
    }
}
