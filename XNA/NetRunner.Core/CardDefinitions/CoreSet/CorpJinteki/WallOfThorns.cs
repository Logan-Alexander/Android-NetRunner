using NetRunner.Core.Corporation;
using NetRunner.Core.InstantEffects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.CardDefinitions.CoreSet.CorpJinteki
{
    [CardBehaviourID(CardSet.CoreSet, 78, "Wall of Thorns")]
    public class WallOfThorns : PieceOfIceCardBehaviour
    {
        public WallOfThorns(Card card)
            : base(
                card,
                1,
                CorporationFaction.Jinteki,
                8,
                5,
                IceTypes.Barrier)
        {
            Subroutine subroutine1 = new Subroutine();
            subroutine1.Effects.Add(new DamageTheRunner(DamageType.Net, 2));
            SubRoutines.Add(subroutine1);

            Subroutine subroutine2 = new Subroutine();
            subroutine2.Effects.Add(new EndTheRun());
            SubRoutines.Add(subroutine2);
        }
    }
}
