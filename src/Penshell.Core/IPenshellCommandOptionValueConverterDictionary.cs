namespace Penshell.Core
{
    using System;
    using System.Collections.Generic;

    public interface IPenshellCommandOptionValueConverterDictionary : IDictionary<Type, IPenshellCommandOptionValueConverter>
    {
    }
}
