using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core
{
    [Flags]
    public enum IceTypes
    {
        None = 0,
        Trap = 1,
        Barrier = 2,
        Sentry = 4,
        CodeGate = 8
    }
}
