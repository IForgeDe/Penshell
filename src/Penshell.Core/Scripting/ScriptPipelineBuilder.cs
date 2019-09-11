namespace Penshell.Core.Scripting
{
    using System;
    using System.IO;
    using CliFx.Services;

    public class ScriptPipelineBuilder
    {
        private ICommandFactory _commandFactory;

        private IConsole? _console;

        private PenshellCommandRegistry? _registry;

        private IScriptReader? _scriptReader;

        public IScriptPipeline Build()
        {
            _console ??= GetDefaultConsole();
            _scriptReader ??= GetDefaultScriptReader();
            _registry = _registry ?? throw new InvalidOperationException("Command registry not set.");
            _commandFactory = _commandFactory ?? throw new InvalidOperationException("Command factory not set.");
            return new ScriptPipeline(_console, _scriptReader.Read(), _registry, _commandFactory);
        }

        public ScriptPipelineBuilder UseCommandRegistry(PenshellCommandRegistry registry)
        {
            _registry = registry;
            return this;
        }

        public ScriptPipelineBuilder UseConsole(IConsole console)
        {
            _console = console;
            return this;
        }

        public ScriptPipelineBuilder UserCommandFactory(ICommandFactory commandFactory)
        {
            _commandFactory = commandFactory;
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
