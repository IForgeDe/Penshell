namespace Penshell.Commands.Math
{
    using System;
    using System.CommandLine;
    using System.CommandLine.Invocation;
    using Penshell.Core;

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

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        public void Execute(double dividend, double divisor)
        {
            this.Console.WriteLine(Convert.ToString(dividend / divisor, this.Console.CultureInfo));
        }

        /// <inheritdoc />
        protected override ICommandHandler CreateCommandHandler()
        {
            return CommandHandler.Create<double, double>((dividend, divisor) => this.Execute(dividend, divisor));
        }
    }
}
