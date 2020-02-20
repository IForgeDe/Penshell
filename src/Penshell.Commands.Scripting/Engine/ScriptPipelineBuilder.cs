namespace Penshell.Commands.Scripting.Engine
{
    using System;
    using Penshell.Core;
    using Penshell.Core.Console;
    using Serilog;

    /// <summary>
    /// This class implements a fluent builder for creating a <see cref="IScriptPipeline"/> instance.
    /// </summary>
    public class ScriptPipelineBuilder
    {
        private IPenshellConsole? _console;
        private ILogger? _logger;
        private PenshellCommandRegistry? _registry;
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
            if (_console == null)
            {
                throw new InvalidOperationException("Console not set");
            }

            if (_registry == null)
            {
                throw new InvalidOperationException("Command registry not set");
            }

            _scriptReader ??= GetDefaultScriptReader();
            return new ScriptPipeline(_console, _scriptReader.Read(), _logger, _registry);
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
            _registry = registry;
            return this;
        }

        /// <summary>
        /// A fluent method to inject a <see cref="IPenshellConsole"/> instance.
        /// </summary>
        /// <param name="console">
        /// The <see cref="IPenshellConsole"/> instance.
        /// </param>
        /// <returns>
        /// The fluent instance.
        /// </returns>
        public ScriptPipelineBuilder UseConsole(IPenshellConsole console)
        {
            _console = console;
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
