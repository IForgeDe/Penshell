namespace Penshell.Commands.Math
{
    using System.Threading.Tasks;
    using CliFx;
    using CliFx.Attributes;
    using CliFx.Services;
    using Dawn;

    [Command("math multiply", Description = "Calculates the multiplication of two values.")]
    public class MultiplyCommand : ICommand
    {
        [CommandOption("multiplicand", 'y', IsRequired = true, Description = "The multiplicand.")]
        public double Multiplicand { get; set; }

        [CommandOption("multiplyer", 'x', IsRequired = true, Description = "The multiplyer.")]
        public double Multiplyer { get; set; }

        public Task ExecuteAsync(IConsole console)
        {
            console = Guard.Argument(console).NotNull().Value;

            var result = this.Multiplyer * this.Multiplicand;
            console.Output.WriteLine(result);
            return Task.CompletedTask;
        }
    }
}
