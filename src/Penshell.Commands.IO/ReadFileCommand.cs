namespace Penshell.Commands.IO
{
    using System.CommandLine;
    using System.CommandLine.Invocation;
    using System.IO;
    using Dawn;
    using Penshell.Core;
    using Penshell.Core.Console;

    /// <summary>
    /// Reads a file.
    /// </summary>
    public class ReadFileCommand : PenshellCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadFileCommand"/> class.
        /// </summary>
        /// <param name="console">The <see cref="IPenshellConsole"/> instance.</param>
        public ReadFileCommand(IPenshellConsole console)
            : base(console, "readfile", "Reads a file.")
        {
            this.AddOption(
                new Option(
                    new string[] { "-p", "--path" },
                    "The fully qualified name of the file, or the relative file name, to read.")
                {
                    Argument = new Argument<FileInfo>(),
                    Required = true,
                });
        }

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="fileInfo">The <see cref="FileInfo"/> to read.</param>
        public void Execute(FileInfo fileInfo)
        {
            fileInfo = Guard.Argument(fileInfo).NotNull();
            using var stream = new BinaryReader(fileInfo.OpenRead());
            var buffer = new byte[1024];
            while (stream.Read(buffer, 0, buffer.Length) > 0)
            {
                this.Console.WriteLine(this.Console.Encoding.GetString(buffer));
            }
        }

        /// <inheritdoc />
        protected override ICommandHandler CreateCommandHandler()
        {
            return CommandHandler.Create<FileInfo>((path) => this.Execute(path));
        }
    }
}
