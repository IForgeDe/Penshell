namespace Penshell.Commands.IO
{
    using System.IO;
    using System.Threading.Tasks;
    using CliFx;
    using CliFx.Attributes;
    using CliFx.Services;
    using Dawn;

    [Command("io readfile", Description = "Reads a file.")]
    public class ReadFileCommand : ICommand
    {
        [CommandOption("path", 'p', IsRequired = true, Description = "The fully qualified name of the file, or the relative file name, to read.")]
        public string? Path { get; set; }

        /// <inheritdoc />
        public Task ExecuteAsync(IConsole console)
        {
            console = Guard.Argument(console).NotNull().Value;
            this.Path = Guard.Argument(this.Path).NotNull().NotEmpty().Value;

            using (var stream = new BinaryReader(File.OpenRead(this.Path)))
            {
                var buffer = new byte[1024];
                while (stream.Read(buffer, 0, buffer.Length) > 0)
                {
                    console.Output.WriteLine(console.Output.Encoding.GetString(buffer));
                }
            }

            return Task.CompletedTask;
        }
    }
}
