using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Runner
{
    public class RunnerStack
    {
        public Stack<Card> DrawPile { get; private set; }

        public int Size
        {
            get { return DrawPile.Count; }
        }

        public RunnerStack()
        {
            DrawPile = new Stack<Card>();
        }

        public Card TopCard
        {
            get { return DrawPile.Peek(); }
        }

        public Card RemoveTopCard()
        {
            return DrawPile.Pop();
        }

        public void AddCard(Card card)
        {
            DrawPile.Push(card);
        }

        public void Shuffle()
        {
            // TODO: Implement a shuffling algorithm.
        }
    }
}
