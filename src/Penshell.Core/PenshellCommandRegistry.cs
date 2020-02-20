namespace Penshell.Core
{
    using System;
    using System.CommandLine;

    /// <summary>
    /// A specialized penshell registry to hold references to command instances.
    /// </summary>
    public class PenshellCommandRegistry
    {
        /// <summary>
        /// Gets the <see cref="RootCommand"/> instance.
        /// </summary>
        public RootCommand? RootCommand { get; private set; }

        /// <summary>
        /// Registers the root command schemas in this registry.
        /// </summary>
        /// <param name="rootCommand">The <see cref="RootCommand"/> instance.</param>
        public void Register(RootCommand rootCommand)
        {
            if (this.RootCommand != null)
            {
                throw new InvalidOperationException("RootCommand already set to this registry.");
            }

            this.RootCommand = rootCommand;
        }
    }
}
