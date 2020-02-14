namespace Penshell.Core.Console
{
    using System;
    using System.IO;
    using System.Text;
    using CliFx.Services;

    /// <summary>
    /// A virtual console builder.
    /// </summary>
    public class VirtualConsoleBuilder
    {
        private Encoding _encoding = Encoding.UTF8;
        private string? _input;
        private StringBuilder? _output;

        /// <summary>
        /// Builds the console instance.
        /// </summary>
        /// <returns>
        /// The console instance.
        /// </returns>
        public IConsole Build()
        {
            var sciptLineBytes = _encoding.GetBytes(_input);
            var scriptLineMemoryStream = new MemoryStream(sciptLineBytes);
            using var inputStream = new StreamReader(scriptLineMemoryStream);
            var outputStream = new StringWriter(_output);
            var errorStream = new StringWriter();
            return new VirtualConsole(inputStream, outputStream, errorStream);
        }

        /// <summary>
        /// Use encoding.
        /// </summary>
        /// <param name="encoding">The encoding.</param>
        /// <returns>
        /// A ConsoleBuilder.
        /// </returns>
        public VirtualConsoleBuilder UseEncoding(Encoding encoding)
        {
            _encoding = encoding;
            return this;
        }

        /// <summary>
        /// Use input.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="input">The input.</param>
        /// <returns>
        /// A ConsoleBuilder.
        /// </returns>
        public VirtualConsoleBuilder UseInput(string input)
        {
            _input = input ?? throw new ArgumentNullException(nameof(input));
            return this;
        }

        /// <summary>
        /// Use output.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="output">The output.</param>
        /// <returns>
        /// A ConsoleBuilder.
        /// </returns>
        public VirtualConsoleBuilder UseOutput(StringBuilder output)
        {
            _output = output ?? throw new ArgumentNullException(nameof(output));
            return this;
        }
    }
}
