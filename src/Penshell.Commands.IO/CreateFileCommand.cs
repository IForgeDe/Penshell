namespace Penshell.Commands.IO
{
    using System.CommandLine;
    using System.CommandLine.Invocation;
    using System.IO;
    using Dawn;
    using Penshell.Core;
    using Penshell.Core.Console;

    /// <summary>
    /// Creates a file.
    /// </summary>
    public class CreateFileCommand : PenshellCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateFileCommand"/> class.
        /// </summary>
        /// <param name="console">The <see cref="IPenshellConsole"/> instance.</param>
        public CreateFileCommand(IPenshellConsole console)
            : base(console, "createfile", "Creates a file.")
        {
            this.AddOption(
                new Option(
                    new string[] { "-p", "--path" },
                    "The fully qualified name of the new file, or the relative file name.")
                {
                    Argument = new Argument<FileInfo>(),
                    Required = true,
                });
        }

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="fileInfo">The <see cref="FileInfo"/> to create.</param>
        public void Execute(FileInfo fileInfo)
        {
            fileInfo = Guard.Argument(fileInfo).NotNull();
            var fileStream = fileInfo.Create();
            fileStream.Close();
            this.Console.Out.Write("Success");
        }

        /// <inheritdoc />
        protected override ICommandHandler CreateCommandHandler()
        {
            return CommandHandler.Create<FileInfo>((path) => this.Execute(path));
        }
    }
}
