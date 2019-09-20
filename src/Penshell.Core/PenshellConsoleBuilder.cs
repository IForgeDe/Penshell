namespace Penshell.Core
{
    using CliFx.Services;

    /// <summary>
    /// A fluent builder for building the penshell console.
    /// </summary>
    public static class PenshellConsoleBuilder
    {
        /// <summary>
        /// Builds the <see cref="IConsole"/> instance.
        /// </summary>
        /// <returns>The <see cref="IConsole"/> instance.</returns>
        public static IConsole Build()
        {
            return new PenshellConsole();
        }
    }
}
