namespace Penshell.Core.Scripting
{
    using System.Collections.Generic;

    public class EmptyScriptReader : IScriptReader
    {
        public IReadOnlyList<ScriptLine> Read()
        {
            return new List<ScriptLine>();
        }
    }
}
