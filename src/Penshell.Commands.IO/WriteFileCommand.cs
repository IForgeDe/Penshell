namespace Penshell.Commands.IO
{
    using System.IO;
    using System.Threading.Tasks;
    using CliFx.Attributes;
    using CliFx.Services;
    using Dawn;

    [Command("io writefile", Description = "Writes content to a file.")]
    public class WriteFileCommand
    {
        [CommandOption("content", 'c', IsRequired = true, Description = "The content to write.")]
        public string? Content { get; set; }

        [CommandOption("path", 'p', IsRequired = true, Description = "The fully qualified name of the file, or the relative file name, to write to.")]
        public string? Path { get; set; }

        public Task ExecuteAsync(IConsole console)
        {
            console = Guard.Argument(console).NotNull().Value;
            this.Path = Guard.Argument(this.Path).NotNull().NotEmpty().Value;
            this.Content = Guard.Argument(this.Content).NotNull().NotEmpty().Value;

            using (var streamWriter = new StreamWriter(this.Path))
            {
                streamWriter.Write(this.Content);
            }

            console.Output.WriteLine("Success");
            return Task.CompletedTask;
        }
    }
}
