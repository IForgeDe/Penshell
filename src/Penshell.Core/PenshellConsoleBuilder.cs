namespace Penshell.Core
{
    using CliFx.Services;

    public class PenshellConsoleBuilder
    {
        public IConsole Build()
        {
            return new PenshellConsole();
        }
    }
}