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
            if (args.Length < 4)
            {
                Console.WriteLine($"Usage: > ProgramWatchdog.exe <pid> <loglevel> <numDaysGen> <numDaysException>");
                Environment.Exit(0);
            }

            #region Parse args
            if (!int.TryParse(args[0], out int pid))
            {
                Console.WriteLine("PID must be an integer.");
                Environment.Exit(0);
            }

            if (!int.TryParse(args[1], out int logLevel))
            {
                Console.WriteLine("Log-level must be an integer.");
                Environment.Exit(0);
            }

            if (!int.TryParse(args[1], out int numDaysGen))
            {
                Console.WriteLine("Number-of-days-general must be an integer.");
                Environment.Exit(0);
            }

            if (!int.TryParse(args[1], out int numDaysExc))
            {
                Console.WriteLine("Number-of-days-exception must be an integer.");
                Environment.Exit(0);
            }
            #endregion

            // initialize logging

            await new ProcessManager().WaitForPidToExitAsync(pid, Apps);
        }
    }
}
