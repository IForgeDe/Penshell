namespace Penshell.Commands.Scripting.Engine
{
    using System.Collections.Generic;

    public interface IScriptReader
    {
        IReadOnlyList<ScriptLine> Read();
    }
}
