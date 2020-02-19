namespace Penshell.Commands.Math
{
    using System;
    using System.CommandLine;
    using System.CommandLine.Invocation;
    using Penshell.Core;
    using Penshell.Core.Console;

    /// <summary>
    /// Returns the absolute value of a specified number.
    /// </summary>
    public class AbsCommand : PenshellCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AbsCommand"/> class.
        /// </summary>
        /// <param name="console">The <see cref="IPenshellConsole"/> instance.</param>
        public AbsCommand(IPenshellConsole console)
            : base(console, "abs", "Returns the absolute value of a specified number.")
        {
            this.AddOption(
                new Option(
                    new string[] { "-v", "--value" },
                    "A number that is greater than or equal to MinValue, but less than or equal to MaxValue.")
                {
                    Argument = new Argument<double>(),
                    Required = true,
                });
        }

        /// <inheritdoc />
        protected override ICommandHandler CreateCommandHandler()
        {
            return CommandHandler.Create<double>((value) => this.Console.Out.Write(Convert.ToString(System.Math.Abs(value), this.Console.CultureInfo)));
        }
    }
}
