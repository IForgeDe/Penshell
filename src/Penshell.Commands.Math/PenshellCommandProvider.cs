namespace Penshell.Commands.Math
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
                typeof(AddCommand),
                typeof(DivideCommand),
                typeof(MultiplyCommand),
                typeof(SubstractCommand),
            };
        }
    }
}
