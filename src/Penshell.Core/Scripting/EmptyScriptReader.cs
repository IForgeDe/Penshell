namespace Penshell.Core.Scripting
{
    using System.Collections.Generic;

    internal class EmptyScriptReader : IScriptReader
    {
        public IEnumerable<ScriptLine> Read()
        {
            return new List<ScriptLine>();
        }
    }
}
