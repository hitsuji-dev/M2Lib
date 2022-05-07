using System;
using System.Collections.Generic;

namespace M2Lib.Utils
{
    public static class EnumUtil
    {
        private static Dictionary<Type, object> _cache = new Dictionary<Type, object>();

        public static List<T> GetEnums<T>() where T : Enum
        {
            var type = typeof(T);
            if (_cache.TryGetValue(type, out var obj))
            {
                return obj as List<T>;
            }

            var list = new List<T>();
            foreach (var e in Enum.GetValues(typeof(T)))
            {
                list.Add((T)e);
            }
            _cache[type] = list;
            return list;
        }

        public static int GetEnumCount<T>() where T : Enum
        {
            return GetEnums<T>().Count;
        }
    }
}
