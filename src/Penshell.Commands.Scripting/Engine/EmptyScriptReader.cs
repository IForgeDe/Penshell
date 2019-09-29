namespace Penshell.Commands.Scripting.Engine
{
    using System.Collections.Generic;

    /// <summary>
    /// An implementation of the <see cref="IScriptReader"/> interface, which simulates reading an empty file.
    /// </summary>
    public class EmptyScriptReader : IScriptReader
    {
        /// <inheritdoc />
        public IReadOnlyList<ScriptLine> Read()
        {
            return new List<ScriptLine>();
        }
    }
}
