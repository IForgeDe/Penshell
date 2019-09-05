namespace Penshell.Commands.Scripting
{
    using FluentValidation;

    public class RunCommandValidator : AbstractValidator<RunCommand>
    {
        public RunCommandValidator()
        {
            this.RuleFor(x => x.ScriptFilePath).NotNull().WithMessage("Please specify a script file path.");
        }
    }
}
