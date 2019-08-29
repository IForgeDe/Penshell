namespace Penshell.Commands.Math
{
    using System;
    using System.Threading.Tasks;
    using CliFx.Attributes;
    using CliFx.Exceptions;
    using CliFx.Services;
    using Penshell.Core;
    using Serilog;

    [Command("math divide", Description = "Calculates the division of two values.")]
    public class DivideCommand : CommandBase
    {
        public DivideCommand(ILogger logger)
            : base(logger)
        {
        }

        [CommandOption("dividend", 'x', IsRequired = true, Description = "The dividend.")]
        public double Dividend { get; set; }

        [CommandOption("divisor", 'y', IsRequired = true, Description = "The devisor.")]
        public double Divisor { get; set; }

        public override Task ExecuteAsync(IConsole console)
        {
            if (Math.Abs(this.Divisor) < double.Epsilon)
            {
                throw new CommandException("Division by zero is not supported.", 1337);
            }

            var result = this.Dividend / this.Divisor;
            console.Output.WriteLine(result);

            return Task.CompletedTask;
        }
    }
}