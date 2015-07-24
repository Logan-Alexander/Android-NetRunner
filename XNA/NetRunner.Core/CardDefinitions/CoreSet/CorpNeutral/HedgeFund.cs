using NetRunner.Core.Corporation;
using NetRunner.Core.InstantEffects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.CardDefinitions.CoreSet.CorpNeutral
{
    [CardBehaviourID(CardSet.CoreSet, 110, "Hedge Fund")]
    public class HedgeFund : OperationCardBehaviour
    {
        public HedgeFund(Card card)
            : base(
                card,
                0,
                CorporationFaction.None,
                5)
        {
            Effects.Add(new AddCredits(PlayerType.Corporation, 9));
        }
    }
}
