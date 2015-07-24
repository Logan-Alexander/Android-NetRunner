using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Runner
{
    public class Grip
    {
        private List<Card> _Cards;
        public ReadOnlyCollection<Card> Cards
        {
            get { return _Cards.AsReadOnly(); }
        }

        public Grip()
        {
            _Cards = new List<Card>();
        }

        public void Add(Card card)
        {
            _Cards.Add(card);
        }

        public void Remove(Card card)
        {
            _Cards.Remove(card);
        }
    }
}
