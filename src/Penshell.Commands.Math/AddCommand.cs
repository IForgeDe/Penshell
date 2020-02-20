namespace Penshell.Commands.Math
{
    using System;
    using System.CommandLine;
    using System.CommandLine.Invocation;
    using Penshell.Core;

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

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="x">The first summand.</param>
        /// <param name="y">The second summand.</param>
        public void Execute(double x, double y)
        {
            this.Console.WriteLine(Convert.ToString(x + y, this.Console.CultureInfo));
        }

        /// <inheritdoc />
        protected override ICommandHandler CreateCommandHandler()
        {
            return CommandHandler.Create<double, double>((first, second) => this.Execute(first, second));
        }
    }
}
