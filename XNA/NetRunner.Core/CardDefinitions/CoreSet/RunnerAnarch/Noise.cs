using NetRunner.Core.Runner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.CardDefinitions.CoreSet.RunnerAnarch
{
    [CardBehaviourID(CardSet.CoreSet, 0, "Noise")]
    public class Noise : RunnerFactionCardBehaviour
    {
        public Noise(Card card)
            : base(card, RunnerFaction.Anarch)
        {
        }
    }
}
