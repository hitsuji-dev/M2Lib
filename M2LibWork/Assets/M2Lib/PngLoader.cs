using System.IO;
using UnityEngine;

namespace M2Lib
{
    /// <summary>
    /// https://qiita.com/r-ngtm/items/6cff25643a1a6ba82a6c
    /// </summary>
    public static class PngLoader
    {
        public static Texture2D ReadPng(string path)
        {
            byte[] readBinary = ReadPngFile(path);

            int pos = 16; // 16バイトから開始

            int width = 0;
            for (int i = 0; i < 4; i++)
            {
                width = width * 256 + readBinary[pos++];
            }

            int height = 0;
            for (int i = 0; i < 4; i++)
            {
                height = height * 256 + readBinary[pos++];
            }

            Texture2D texture = new Texture2D(width, height);
            texture.LoadImage(readBinary);

            return texture;
        }

        private static byte[] ReadPngFile(string path)
        {
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            BinaryReader bin = new BinaryReader(fileStream);
            byte[] values = bin.ReadBytes((int)bin.BaseStream.Length);

            bin.Close();

            return values;
        }

        private static Texture ReadTexture(string path, int width, int height)
        {
            byte[] readBinary = ReadPngFile(path);

            Texture2D texture = new Texture2D(width, height);
            texture.LoadImage(readBinary);

            return texture;
        }
    }
}