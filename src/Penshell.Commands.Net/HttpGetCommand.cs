namespace Penshell.Commands.Net
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using CliFx;
    using CliFx.Attributes;
    using CliFx.Services;
    using Dawn;

    [Command("net httpget", Description = "Gets the response of a http get method call.")]
    public class HttpGetCommand : ICommand
    {
        [CommandOption("property", 'p', IsRequired = false, Description = "The property of the response for the output (default is StatusCode).")]
        public string? Property { get; set; }

        [CommandOption("uri", 'u', IsRequired = true, Description = "The uri for the http request.")]
        public Uri? Uri { get; set; }

        public Task ExecuteAsync(IConsole console)
        {
            // validate
            console = Guard.Argument(console).NotNull().Value;
            this.Property ??= "StatusCode";
            this.Uri = Guard.Argument(this.Uri).NotNull().Value;

            // perform
            using var httpClient = new HttpClient();
            var result = httpClient.GetAsync(this.Uri).Result;
            console.Output.WriteLine(FromProperty(result, this.Property), Thread.CurrentThread.CurrentCulture);
            return Task.CompletedTask;
        }

        private static string FromProperty(HttpResponseMessage httpResponseMessage, string property)
        {
            switch (property)
            {
                case "Content":
                    using (var streamReader = new StreamReader(httpResponseMessage.Content.ReadAsStreamAsync().Result, Encoding.UTF8))
                    {
                        return streamReader.ReadToEnd();
                    }

                case "Headers":
                    return httpResponseMessage.Headers.ToString();

                case "StatusCode":
                    return httpResponseMessage.StatusCode.ToString();

                default:
                    throw new ArgumentException(message: "Invalid property set.", paramName: nameof(property));
            }
        }
    }
}