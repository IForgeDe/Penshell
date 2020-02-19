namespace Penshell.Commands.IO
{
    using System.CommandLine;
    using System.CommandLine.Invocation;
    using System.IO;
    using Dawn;
    using Penshell.Core;
    using Penshell.Core.Console;

    /// <summary>
    /// Writes content to a file.
    /// </summary>
    public class WriteFileCommand : PenshellCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WriteFileCommand"/> class.
        /// </summary>
        /// <param name="console">The <see cref="IPenshellConsole"/> instance.</param>
        public WriteFileCommand(IPenshellConsole console)
            : base(console, "writefile", "Writes content to a file.")
        {
            this.AddOption(
                new Option(
                    new string[] { "-p", "--path" },
                    "The fully qualified name of the file, or the relative file name, to write to.")
                {
                    Argument = new Argument<FileInfo>(),
                    Required = true,
                });
            this.AddOption(
                new Option(
                    new string[] { "-c", "--content" },
                    "The content to write.")
                {
                    Argument = new Argument<string>(),
                    Required = true,
                });
        }

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="fileInfo">The <see cref="FileInfo"/> to write to.</param>
        /// <param name="content">The conente to write.</param>
        public void Execute(FileInfo fileInfo, string content)
        {
            fileInfo = Guard.Argument(fileInfo).NotNull();
            using (var fileStream = fileInfo.OpenWrite())
            {
                using var streamWriter = new StreamWriter(fileStream);
                streamWriter.Write(content);
            }

            this.Console.Out.Write("Success");
        }

        /// <inheritdoc />
        protected override ICommandHandler CreateCommandHandler()
        {
            return CommandHandler.Create<FileInfo, string>((path, content) => this.Execute(path, content));
        }
    }
}
