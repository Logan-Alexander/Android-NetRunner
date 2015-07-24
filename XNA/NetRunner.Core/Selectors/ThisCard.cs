using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Selectors
{
    public class ThisCard : ISelector<CardBehaviour>
    {
        public CardBehaviour Card { get; private set; }

        public ThisCard(CardBehaviour card)
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

        public IEnumerable<CardBehaviour> Items
        {
            get { yield return Card; }
        }
    }
}
