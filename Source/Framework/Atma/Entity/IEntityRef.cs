﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atma.Entity
{
    public interface IEntityRef : IMutableComponentContainer
    {
        int id { get; }

    }
}
