namespace Penshell.Commands.Scripting.Engine
{
    using System.IO;

    public class ScriptReaderBuilder
    {
        public ScriptReaderBuilder(FileInfo scriptFile)
        {
            this.ScriptFile = scriptFile;
        }

        public FileInfo ScriptFile { get; }

        public IScriptReader Build()
        {
            return new ScriptReader(this.ScriptFile);
        }
    }
}
