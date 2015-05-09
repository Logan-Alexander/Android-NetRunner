using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core
{
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
