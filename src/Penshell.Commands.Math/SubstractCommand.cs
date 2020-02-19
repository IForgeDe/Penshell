namespace Penshell.Commands.Math
{
    using System;
    using System.CommandLine;
    using System.CommandLine.Invocation;
    using Penshell.Core;
    using Penshell.Core.Console;

    /// <summary>
    /// Calculates the substraction of two values.
    /// </summary>
    public class SubstractCommand : PenshellCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubstractCommand"/> class.
        /// </summary>
        /// <param name="console">The <see cref="IPenshellConsole"/> instance.</param>
        public SubstractCommand(IPenshellConsole console)
            : base(console, "substract", "Calculates the substraction of two values.")
        {
            this.AddOption(
                new Option(
                    new string[] { "-x", "--minuend" },
                    "The minuend.")
                {
                    Argument = new Argument<double>(),
                    Required = true,
                });
            this.AddOption(
                new Option(
                    new string[] { "-y", "--subtrahend" },
                    "The subtrahend.")
                {
                    Argument = new Argument<double>(),
                    Required = true,
                });
        }

        /// <inheritdoc />
        protected override ICommandHandler CreateCommandHandler()
        {
            return CommandHandler.Create<double, double>((minuend, subtrahend) =>
            {
                this.Console.Out.Write(Convert.ToString(minuend - subtrahend, this.Console.CultureInfo));
            });
        }
    }
}
