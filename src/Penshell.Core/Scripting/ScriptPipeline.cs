namespace Penshell.Core.Scripting
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using CliFx.Services;

    internal class ScriptPipeline : IScriptPipeline
    {
        private readonly IConsole _console;

        private readonly IEnumerable<ScriptLine> _scriptLines;

        public ScriptPipeline(
            IConsole console,
            IEnumerable<ScriptLine> scriptLines)
        {
            _console = console;
            _scriptLines = scriptLines;
        }

        public void Execute()
        {
            var encoding = Encoding.UTF8;
            foreach (var scriptLine in _scriptLines)
            {
                var sciptLineBytes = encoding.GetBytes(scriptLine.Content);
                var scriptLineMemoryStream = new MemoryStream(sciptLineBytes);
                var inputStream = new StreamReader(scriptLineMemoryStream);
                var outputStream = new StringWriter();
                var errorStream = new StringWriter();
                var virtualConsole = new VirtualConsole(inputStream, outputStream, errorStream);
            }
        }
    }
}
