using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.CardIdentifiers
{
    /// <summary>
    /// Thrown when the a card cannot be resolved from the identifier provided.
    /// This most likely indicates that something is out of sync with the player
    /// who provided the identifier.
    /// </summary>
    [Serializable]
    public class CardIdentifierResolutionException : System.Exception
    {
        public CardIdentifierResolutionException() { }
        public CardIdentifierResolutionException(string message) : base(message) { }
        public CardIdentifierResolutionException(string message, System.Exception inner) : base(message, inner) { }
        protected CardIdentifierResolutionException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
