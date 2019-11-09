namespace Penshell.Commands.Process
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;
    using CliFx;
    using CliFx.Attributes;
    using CliFx.Services;
    using Dawn;

    /// <summary>
    /// Command to let the current process sleep for a defined time.
    /// </summary>
    [Command("process sleep", Description = "Opens the browser of a uri.")]
    public class SleepCommand : ICommand
    {
        /// <summary>
        /// Gets or sets the time in milliseconds that the process should sleep.
        /// </summary>
        /// <value>
        /// The time in milliseconds that the process should sleep.
        /// </value>
        [CommandOption("milliseconds", 'm', IsRequired = true, Description = "The time in milliseconds that the process should sleep.")]
        public int? Milliseconds { get; set; }

        /// <inheritdoc />
        public Task ExecuteAsync(IConsole console)
        {
            console = Guard.Argument(console).NotNull().Value;
            this.Milliseconds = Guard.Argument(this.Milliseconds).NotNull().NotNegative().Value;

            Thread.Sleep(this.Milliseconds.Value);

            console.Output.WriteLine("Success");
            return Task.CompletedTask;
        }
    }
}
