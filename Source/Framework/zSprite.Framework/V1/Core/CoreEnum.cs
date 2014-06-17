using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zSpriteOld.Core
{
    /// <summary>
    ///     The level of detail in which the log will go into.
    /// </summary>
    public enum LoggingLevel
    {
        Low = 1,
        Normal,
        Verbose
    }

    /// <summary>
    ///     The importance of a logged message.
    /// </summary>
    public enum LogMessageLevel
    {
        Trivial = 1,
        Normal,
        Critical
    }
}
