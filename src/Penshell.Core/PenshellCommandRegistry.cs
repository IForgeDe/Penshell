namespace Penshell.Core
{
    using System;
    using System.Collections.Generic;
    using System.CommandLine;

    /// <summary>
    /// A specialized penshell registry to hold references for the <see cref="ICommand"/> instances.
    /// </summary>
    public class PenshellCommandRegistry
    {
        /// <summary>
        /// Gets the list of known <see cref="ICommand"/> instances.
        /// </summary>
        public IReadOnlyList<ICommand>? Commands { get; private set; }

        /// <summary>
        /// Registers the command schemas in this registry.
        /// </summary>
        /// <param name="commands">The list of commands to register.</param>
        public void Register(IReadOnlyList<ICommand> commands)
        {
            if (this.Commands != null)
            {
                throw new InvalidOperationException("Commands already set to this registry.");
            }

            this.Commands = commands;
        }
    }
}
