using System;
using System.Linq;

namespace M2Lib
{
    public static class PasswordGenerator
    {
        private static readonly string passwordChars = "0123456789abcdefghijklmnopqrstuvwxyz";
        private static readonly Random random = new Random();

        public static string create(int length)
        {
            var chars = Enumerable.Range(0, length - 1).Select(_ => passwordChars[random.Next(passwordChars.Length - 1)]);
            return string.Join("", chars);
        }
    }
}