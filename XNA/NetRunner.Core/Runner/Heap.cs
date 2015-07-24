using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Runner
{
    public class Heap
    {
        public Stack<Card> DiscardPile { get; private set; }

        public Heap()
        {
            DiscardPile = new Stack<Card>();
        }

        public void Add(Card card)
        {
            DiscardPile.Push(card);
        }
    }
}
