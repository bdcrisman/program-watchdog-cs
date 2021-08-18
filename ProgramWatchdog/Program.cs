using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramWatchdog
{
    class Program
    {
        private static readonly List<string> Apps = new List<string>
        {
            // any applications that you want closed
            ""
        };

        static async Task Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine($"Usage: > ProgramWatchdog.exe <pid>");
                Environment.Exit(0);
            }

            // parse PID
            if (!int.TryParse(args[0], out int pid))
            {
                Console.WriteLine("PID must be an integer.");
                Environment.Exit(0);
            }

            // initialize logging

            await new ProcessManager().WaitForPidToExitAsync(pid, Apps);
        }
    }
}
