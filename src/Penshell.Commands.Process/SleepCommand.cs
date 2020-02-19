namespace Penshell.Commands.Process
{
    using System;
    using System.CommandLine;
    using System.CommandLine.Invocation;
    using System.Threading;
    using Penshell.Core;
    using Penshell.Core.Console;

    /// <summary>
    /// Command to let the current process sleep for a defined time.
    /// </summary>
    public class SleepCommand : PenshellCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SleepCommand"/> class.
        /// </summary>
        /// <param name="console">The <see cref="IPenshellConsole"/> instance.</param>
        public SleepCommand(IPenshellConsole console)
            : base(console, "sleep", "Let the current process sleep for a defined time.")
        {
            this.AddOption(
                new Option(
                    new string[] { "-H", "--hours" },
                    "The time in hours that the process should sleep.")
                {
                    Argument = new Argument<int>(),
                    Required = false,
                });
            this.AddOption(
                new Option(
                    new string[] { "-z", "--milliseconds" },
                    "The time in milliseconds that the process should sleep.")
                {
                    Argument = new Argument<int>(),
                    Required = false,
                });
            this.AddOption(
                new Option(
                    new string[] { "-m", "--minutes" },
                    "The time in minutes that the process should sleep.")
                {
                    Argument = new Argument<int>(),
                    Required = false,
                });
            this.AddOption(
                new Option(
                    new string[] { "-s", "--seconds" },
                    "The time in seconds that the process should sleep.")
                {
                    Argument = new Argument<int>(),
                    Required = false,
                });
        }

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="milliseconds">The milliseconds to sleep.</param>
        /// <param name="seconds">The seconds to sleep.</param>
        /// <param name="minutes">The minutes to sleep.</param>
        /// <param name="hours">The hours to sleep.</param>
        public void Execute(int milliseconds, int seconds, int minutes, int hours)
        {
            var delay = CalculateTimeSpanFromArguments(milliseconds, seconds, minutes, hours);

            Thread.Sleep(delay);

            this.Console.Out.Write("Success");
        }

        /// <summary>
        /// Calculates the <see cref="TimeSpan"/> instance from the given arguments.
        /// </summary>
        /// <param name="milliseconds">The milliseconds to sleep.</param>
        /// <param name="seconds">The seconds to sleep.</param>
        /// <param name="minutes">The minutes to sleep.</param>
        /// <param name="hours">The hours to sleep.</param>
        /// <returns>The <see cref="TimeSpan"/> instance of the calculated delay.</returns>
        internal TimeSpan CalculateTimeSpanFromArguments(int milliseconds, int seconds, int minutes, int hours)
        {
            var delay = TimeSpan.Zero;

            if (milliseconds > 0)
            {
                delay = delay.Add(TimeSpan.FromMilliseconds(milliseconds));
            }

            if (seconds > 0)
            {
                delay = delay.Add(TimeSpan.FromSeconds(seconds));
            }

            if (minutes > 0)
            {
                delay = delay.Add(TimeSpan.FromMinutes(minutes));
            }

            if (hours > 0)
            {
                delay = delay.Add(TimeSpan.FromHours(hours));
            }

            return delay;
        }

        /// <inheritdoc />
        protected override ICommandHandler CreateCommandHandler()
        {
            return CommandHandler.Create<int, int, int, int>((milliseconds, seconds, minutes, hours) => this.Execute(milliseconds, seconds, minutes, hours));
        }
    }
}
