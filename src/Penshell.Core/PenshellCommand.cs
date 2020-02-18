namespace Penshell.Core
{
    using System;
    using System.CommandLine;
    using System.CommandLine.Invocation;

    /// <summary>
    /// Abstract base class for all penshell commands.
    /// </summary>
    public abstract class PenshellCommand : Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PenshellCommand"/> class.
        /// </summary>
        /// <param name="console">The <see cref="IConsole"/> instance.</param>
        /// <param name="formatProvider">The <see cref="IFormatProvider"/> instance.</param>
        /// <param name="name">The name.</param>
        /// <param name="description">The desciption.</param>
        public PenshellCommand(IConsole console, IFormatProvider formatProvider, string name, string? description = null)
            : base(name, description)
        {
            this.Console = console ?? throw new ArgumentNullException(nameof(console));
            this.FormatProvider = formatProvider ?? throw new ArgumentNullException(nameof(formatProvider));
            this.Handler = this.CreateCommandHandler();
        }

        /// <summary>
        /// Gets the <see cref="IConsole"/> instance.
        /// </summary>
        public IConsole Console { get; }

        /// <summary>
        /// Gets the <see cref="IFormatProvider"/> instance.
        /// </summary>
        public IFormatProvider FormatProvider { get; }

        /// <summary>
        /// Creates the command handler.
        /// </summary>
        /// <returns>The <see cref="ICommandHandler"/> instance.</returns>
        protected abstract ICommandHandler CreateCommandHandler();
    }
}
