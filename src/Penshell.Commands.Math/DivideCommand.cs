namespace Penshell.Commands.Math
{
    using System;
    using System.Globalization;
    using System.Threading;
    using System.Threading.Tasks;
    using CliFx;
    using CliFx.Attributes;
    using CliFx.Exceptions;
    using CliFx.Services;
    using Dawn;

    /// <summary>
    /// Calculates the division of two values.
    /// </summary>
    [Command("math divide", Description = "Calculates the division of two values.")]
    public class DivideCommand : ICommand
    {
        /// <summary>
        /// Gets or sets the dividend.
        /// </summary>
        /// <value>
        /// The dividend.
        /// </value>
        [CommandOption("dividend", 'x', IsRequired = true, Description = "The dividend.")]
        public double Dividend { get; set; }

        /// <summary>
        /// Gets or sets the devisor.
        /// </summary>
        /// <value>
        /// The devisor.
        /// </value>
        [CommandOption("divisor", 'y', IsRequired = true, Description = "The devisor.")]
        public double Divisor { get; set; }

        /// <inheritdoc />
        public Task ExecuteAsync(IConsole console, CancellationToken cancellationToken)
        {
            console = Guard.Argument(console).NotNull().Value;

            if (Math.Abs(this.Divisor) < double.Epsilon)
            {
                throw new CommandException("Division by zero is not supported.", 1337);
            }

            var result = this.Dividend / this.Divisor;
            console.Output.WriteLine(Convert.ToString(result, CultureInfo.InvariantCulture));

            return Task.CompletedTask;
        }
    }
}
