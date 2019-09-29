namespace Penshell.Commands.Scripting.Engine
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface for reading a penshell script file.
    /// </summary>
    public interface IScriptReader
    {
        /// <summary>
        /// Method to read the lines of a penshell script file.
        /// </summary>
        /// <returns>
        /// The list of <see cref="ScriptLine"/> instances of the file.
        /// </returns>
        IReadOnlyList<ScriptLine> Read();
    }
}
