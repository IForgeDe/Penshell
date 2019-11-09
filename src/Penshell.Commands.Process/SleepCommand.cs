namespace Penshell.Commands.Process
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using CliFx;
    using CliFx.Attributes;
    using CliFx.Services;
    using Dawn;

    /// <summary>
    /// Command to let the current process sleep for a defined time.
    /// </summary>
    [Command("process sleep", Description = "Let the current process sleep for a defined time.")]
    public class SleepCommand : ICommand
    {
        /// <summary>
        /// Gets or sets the time in hours that the process should sleep.
        /// </summary>
        /// <value>
        /// The time in hours that the process should sleep.
        /// </value>
        [CommandOption("hours", 'H', IsRequired = false, Description = "The time in hours that the process should sleep.")]
        public int? Hours { get; set; }

        /// <summary>
        /// Gets or sets the time in milliseconds that the process should sleep.
        /// </summary>
        /// <value>
        /// The time in milliseconds that the process should sleep.
        /// </value>
        [CommandOption("milliseconds", 'z', IsRequired = false, Description = "The time in milliseconds that the process should sleep.")]
        public int? Milliseconds { get; set; }

        /// <summary>
        /// Gets or sets the time in minutes that the process should sleep.
        /// </summary>
        /// <value>
        /// The time in minutes that the process should sleep.
        /// </value>
        [CommandOption("minutes", 'm', IsRequired = false, Description = "The time in minutes that the process should sleep.")]
        public int? Minutes { get; set; }

        /// <summary>
        /// Gets or sets the time in seconds that the process should sleep.
        /// </summary>
        /// <value>
        /// The time in seconds that the process should sleep.
        /// </value>
        [CommandOption("seconds", 's', IsRequired = false, Description = "The time in seconds that the process should sleep.")]
        public int? Seconds { get; set; }

        /// <inheritdoc />
        public Task ExecuteAsync(IConsole console)
        {
            console = Guard.Argument(console).NotNull().Value;

            var delay = CalculateTimeSpanFromArguments();

            delay = Guard.Argument(delay)
                .NotNegative()
                .Value;

            Thread.Sleep(delay);

            console.Output.WriteLine("Success");
            return Task.CompletedTask;
        }

        /// <summary>
        /// Calculates the <see cref="TimeSpan"/> instance from the given arguments.
        /// </summary>
        /// <returns>The calculated <see cref="TimeSpan"/> instance from the given arguments.</returns>
        internal TimeSpan CalculateTimeSpanFromArguments()
        {
            var delay = TimeSpan.Zero;

            if (this.Milliseconds != null)
            {
                delay = delay.Add(TimeSpan.FromMilliseconds(this.Milliseconds.Value));
            }

            if (this.Seconds != null)
            {
                delay = delay.Add(TimeSpan.FromSeconds(this.Seconds.Value));
            }

            if (this.Minutes != null)
            {
                delay = delay.Add(TimeSpan.FromMinutes(this.Minutes.Value));
            }

            if (this.Hours != null)
            {
                delay = delay.Add(TimeSpan.FromHours(this.Hours.Value));
            }

            return delay;
        }
    }
}
