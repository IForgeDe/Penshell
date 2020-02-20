namespace Penshell.Commands.Process
{
    using System;
    using System.CommandLine;
    using System.CommandLine.Invocation;
    using System.IO;
    using System.Threading.Tasks;
    using Dawn;
    using Penshell.Core;

    /// <summary>
    /// Starts a process.
    /// </summary>
    public class StartCommand : PenshellCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StartCommand"/> class.
        /// </summary>
        /// <param name="console">The <see cref="IPenshellConsole"/> instance.</param>
        public StartCommand(IPenshellConsole console)
            : base(console, "start", "Starts a process.")
        {
            this.AddOption(
                new Option(
                    new string[] { "-p", "--path" },
                    "The fully qualified name of the file for the new process, or the relative file name.")
                {
                    Argument = new Argument<FileInfo>(),
                    Required = true,
                });
        }

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="fileInfo">The <see cref="FileInfo"/> instance to start.</param>
        public void Execute(FileInfo fileInfo)
        {
            fileInfo = Guard.Argument(fileInfo).NotNull();
            _ = RunProcessAsync(fileInfo.FullName, string.Empty);
            this.Console.WriteLine("Success");
        }

        /// <inheritdoc />
        protected override ICommandHandler CreateCommandHandler()
        {
            return CommandHandler.Create<FileInfo>((path) => this.Execute(path));
        }

        private static async Task<int> RunProcessAsync(string fileName, string args)
        {
            using var process = new System.Diagnostics.Process
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

        private static Task<int> RunProcessAsync(System.Diagnostics.Process process)
        {
            var tcs = new TaskCompletionSource<int>();

            process.Exited += (s, ea) => tcs.SetResult(process.ExitCode);
            process.OutputDataReceived += (s, ea) => System.Console.WriteLine(ea.Data);
            process.ErrorDataReceived += (s, ea) => System.Console.WriteLine("ERR: " + ea.Data);

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
