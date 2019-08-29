﻿namespace Penshell.Core
{
    using System;
    using System.Collections.Generic;

    public interface IAssemblyCommandProvider
    {
        IEnumerable<Type> GetCommandTypes();
    }
}