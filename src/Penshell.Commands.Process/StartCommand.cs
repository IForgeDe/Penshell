namespace Penshell.Commands.Process
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using CliFx;
    using CliFx.Attributes;
    using CliFx.Services;
    using Dawn;

    [Command("process start", Description = "Starts a process.")]
    public class StartCommand : ICommand
    {
        [CommandOption("path", 'p', IsRequired = true, Description = "The fully qualified name of the file for the new process, or the relative file name.")]
        public string Path { get; set; } = string.Empty;

        public static async Task<int> RunProcessAsync(string fileName, string args)
        {
            using var process = new Process
            {
                StartInfo =
                {
                    FileName = fileName,
                    Arguments = args,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                },
                EnableRaisingEvents = true,
            };
            return await RunProcessAsync(process).ConfigureAwait(false);
        }

        public Task ExecuteAsync(IConsole console)
        {
            console = Guard.Argument(console).NotNull().Value;
            _ = RunProcessAsync(this.Path, string.Empty);
            console.Output.WriteLine("Success");
            return Task.CompletedTask;
        }

        private static Task<int> RunProcessAsync(Process process)
        {
            var tcs = new TaskCompletionSource<int>();

            process.Exited += (s, ea) => tcs.SetResult(process.ExitCode);
            process.OutputDataReceived += (s, ea) => Console.WriteLine(ea.Data);
            process.ErrorDataReceived += (s, ea) => Console.WriteLine("ERR: " + ea.Data);

            bool started = process.Start();
            if (!started)
            {
                // you may allow for the process to be re-used (started = false)
                // but I'm not sure about the guarantees of the Exited event in such a case
                throw new InvalidOperationException("Could not start process: " + process);
            }

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            return tcs.Task;
        }
    }
}
