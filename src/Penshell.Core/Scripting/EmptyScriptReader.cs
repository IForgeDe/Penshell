namespace Penshell.Core.Scripting
{
    using System.Collections.Generic;

    internal class EmptyScriptReader : IScriptReader
    {
        public IReadOnlyList<ScriptLine> Read()
        {
            return new List<ScriptLine>();
        }
    }
}
