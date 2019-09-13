namespace Penshell.Core
{
    using System;

    public interface IPenshellCommandOptionValueConverter
    {
        Type TargetType { get; }

        object Convert(string value);
    }
}
