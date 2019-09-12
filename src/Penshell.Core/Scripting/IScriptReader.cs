namespace Penshell.Core.Scripting
{
    using System.Collections.Generic;

    public interface IScriptReader
    {
        IReadOnlyList<ScriptLine> Read();
    }
}
