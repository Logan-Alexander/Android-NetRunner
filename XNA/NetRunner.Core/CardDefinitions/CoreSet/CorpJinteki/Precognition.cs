using NetRunner.Core.Corporation;
using NetRunner.Core.InstantEffects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.CardDefinitions.CoreSet.CorpJinteki
{
    [CardBehaviourID(CardSet.CoreSet, 73, "Precognition")]
    public class Precognition : OperationCardBehaviour
    {
        public Precognition(Card card)
            : base(
                card,
                3,
                CorporationFaction.Jinteki,
                0)
        {
            Effects.Add(new ArrangeTopFiveCardsOfResearchAndDevelopment());
        }
    }
}
