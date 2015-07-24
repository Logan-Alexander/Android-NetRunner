using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.CardIdentifiers
{
    /// <summary>
    /// A card identifier provides a transportable way of referencing a specific card.
    /// A reference to the card can be resolved by providing the game context.
    /// </summary>
    [Serializable]
    public abstract class CardIdentifier
    {
        public abstract bool TryResolve(GameContext context, out Card card);
        public abstract Card Resolve(GameContext context);
    }
}
