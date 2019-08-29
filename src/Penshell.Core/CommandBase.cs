namespace Penshell.Core
{
    using System;
    using System.Threading.Tasks;
    using CliFx;
    using CliFx.Services;
    using Serilog;

    public abstract class CommandBase : ICommand
    {
        public CommandBase(ILogger logger)
        {
            this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public ILogger Logger { get; }

        public abstract Task ExecuteAsync(IConsole console);
    }
}