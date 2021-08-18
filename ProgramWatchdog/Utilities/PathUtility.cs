using System;
using System.IO;

namespace ProgramWatchdog
{
    public class PathUtility
    {
        // global
        public static string LocalAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public static string AppDataDir = Path.Combine(LocalAppData, "path", "to", "Watchdog");

        // logger
        public static string LogDir = Path.Combine(AppDataDir, "Logs");
    }
}
