namespace Penshell.Core
{
    using System;
    using System.CommandLine;
    using System.CommandLine.Invocation;
    using Penshell.Core.Console;

    /// <summary>
    /// Abstract base class for all penshell commands.
    /// </summary>
    public abstract class PenshellCommand : Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PenshellCommand"/> class.
        /// </summary>
        /// <param name="console">The <see cref="IPenshellConsole"/> instance.</param>
        /// <param name="name">The name.</param>
        /// <param name="description">The desciption.</param>
        public PenshellCommand(IPenshellConsole console, string name, string? description = null)
            : base(name, description)
        {
            this.Console = console ?? throw new ArgumentNullException(nameof(console));
            this.Handler = this.CreateCommandHandler();
        }

        /// <summary>
        /// Gets the <see cref="IPenshellConsole"/> instance.
        /// </summary>
        public IPenshellConsole Console { get; }

        /// <summary>
        /// Creates the command handler.
        /// </summary>
        /// <returns>The <see cref="ICommandHandler"/> instance.</returns>
        protected abstract ICommandHandler CreateCommandHandler();
    }
}
