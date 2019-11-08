namespace Penshell.Commands.Scripting
{
    using System;
    using System.Threading.Tasks;
    using CliFx;
    using CliFx.Attributes;
    using CliFx.Services;
    using Dawn;

    /// <summary>
    /// Exits a penshell script.
    /// </summary>
    [Command("script exit", Description = "Exits a penshell script.")]
    public class ExitCommand : ICommand
    {
        /// <inheritdoc />
        public Task ExecuteAsync(IConsole console)
        {
            console = Guard.Argument(console).NotNull().Value;
            console.Output.Write("Exit");
            Environment.Exit(0);
            return Task.CompletedTask;
        }
    }
}
