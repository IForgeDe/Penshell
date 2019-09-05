namespace Penshell.Commands.Scripting
{
    using System;
    using System.Collections.Generic;
    using System.Composition;
    using Penshell.Core;

    [Export(typeof(IAssemblyCommandProvider))]
    public class PenshellCommandProvider : IAssemblyCommandProvider
    {
        public IEnumerable<Type> GetCommandTypes()
        {
            return new List<Type>()
            {
                typeof(RunCommand),
            };
        }

        public IEnumerable<Type> GetCommandValidatorTypes()
        {
            return new List<Type>()
            {
                typeof(RunCommandValidator),
            };
        }
    }
}
