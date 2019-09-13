namespace Penshell.Commands.Scripting
{
    using FluentValidation;

    public class RunCommandValidator : AbstractValidator<RunCommand>
    {
        public RunCommandValidator()
        {
            this.RuleFor(x => x.Path).NotNull().WithMessage("Path must be set.");
        }
    }
}
