namespace Penshell.Commands.Scripting.Engine
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
