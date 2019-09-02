namespace Penshell.Commands.Math
{
    using System.Threading.Tasks;
    using CliFx;
    using CliFx.Attributes;
    using CliFx.Services;

    [Command("math add", Description = "Calculates the addition of two values.")]
    public class AddCommand : ICommand
    {
        [CommandOption("first", 'x', IsRequired = true, Description = "The first summand.")]
        public double FirstSummand { get; set; }

        [CommandOption("second", 'y', IsRequired = true, Description = "The second summand.")]
        public double SecondSummand { get; set; }

        public Task ExecuteAsync(IConsole console)
        {
            var result = this.FirstSummand + this.SecondSummand;
            console.Output.WriteLine(result);
            return Task.CompletedTask;
        }
    }
}