namespace Penshell.Commands.Net
{
    using FluentValidation;

    public class HttpCommandValidator : AbstractValidator<HttpCommand>
    {
        public HttpCommandValidator()
        {
            RuleFor(x => x.Method).NotNull().NotEmpty().WithMessage("Method must be set.");
            RuleFor(x => x.Uri).NotNull().WithMessage("Uri must be set.");
        }
    }
}
