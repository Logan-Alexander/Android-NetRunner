using NetRunner.Core.Conditions;
using NetRunner.Core.Corporation;
using NetRunner.Core.InstantEffects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.CardDefinitions.CoreSet.CorpJinteki
{
    [CardBehaviourID(CardSet.CoreSet, 72, "Neural EMP")]
    public class NeuralEMP : OperationCardBehaviour
    {
        public NeuralEMP(Card card)
            : base(
                card,
                2,
                CorporationFaction.Jinteki,
                2)
        {

            PlayConditions.Add(new RunLastTurn());

            Effects.Add(new DamageTheRunner(DamageType.Net, 1));
        }
    }
}