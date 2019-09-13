namespace Penshell.Commands.Math
{
    using System.Threading.Tasks;
    using CliFx;
    using CliFx.Attributes;
    using CliFx.Services;
    using Dawn;

    [Command("math substract", Description = "Calculates the substraction of two values.")]
    public class SubstractCommand : ICommand
    {
        [CommandOption("minuend", 'x', IsRequired = true, Description = "The minuend.")]
        public double Minuend { get; set; }

        [CommandOption("subtrahend", 'y', IsRequired = true, Description = "The subtrahend.")]
        public double Subtrahend { get; set; }

        public Task ExecuteAsync(IConsole console)
        {
            console = Guard.Argument(console).NotNull().Value;

            var result = this.Minuend - this.Subtrahend;
            console.Output.WriteLine(result);
            return Task.CompletedTask;
        }
    }
}
