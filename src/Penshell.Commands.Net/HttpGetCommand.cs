namespace Penshell.Commands.Net
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using CliFx;
    using CliFx.Attributes;
    using CliFx.Services;
    using Dawn;

    /// <summary>
    /// Gets the response of a http get method call.
    /// </summary>
    [Command("net httpget", Description = "Gets the response of a http get method call.")]
    public class HttpGetCommand : ICommand
    {
        /// <summary>
        /// Gets or sets the property of the response for the output (default is StatusCode).
        /// </summary>
        /// <value>
        /// The property of the response for the output (default is StatusCode).
        /// </value>
        [CommandOption("property", 'p', IsRequired = false, Description = "The property of the response for the output (default is StatusCode).")]
        public string? Property { get; set; }

        /// <summary>
        /// Gets or sets the uri for the http request.
        /// </summary>
        /// <value>
        /// The uri for the http request.
        /// </value>
        [CommandOption("uri", 'u', IsRequired = true, Description = "The uri for the http request.")]
        public Uri? Uri { get; set; }

        /// <inheritdoc />
        public Task ExecuteAsync(IConsole console)
        {
            // validate
            console = Guard.Argument(console).NotNull().Value;
            this.Property ??= "StatusCode";
            this.Uri = Guard.Argument(this.Uri).NotNull().Value;

            // perform
            using var httpClient = new HttpClient();
            var result = httpClient.GetAsync(this.Uri).Result;
            var output = FromProperty(result, this.Property);
            console.Output.WriteLine(output);
            return Task.CompletedTask;
        }

        private static string FromProperty(HttpResponseMessage httpResponseMessage, string property)
        {
            return property switch
            {
                "Content" => httpResponseMessage.Content.ReadAsStringAsync().Result,
                "Headers" => httpResponseMessage.Headers.ToString(),
                "StatusCode" => httpResponseMessage.StatusCode.ToString(),
                _ => throw new ArgumentException(message: "Invalid property set.", paramName: nameof(property)),
            };
        }
    }
}
