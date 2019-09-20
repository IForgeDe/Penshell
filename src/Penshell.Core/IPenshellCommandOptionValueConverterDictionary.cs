namespace Penshell.Core
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface for a ditionary of penshell command option value converters.
    /// </summary>
    public interface IPenshellCommandOptionValueConverterDictionary : IDictionary<Type, IPenshellCommandOptionValueConverter>
    {
    }
}
