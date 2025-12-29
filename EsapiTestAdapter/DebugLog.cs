using System;
using System.IO;

namespace EsapiTestAdapter
{
    public static class DebugLog
    {
        // Saves to your Desktop -> EsapiDebug.txt
        private static readonly string LogPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            "EsapiDebug.txt");

        public static void Write(string message)
        {
            try
            {
                var line = $"[{DateTime.Now:HH:mm:ss.fff}] {message}\r\n";
                File.AppendAllText(LogPath, line);
            }
            catch { /* Ignore logging errors to prevent recursive crashes */ }
        }
    }
}