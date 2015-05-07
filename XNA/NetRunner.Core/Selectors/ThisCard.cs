using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Selectors
{
    public class ThisCard : ISelector<Card>
    {
        public Card Card { get; private set; }

        public ThisCard(Card card)
        {
            Card = card;
        }

        public void Resolve(GameContext context)
        {
        }

        public bool IsResolved
        {
            get { return true; }
        }

        public IEnumerable<Card> Items
        {
            get { yield return Card; }
        }
    }
}
