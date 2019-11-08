namespace Penshell.Commands.Math
{
    using System.Threading;
    using System.Threading.Tasks;
    using CliFx;
    using CliFx.Attributes;
    using CliFx.Services;
    using Dawn;

    /// <summary>
    /// Returns the absolute value of a specified number.
    /// </summary>
    [Command("math abs", Description = "Returns the absolute value of a specified number.")]
    public class AbsCommand : ICommand
    {
        /// <summary>
        /// Gets or sets a number that is greater than or equal to MinValue, but less than or equal to MaxValue.
        /// </summary>
        /// <value>
        /// A number that is greater than or equal to MinValue, but less than or equal to MaxValue.
        /// </value>
        [CommandOption("value", 'v', IsRequired = true, Description = "A number that is greater than or equal to MinValue, but less than or equal to MaxValue.")]
        public double Value { get; set; }

        /// <inheritdoc />
        public Task ExecuteAsync(IConsole console, CancellationToken cancellationToken)
        {
            console = Guard.Argument(console).NotNull().Value;

            var result = System.Math.Abs(this.Value);
            console.Output.WriteLine(result);
            return Task.CompletedTask;
        }
    }
}
