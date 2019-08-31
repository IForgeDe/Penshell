namespace Penshell.Commands.Math
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using CliFx.Attributes;
    using CliFx.Services;
    using Penshell.Core;
    using Serilog;

    [Command("math add", Description = "Calculates the addition of two values.")]
    public class AddCommand : CommandBase
    {
        public AddCommand(ILogger logger)
            : base(logger)
        {
        }

        [CommandOption("first", 'x', IsRequired = true, Description = "The first summand.")]
        public double FirstSummand { get; set; }

        [CommandOption("second", 'y', IsRequired = true, Description = "The second summand.")]
        public double SecondSummand { get; set; }

        public override Task ExecuteAsync(IConsole console)
        {
            var result = this.FirstSummand + this.SecondSummand;
            console.Output.WriteLine(Convert.ToString(result, CultureInfo.InvariantCulture));

            return Task.CompletedTask;
        }
    }
}