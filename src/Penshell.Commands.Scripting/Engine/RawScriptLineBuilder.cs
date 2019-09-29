namespace Penshell.Commands.Scripting.Engine
{
    using System;
    using System.Collections.Generic;
    using CliFx.Models;
    using Dawn;

    /// <summary>
    /// This class builds a new raw script line with the substitution from the last command output.
    /// </summary>
    public class RawScriptLineBuilder
    {
        private CommandInput? _commandInput;
        private ScriptLine? _scriptLine;
        private string? _substitution;

        /// <summary>
        /// Returns a adjusted substitution for example negative numerics.
        /// </summary>
        /// <param name="substitution">The original substiution value.</param>
        /// <returns>The adjusted substitution.</returns>
        public static string CreateAdjustedSubstitution(string substitution)
        {
            substitution = Guard.Argument(substitution).NotNull().Value;
            if (substitution.StartsWith("-", StringComparison.Ordinal))
            {
                return $"\"{substitution}\"";
            }

            return substitution;
        }

        /// <summary>
        /// Gets the new list of raw command arguments.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown when one or more arguments have unsupported or illegal values.
        /// </exception>
        /// <returns>
        /// A new list of raw command arguments.
        /// </returns>
        public IReadOnlyList<string> Build()
        {
            _scriptLine = _scriptLine ?? throw new ArgumentException("Script line not set.");
            _commandInput = _commandInput ?? throw new ArgumentException("Command input not set.");
            _substitution = _substitution ?? throw new ArgumentException("Substitution not set.");

            // perform substitution not in first line
            if (_scriptLine.LineNumber == 1)
            {
                return _scriptLine.CommandArguments;
            }

            var commandArguments = new List<string>(_commandInput.CommandName.Split(" "));
            foreach (var option in _commandInput.Options)
            {
                commandArguments.Add($"{(option.Alias.Length > 1 ? "--" : "-")}{option.Alias}");
                foreach (var value in option.Values)
                {
                    if (value.Equals(_scriptLine.Substitution, StringComparison.Ordinal))
                    {
                        commandArguments.Add(CreateAdjustedSubstitution(_substitution));
                    }
                    else
                    {
                        commandArguments.Add(value);
                    }
                }
            }

            return commandArguments;
        }

        /// <summary>
        /// A fluent method to inject a <see cref="CommandInput"/> instance.
        /// </summary>
        /// <param name="commandInput">
        /// The <see cref="CommandInput"/> instance.
        /// </param>
        /// <returns>
        /// The fluent instance.
        /// </returns>
        public RawScriptLineBuilder UseCommandInput(CommandInput commandInput)
        {
            _commandInput = commandInput;
            return this;
        }

        /// <summary>
        /// A fluent method to inject a <see cref="ScriptLine"/> instance.
        /// </summary>
        /// <param name="scriptLine">
        /// The <see cref="ScriptLine"/> instance.
        /// </param>
        /// <returns>
        /// The fluent instance.
        /// </returns>
        public RawScriptLineBuilder UseScriptLine(ScriptLine scriptLine)
        {
            _scriptLine = scriptLine;
            return this;
        }

        /// <summary>
        /// A fluent method to inject a <see cref="string"/> instance for substitution.
        /// </summary>
        /// <param name="substitution">
        /// The <see cref="string"/> instance for substitution.
        /// </param>
        /// <returns>
        /// The fluent instance.
        /// </returns>
        public RawScriptLineBuilder UseSubstitution(string substitution)
        {
            _substitution = substitution;
            return this;
        }
    }
}
