using NetRunner.Core.GameFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core
{
    /// <summary>
    /// Temporary class. This will probably get replaced by a class similar to
    /// GameContextSerializer to provide a string of XML representing the stack
    /// of state machines comprising the game flow.
    /// </summary>
    public class SerializedFlow
    {
        public Flow Flow { get; private set; }

        public SerializedFlow(Flow flow)
        {
            // TODO: Serialize this properly.
            Flow = flow;
        }
    }
}
