using System;
using System.Collections.Generic;
using System.Linq;

namespace NetRunner.Core
{
    /// <summary>
    /// A card behaviour defines a type of card that exists in Net Runner.
    /// The behaviour is attached to a physical Card once the behaviour of
    /// the card is known to the player.
    /// </summary>
    public abstract class CardBehaviour
    {
        public Card Card { get; private set; }
        public CardBehaviourID CardBehaviourID { get; private set; }
        public string Title { get; private set; }
        public PlayerType PlayerType { get; private set; }
        public int Influence { get; private set; }

        public CardBehaviour(
            Card card,
            PlayerType playerType,
            int influence)
        {
            Card = card;

            ExtractCardBehaviourIDAndTitle();
            PlayerType = playerType;
            Influence = influence;
        }

        private void ExtractCardBehaviourIDAndTitle()
        {
            CardBehaviourIDAttribute[] cardBehaviourIDAttributes =
                (CardBehaviourIDAttribute[])GetType().GetCustomAttributes(typeof(CardBehaviourIDAttribute), false);
            
            if (cardBehaviourIDAttributes.Length != 1)
            {
                throw new Exception("Each class representing a card behaviour must use the CardBehaviourIDAttribute.");
            }

            CardBehaviourIDAttribute cardBehaviourIDAttribute = cardBehaviourIDAttributes.Single();
            
            CardBehaviourID = cardBehaviourIDAttribute.ID;
            Title = cardBehaviourIDAttribute.Title;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
