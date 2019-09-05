namespace Penshell.Core
{
    using System.Collections.Generic;
    using CliFx.Models;

    public class PenshellCommandRegistry
    {
        public IReadOnlyList<CommandSchema>? CommandSchemas { get; private set; }

        public void Register(IReadOnlyList<CommandSchema> commandSchemas)
        {
            this.CommandSchemas = commandSchemas;
        }
    }
}
