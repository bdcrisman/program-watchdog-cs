using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ProgramWatchdog
{
    public class ProcessManager
    {
        private CancellationTokenSource _cts;

        /// <summary>
        /// Awaits the 
        /// </summary>
        /// <returns></returns>
        public async Task WaitForPidToExitAsync(int pid, List<string> appsToCleanup)
        {
            try
            {
                // log
                _cts = new CancellationTokenSource();
                await GetProcessById(pid)?.WaitForExitAsync(_cts.Token);
                await Cleanup(appsToCleanup);
            }
            catch { }
        }

        /// <summary>
        /// Cleanup apps and token.
        /// </summary>
        /// <param name="apps"></param>
        private async Task Cleanup(List<string> apps)
        {
            DisposeTokenSource();
            
            if (apps == null)
            {
                // log
                return;
            }

            // log: cleaning up apps...

            foreach(var app in apps)
            {
                try
                {
                    var procs = new List<Process>();
                    procs.AddRange(Process.GetProcessesByName(app));
                    procs.AddRange(Process.GetProcessesByName(Path.GetFileNameWithoutExtension(app)));

                    foreach(var p in procs)
                    {
                        // log: closing app: {app}
                        p.CloseMainWindow();
                    }

                    await Task.Delay(250);

                    foreach (var p in procs)
                    {
                        // log: killing app: {app}
                        p.Kill();
                    }
                }
                catch{ }
            };
        }

        /// <summary>
        /// Gets the process by ID.
        /// </summary>
        /// <returns></returns>
        private Process GetProcessById(int pid)
        {
            try
            {
                return Process.GetProcessById(pid);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Disposes the token source.
        /// </summary>
        private void DisposeTokenSource()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }
    }
}
