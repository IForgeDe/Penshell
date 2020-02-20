namespace Penshell.Commands.Math
{
    using System;
    using System.CommandLine;
    using System.CommandLine.Invocation;
    using Penshell.Core;
    using Penshell.Core.Console;

    /// <summary>
    /// Calculates the multiplication of two values.
    /// </summary>
    public class MultiplyCommand : PenshellCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiplyCommand"/> class.
        /// </summary>
        /// <param name="console">The <see cref="IPenshellConsole"/> instance.</param>
        public MultiplyCommand(IPenshellConsole console)
            : base(console, "multiply", "Calculates the multiplication of two values.")
        {
            this.AddOption(
                new Option(
                    new string[] { "-x", "--multiplyer" },
                    "The multiplyer.")
                {
                    Argument = new Argument<double>(),
                    Required = true,
                });
            this.AddOption(
                new Option(
                    new string[] { "-y", "--multiplicand" },
                    "The multiplicand.")
                {
                    Argument = new Argument<double>(),
                    Required = true,
                });
        }

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="multiplyer">The multiplyer.</param>
        /// <param name="multiplicand">The divisor.</param>
        public void Execute(double multiplyer, double multiplicand)
        {
            this.Console.WriteLine(Convert.ToString(multiplyer * multiplicand, this.Console.CultureInfo));
        }

        /// <inheritdoc />
        protected override ICommandHandler CreateCommandHandler()
        {
            return CommandHandler.Create<double, double>((multiplyer, multiplicand) => this.Execute(multiplyer, multiplicand));
        }
    }
}
