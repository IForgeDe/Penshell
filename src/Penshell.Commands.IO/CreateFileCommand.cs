namespace Penshell.Commands.IO
{
    using System.IO;
    using System.Threading.Tasks;
    using CliFx;
    using CliFx.Attributes;
    using CliFx.Services;
    using Dawn;

    /// <summary>
    /// Creates a file.
    /// </summary>
    [Command("io createfile", Description = "Creates a file.")]
    public class CreateFileCommand : ICommand
    {
        /// <summary>
        /// Gets or sets the fully qualified name of the new file, or the relative file name.
        /// </summary>
        /// <value>
        /// The fully qualified name of the new file, or the relative file name.
        /// </value>
        [CommandOption("path", 'p', IsRequired = true, Description = "The fully qualified name of the new file, or the relative file name.")]
        public string Path { get; set; } = string.Empty;

        /// <inheritdoc />
        public Task ExecuteAsync(IConsole console)
        {
            console = Guard.Argument(console).NotNull().Value;

            var fileInfo = new FileInfo(this.Path);
            var fileStream = fileInfo.Create();
            fileStream.Close();
            console.Output.WriteLine("Success");
            return Task.CompletedTask;
        }
    }
}
