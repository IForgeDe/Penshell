namespace Penshell.Commands.Math
{
    using System;
    using System.CommandLine;
    using System.CommandLine.Invocation;
    using Penshell.Core;

    /// <summary>
    /// Calculates the multiplication of two values.
    /// </summary>
    public class MultiplyCommand : PenshellCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiplyCommand"/> class.
        /// </summary>
        /// <param name="console">The <see cref="IConsole"/> instance.</param>
        /// <param name="formatProvider">The <see cref="IFormatProvider"/> instance.</param>
        public MultiplyCommand(IConsole console, IFormatProvider formatProvider)
            : base(console, formatProvider, "multiply", "Calculates the multiplication of two values.")
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

        /// <inheritdoc />
        protected override ICommandHandler CreateCommandHandler()
        {
            return CommandHandler.Create<double, double>((multiplyer, multiplicand) =>
            {
                this.Console.Out.Write(Convert.ToString(multiplyer * multiplicand, this.FormatProvider));
            });
        }
    }
}
