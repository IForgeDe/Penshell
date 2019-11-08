namespace Penshell.Core
{
    using System;
    using CliFx.Services;

    /// <summary>
    /// Specialization of <see cref="VirtualConsole"/> that wraps Penshell functionality.
    /// </summary>
    public class PenshellConsole : VirtualConsole
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PenshellConsole"/> class.
        /// </summary>
        public PenshellConsole()
            : base(
                  Console.In,
                  Console.IsInputRedirected,
                  Console.Out,
                  Console.IsOutputRedirected,
                  Console.Error,
                  Console.IsErrorRedirected)
        {
        }
    }
}
