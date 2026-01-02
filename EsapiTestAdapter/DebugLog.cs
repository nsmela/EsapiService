using System;
using System.IO;

namespace EsapiTestAdapter
{
    public static class DebugLog
    {
        public static void Write(string message)
        {
            try
            {
                var line = $"[{DateTime.Now:HH:mm:ss.fff}] {message}"; // No \r\n, Console adds it

                // 1. Write to Debugger (Viewable if you Right Click -> Debug Tests)
                System.Diagnostics.Trace.WriteLine(line);

                // 2. Write to Standard Output (Viewable in Test Explorer -> "Output" link)
                Console.WriteLine(line);
            }
            catch { }
        }
    }
}