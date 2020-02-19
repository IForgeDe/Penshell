namespace Penshell.Commands.Scripting.Engine
{
    using System;
    using Dawn;
    using Penshell.Core;
    using Serilog;

    /// <summary>
    /// This class implements a fluent builder for creating a <see cref="IScriptPipeline"/> instance.
    /// </summary>
    public class ScriptPipelineBuilder
    {
        private PenshellCommandRegistry? _commandRegistry;
        private ILogger? _logger;
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
            _commandRegistry = Guard.Argument(_commandRegistry).NotNull("Command registry not set.");
            return new ScriptPipeline(_scriptReader.Read(), _logger);
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
