using NetRunner.Core.InstantEffects;
using NetRunner.Core.Runner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.CardDefinitions.CoreSet.RunnerNeural
{
    [CardBehaviourID(CardSet.CoreSet, 50, "Sure Gamble")]
    public class SureGamble : EventCardBehaviour
    {
        public SureGamble(Card card)
            : base(
                card,
                0,
                RunnerFaction.None,
                5)
        {
            AddEffect(new AddCredits(PlayerType.Runner, 9));
        }
    }
}
