namespace Penshell.Commands.Math
{
    using System;
    using System.CommandLine;
    using System.CommandLine.Invocation;
    using Penshell.Core;
    using Penshell.Core.Console;

    /// <summary>
    /// Calculates the division of two values.
    /// </summary>
    public class DivideCommand : PenshellCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DivideCommand"/> class.
        /// </summary>
        /// <param name="console">The <see cref="IPenshellConsole"/> instance.</param>
        public DivideCommand(IPenshellConsole console)
            : base(console, "divide", "Calculates the division of two values.")
        {
            this.AddOption(
                new Option(
                    new string[] { "-x", "--dividend" },
                    "The dividend.")
                {
                    Argument = new Argument<double>(),
                    Required = true,
                });
            this.AddOption(
                new Option(
                    new string[] { "-y", "--divisor" },
                    "The devisor.")
                {
                    Argument = new Argument<double>(),
                    Required = true,
                });
        }

        /// <inheritdoc />
        protected override ICommandHandler CreateCommandHandler()
        {
            return CommandHandler.Create<double, double>((dividend, divisor) =>
            {
                this.Console.Out.Write(Convert.ToString(dividend / divisor, this.Console.CultureInfo));
            });
        }
    }
}
