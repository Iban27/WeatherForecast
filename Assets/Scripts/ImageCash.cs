using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Security.Cryptography;
using System.Text;
using Random = UnityEngine.Random;

namespace Utils
{
    public static class ImageCache
    {
        public static readonly string CashDir = Path.Combine(Application.persistentDataPath, "imageCash");

        public static bool TryLoadTexture(string url, TimeSpan ttl, out Texture2D texture)
        {
            Debug.Log(CashDir);
            texture = null;
            try
            {
                var path = PathFor(url);
                if (File.Exists(path) == false)
                {
                    return false;
                }
                var age =  DateTime.UtcNow -  File.GetLastWriteTime(path);
                if (age > ttl)
                {
                    return false;
                }
                var bytes = File.ReadAllBytes(path);
                if (bytes == null || bytes.Length == 0)
                {
                    return false;
                }

                var tex = new Texture2D(2, 2, TextureFormat.RGBA32, false, false);
                if (ImageConversion.LoadImage(tex, bytes) == false)
                {
                    Debug.Log("Failed to load image");
                    UnityEngine.Object.Destroy(tex);
                    return false;
                }
                Debug.Log("Loaded image");
                texture = tex;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static string PathFor(string url)
        {
            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(url));
            var name = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant() + ".png";
            return Path.Combine(CashDir, name);
        }

        public static void SaveTexture(string url, Texture2D texture)
        {
            try
            {
                if (texture == null)
                {
                    Debug.LogWarning("texture is null");
                    return;
                }
                var png = ImageConversion.EncodeToPNG(texture);
                if (png == null || png.Length == 0)
                {
                    Debug.LogWarning("png is null");
                    return;
                }
                if (Directory.Exists(CashDir) == false)
                {
                    Directory.CreateDirectory(CashDir);
                    Debug.Log($"Сохранил {CashDir}");
                }
                File.WriteAllBytes(PathFor(url), png);
                Debug.LogWarning("Не удалось сохранить, нет файла");
                
            }
            catch(Exception e)
            {
                Debug.LogWarning($"[imageCash] Error saving texture: {e.Message}]");
            }
        }
    }
}
