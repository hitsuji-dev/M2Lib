using System.Text;

namespace M2Lib.Utils
{
    public static class StringUtil
    {
        private static Encoding utf8 = Encoding.UTF8;

        public static byte[] ToBytes(this string str) => utf8.GetBytes(str);

        public static string ToUTF8String(this byte[] bytes) => utf8.GetString(bytes);
    }
}
