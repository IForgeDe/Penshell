namespace Penshell.Core.Scripting
{
    using System.IO;
    using CliFx.Services;

    public class ScriptPipelineBuilder
    {
        private IConsole? _console;

        private IScriptReader? _scriptReader;

        public IScriptPipeline Build()
        {
            _console ??= GetDefaultConsole();
            _scriptReader ??= GetDefaultScriptReader();
            return new ScriptPipeline(_console, _scriptReader.Read());
        }

        public ScriptPipelineBuilder UseConsole(IConsole console)
        {
            _console = console;
            return this;
        }

        public ScriptPipelineBuilder UseScriptReader(IScriptReader scriptReader)
        {
            _scriptReader = scriptReader;
            return this;
        }

        private IConsole GetDefaultConsole()
        {
            using var stdout = new StringWriter();
            return new VirtualConsole(new StringWriter());
        }

        private IScriptReader GetDefaultScriptReader()
        {
            return new EmptyScriptReader();
        }
    }
}
