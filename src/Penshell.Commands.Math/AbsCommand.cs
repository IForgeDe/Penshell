namespace Penshell.Commands.Math
{
    using System.Threading.Tasks;
    using CliFx;
    using CliFx.Attributes;
    using CliFx.Services;
    using Dawn;

    [Command("math abs", Description = "Returns the absolute value of a specified number.")]
    public class AbsCommand : ICommand
    {
        [CommandOption("value", 'v', IsRequired = true, Description = "A number that is greater than or equal to MinValue, but less than or equal to MaxValue.")]
        public double Value { get; set; }

        /// <inheritdoc />
        public Task ExecuteAsync(IConsole console)
        {
            console = Guard.Argument(console).NotNull().Value;

            var result = System.Math.Abs(this.Value);
            console.Output.WriteLine(result);
            return Task.CompletedTask;
        }
    }
}
