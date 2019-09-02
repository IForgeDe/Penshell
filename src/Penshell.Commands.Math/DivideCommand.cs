namespace Penshell.Commands.Math
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using CliFx;
    using CliFx.Attributes;
    using CliFx.Exceptions;
    using CliFx.Services;

    [Command("math divide", Description = "Calculates the division of two values.")]
    public class DivideCommand : ICommand
    {
        public DivideCommand()
        {
        }

        [CommandOption("dividend", 'x', IsRequired = true, Description = "The dividend.")]
        public double Dividend { get; set; }

        [CommandOption("divisor", 'y', IsRequired = true, Description = "The devisor.")]
        public double Divisor { get; set; }

        public Task ExecuteAsync(IConsole console)
        {
            if (Math.Abs(this.Divisor) < double.Epsilon)
            {
                throw new CommandException("Division by zero is not supported.", 1337);
            }

            var result = this.Dividend / this.Divisor;
            console.Output.WriteLine(Convert.ToString(result, CultureInfo.InvariantCulture));

            return Task.CompletedTask;
        }
    }
}