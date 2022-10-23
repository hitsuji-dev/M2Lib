using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace M2Lib.Utils
{
    public static class StringUtil
    {
        private static Encoding Utf8 { get; } = Encoding.UTF8;

        private static string StringChar { get; } = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static byte[] ToBytes(this string str) => Utf8.GetBytes(str);

        public static string ToUTF8String(this byte[] bytes) => Utf8.GetString(bytes);

        public static string CreateRandomString(int length)
        {
            var stringBuilder = new StringBuilder(length);
            for (var i = 0; i < length; i++)
            {
                var pos = UnityEngine.Random.Range(0, StringChar.Length);
                var c = StringChar[pos];
                stringBuilder.Append(c);
            }

            return stringBuilder.ToString();
        }

        public static string ToCSV<T>(IEnumerable<T> src)
        {
            if (src == null)
            {
                return "";
            }
            return string.Join(',', src);
        }

        public static IEnumerable<int> Int32FromCSV(string csv)
        {
            return StringFromCSV(csv).Select(s => Convert.ToInt32(s));
        }

        public static IEnumerable<string> StringFromCSV(string csv)
        {
            if (string.IsNullOrEmpty(csv))
            {
                return Array.Empty<string>();
            }
            return csv.Split(',') ?? Array.Empty<string>();
        }
    }
}
