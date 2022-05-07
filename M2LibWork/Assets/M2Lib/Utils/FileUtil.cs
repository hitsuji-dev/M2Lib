using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace M2Lib.Utils
{
    public static class FileUtil
    {
        public static async Task<byte[]> TempReadAsync(this string fileName)
        {
            var fullpath = Application.temporaryCachePath + "/" + fileName;
            Debug.Log(fullpath);
            if (!File.Exists(fullpath))
            {
                return null;
            }

            try
            {
                using var reader = new FileStream(fullpath, FileMode.Open);
                var buffer = new byte[reader.Length];
                await reader.ReadAsync(buffer, 0, buffer.Length);
                return buffer;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return null;
            }
        }

        public static async Task TempWriteAsync(this string fileName, byte[] bin)
        {
            var fullpath = Application.temporaryCachePath + "/" + fileName;
            try
            {
                using var writer = new FileStream(fullpath, FileMode.Create);
                await writer.WriteAsync(bin, 0, bin.Length);
            }
            catch (Exception)
            {
            }
        }
    }
}
