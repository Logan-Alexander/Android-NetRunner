using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core
{
    /// <summary>
    /// Thrown when an action is not valid.
    /// This indicates that the game is out of sync for the player who created the action.
    /// </summary>
    [Serializable]
    public class IllegalActionException : Exception
    {
        public IllegalActionException() { }
        public IllegalActionException(string message) : base(message) { }
        public IllegalActionException(string message, Exception inner) : base(message, inner) { }
        protected IllegalActionException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
