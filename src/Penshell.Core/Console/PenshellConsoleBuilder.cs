namespace Penshell.Core.Console
{
    using System.CommandLine;

    /// <summary>
    /// A fluent builder for building the penshell console.
    /// </summary>
    public class PenshellConsoleBuilder
    {
        /// <summary>
        /// Builds the <see cref="IConsole"/> instance.
        /// </summary>
        /// <returns>The <see cref="IConsole"/> instance.</returns>
        public IConsole Build()
        {
            return new PenshellConsole();
        }
    }
}
