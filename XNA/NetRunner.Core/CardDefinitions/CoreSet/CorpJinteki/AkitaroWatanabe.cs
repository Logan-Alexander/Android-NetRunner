using NetRunner.Core.ContinuousEffects;
using NetRunner.Core.Corporation;
using NetRunner.Core.Selectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.CardDefinitions.CoreSet.CorpJinteki
{
    [CardBehaviourID(CardSet.CoreSet, 79, "Akitaro Watanabe")]
    public class AkitaroWatanabe : UpgradeCardBehaviour
    {
        public AkitaroWatanabe(Card card)
            : base(
                card,
                2,
                CorporationFaction.Jinteki,
                1)
        {

            ContinuousEffect effect1 = new ModifyRezCost(
                new IceProtectingServer(new ThisCardsServer(card)),
                -2);

            Effects.Add(effect1);
        }
    }
}
