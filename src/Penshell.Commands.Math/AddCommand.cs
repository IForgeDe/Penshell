namespace Penshell.Commands.Math
{
    using System;
    using System.CommandLine;
    using System.CommandLine.Invocation;
    using Penshell.Core;
    using Penshell.Core.Console;

    /// <summary>
    /// Calculates the addition of two values.
    /// </summary>
    public class AddCommand : PenshellCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddCommand"/> class.
        /// </summary>
        /// <param name="console">The <see cref="IPenshellConsole"/> instance.</param>
        public AddCommand(IPenshellConsole console)
            : base(console, "add", "Calculates the addition of two values.")
        {
            this.AddOption(
                new Option(
                    new string[] { "-x", "--first" },
                    "The first summand.")
                {
                    Argument = new Argument<double>(),
                    Required = true,
                });
            this.AddOption(
                new Option(
                    new string[] { "-y", "--second" },
                    "The second summand.")
                {
                    Argument = new Argument<double>(),
                    Required = true,
                });
        }

        /// <inheritdoc />
        protected override ICommandHandler CreateCommandHandler()
        {
            return CommandHandler.Create<double, double>((first, second) => this.Console.Out.Write(Convert.ToString(first + second, this.Console.CultureInfo)));
        }
    }
}
