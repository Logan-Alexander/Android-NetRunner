using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.IdentifierPredicates
{
    public class ThisCard : SingleItemPredicate<Card>
    {
        public Card Card { get; private set; }

        public ThisCard(Card card)
        {
            Card = card;
        }

        public override void Resolve(GameContext context)
        {
            base.Resolve(context);
            OnItemAcquired(new GameContextEventArgs(context));
        }

        public override Card GetItem(GameContext context)
        {
            return Card;
        }
    }
}
