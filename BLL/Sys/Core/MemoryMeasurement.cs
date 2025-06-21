using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Sys.Core
{
    public static class MemoryMeasurement
    {
        #region Core_Methods:

        public static long GetStringSizeInBytes(string s)
        {
            if (string.IsNullOrEmpty(s))
                return 0;

            // Each character is 2 bytes in UTF-16 + estimated object overhead
            return sizeof(char) * s.Length + 20;
        }

        public static long GetDictionaryMemorySize(Dictionary<string, string> dictionary)
        {
            if (dictionary == null || dictionary.Count == 0)
                return 0;

            long totalSize = 0;
            foreach (var kvp in dictionary)
            {
                totalSize += GetStringSizeInBytes(kvp.Key);
                totalSize += GetStringSizeInBytes(kvp.Value);
            }

            // Estimate dictionary internal structure overhead
            long estimatedDictionaryOverhead = 32 + (dictionary.Count * 16);
            totalSize += estimatedDictionaryOverhead;

            return totalSize;
        }

        public static string GetFormattedDictionaryMemorySize(Dictionary<string, string> dictionary)
        {
            long bytes = GetDictionaryMemorySize(dictionary);
            return FormatBytes(bytes);
        }

        public static string FormatBytes(long bytes)
        {
            if (bytes < 1024) return $"{bytes} B";
            double kb = bytes / 1024.0;
            if (kb < 1024) return $"{kb:F2} KB";
            double mb = kb / 1024.0;
            if (mb < 1024) return $"{mb:F2} MB";
            double gb = mb / 1024.0;
            return $"{gb:F2} GB";
        }

        #endregion

    }
}
