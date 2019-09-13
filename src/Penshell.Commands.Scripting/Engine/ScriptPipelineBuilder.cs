namespace Penshell.Commands.Scripting.Engine
{
    using System;
    using CliFx.Services;
    using Penshell.Core;
    using Serilog;

    public class ScriptPipelineBuilder
    {
        private ICommandFactory? _commandFactory;
        private ILogger? _logger;
        private PenshellCommandRegistry? _commandRegistry;
        private ICommandOptionInputConverter? _commandOptionInputConverter;
        private IScriptReader? _scriptReader;

        public IScriptPipeline Build()
        {
            _scriptReader ??= GetDefaultScriptReader();
            _commandRegistry = _commandRegistry ?? throw new InvalidOperationException("Command registry not set.");
            _commandOptionInputConverter = _commandOptionInputConverter ?? throw new InvalidOperationException("Command option input converter not set.");
            _commandFactory = _commandFactory ?? throw new InvalidOperationException("Command factory not set.");
            return new ScriptPipeline(_scriptReader.Read(), _commandRegistry, _commandFactory, _commandOptionInputConverter, _logger);
        }

        public ScriptPipelineBuilder UseCommandFactory(ICommandFactory commandFactory)
        {
            _commandFactory = commandFactory;
            return this;
        }

        public ScriptPipelineBuilder UseCommandOptionInputConverter(ICommandOptionInputConverter commandOptionInputConverter)
        {
            _commandOptionInputConverter = commandOptionInputConverter;
            return this;
        }

        public ScriptPipelineBuilder UseCommandRegistry(PenshellCommandRegistry registry)
        {
            _commandRegistry = registry;
            return this;
        }

        public ScriptPipelineBuilder UseLogger(ILogger logger)
        {
            _logger = logger;
            return this;
        }

        public ScriptPipelineBuilder UseScriptReader(IScriptReader scriptReader)
        {
            _scriptReader = scriptReader;
            return this;
        }

        private static IScriptReader GetDefaultScriptReader()
        {
            return new EmptyScriptReader();
        }
    }
}
