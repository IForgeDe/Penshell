namespace Penshell.Commands.Math
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using CliFx;
    using CliFx.Attributes;
    using CliFx.Services;
    using Dawn;

    /// <summary>
    /// Calculates the addition of two values.
    /// </summary>
    [Command("math add", Description = "Calculates the addition of two values.")]
    public class AddCommand : ICommand
    {
        /// <summary>
        /// Gets or sets the first summand.
        /// </summary>
        /// <value>
        /// The first summand.
        /// </value>
        [CommandOption("first", 'x', IsRequired = true, Description = "The first summand.")]
        public double FirstSummand { get; set; }

        /// <summary>
        /// Gets or sets the second summand.
        /// </summary>
        /// <value>
        /// The second summand.
        /// </value>
        [CommandOption("second", 'y', IsRequired = true, Description = "The second summand.")]
        public double SecondSummand { get; set; }

        /// <inheritdoc />
        public Task ExecuteAsync(IConsole console, CancellationToken cancellationToken)
        {
            console = Guard.Argument(console).NotNull().Value;

            var result = this.FirstSummand + this.SecondSummand;
            console.Output.WriteLine(result);
            return Task.CompletedTask;
        }
    }
}
