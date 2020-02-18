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
        /// <param name="console">The <see cref="IConsole"/> instance.</param>
        /// <param name="formatProvider">The <see cref="IFormatProvider"/> instance.</param>
        public DivideCommand(IConsole console, IFormatProvider formatProvider)
            : base(console, "divide", "Calculates the division of two values.")
        {
            this.FormatProvider = formatProvider ?? throw new ArgumentNullException(nameof(formatProvider));
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
        /// Gets the <see cref="IFormatProvider"/> instance.
        /// </summary>
        public IFormatProvider FormatProvider { get; }

        /// <inheritdoc />
        protected override ICommandHandler CreateCommandHandler()
        {
            return CommandHandler.Create<double, double>((dividend, divisor) =>
            {
                this.Console.Out.Write(Convert.ToString(dividend / divisor, this.FormatProvider));
            });
        }
    }
}
