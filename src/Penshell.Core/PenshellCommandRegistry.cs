namespace Penshell.Core
{
    using System;
    using System.Collections.Generic;
    using CliFx.Models;
    using Dawn;

    /// <summary>
    /// A specialized penshell registry to hold references for the CliFx command schemas.
    /// </summary>
    public class PenshellCommandRegistry
    {
        /// <summary>
        /// Gets the list of CliFx command schemas.
        /// </summary>
        public IReadOnlyList<CommandSchema>? CommandSchemas { get; private set; }

        /// <summary>
        /// Registers the command schemas in this registry.
        /// </summary>
        /// <param name="commandSchemas">The list of command schemas to register.</param>
        public void Register(IReadOnlyList<CommandSchema> commandSchemas)
        {
            if (this.CommandSchemas != null)
            {
                throw new InvalidOperationException("Command schemas already set to this registry.");
            }

            this.CommandSchemas = commandSchemas;
        }
    }
}
