namespace Penshell.Commands.Net
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using CliFx;
    using CliFx.Attributes;
    using CliFx.Services;
    using FluentValidation;

    [Command("net http", Description = "Gets the response of a http method.")]
    public class HttpCommand : ICommand
    {
        public HttpCommand(IValidator<HttpCommand> validator)
        {
            this.Validator = validator;
        }

        [CommandOption("method", 'm', IsRequired = true, Description = "The http method (delete, get, post, put).")]
        public string? Method { get; set; }

        [CommandOption("uri", 'u', IsRequired = true, Description = "The uri for the http request.")]
        public Uri? Uri { get; set; }

        public IValidator<HttpCommand> Validator { get; }

        public Task ExecuteAsync(IConsole console)
        {
            // validate
            this.Validator.ValidateAndThrow(this);

            // perform
            using var httpClient = new HttpClient();
            var httpResponseMessage = httpClient.GetAsync(this.Uri);
            httpResponseMessage.Wait();
            console.Output.WriteLine(Convert.ToString(httpResponseMessage.Result.StatusCode, Thread.CurrentThread.CurrentCulture));
            return Task.CompletedTask;
        }
    }
}
