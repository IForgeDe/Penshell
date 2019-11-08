namespace Penshell.Commands.Math
{
    using System.Threading;
    using System.Threading.Tasks;
    using CliFx;
    using CliFx.Attributes;
    using CliFx.Services;
    using Dawn;

    /// <summary>
    /// Calculates the substraction of two values.
    /// </summary>
    [Command("math substract", Description = "Calculates the substraction of two values.")]
    public class SubstractCommand : ICommand
    {
        /// <summary>
        /// Gets or sets the minuend.
        /// </summary>
        /// <value>
        /// The minuend.
        /// </value>
        [CommandOption("minuend", 'x', IsRequired = true, Description = "The minuend.")]
        public double Minuend { get; set; }

        /// <summary>
        /// Gets or sets the subtrahend.
        /// </summary>
        /// <value>
        /// The subtrahend.
        /// </value>
        [CommandOption("subtrahend", 'y', IsRequired = true, Description = "The subtrahend.")]
        public double Subtrahend { get; set; }

        /// <inheritdoc />
        public Task ExecuteAsync(IConsole console, CancellationToken cancellationToken)
        {
            console = Guard.Argument(console).NotNull().Value;

            var result = this.Minuend - this.Subtrahend;
            console.Output.WriteLine(result);
            return Task.CompletedTask;
        }
    }
}
