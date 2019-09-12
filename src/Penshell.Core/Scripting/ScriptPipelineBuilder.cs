namespace Penshell.Core.Scripting
{
    using System;
    using System.IO;
    using CliFx.Services;
    using Serilog;

    public class ScriptPipelineBuilder
    {
        private ICommandFactory? _commandFactory;
        private IConsole? _console;
        private ILogger? _logger;
        private PenshellCommandRegistry? _registry;
        private IScriptReader? _scriptReader;

        public IScriptPipeline Build()
        {
            _console ??= GetDefaultConsole();
            _scriptReader ??= GetDefaultScriptReader();
            _registry = _registry ?? throw new InvalidOperationException("Command registry not set.");
            _commandFactory = _commandFactory ?? throw new InvalidOperationException("Command factory not set.");
            return new ScriptPipeline(_scriptReader.Read(), _registry, _commandFactory, _logger);
        }

        public ScriptPipelineBuilder UseCommandRegistry(PenshellCommandRegistry registry)
        {
            _registry = registry;
            return this;
        }

        public ScriptPipelineBuilder UseLogger(ILogger logger)
        {
            _logger = logger;
            return this;
        }

        public ScriptPipelineBuilder UseCommandFactory(ICommandFactory commandFactory)
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
