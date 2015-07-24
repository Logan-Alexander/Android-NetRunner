using NetRunner.Core.Conditions;
using NetRunner.Core.Corporation;
using NetRunner.Core.InstantEffects;
using NetRunner.Core.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.CardDefinitions.CoreSet.CorpJinteki
{
    [CardBehaviourID(CardSet.CoreSet, 67, "Jinteki")]
    public class Jinteki : CorporationFactionCardBehaviour
    {
        public Jinteki(Card card)
            : base(card, CorporationFaction.Jinteki)
        {
            Effect effect1 = new DamageTheRunner(DamageType.Net, 1);

            //PostTrigger trigger1 = new CorporationScoresAgenda();
            //trigger1.Effects.Add(effect1);
            //Triggers.Add(trigger1);
            
            //PostTrigger trigger2 = new RunnerStealsAgenda();
            //trigger2.Effects.Add(effect1);
            //Triggers.Add(trigger2);
        }
    }
}
