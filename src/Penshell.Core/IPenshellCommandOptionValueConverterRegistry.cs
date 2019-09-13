namespace Penshell.Core
{
    using System;
    using System.Collections.Generic;

    public interface IPenshellCommandOptionValueConverterRegistry : IDictionary<Type, IPenshellCommandOptionValueConverter>
    {
    }
}
