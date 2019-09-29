namespace Penshell.Commands.Math
{
    using System.Threading.Tasks;
    using CliFx;
    using CliFx.Attributes;
    using CliFx.Services;
    using Dawn;

    /// <summary>
    /// Calculates the multiplication of two values.
    /// </summary>
    [Command("math multiply", Description = "Calculates the multiplication of two values.")]
    public class MultiplyCommand : ICommand
    {
        /// <summary>
        /// Gets or sets the multiplicand.
        /// </summary>
        /// <value>
        /// The multiplicand.
        /// </value>
        [CommandOption("multiplicand", 'y', IsRequired = true, Description = "The multiplicand.")]
        public double Multiplicand { get; set; }

        /// <summary>
        /// Gets or sets the multiplyer.
        /// </summary>
        /// <value>
        /// The multiplyer.
        /// </value>
        [CommandOption("multiplyer", 'x', IsRequired = true, Description = "The multiplyer.")]
        public double Multiplyer { get; set; }

        /// <inheritdoc />
        public Task ExecuteAsync(IConsole console)
        {
            console = Guard.Argument(console).NotNull().Value;

            var result = this.Multiplyer * this.Multiplicand;
            console.Output.WriteLine(result);
            return Task.CompletedTask;
        }
    }
}
