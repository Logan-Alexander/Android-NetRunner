using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Corporation
{
    public class Archives : Server
    {
        public Stack<Card> DiscardPile { get; private set; }

        public Archives()
            : base(ServerType.Central)
        {
            DiscardPile = new Stack<Card>();
        }

        public void Add(Card card)
        {
            DiscardPile.Push(card);
        }
    }
}
