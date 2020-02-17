namespace Penshell.Commands.Scripting.Engine
{
    using System;
    using CliFx.Services;
    using Penshell.Core;
    using Serilog;

    /// <summary>
    /// This class implements a fluent builder for creating a <see cref="IScriptPipeline"/> instance.
    /// </summary>
    public class ScriptPipelineBuilder
    {
        private ICommandFactory? _commandFactory;
        private ILogger? _logger;
        private PenshellCommandRegistry? _commandRegistry;
        private ICommandOptionInputConverter? _commandOptionInputConverter;
        private IScriptReader? _scriptReader;

        /// <summary>
        /// Gets the <see cref="IScriptPipeline"/> instance.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the requested operation is invalid.
        /// </exception>
        /// <returns>
        /// The <see cref="IScriptPipeline"/> instance.
        /// </returns>
        public IScriptPipeline Build()
        {
            _scriptReader ??= GetDefaultScriptReader();
            _commandRegistry = _commandRegistry ?? throw new InvalidOperationException("Command registry not set.");
            _commandOptionInputConverter = _commandOptionInputConverter ?? throw new InvalidOperationException("Command option input converter not set.");
            _commandFactory = _commandFactory ?? throw new InvalidOperationException("Command factory not set.");
            return new ScriptPipeline(_scriptReader.Read(), _logger);
        }

        /// <summary>
        /// A fluent method to inject a <see cref="ICommandFactory"/> instance.
        /// </summary>
        /// <param name="commandFactory">
        /// The <see cref="ICommandFactory"/> instance.
        /// </param>
        /// <returns>
        /// The fluent instance.
        /// </returns>
        public ScriptPipelineBuilder UseCommandFactory(ICommandFactory commandFactory)
        {
            _commandFactory = commandFactory;
            return this;
        }

        /// <summary>
        /// A fluent method to inject a <see cref="ICommandOptionInputConverter"/> instance.
        /// </summary>
        /// <param name="commandOptionInputConverter">
        /// The <see cref="ICommandOptionInputConverter"/> instance.
        /// </param>
        /// <returns>
        /// The fluent instance.
        /// </returns>
        public ScriptPipelineBuilder UseCommandOptionInputConverter(ICommandOptionInputConverter commandOptionInputConverter)
        {
            _commandOptionInputConverter = commandOptionInputConverter;
            return this;
        }

        /// <summary>
        /// A fluent method to inject a <see cref="PenshellCommandRegistry"/> instance.
        /// </summary>
        /// <param name="registry">
        /// The <see cref="PenshellCommandRegistry"/> instance.
        /// </param>
        /// <returns>
        /// The fluent instance.
        /// </returns>
        public ScriptPipelineBuilder UseCommandRegistry(PenshellCommandRegistry registry)
        {
            _commandRegistry = registry;
            return this;
        }

        /// <summary>
        /// A fluent method to inject a <see cref="ILogger"/> instance.
        /// </summary>
        /// <param name="logger">
        /// The <see cref="ILogger"/> instance.
        /// </param>
        /// <returns>
        /// The fluent instance.
        /// </returns>
        public ScriptPipelineBuilder UseLogger(ILogger logger)
        {
            _logger = logger;
            return this;
        }

        /// <summary>
        /// A fluent method to inject a <see cref="IScriptReader"/> instance.
        /// </summary>
        /// <param name="scriptReader">
        /// The <see cref="IScriptReader"/> instance.
        /// </param>
        /// <returns>
        /// The fluent instance.
        /// </returns>
        public ScriptPipelineBuilder UseScriptReader(IScriptReader scriptReader)
        {
            _scriptReader = scriptReader;
            return this;
        }

        /// <summary>
        /// Gets the default <see cref="IScriptReader"/> instance.
        /// </summary>
        /// <returns>
        /// The default <see cref="IScriptReader"/> instance.
        /// </returns>
        private static IScriptReader GetDefaultScriptReader()
        {
            return new EmptyScriptReader();
        }
    }
}
