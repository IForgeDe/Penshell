namespace Penshell.Commands.Scripting
{
    using System;
    using System.CommandLine.Invocation;
    using Penshell.Core;
    using Penshell.Core.Console;

    /// <summary>
    /// Exits a penshell script.
    /// </summary>
    public class ExitCommand : PenshellCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExitCommand"/> class.
        /// </summary>
        /// <param name="console">The <see cref="IPenshellConsole"/> instance.</param>
        public ExitCommand(IPenshellConsole console)
            : base(console, "exit", "Exits a penshell script.")
        {
        }

        /// <summary>
        /// Executes this command.
        /// </summary>
        public void Execute()
        {
            this.Console.WriteLine("Exit");
            Environment.Exit(0);
        }

        /// <inheritdoc />
        protected override ICommandHandler CreateCommandHandler()
        {
            return CommandHandler.Create(() => this.Execute());
        }
    }
}
