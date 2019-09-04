namespace Penshell.Core.Scripting
{
    using System.IO;

    internal class ScriptReader : IScriptReader
    {
        internal ScriptReader(FileInfo scriptFile)
        {
            this.ScriptFile = scriptFile;
        }

        internal FileInfo ScriptFile { get; }

        public void Read()
        {
            throw new System.NotImplementedException();
        }
    }
}
